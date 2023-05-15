using Data.DTOs.Restaurant;
using Data.Entities;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Restaurants
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        IList<RestaurantForSelectDto> RestaurantForSelectDto();
    }
}
