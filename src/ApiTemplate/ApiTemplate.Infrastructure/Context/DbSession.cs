using System.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace ApiTemplate.Infrastructure.Context
{
    public sealed class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession(IConfiguration config)
        {
            Connection = new MySqlConnection(config.GetConnectionString("db"));
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
