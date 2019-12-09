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

        public async Task Add(CreateCarDto carDto)
        {
            var count = await _carContext.Cars.CountDocumentsAsync(Builders<Car>.Filter.Empty) + 1;
            var car = new Car();
            car.FromDto(carDto, (int) count);

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

        public async Task Update(UpdateCarDto carDto)
        {
            var existingCar = await Get(carDto.Id.Value);
            if (existingCar == null)
                throw new InvalidOperationException($"Car with id: {carDto.Id} is not exist");

            existingCar.FromDto(carDto, carDto.Id.Value);

            var updateResult = await _carContext.Cars.ReplaceOneAsync(new BsonDocument(ID_FIELD, existingCar.Id), existingCar);
            if (!updateResult.IsAcknowledged)
                throw new InvalidOperationException($"Cannot be update car {carDto}");
        }
    }
}
