using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MyOpenBanking.Domain.Entities.Base;
using MyOpenBanking.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyOpenBanking.DataAccess.Base
{
    public class CrudRepository<T> : ICrudRepository<T> where T : BaseEntity
    {
        private readonly string _databaseName;
        private readonly IMongoClient _mongoClient;
        private readonly IClientSessionHandle _clientSessionHandle;
        private readonly string _collection;
        private readonly IConfiguration _configuration;
        protected virtual IMongoCollection<T> Collection =>
        _mongoClient.GetDatabase(_databaseName).GetCollection<T>(_collection);

        public CrudRepository(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle, string collection, IConfiguration configuration)
        {
            _configuration = configuration;
            _databaseName = _configuration["MyOpenBankingDatabaseSettings:DatabaseName"];

            (_mongoClient, _clientSessionHandle, _collection) = (mongoClient, clientSessionHandle, collection);

            if (!_mongoClient.GetDatabase(_databaseName).ListCollectionNames().ToList().Contains(collection))
                _mongoClient.GetDatabase(_databaseName).CreateCollection(collection);
        }

        public async Task Create(T entity) =>
            await Collection.InsertOneAsync(_clientSessionHandle, entity);


        public async Task DeleteAsync(string id) =>
            await Collection.DeleteOneAsync(_clientSessionHandle, f => f.Id == id);

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await Collection.Find(_clientSessionHandle, e => true).ToListAsync();
        
        public async Task<T> GetByIdAsync(string id) =>
            await Collection.Find(_clientSessionHandle, e => e.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(T obj)
        {
            Expression<Func<T, string>> func = f => f.Id;
            var value = (string)obj.GetType().GetProperty(func.Body.ToString().Split(".")[1]).GetValue(obj, null);
            var filter = Builders<T>.Filter.Eq(func, value);

            if (obj != null)
                await Collection.ReplaceOneAsync(_clientSessionHandle, filter, obj);
        }
    }
}
