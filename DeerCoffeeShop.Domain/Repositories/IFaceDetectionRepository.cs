using Microsoft.AspNetCore.Http;

namespace DeerCoffeeShop.Domain.Repositories;

public interface IFaceDetectionRepository
{
    Task<string> DetectFaceFromImage(IFormFile image, string[] directories);
    Task<string> SaveImage(IFormFile image, string employeeID, string employeeFolderPath, CancellationToken cancellationToken = default);
}
