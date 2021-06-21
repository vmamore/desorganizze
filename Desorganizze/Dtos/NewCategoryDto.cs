using System.ComponentModel.DataAnnotations;

namespace Desorganizze.Dtos
{
    public class NewCategoryDto
    {
        [Required]
        [MaxLength(64)]
        public string Description { get; set; }
    }
}
