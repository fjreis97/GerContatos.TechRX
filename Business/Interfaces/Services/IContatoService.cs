using Business.Dtos.Responses;
using Core.Dto.Contato;
using Core.Entities;

namespace Business.Interfaces.Services;

public interface IContatoService
{
    Task<PagedResponse<IList<GetAllContatoDto>>> GetAll();
    Task<Response<GetByIdContatoDto?>> GetById(int id);
    Task<Response<CreateContatoDto?>> Create(Contato entidade);
    Task<Response<UpdateContatoDto?>> Update(Contato entidade);
    Task<Response<DeleteContatoDto?>> Delete(int id);
}
