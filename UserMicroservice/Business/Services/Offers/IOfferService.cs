using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Offers
{
    public interface IOfferService
    {
        IList<OfferDto> GetAll();
        OfferDto CreateOffer(OfferCreateDto offer);
        void DeleteOffer(int id);
        OfferDto GetOffer(int id);
        OfferDto EditOffer(OfferDto offer);

    }
}
