using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using TopMarket.Models;

namespace TopMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokensService _tokensService;

        public TokenController(ITokensService tokensService)
        {
            _tokensService = tokensService;
        }

        [HttpPost("login")]
        public async ValueTask<IActionResult> PostAsync(string phone , string password )
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _tokensService.Generatetoken(phone,password)
            });
    }
}
