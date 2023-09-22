using AutoMapper.Configuration.Annotations;
using Data.IRepositories;
using Domain.Entities.UserFolder;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Exceptions;
using Service.Helpers;
using Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Services;

public class TokensService : ITokensService
{
    private readonly IConfiguration configuration;
    private readonly IRepository<User> repository;
    public TokensService(IRepository<User> repository, IConfiguration configuration)
    {
        this.repository = repository;
        this.configuration = configuration;
    }
    public async ValueTask<string> Generatetoken(string phone, string password)
    {
        var user = await this.repository.GetAsync(u => u.Phone.Equals(phone));
        if (user is null)
            throw new NotFoundException("This user is not found");

        bool verifiedPassword = PasswordHash.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
        if (!verifiedPassword)
            throw new CustomException("Phone or password is invalid");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
          {
             new Claim("Phone", user.Phone),
             new Claim("Id", user.Id.ToString()),
             new Claim(ClaimTypes.Role, user.UserRole.ToString()),
          }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        string result = tokenHandler.WriteToken(token);
        return result;
    }
}
