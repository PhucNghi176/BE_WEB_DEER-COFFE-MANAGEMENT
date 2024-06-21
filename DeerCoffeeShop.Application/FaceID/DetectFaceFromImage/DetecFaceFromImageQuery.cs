using DeerCoffeeShop.Application.Common.Interfaces;
using DeerCoffeeShop.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeerCoffeeShop.Application.FaceID.DetectFaceFromImage;

public record DetecFaceFromImageQuery(IFormFile Image) : IRequest<string>, IQuery
{
    public IFormFile Image { get; set; } = Image;
}
internal class DetecFaceFromImageQueryHandler : IRequestHandler<DetecFaceFromImageQuery, string>
{
    private readonly IFaceDetectionRepository _faceDetectionService;
    private readonly string[] _rootPath = Directory.GetDirectories(Directory.GetCurrentDirectory()+ "/TrainedFaces/");
    public DetecFaceFromImageQueryHandler(IFaceDetectionRepository faceDetectionService)
    {
        _faceDetectionService = faceDetectionService;
    }

    public async Task<string> Handle(DetecFaceFromImageQuery request, CancellationToken cancellationToken)
    {
        
        string result = await _faceDetectionService.DetectFaceFromImage(request.Image, _rootPath);
        return result;
    }
}
