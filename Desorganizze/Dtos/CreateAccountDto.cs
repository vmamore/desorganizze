using System.ComponentModel.DataAnnotations;

namespace Desorganizze.Dtos
{
    public class CreateAccountDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
    }
}
