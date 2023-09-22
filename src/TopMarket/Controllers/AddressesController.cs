using Domain.Configuration;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Addresses;
using Service.Interfaces;
using TopMarket.Models;

namespace TopMarket.Controllers;

public class AddressesController:BaseController
{
    private readonly IAddressService addressService;
    public AddressesController(IAddressService addressService)
    {
        this.addressService = addressService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(AddressCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.addressService.CreateAsync(dto)
        });


    [HttpPut("update")]
    public async Task<IActionResult> PutAsync(AddressUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.addressService.ModifyAsync(dto)
        });


    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.addressService.RemoveAsync(id)
        });


    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.addressService.RetrieveByIdAsync(id)
        });


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.addressService.RetrieveAllAsync(@params)
        });
}
