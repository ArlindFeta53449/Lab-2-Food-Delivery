using Data.DTOs;
using Data.DTOs.Offer;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Offers
{
    public interface IOfferService
    {
        ApiResponse<IList<OfferDto>> GetAll();
        ApiResponse<OfferDto> CreateOffer(OfferCreateDto offer, string path, IFormFile file,string menuItemOffersJson);
        ApiResponse<string> DeleteOffer(int id);
        ApiResponse<OfferDto> GetOffer(int id);
        ApiResponse<OfferDto> EditOffer(OfferDto offer, string path, IFormFile file);
        ApiResponse<IList<OfferForDisplayDto>> GetOffersForDisplay();

    }
}
