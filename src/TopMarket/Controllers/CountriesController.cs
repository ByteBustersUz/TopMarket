using Domain.Configuration;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using TopMarket.Models;

namespace TopMarket.Controllers;

public class CountriesController:BaseController
{
    private readonly ICountryService countryService;
    public CountriesController(ICountryService countryService)
    {
        this.countryService = countryService;
    }

    [HttpPost("set")]
    public async Task<IActionResult> PostAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.countryService.SetAsync()
        });


    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.countryService.RetrieveByIdAsync(id)
        });


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.countryService.RetrieveAllAsync(@params)
        });
}
