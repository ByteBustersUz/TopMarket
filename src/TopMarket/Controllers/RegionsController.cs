using Domain.Configuration;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using TopMarket.Models;

namespace TopMarket.Controllers;

public class RegionsController:BaseController
{
    private readonly IRegionService regionService;
    public RegionsController(IRegionService regionService)
    {
        this.regionService = regionService;
    }

    [HttpPost("set")]
    public async Task<IActionResult> PostAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.regionService.SetAsync()
        });

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.regionService.RetrieveByIdAsync(id)
        });


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.regionService.RetrieveAllAsync(@params)
        });
}
