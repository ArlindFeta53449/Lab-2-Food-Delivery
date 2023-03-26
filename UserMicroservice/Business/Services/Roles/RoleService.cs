using AutoMapper;
using Data.DTOs;
using Repositories.Repositories.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Roles
{
    public class RoleService:IRoleService
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly IMapper _mapper;

        public RoleService(IRolesRepository rolesRepository,IMapper mapper)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        public IList<RoleDto> GetAll()
        {
            var roles = _rolesRepository.GetAll();
            return _mapper.Map<IList<RoleDto>>(roles);
        }
    }
}
