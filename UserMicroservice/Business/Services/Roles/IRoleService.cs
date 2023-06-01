using Data.DTOs.Roles;
using Data.Entities;
using Repositories.Repositories.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Roles
{
    public interface IRoleService
    {
        ApiResponse<IList<RoleDto>> GetAll();

        ApiResponse<RoleDto> GetRole(int id);

        ApiResponse<string> DeleteRole(int id);

        ApiResponse<RoleDto> CreateRole(RoleCreateDto role);

        ApiResponse<RoleDto> EditRole(RoleDto role);
    }
}
