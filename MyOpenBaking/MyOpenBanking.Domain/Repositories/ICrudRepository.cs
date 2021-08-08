using MongoDB.Driver;
using MyOpenBanking.Domain.Entities.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyOpenBanking.Domain.Repositories
{
    public interface ICrudRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task Create(T entity);
        Task UpdateAsync(T obj);
        Task DeleteAsync(string id);
    }
}
