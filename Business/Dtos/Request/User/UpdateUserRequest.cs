using System.ComponentModel.DataAnnotations;
using Business.Enums;

namespace Business.Dtos.Request.User;

public class UpdateUserRequest
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(255, ErrorMessage = "O nome pode conter até 255 caracteres")]
    public string Nome { get; set; }
    [EmailAddress]
    [Required(ErrorMessage = "Email é obrigatório")]
    public string Email { get; set; }
    [Required(ErrorMessage = "O preenchimento da senha é obrigatório")]
    [StringLength(16, MinimumLength = 8, ErrorMessage = "A senha deve conter entre 8 e 16 caracteres")]
    public string Senha { get; set; }
    [Required(ErrorMessage = "Obrigatório o tipo do Papel do usuário")]
    public ERole Role { get; set; }
}
