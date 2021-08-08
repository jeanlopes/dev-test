using MyOpenBanking.Domain.Entities;

namespace MyOpenBanking.Domain.Repositories
{
    public interface IUserRepository : ICrudRepository<User>
    {
        User GetByUserAndPassword(string userName, string password);
    }
}
