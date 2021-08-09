using MyOpenBanking.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyOpenBanking.Application.Services.Interface
{
    public interface IUserService
    {
        Task<string> Authenticate(string userName, string password);
        Task<User> Create(User user);
        Task<User> GetUser(string id);
        Task<IEnumerable<User>> GetUsers();
    }
}
