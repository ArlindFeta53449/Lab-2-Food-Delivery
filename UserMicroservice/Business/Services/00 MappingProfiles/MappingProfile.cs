using AutoMapper;
using Data.DTOs.Roles;
using Data.DTOs.Users;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services._00_MappingProfiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();
            CreateMap<Role, RoleCreateDto>();
            CreateMap<RoleCreateDto, Role>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserCreateDto, User>();   
            CreateMap<User, UserCreateDto>();
            
        }
    }
}
