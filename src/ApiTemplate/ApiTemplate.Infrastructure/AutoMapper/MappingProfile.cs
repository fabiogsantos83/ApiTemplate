using ApiTemplate.Application.Commands;
using ApiTemplate.Application.Queries;
using ApiTemplate.Domain.Entities;
using AutoMapper;

namespace ApiTemplate.Infrastructure.AutoMapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserAddRequest, UserEntity>();
            CreateMap<UserEntity, UserQueryResponse>();
        }
    }
}
