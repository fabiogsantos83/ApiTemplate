using ApiTemplate.Application.Commands;
using ApiTemplate.Application.Models;
using ApiTemplate.Domain.Interfaces;
using ApiTemplate.Infrastructure.Context;
using ApiTemplate.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTemplate.Infrastructure.BuilderServices
{
    public static class Ioc
    {
        public static void RegisterServices(this IServiceCollection services, JwtOptions jwtOptions)
        {
            services.AddSingleton(jwtOptions);
            services.AddScoped<DbSession>();
            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UserAddCommand).Assembly));
        }
    }
}
