using Core.Entities;

namespace Business.Interfaces.Services;

public interface ITokenService
{
    public Task<string> GetToken(Usuario usuario);

}
