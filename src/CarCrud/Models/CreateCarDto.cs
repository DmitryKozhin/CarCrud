using System.ComponentModel.DataAnnotations;

namespace CarCrud.Models
{
    public class CreateCarDto : UpdateCarDto
    {
        [Required(AllowEmptyStrings = false)]
        public override string Name { get; set; }
    }
}