using ApiTemplate.Domain.Entities;

namespace ApiTemplate.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task Add(UserEntity user);
        Task<IEnumerable<UserEntity>> List();
    }
}
