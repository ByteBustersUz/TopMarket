using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Categories;
using Service.DTOs.Promotions;
using Service.Interfaces;
using TopMarket.Models;

namespace TopMarket.Controllers;

public class PromotionsController : BaseController
{
    private readonly IPromotionService promotionService;
    public PromotionsController(IPromotionService promotionService)
    {
        this.promotionService = promotionService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(PromotionCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.promotionService.CreateAsync(dto)
        });


    [HttpPut("update")]
    public async Task<IActionResult> PutAsync(PromotionUpdateDto dto)
       => Ok(new Response
       {
           StatusCode = 200,
           Message = "Success",
           Data = await this.promotionService.UpdateAsync(dto)
       });


    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
       => Ok(new Response
       {
           StatusCode = 200,
           Message = "Success",
           Data = await this.promotionService.DeleteAsync(id)
       });


    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
       => Ok(new Response
       {
           StatusCode = 200,
           Message = "Success",
           Data = await this.promotionService.GetByIdAsync(id)
       });


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
       => Ok(new Response
       {
           StatusCode = 200,
           Message = "Success",
           Data = await this.promotionService.GetAllAsync()
       });
}
