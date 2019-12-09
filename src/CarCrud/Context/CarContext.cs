using System;

using CarCrud.Models;
using MongoDB.Driver;

namespace CarCrud.Context
{
    public class CarContext : IDisposable
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly MongoClient _client;

        public CarContext(string databaseName)
        {
            _client = new MongoClient();
            _mongoDatabase = _client.GetDatabase(databaseName);
        }

        public IMongoCollection<Car> Cars => _mongoDatabase.GetCollection<Car>(nameof(Cars));

        public void Dispose()
        {
            _client.Cluster.Dispose();
        }
    }
}
