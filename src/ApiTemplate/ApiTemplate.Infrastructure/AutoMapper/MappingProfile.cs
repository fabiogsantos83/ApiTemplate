using ApiTemplate.Application.Commands;
using ApiTemplate.Domain.Entities;
using AutoMapper;

namespace ApiTemplate.Infrastructure.AutoMapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserAddCommand, UserEntity>();
            CreateMap<UserEntity, UserListCommandRespose>();
        }
    }
}
