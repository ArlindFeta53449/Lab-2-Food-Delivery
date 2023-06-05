using Data.DTOs.Roles;
using Data.DTOs.Users;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.ZSyncDataServices.Http
{
    public interface INotificationDataClient
    {
        Task SendPlatformToCommand(RoleDto role);
        Task SendUserToNotificationMicroservice(User user);
        Task DeleteUserInNotificationMicroservice(string userId);
    }
}
