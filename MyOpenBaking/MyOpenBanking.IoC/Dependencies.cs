using Microsoft.Extensions.DependencyInjection;
using MyOpenBanking.Application.Services;
using System;

namespace MyOpenBanking.IoC
{
    public static class Dependencies
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            return services;
        }
    }
}
