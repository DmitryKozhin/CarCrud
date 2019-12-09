using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarCrud.Context;
using CarCrud.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarCrud.Repositories
{
    public class CarRepository : ICarRepository
    {
        private const string ID_FIELD = "_id";

        private readonly CarContext _carContext;

        public CarRepository(CarContext carContext)
        {
            _carContext = carContext;
        }

        public async Task Add(CarDto carDto)
        {
            var count = await _carContext.Cars.CountDocumentsAsync(Builders<Car>.Filter.Empty) + 1;
            var car = new Car
            {
                Id = (int)count,
                Name = carDto.Name, Description = carDto.Description
            };

            await _carContext.Cars.InsertOneAsync(car);
        }

        public async Task Delete(int id)
        {
            var deleteResult = await _carContext.Cars.DeleteOneAsync(new BsonDocument(ID_FIELD, id));
            if (!deleteResult.IsAcknowledged)
                throw new InvalidOperationException($"Cannot be delete car with id: {id}");
        }

        public async Task<IEnumerable<Car>> Get()
        {
            var result = await _carContext.Cars.Find(Builders<Car>.Filter.Empty).ToListAsync();
            return result;
        }

        public async Task<Car> Get(int id)
        {
            return await _carContext.Cars.Find(new BsonDocument(ID_FIELD, id)).FirstOrDefaultAsync();
        }

        public async Task Update(CarDto carDto)
        {
            var existingCar = await Get(carDto.Id.Value);
            if (existingCar == null)
                throw new InvalidOperationException($"Car with id: {carDto.Id} is not exist");

            var car = Car.FromDto(carDto);
            var updateResult = await _carContext.Cars.ReplaceOneAsync(new BsonDocument(ID_FIELD, car.Id), car);
            if (!updateResult.IsAcknowledged)
                throw new InvalidOperationException($"Cannot be update car {carDto}");
        }

        public void Dispose()
        {
            _carContext.Dispose();
        }
    }
}
