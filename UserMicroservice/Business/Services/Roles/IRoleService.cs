using Data.DTOs;
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
        IList<RoleDto> GetAll();
    }
}
