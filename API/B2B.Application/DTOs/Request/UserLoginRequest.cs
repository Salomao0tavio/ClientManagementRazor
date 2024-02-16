using System.ComponentModel.DataAnnotations;

namespace B2B.Application.DTOs.Request;

public class UserLoginRequest
{
    [Required(ErrorMessage = "Insira o Email.")]
    [EmailAddress(ErrorMessage = "O email é inválido.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Insira a senha.")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}