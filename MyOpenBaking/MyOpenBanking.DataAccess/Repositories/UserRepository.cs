using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MyOpenBanking.DataAccess.Base;
using MyOpenBanking.Domain.Entities;
using MyOpenBanking.Domain.Repositories;
using System.Linq;

namespace MyOpenBanking.DataAccess.Repositories
{
    public class UserRepository : CrudRepository<User>, IUserRepository
    {
        public UserRepository(IMongoClient mongoClient,
        IClientSessionHandle clientSessionHandle,
        IConfiguration configuration) : base(mongoClient, clientSessionHandle, "Users", configuration)
        {            
        }

        public User GetByUserAndPassword(string userName, string password)
        {
            return Collection.Find(u => u.UserName == userName && u.Password == password).FirstOrDefault();
        }
    }
}
