using System.ComponentModel.DataAnnotations;


namespace Application.B2B_Application.DTOs.Request
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} é inválido")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public required string Password { get; set; }
    }
}
