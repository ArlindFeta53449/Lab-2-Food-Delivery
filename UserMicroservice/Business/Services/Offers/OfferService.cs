using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Repository.Repositories.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Offers
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IMapper _mapper;

        public OfferService(IOfferRepository offerRepository, IMapper mapper)
        {
            _offerRepository = offerRepository;
            _mapper = mapper;
        }

        public IList<OfferDto> GetAll()
        {
            var offers = _offerRepository.GetAll();
            return _mapper.Map<IList<OfferDto>>(offers);
        }

        public OfferDto GetOffer(int id)
        {
            var offer = _offerRepository.Get(id);
            return _mapper.Map<OfferDto>(offer);
        }

        public void DeleteOffer(int id)
        {
            var offer = _offerRepository.Get(id);
            _offerRepository.Remove(offer);
        }

        public OfferDto CreateOffer(OfferCreateDto offer)
        {
            var mappedOffer = _mapper.Map<Offer>(offer);
            _offerRepository.Add(mappedOffer);
            return _mapper.Map<OfferDto>(mappedOffer);
        }

        public OfferDto EditOffer(OfferDto offer)
        {
            var mappedOffer = _mapper.Map<Offer>(offer);
            _offerRepository.Update(mappedOffer);
            return _mapper.Map<OfferDto>(mappedOffer);
        }

    }
}
