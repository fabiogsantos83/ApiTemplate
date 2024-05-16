using ApiTemplate.Domain.Entities;
using ApiTemplate.Domain.Interfaces;
using ApiTemplate.Infrastructure.Context;
using Dapper;
using MySqlConnector;
using System.Text;

namespace ApiTemplate.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(DbSession session) : base(session)
        {
        }

        public async Task Add(UserEntity user)
        {
            var query = new StringBuilder();

            query.AppendLine("insert into user");
            query.AppendLine("(id,name,email)");
            query.AppendLine("values");
            query.AppendLine("(@id,@name,@email)");
            
            var parameters = new { id = user.Id, name = user.Name, email = user.Email };

            await _session.Connection.ExecuteAsync(query.ToString(), parameters, _session.Transaction);
        }

        public async Task<IEnumerable<UserEntity>> List()
        {
            var query = new StringBuilder();

            query.AppendLine("select id,name,email from user");
            
            return await _session.Connection.QueryAsync<UserEntity>(query.ToString(), _session.Transaction);
        }
    }
}
    