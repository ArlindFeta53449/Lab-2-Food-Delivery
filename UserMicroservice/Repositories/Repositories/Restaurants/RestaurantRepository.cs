using Data.Entities;
using Repositories;
using Repositories.Repositories.GenericRepository;


namespace Repository.Repositories.Restaurants
{
    public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(AppDbContext context) : base(context)
        {

        }
    }
}
