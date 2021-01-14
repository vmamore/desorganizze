using System.ComponentModel.DataAnnotations;

namespace Desorganizze.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "É obrigatório preencher o username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "É obrigatório preencher a senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "É obrigatório preencher o CPF")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "É obrigatório preencher o nome")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "É obrigatório preencher o sobrenome")]
        public string LastName { get; set; }
    }
}
