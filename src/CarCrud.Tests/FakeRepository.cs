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
        private List<Car> _cars = new List<Car>();

        public async Task Add(CarDto carDto)
        {
            await Task.Delay(1);
            var count = _cars.Count + 1;
            var car = new Car
            {
                Id = (int)count,
                Name = carDto.Name,
                Description = carDto.Description
            };

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

        public async Task Update(CarDto carDto)
        {
            var existingCar = await Get(carDto.Id.Value);
            if (existingCar == null)
                throw new InvalidOperationException($"Car with id: {carDto.Id} is not exist");

            var car = Car.FromDto(carDto);
            existingCar.Id = car.Id;
            existingCar.Name = car.Name;
            existingCar.Description = car.Description;
        }
    }
}