using System.ComponentModel.DataAnnotations;

namespace Desorganizze.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "É obrigatório preencher o username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "É obrigatório preencher a senha")]
        public string Password { get; set; }
    }
}
