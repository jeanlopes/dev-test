using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MyOpenBanking.Application.Services;
using MyOpenBanking.Application.Services.Interface;
using MyOpenBanking.DataAccess.Repositories;
using MyOpenBanking.Domain.Repositories;

namespace MyOpenBanking.IoC
{
    public static class Dependencies
    {
        public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseName = configuration.GetValue<string>("MyOpenBankingDatabaseSettings:DatabaseName");
            //var client = new MongoClient(configuration.GetConnectionString("DefaultConnection"));
            //var database = client.GetDatabase(databaseName);

            //_users = database.GetCollection<User>("Users");

            services.AddSingleton(configuration);

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddSingleton<IMongoClient>(c => new MongoClient(connectionString));

            services.AddScoped(c =>
                c.GetService<IMongoClient>().StartSession());



            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region Services
            services.AddScoped<IUserService, UserService>();
            #endregion

            return services;
        }
    }
}
