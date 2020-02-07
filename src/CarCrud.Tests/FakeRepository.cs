using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarCrud.Models;
using CarCrud.Repositories;

namespace CarCrud.Tests
{
    public class FakeRepository : ICarRepository
    {
        private readonly List<Car> _cars = new List<Car>();

        public async Task Add(CreateCarDto carDto)
        {
            await Task.Delay(1);
            var count = _cars.Count + 1;
            var car = new Car();
            car.FromDto(carDto, (int)count);

            _cars.Add(car);
        }

        public async Task Delete(int id)
        {
            await Task.Delay(1);
            var existingCar = _cars.Single(t => t.Id == id);
            _cars.Remove(existingCar);
        }

        public async Task<IEnumerable<Car>> Get()
        {
            await Task.Delay(1);
            return _cars;
        }

        public async Task<Car> Get(int id)
        {
            await Task.Delay(1);
            return _cars.FirstOrDefault(t => t.Id == id);
        }

        public async Task Update(int id, UpdateCarDto carDto)
        {
            var existingCar = await Get(id);
            if (existingCar == null)
                throw new InvalidOperationException($"Car with id: {id} is not exist");

            existingCar.FromDto(carDto, id);
        }
    }
}