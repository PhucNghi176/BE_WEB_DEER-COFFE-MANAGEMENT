﻿using DeerCoffeeShop.API.Controllers.ResponseTypes;
using DeerCoffeeShop.Application.FaceID.DetectFaceFromImage;
using DeerCoffeeShop.Application.FaceID.SaveImage;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeerCoffeeShop.API.Controllers.ImageController;

public class ImageController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    public async Task<IActionResult> SaveImage([FromForm] SaveImageCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }
    [HttpPost("detect-image")]
    public async Task<IActionResult> DetectFaceFromImage([FromForm]DetecFaceFromImageQuery query)
    {
        var result = await _sender.Send(new DetecFaceFromImageQuery(query.Image));
        return Ok(result);
    }
}