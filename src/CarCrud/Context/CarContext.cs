using System;

using CarCrud.Models;
using MongoDB.Driver;

namespace CarCrud.Context
{
    public class CarContext
    {
        private const string DATABASE_NAME = "carstore";

        private readonly IMongoDatabase _mongoDatabase;

        public CarContext()
        {
            var client = new MongoClient();
            _mongoDatabase = client.GetDatabase(DATABASE_NAME);
        }

        public IMongoCollection<Car> Cars => _mongoDatabase.GetCollection<Car>(nameof(Cars));
    }
}
