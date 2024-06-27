using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Common.Exceptions;
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
    Cloudinary cloudinary = new(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
    private readonly string[] _rootPath = Directory.GetDirectories(Directory.GetCurrentDirectory() + "/TrainedFaces/");

    public async Task<string> Handle(CheckOutCommand request, CancellationToken cancellationToken)
    {
        var EmployeeID = await _faceDetectionRepository.DetectFaceFromImage(request.Image, _rootPath);
        if (!await _employeeRepository.AnyAsync(x => x.ID == EmployeeID, cancellationToken))
            throw new NotFoundException("Employee not found!");
        var DateOfWork = DateOnly.FromDateTime(request.CheckOut);
        var empShift = await _employeeShiftRepository.CheckShiftEmployee(EmployeeID, DateOfWork, cancellationToken);
        if (empShift != null)
        {
            var uploadResult = await UploadEmployeeImage(request.Image);
            if (uploadResult == null)
                throw new Exception("File upload failed!");
            empShift.Actual_CheckOut = request.CheckOut;
            var attendence = await _attdenceRepository.FindAsync(x => x.EmployeeShiftID == empShift.ID, cancellationToken);
            attendence.EmployeePictureUrlCheckOut = uploadResult.Url.ToString();
            _employeeShiftRepository.Update(empShift);

        }
        return await _employeeShiftRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Check out successfull with EmployeeID: {EmployeeID}" : $"Check out failed with EmployeeID: {EmployeeID}";
    }
    private async Task<CloudinaryDotNet.Actions.ImageUploadResult> UploadEmployeeImage(IFormFile imageFile)
    {
        using (var stream = imageFile.OpenReadStream())
        {
            var uploadParams = new ImageUploadParams()
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
