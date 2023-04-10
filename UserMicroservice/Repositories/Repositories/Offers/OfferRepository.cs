using Data.DTOs;
using Data.Entities;
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
    }
}
