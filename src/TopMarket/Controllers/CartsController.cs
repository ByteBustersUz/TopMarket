using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using TopMarket.Models;

namespace TopMarket.Controllers
{
    public class CartsController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;
        public CartsController(ICartService cartService, IAuthService authService)
        {
            _cartService = cartService;
            _authService = authService;
        }


        [HttpDelete("ClearCart")]
        public async Task<IActionResult> ClearCart()
        {
            long userId = Convert.ToInt32(HttpContext.User.FindFirstValue("id"));
            long cartId = (await _authService.GetByIdAsync(userId)).CartId;

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _cartService.ClearCartAsync(cartId)
            });
        }


        [HttpGet("GetItems")]
        public async Task<IActionResult> GetCartItems()
        {
            long userId = Convert.ToInt32(HttpContext.User.FindFirstValue("id"));
            long cartId = (await _authService.GetByIdAsync(userId)).CartId;

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _cartService.RetrieveAllItemsAsync(cartId)
            });
        }


        [HttpPut("RemoveItem")]
        public async Task<IActionResult> RemoveItemFromCart(long cartItemId)
            => Ok(new Response
            {
                StatusCode =200,
                Message = "Success",
                Data = await _cartService.RemoveFromCartAsync(cartItemId)
            });
    }
}
