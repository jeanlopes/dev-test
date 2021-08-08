using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MyOpenBanking.Domain.Entities.Base;
using MyOpenBanking.Domain.Repositories;
using System.Linq;

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
            _databaseName = _configuration["MyOpenBankingDatabaseSettings:DatabaseName"]; //GetValue<string>("MyOpenBankingDatabaseSettings:DatabaseName");

            (_mongoClient, _clientSessionHandle, _collection) = (mongoClient, clientSessionHandle, collection);

            if (!_mongoClient.GetDatabase(_databaseName).ListCollectionNames().ToList().Contains(collection))
                _mongoClient.GetDatabase(_databaseName).CreateCollection(collection);
        }

        public T Create(T entity)
        {
            Collection.InsertOne(entity);
            return entity;
        }

        public DeleteResult Delete(string id)
        {
            return Collection.DeleteOne(e => e.Id == id);
        }

        public IFindFluent<T, T> GetAll()
        {
            return Collection.Find(e => true);
        }

        public T GetById(string id)
        {
            return Collection.Find(e => e.Id == id).FirstOrDefault();
        }

        public ReplaceOneResult Update(T entity)
        {
            return Collection.ReplaceOne(e => e.Id == entity.Id, entity);
        }
    }
}
