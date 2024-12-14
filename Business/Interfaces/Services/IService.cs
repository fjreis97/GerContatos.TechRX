using Business.Dtos.Responses;

namespace Business.Interfaces.Services;

public interface IService<T>
{
    Task<PagedResponse<IList<T>>> GetAll();
    Task<Response<T?>> GetById(int id);
    Task<Response<T?>> Create(T entidade);
    Task<Response<T?>> Update(T entidade);
    Task<Response<T?>> Delete(int id);

}
