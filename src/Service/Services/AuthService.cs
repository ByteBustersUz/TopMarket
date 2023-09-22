using AutoMapper;
using Service.Helpers;
using Service.Interfaces;
using Data.IRepositories;
using Service.DTOs.Users;
using Service.Exceptions;
using Domain.Entities.UserFolder;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace Service.Services;


public class AuthService : IAuthsService
{
    private readonly IMapper mapper;
    private readonly IRepository<User> repository;

    public AuthService(IRepository<User> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }
    public async Task<UserResultDto> RegisterAsync(UserCreationDto dto)
    {
        User user= await repository.GetAsync(x=>x.Phone.Equals(dto.Phone));
        if (user is not null)
            throw new AlreadyExistException("This phone already exist");
        var mapped= mapper.Map<User>(dto);

        PasswordHash.Encrypt(dto.Password,out byte[] passwordhash,out byte[] salt);
        mapped.PasswordSalt= salt;
        mapped.PasswordHash= passwordhash;
        mapped.UserRole = Domain.Enums.UserRole.Customer;

        await repository.AddAsync(mapped);
        await repository.SaveAsync();

        var result = mapper.Map<UserResultDto>(mapped);

        return result;

    }
    public async Task<UserResultDto> LoginAsync(UserLoginDto dto)
    {
        User user = await repository.GetAsync(x => x.Phone.Equals(dto.Phone));
        if (user is null)
            throw new NotFoundException("This nomber not found");
        else if (!PasswordHash.VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt))
            throw new Exception("Wrong password");
        
        var mapped=mapper.Map<UserResultDto>(user);
        return mapped;
    }
    public async Task<bool> ChangePasswordAsync(UserChangePassword dto)
    {
        User user= await repository.GetAsync(x=>x.Id.Equals(dto.UserId));
        if (user is null)
            throw new NotFoundException("User not found");
        PasswordHash.Encrypt(dto.Password,out byte[] passwordhash, out byte[] salt);

        user.PasswordHash= passwordhash;
        user.PasswordSalt= salt;
        repository.Update(user);
        await repository.SaveAsync();

        return true;
    }

    public async Task<UserResultDto> UpdateAsync(UserUpdateDto dto)
    {
        User user= await repository.GetAsync(x=>x.Id.Equals(dto.Id));
        if (user is null)
            throw new NotFoundException("User not Found");

         user.UpdatetAt= DateTime.UtcNow;
        var mapped= mapper.Map(dto,user);
        repository.Update(mapped);
        await repository.SaveAsync();
        var result= mapper.Map<UserResultDto>(mapped);
        return result;
    }

    public async Task<UserResultDto> GetByIdAsync(long id)
    {
        throw new Exception();
    }

    public Task<IEnumerable<UserResultDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
