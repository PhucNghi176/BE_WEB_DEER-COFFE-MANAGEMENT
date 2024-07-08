using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DeerCoffeeShop.Application.FaceID.SaveImage;

public class SaveImageCommand : IRequest<string>, ICommand
{
    public required IFormFile Image { get; set; }
    public string EmployeeID { get; set; }
}
internal class SaveImageCommandHandler : IRequestHandler<SaveImageCommand, string>
{
    private readonly string _rootPath = Directory.GetCurrentDirectory();
    private readonly IFaceDetectionRepository _faceDetectionRepository;

    public SaveImageCommandHandler(IFaceDetectionRepository faceDetectionRepository)
    {
        _faceDetectionRepository = faceDetectionRepository;
    }

    public async Task<string> Handle(SaveImageCommand request, CancellationToken cancellationToken)
    {
        // Construct the path to the employee's folder
        string employeeFolderPath = Path.Combine(_rootPath, "TrainedFaces", request.EmployeeID);

        return await _faceDetectionRepository.SaveImage(request.Image, request.EmployeeID, employeeFolderPath, cancellationToken);

    }

}
