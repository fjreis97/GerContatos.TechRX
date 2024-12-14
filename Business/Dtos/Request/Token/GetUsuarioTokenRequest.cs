using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.Request.Token;

public class GetUsuarioTokenRequest
{
    [Required(ErrorMessage = "O preenchimento do e-mail é obrigatório")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O preenchimento da senha é obrigatório")]
    public string Password { get; set; }
}
