using Data.DTOs.Restaurant;
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

        public IList<RestaurantForSelectDto> RestaurantForSelectDto()
        { 
        return Context.Set<Restaurant>().Select(x=>
            new RestaurantForSelectDto()
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                ImagePath = x.ImagePath,
            }
            ).ToList();
        }
    }
}
