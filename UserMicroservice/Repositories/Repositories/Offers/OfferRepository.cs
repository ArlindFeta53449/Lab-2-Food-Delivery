using Data.DTOs;
using Data.DTOs.MenuItem;
using Data.DTOs.Offer;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Offers
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {

        public OfferRepository(AppDbContext context) : base(context)
        {

        }
        public IList<OfferForDisplayDto> GetOffersIncludeRestaurants()
        {
            return Context.Set<Offer>().Include(x => x.Restaurant).Select(
                x => new OfferForDisplayDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    DiscountPercent = x.DiscountPercent,
                    StartDate  = x.StartDate,
                    EndDate = x.EndDate,
                    Image = x.Image,
                    ImagePath = x.ImagePath,
                    Restaurant = x.Restaurant.Name,
                    RestaurantId = x.RestaurantId

                }).ToList();
        }
    }
}
