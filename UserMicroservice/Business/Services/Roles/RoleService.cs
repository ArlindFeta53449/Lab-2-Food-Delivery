using AutoMapper;
using Business.Services.ZSyncDataServices.Http;
using Data.DTOs.Roles;
using Data.Entities;
using Repositories.Repositories.Roles;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Roles
{
    public class RoleService:IRoleService
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly IMapper _mapper;
        private readonly INotificationDataClient _notificationDataClient;
        public RoleService(IRolesRepository rolesRepository,
            IMapper mapper,
            INotificationDataClient notificationDataClient)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
            _notificationDataClient = notificationDataClient;
        }

        public ApiResponse<IList<RoleDto>> GetAll()
        {
            try
            {
                var roles = _rolesRepository.GetAll();
                return new ApiResponse<IList<RoleDto>>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<IList<RoleDto>>(roles)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<IList<RoleDto>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
            
        }

        public ApiResponse<RoleDto> GetRole(int id)
        {
            try
            {
                var role = _rolesRepository.Get(id);
                if (role == null)
                {
                    return new ApiResponse<RoleDto>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The role does not exist" }
                    };

                }
                return new ApiResponse<RoleDto>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<RoleDto>(role)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<RoleDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
            
        }
        public ApiResponse<string> DeleteRole(int id)
        {
            try
            {

                var role = _rolesRepository.Get(id);
                if (role == null)
                {
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The role does not exist" }
                    };

                }
                _rolesRepository.Remove(role);
                
                 return new ApiResponse<string>()
                 {
                    StatusCode = HttpStatusCode.OK,
                    Message = "The role was deleted successfully"
                 };
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }

        }
        public ApiResponse<RoleDto> CreateRole(RoleCreateDto role)
        {
            try
            {
                var mappedRole = _mapper.Map<Role>(role);
                if (_rolesRepository.Add(mappedRole))
                {
                    _notificationDataClient.SendPlatformToCommand(_mapper.Map<RoleDto>(mappedRole));
                    return new ApiResponse<RoleDto>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = _mapper.Map<RoleDto>(mappedRole)
                    };
                }
                return new ApiResponse<RoleDto>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "An error occurred while processing your request." }
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<RoleDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> {"An error occurred while processing your request. Please try again later." }
                };
            }


        }
        public ApiResponse<RoleDto> EditRole(RoleDto role)
        {
            try
            {
                var mappedRole = _mapper.Map<Role>(role);
                _rolesRepository.Update(mappedRole);
                
                    return new ApiResponse<RoleDto>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = _mapper.Map<RoleDto>(mappedRole)
                    };
               
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<RoleDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }

        }
    }
}
