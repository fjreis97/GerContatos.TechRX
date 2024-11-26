using Business.Dtos.Responses;
using Core.Dto.DDD;
using Core.Entities;

namespace Business.Interfaces.Services;

public interface IDDDService
{
    Task<PagedResponse<IList<GetAllDDDDto>>> GetAll();
    Task<Response<GetByIdDDDDto?>> GetById(int id);
    Task<Response<CreateDDDDto?>> Create(DDD entidade);
    Task<Response<UpdateDDDDto?>> Update(DDD entidade);
    Task<Response<DeleteDDDDto?>> Delete(int id);

    
}
