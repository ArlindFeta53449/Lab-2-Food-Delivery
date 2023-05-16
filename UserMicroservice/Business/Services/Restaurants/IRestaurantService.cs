using Data.DTOs;
using Data.DTOs.Restaurant;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Restaurants
{
    public interface IRestaurantService
    {
        ApiResponse<IList<RestaurantDto>> GetAll();
        ApiResponse<RestaurantDto> GetRestaurant(int id);
        ApiResponse<string> DeleteRestaurant(int id);
        ApiResponse<RestaurantDto> CreateRestaurant(RestaurantCreateDto restaurant, string path, IFormFile file);
        ApiResponse<RestaurantDto> EditRestaurant(RestaurantDto restaurant, string path, IFormFile file);
        ApiResponse<IList<RestaurantForSelectDto>> GetRestaurantsForSelect();
    }
}
