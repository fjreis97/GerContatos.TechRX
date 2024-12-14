using Business.Dtos.Responses;
using Core.Dto.Role;
using Core.Entities;

namespace Business.Interfaces.Services;

public interface IRoleService
{
    Task<PagedResponse<IList<GetAllRoleDto>>> GetAll();
    Task<Response<GetByIdRoleDto?>> GetById(int id);
    Task<Response<CreateRoleDto?>> Create(Role entidade);
    Task<Response<UpdateRoleDto?>> Update(Role entidade);
    Task<Response<DeleteRoleDto?>> Delete(int id);
}
