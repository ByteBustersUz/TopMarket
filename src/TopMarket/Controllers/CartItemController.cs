using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using TopMarket.Models;

namespace TopMarket.Controllers
{
    public class CartItemController : Controller
    {
        private readonly ICartItemService itemService;
        public CartItemController(ICartItemService itemService)
        {
            this.itemService = itemService;
        }
    }
}
