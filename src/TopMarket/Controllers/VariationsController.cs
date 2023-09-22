using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Variations;
using Service.Interfaces;
using TopMarket.Models;

namespace TopMarket.Controllers;

public class VariationsController : BaseController
{
    private readonly IVariationService variationService;
    public VariationsController(IVariationService variationService)
    {
        this.variationService = variationService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(VariationCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.variationService.CreateAsync(dto)
        });


    [HttpPut("update")]
    public async Task<IActionResult> PutAsync(VariationUpdateDto dto)
       => Ok(new Response
       {
           StatusCode = 200,
           Message = "Success",
           Data = await this.variationService.UpdateAsync(dto)
       });


    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
       => Ok(new Response
       {
           StatusCode = 200,
           Message = "Success",
           Data = await this.variationService.DeleteAsync(id)
       });


    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
       => Ok(new Response
       {
           StatusCode = 200,
           Message = "Success",
           Data = await this.variationService.GetByIdAsync(id)
       });


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
       => Ok(new Response
       {
           StatusCode = 200,
           Message = "Success",
           Data = await this.variationService.GetAllAsync()
       });
}
