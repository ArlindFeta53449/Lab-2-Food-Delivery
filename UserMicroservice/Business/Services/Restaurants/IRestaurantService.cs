﻿using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Restaurants
{
    public interface IRestaurantService
    {
        IList<RestaurantDto> GetAll();
        RestaurantDto GetRestaurant(int id);
        void DeleteRestaurant(int id);
        RestaurantDto CreateRestaurant(RestaurantCreateDto restaurant);
        RestaurantDto EditRestaurant(RestaurantDto restaurant);
    }
}