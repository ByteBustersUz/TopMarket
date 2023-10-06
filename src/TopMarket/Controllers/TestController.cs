using Data.Contexts;
using Domain.Entities.UserFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TopMarket.Controllers;

public class TestController : BaseController
{
    private readonly AppDbContext _appDbContext;

    public TestController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet("get-user-addresses")]
    public async Task<IActionResult> GetAddresses(long userId)
    {
        var user = await _appDbContext.UserAddresses
                            .Include(ua => ua.Address)
                            .Include(ua => ua.User)
                            .Where(ua => ua.UserId.Equals(userId))
                            .ToListAsync();
        return Ok(user);
    }

    [HttpPost("assign-address")]
    public async Task<IActionResult> AssignAddress(long userId, long addressId, bool isDefault)
    {
        var result = await _appDbContext.UserAddresses
            .FirstOrDefaultAsync(ua => ua.UserId.Equals(userId) && ua.AddressId.Equals(addressId));

        if (result is null)
            await _appDbContext.UserAddresses.AddAsync(new UserAddress
            {
                UserId = userId,
                AddressId = addressId,
                IsDefault = isDefault
            });
        else
        {
            result.IsDefault = isDefault;
        }
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }
}
