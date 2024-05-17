using ApiTemplate.Application.Commands;
using ApiTemplate.Domain.Interfaces;
using ApiTemplate.Infrastructure.Context;
using ApiTemplate.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTemplate.Infrastructure.Ioc
{
    public static class Ioc
    {
        public static void RegisterServices(this IServiceCollection services) 
        {
            services.AddScoped<DbSession>();
            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UserAddCommand).Assembly));

        }
    }
}
