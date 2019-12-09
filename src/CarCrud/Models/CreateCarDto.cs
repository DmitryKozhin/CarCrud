using System.ComponentModel.DataAnnotations;

namespace CarCrud.Models
{
    public class CreateCarDto : UpdateCarDto
    {
        public override int? Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public override string Name { get; set; }
    }
}