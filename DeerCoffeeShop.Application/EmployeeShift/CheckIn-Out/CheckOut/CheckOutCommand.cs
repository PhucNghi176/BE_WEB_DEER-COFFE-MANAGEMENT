using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Enums;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DeerCoffeeShop.Application.EmployeeShift.CheckIn_Out.CheckOut;

public record CheckOutCommand : IRequest<string>, ICommand
{
    public IFormFile Image { get; set; }
    public DateTime CheckOut { get; set; }
    public string RestaurantID { get; set; }
}
internal class CheckOutCommandHandler(IEmployeeShiftRepository employeeShiftRepository, IEmployeeRepository employeeRepository, IFaceDetectionRepository faceDetectionRepository, IAttdenceRepository attdenceRepository) : IRequestHandler<CheckOutCommand, string>
{
    private readonly IEmployeeShiftRepository _employeeShiftRepository = employeeShiftRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IFaceDetectionRepository _faceDetectionRepository = faceDetectionRepository;
    private readonly IAttdenceRepository _attdenceRepository = attdenceRepository;
    private readonly Cloudinary cloudinary = new("cloudinary://176963282532847:kONanxuhiEwEmJKFPC72M1a2rUs@dmiueqpah");
    private readonly string[] _rootPath = Directory.GetDirectories(Directory.GetCurrentDirectory() + "/TrainedFaces/");

    public async Task<string> Handle(CheckOutCommand request, CancellationToken cancellationToken)
    {
        string EmployeeID = await _faceDetectionRepository.DetectFaceFromImage(request.Image, _rootPath);
        if (!await _employeeRepository.AnyAsync(x => x.ID == EmployeeID, cancellationToken))
            throw new NotFoundException("Employee not found!");
        DateOnly DateOfWork = DateOnly.FromDateTime(request.CheckOut);
        Domain.Entities.EmployeeShift empShift = await _employeeShiftRepository.CheckShiftEmployee(EmployeeID, DateOfWork, request.RestaurantID, cancellationToken);
        if (empShift != null)
        {
            ImageUploadResult uploadResult = await UploadEmployeeImage(request.Image);
            if (uploadResult == null)
                throw new Exception("File upload failed!");
            empShift.Actual_CheckOut = request.CheckOut;
            empShift.TotalHours = empShift.Actual_CheckOut.Value.Hour - request.CheckOut.Hour;
            //check if the check in/out match the actual check in/out
            if (empShift.CheckIn != null && empShift.CheckOut != null)
            {
                empShift.IsOnTime = empShift.CheckIn.Value.Hour == empShift.Actual_CheckIn.Value.Hour && empShift.CheckOut.Value.Hour == request.CheckOut.Hour;
                //base on the check in/out time, set the status of the employee
                if (empShift.IsOnTime)
                {
                    empShift.Status = EmployeeShiftStatus.OnTime;
                }
                else if (empShift.CheckIn.Value.Hour < empShift.Actual_CheckIn.Value.Hour)
                {
                    empShift.Status = EmployeeShiftStatus.Late;
                }
                else if (empShift.CheckOut.Value.Hour > request.CheckOut.Hour)
                {
                    empShift.Status = EmployeeShiftStatus.EarlyLeave;
                }
                // set the note for the employee if late then negative and positive if early leave and the value will be the time difference
                empShift.EmployeeNote = empShift.Status switch
                {
                    EmployeeShiftStatus.Late => empShift.CheckIn.Value.Hour - empShift.Actual_CheckIn.Value.Hour,
                    EmployeeShiftStatus.EarlyLeave => request.CheckOut.Hour - empShift.CheckOut.Value.Hour,
                    _ => 0
                };

            }


            Domain.Entities.Attendence? attendence = await _attdenceRepository.FindAsync(x => x.EmployeeShiftID == empShift.ID, cancellationToken);
            attendence.EmployeePictureUrlCheckOut = uploadResult.Url.ToString();
            _employeeShiftRepository.Update(empShift);

        }
        return await _employeeShiftRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Check out successfull with EmployeeID: {EmployeeID}" : $"Check out failed with EmployeeID: {EmployeeID}";
    }
    private async Task<CloudinaryDotNet.Actions.ImageUploadResult> UploadEmployeeImage(IFormFile imageFile)
    {
        using (Stream stream = imageFile.OpenReadStream())
        {
            ImageUploadParams uploadParams = new()
            {
                File = new FileDescription(imageFile.FileName, stream),
                UseFilename = true,
                UniqueFilename = false,
                Folder = "EmployeeCheckIn",
                Overwrite = true
            };

            try
            {
                return await cloudinary.UploadAsync(uploadParams);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as necessary
                return null;
            }
        }

    }
}
