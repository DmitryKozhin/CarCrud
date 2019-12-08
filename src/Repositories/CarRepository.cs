using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarCrud.Context;
using CarCrud.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarCrud.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarContext _carContext;

        public CarRepository(CarContext carContext)
        {
            _carContext = carContext;
        }

        public async Task Add(CarDto carDto)
        {
            var count = await _carContext.Cars.CountDocumentsAsync(Builders<Car>.Filter.Empty) + 1;
            var car = new Car()
            {
                Id = (int)count,
                Name = carDto.Name, Description = carDto.Description
            };

            await _carContext.Cars.InsertOneAsync(car);
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Car>> Get()
        {
            var result = await _carContext.Cars.Find(Builders<Car>.Filter.Empty).ToListAsync();
            return result;
        }

        public async Task<Car> Get(int id)
        {
            return await _carContext.Cars.Find(new BsonDocument("_id", id)).FirstOrDefaultAsync();
        }

        public Task Update(CarDto car)
        {
            throw new NotImplementedException();
        }
    }
}
