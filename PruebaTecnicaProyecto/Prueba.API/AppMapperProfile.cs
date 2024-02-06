using Application.OrganizationDto;
using AutoMapper;
using Domain.Organizations;
using Domain.Users;
namespace Prueba.API;

public class AppMapperProfile : Profile
{
    protected AppMapperProfile()
    {
        CreateMap<OrganizationDTO, Organization>();
        CreateMap<UserDto, User>();
    }
}