using Business.Dtos.Responses;
using Core.Dto.Usuarios;
using Core.Entities;

namespace Business.Interfaces.Services;

public interface IUsuarioService 
{
    Task<PagedResponse<IList<GetAllUsuarioDto>>> GetAll();
    Task<Response<UsuarioGetByIdDto?>> GetById(int id);
    Task<Response<CreateUsuarioDto?>> Create(Usuario entidade);
    Task<Response<UpdateUsuarioDto?>> Update(Usuario entidade);
    Task<Response<DeleteUsuarioDto?>> Delete(int id);
    Task<PagedResponse<IList<GetAllByTokenDto>>> GetAllToken();
}

