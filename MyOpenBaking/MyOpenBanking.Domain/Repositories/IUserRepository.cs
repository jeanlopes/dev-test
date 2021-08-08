using MyOpenBanking.Domain.Entities;
using System.Threading.Tasks;

namespace MyOpenBanking.Domain.Repositories
{
    public interface IUserRepository : ICrudRepository<User>
    {
        Task<User> GetByUserAndPasswordAsync(string userName, string password);
    }
}
