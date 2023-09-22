using AutoMapper;
using Domain.Entities.UserFolder;
using Service.DTOs.Users;

namespace Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserCreationDto,User>().ReverseMap();
        CreateMap<UserResultDto,User>().ReverseMap();
        CreateMap<UserUpdateDto,User>().ReverseMap();  
    }
}
