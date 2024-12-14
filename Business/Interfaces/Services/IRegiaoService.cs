using Business.Dtos.Responses;
using Core.Dto.Regiao;
using Core.Entities;

namespace Business.Interfaces.Services;

public interface IRegiaoService
{
    Task<PagedResponse<IList<GetAllRegiaoDto>>> GetAll();
    Task<Response<GetByIdRegiaoDto?>> GetById(int id);
    Task<Response<CreateRegiaoDto?>> Create(Regiao entidade);
    Task<Response<UpdateRegiaoDto?>> Update(Regiao entidade);
    Task<Response<DeleteRegiaoDto?>> Delete(int id);
}
