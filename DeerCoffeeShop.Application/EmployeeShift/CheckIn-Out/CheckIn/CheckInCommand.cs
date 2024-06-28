using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Entities;
using DeerCoffeeShop.Domain.Enums;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DeerCoffeeShop.Application.EmployeeShift.CheckIn_Out.CheckIn;

public record CheckInCommand : ICommand, IRequest<string>
{
    public IFormFile Image { get; set; }
    public DateTime CheckIn { get; set; }
    public string RestaurantID { get; set; }
}
internal class CheckInCommandHandler(IEmployeeShiftRepository employeeShiftRepository, IEmployeeRepository employeeRepository, IFaceDetectionRepository faceDetectionRepository, IAttdenceRepository attdenceRepository) : IRequestHandler<CheckInCommand, string>
{
    private readonly IEmployeeShiftRepository _employeeShiftRepository = employeeShiftRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IFaceDetectionRepository _faceDetectionRepository = faceDetectionRepository;
    private readonly IAttdenceRepository _attdenceRepository = attdenceRepository;
    private readonly Cloudinary cloudinary = new("cloudinary://176963282532847:kONanxuhiEwEmJKFPC72M1a2rUs@dmiueqpah");
    private readonly string[] _rootPath = Directory.GetDirectories(Directory.GetCurrentDirectory() + "/TrainedFaces/");
    public async Task<string> Handle(CheckInCommand request, CancellationToken cancellationToken)
    {
        #region Validate Employee
        string EmployeeID = await _faceDetectionRepository.DetectFaceFromImage(request.Image, _rootPath) ?? throw new NotFoundException("Employee not found!");
        if (!await _employeeRepository.AnyAsync(x => x.ID == EmployeeID, cancellationToken))
            throw new NotFoundException("Employee not found!");
        #endregion
        #region Check Shift
        DateOnly DateOfWork = DateOnly.FromDateTime(request.CheckIn);
        Domain.Entities.EmployeeShift? empShift = await _employeeShiftRepository.CheckShiftEmployee(EmployeeID, DateOfWork, request.RestaurantID, cancellationToken) ?? throw new NotFoundException("Employee Shift not found!");
        ImageUploadResult uploadResult = await UploadEmployeeImage(request.Image) ?? throw new Exception("File upload failed!");
        if (empShift != null && request.CheckIn.Subtract(empShift.CheckIn.Value).TotalHours > -1)
        {
            //check if the checkintime from the request and the checkintime from the database is not 1 hour apart
            // this mean the employee work the normal shift not the extra shift
            empShift.Actual_CheckIn = request.CheckIn;
            Attendence? attendence = await _attdenceRepository.FindAsync(x => x.EmployeeShiftID == empShift.ID, cancellationToken);
            attendence.EmployeePictureUrlCheckIn = uploadResult.Url.ToString();
            _employeeShiftRepository.Update(empShift);
        }
        else //  This mean the employee work the extra shift in that same day
        {
            empShift = new Domain.Entities.EmployeeShift
            {
                RestaurantID = request.RestaurantID,
                EmployeeID = EmployeeID,
                DateOfWork = DateOfWork,
                Month = DateOfWork.Month,
                Year = DateOfWork.Year,
                CheckIn = null,
                CheckOut = null,
                Actual_CheckIn = request.CheckIn,
                Status = EmployeeShiftStatus.Absent,
                IsOnTime = false,
                IsEmpty = false,
                IsReviewRequired = true
            };

            Attendence attendence = new()
            {
                EmployeeShiftID = empShift.ID,
                EmployeePictureUrlCheckIn = uploadResult.Url.ToString(),
                EmployeePictureUrlCheckOut = ""
            };
            _attdenceRepository.Add(attendence);
            _employeeShiftRepository.Add(empShift);
        }
        #endregion


        return await _employeeShiftRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Check in successfull with EmployeeID: {EmployeeID}" : $"Check in failed with EmployeeID: {EmployeeID}";
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
                Console.WriteLine($"File upload error: {ex.Message}");
                return null;
            }
        }

    }
}
