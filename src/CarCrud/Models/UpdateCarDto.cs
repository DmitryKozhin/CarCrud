using System.ComponentModel.DataAnnotations;

namespace CarCrud.Models
{
    public class UpdateCarDto
    {
        [Required]
        public virtual int? Id { get; set; }

        public virtual string Name { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return $"name: {Name}; description: {Description}";
        }
    }
}
