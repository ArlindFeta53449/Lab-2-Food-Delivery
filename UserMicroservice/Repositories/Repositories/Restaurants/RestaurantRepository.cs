using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Restaurants
{
    public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(AppDbContext context) : base(context)
        {

        }
    }
}
