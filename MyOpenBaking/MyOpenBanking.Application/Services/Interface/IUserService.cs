using MyOpenBanking.Domain.Entities;
using System.Collections.Generic;

namespace MyOpenBanking.Application.Services.Interface
{
    public interface IUserService
    {
        string Authenticate(string userName, string password);
        User Create(User user);
        User GetUser(string id);

        IEnumerable<User> GetUsers();

    }
}
