using CarCrud.Models;
using MongoDB.Driver;

namespace CarCrud.Context
{
    public class CarContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public CarContext(string databaseName)
        {
            var client = new MongoClient();
            _mongoDatabase = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Car> Cars => _mongoDatabase.GetCollection<Car>(nameof(Cars));
    }
}
