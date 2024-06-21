using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Domain.Repositories;

public interface IFaceDetectionRepository
{
    Task<string> DetectFaceFromImage(IFormFile image, string[] directories);
    Task<string> SaveImage(IFormFile image, string employeeID, string employeeFolderPath, CancellationToken cancellationToken = default);
}
