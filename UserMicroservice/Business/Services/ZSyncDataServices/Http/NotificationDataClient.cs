using Data.DTOs.Roles;
using Data.DTOs.Users;
using Data.Entities;
using Microsoft.Extensions.Configuration;
using Repositories.Repositories.Roles;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Business.Services.ZSyncDataServices.Http
{
    public class NotificationDataClient:INotificationDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public NotificationDataClient(
            HttpClient httpClient,
            IConfiguration configuration){

            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendPlatformToCommand(RoleDto role)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(role),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["NotificationService"]}",httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync Post to Command Service was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync Post to Command Service was not OK!");
            }
        }
        public async Task SendUserToNotificationMicroservice(User user)
        {
            var httpContent = new StringContent(
               JsonSerializer.Serialize(new {
               ExternalId = user.Id,
               Name = user.Name,
               Email = user.Email,
               Role = "Customer"
               }),
               Encoding.UTF8,
               "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["NotificationService"]}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync Post to Command Service was OK!");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine("--> Sync Post to Command Service was not OK!");
            }
        }
        public async Task DeleteUserInNotificationMicroservice(string userId)
        {
            var httpContent = new StringContent(
               JsonSerializer.Serialize(userId),
               Encoding.UTF8,
               "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7061/api/u/UserMicroservice/DeleteUser", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync Post to Command Service was OK!");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine("--> Sync Post to Command Service was not OK!");
            }
        }
    }
}
