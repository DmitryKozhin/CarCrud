using System;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Linq;

namespace CarCrud.Models
{
    public class Car
    {
        [BsonId]
        public int Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }
        public string Description { get; set; }

        public static Car FromDto(CarDto dto)
        {
            if (!dto.Id.HasValue)
                throw new ArgumentException($"DTO for update model should contain {nameof(dto.Id)}");

            return new Car
            {
                Id = dto.Id.Value,
                Name = dto.Name ?? throw new ArgumentException("Car name is required"),
                Description = dto.Description,
            };
        }
    }   
}
