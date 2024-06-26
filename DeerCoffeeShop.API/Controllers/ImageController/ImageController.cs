﻿using DeerCoffeeShop.API.Controllers.ResponseTypes;
using DeerCoffeeShop.Application.EmployeeShift.CheckIn_Out.CheckIn;
using DeerCoffeeShop.Application.FaceID.SaveImage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeerCoffeeShop.API.Controllers.ImageController;

public class ImageController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    public async Task<IActionResult> SaveImage([FromForm] SaveImageCommand command)
    {
        string result = await _sender.Send(command);
        return Ok(result);
    }
    [HttpPost("detect-image")]
    public async Task<IActionResult> DetectFaceFromImage([FromForm] CheckInCommand query)
    {
        string response = await _sender.Send(query);
        return Ok(response);
    }
}
