using MongoDB.Driver;
using MyOpenBanking.Domain.Entities.Base;

namespace MyOpenBanking.Domain.Repositories
{
    public interface ICrudRepository<T> where T : BaseEntity
    {
        IFindFluent<T, T> GetAll();
        T GetById(string id);
        T Create(T entity);
        ReplaceOneResult Update(T entity);
        DeleteResult Delete(string id);
    }
}
