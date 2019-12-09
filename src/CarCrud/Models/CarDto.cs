using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarCrud.Models
{
    public class CarDto
    {
        public int? Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"name: {Name}; description: {Description}";
        }
    }
}
