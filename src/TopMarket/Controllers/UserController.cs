using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Users;
using Service.Interfaces;
using System.Security.Claims;
using TopMarket.Models;

namespace TopMarket.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthsService authsService;
        public UserController(IAuthsService authsService)
        {
            this.authsService = authsService;
        }
        [HttpPost("create")]
        public async ValueTask<IActionResult> PostAsync(UserCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await authsService.RegisterAsync(dto)
        });

        [HttpPost("login")]
        public async ValueTask<IActionResult> LoginAsync(UserLoginDto dto)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Succes",
                Data = await authsService.LoginAsync(dto)
            });
        //[HttpPost("token")]
        //public async ValueTask<IActionResult> GenerateToken(string phone, string password)
        //    => Ok(new Response
        //    {
        //        StatusCode = 200,
        //        Message = "Succes",
        //        Data = await authsService.GenerateTokenAsync(phone, password)
        //    });

        [Authorize]
        [HttpPut("update")]

        public async ValueTask<IActionResult> UpdateAsync(UserViewDto dto)
        {
            long id = Convert.ToInt32(HttpContext.User.FindFirstValue("id"));
            var role = Convert.ToInt32(HttpContext.User.FindFirstValue("Role"));
            UserRole user = (UserRole)role;

            var update = new UserUpdateDto()
            {
                Id = id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                UserRole = user,
            };

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Succes",
                Data = await authsService.UpdateAsync(update)
            });
        }

        [Authorize]
        [HttpPut("changepassword")]

        public async Task<IActionResult> ChangePasswordAsycn(UserPasswordView dto)
        {
            long id = Convert.ToInt32(HttpContext.User.FindFirstValue("id"));

            var user = new UserChangePassword
            {
                UserId = id,
                Password = dto.Password,
                ConfirmPassword = dto.ConfirmPassword
            };

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Succes",
                Data = await authsService.ChangePasswordAsync(user)
            });
        }
    }

}
