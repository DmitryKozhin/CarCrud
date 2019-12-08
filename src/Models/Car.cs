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
    }   
}
