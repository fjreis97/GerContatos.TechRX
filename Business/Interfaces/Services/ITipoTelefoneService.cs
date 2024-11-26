using Business.Dtos.Responses;
using Core.Dto.TipoTelefone;
using Core.Entities;

namespace Business.Interfaces.Services;

public interface ITipoTelefoneService
{
    Task<PagedResponse<IList<GetAllTipoTelefoneDto>>> GetAll();
    Task<Response<GetByIdTipoTelefoneDto?>> GetById(int id);
    Task<Response<CreateTipoTelefoneDto?>> Create(TipoTelefone entidade);
    Task<Response<UpdateTipoTelefoneDto?>> Update(TipoTelefone entidade);
    Task<Response<DeleteTipoTelefoneDto?>> Delete(int id);
}
