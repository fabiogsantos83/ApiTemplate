using ApiTemplate.Infrastructure.Context;

namespace ApiTemplate.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        protected DbSession _session;

        public BaseRepository(DbSession session)
        {
            _session = session;
        }
    }
}
