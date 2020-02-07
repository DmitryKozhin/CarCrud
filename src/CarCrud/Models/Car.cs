using System;
using MongoDB.Bson.Serialization.Attributes;

namespace CarCrud.Models
{
    public class Car
    {
        [BsonId]
        public int Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public static class CarExtensions
    {
        public static void FromDto(this Car car, UpdateCarDto dto, int id)
        {
            car.Id = id;
            car.Description = dto.Description;

            if (string.Equals(dto.Name, string.Empty))
                throw new ArgumentException("Car name is required");

            car.Name = dto.Name ?? car.Name;
        }

        public static void FromDto(this Car car, CreateCarDto dto, int id)
        {
            car.Id = id;
            car.Name = string.IsNullOrEmpty(dto.Name)
                ? throw new ArgumentException("Car name is required")
                : dto.Name;

            car.Description = dto.Description;
        }
    }
}
