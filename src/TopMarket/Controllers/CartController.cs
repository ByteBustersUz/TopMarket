using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Security.Claims;
using TopMarket.Models;

namespace TopMarket.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

    }
}
