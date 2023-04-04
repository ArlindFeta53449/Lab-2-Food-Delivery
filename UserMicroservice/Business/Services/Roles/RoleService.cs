using AutoMapper;
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

        public RoleDto GetRole(int id)
        {
            var role = _rolesRepository.Get(id);
            return _mapper.Map<RoleDto>(role);
        }
        public void DeleteRole(int id)
        {
            var role = _rolesRepository.Get(id);
           _rolesRepository.Remove(role);
        }
        public RoleDto CreateRole(RoleCreateDto role)
        {
            var mappedRole = _mapper.Map<Role>(role);
            _rolesRepository.Add(mappedRole);
            return _mapper.Map<RoleDto>(mappedRole);
        }
        public RoleDto EditRole(RoleDto role)
        {
            var mappedRole = _mapper.Map<Role>(role);
            _rolesRepository.Update(mappedRole);
            return _mapper.Map<RoleDto>(mappedRole);
        }
    }
}
