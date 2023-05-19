using AutoMapper;
using Business.Services.FileHandling;
using Data.DTOs;
using Data.DTOs.MenuItem;
using Data.DTOs.Offer;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Repositories.Repositories.MenuItemOffers;
using Repository.Repositories.Offers;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Business.Services.Offers
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IMapper _mapper;
        private readonly IFileHandlingService _fileHandlingService;
        private readonly IMenuItemOffersRepository _menuItemOffersRepository;

        public OfferService(
            IOfferRepository offerRepository,
            IMapper mapper,
            IFileHandlingService fileHandlingService,
            IMenuItemOffersRepository menuItemOffersRepository
            
            )
        {
            _offerRepository = offerRepository;
            _mapper = mapper;
            _fileHandlingService = fileHandlingService;
            _menuItemOffersRepository = menuItemOffersRepository;
        }
        public ApiResponse<IList<OfferForDisplayDto>> GetOffersForDisplay()
        {
            try
            {
                var offers = _offerRepository.GetOffersIncludeRestaurants();
                foreach (var offer in offers)
                {
                    offer.ImagePath = _fileHandlingService.ConvertFilePathForImage(offer.ImagePath);
                }
                return new ApiResponse<IList<OfferForDisplayDto>>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<IList<OfferForDisplayDto>>(offers)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<IList<OfferForDisplayDto>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public ApiResponse<IList<OfferDto>> GetAll()
        {
            try
            {
                var offers = _offerRepository.GetAll();
                foreach (var offer in offers)
                {
                    offer.ImagePath = _fileHandlingService.ConvertFilePathForImage(offer.ImagePath);
                }
                return new ApiResponse<IList<OfferDto>>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<IList<OfferDto>>(offers)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<IList<OfferDto>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }

        public ApiResponse<OfferDto> GetOffer(int id)
        {
            try
            {
                var offer = _offerRepository.Get(id);
                if (offer == null)
                {
                    return new ApiResponse<OfferDto>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The offer was not found" }
                    };
                }
                offer.ImagePath = _fileHandlingService.ConvertFilePathForImage(offer.ImagePath);
                return new ApiResponse<OfferDto>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<OfferDto>(offer)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<OfferDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }

        public ApiResponse<string> DeleteOffer(int id)
        {
            try
            {
                var offer = _offerRepository.Get(id);
                if (offer == null)
                {
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The offer does not exist" }
                    };
                }
                _menuItemOffersRepository.RemoveMenuItemOffersByOfferId(id);
                if (_offerRepository.Remove(offer))
                {
                    var fileDeleted = _fileHandlingService.DeleteFile(offer.ImagePath);
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "The offer was deleted successfully"
                    };
                }
                return new ApiResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "The offer was not deleted. Try again." }
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }

        public ApiResponse<OfferDto> CreateOffer(OfferCreateDto offer, string path, IFormFile file,string menuItemOffersJson)
        {
            try
            {
                
                var menuItemOffers = JsonSerializer.Deserialize<List<MenuItemOfferCreateDto>>(menuItemOffersJson);
                offer.MenuItemOffers = menuItemOffers;
                var mappedOffer = _mapper.Map<Offer>(offer);

               if (file != null && file.Length > 0)
                {

                    var fileObject = _fileHandlingService.SaveFile(file, "Offers", path, new string[] { ".jpeg", ".png", ".jpg" });
                    if (fileObject == null)
                    {
                        return new ApiResponse<OfferDto>()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Errors = new List<string>() { "The file type is not correct" }
                        };
                    }
                    mappedOffer.Image = fileObject.fileName;
                    mappedOffer.ImagePath = fileObject.filePath;
                }
                if (_offerRepository.Add(mappedOffer))
                {

                    mappedOffer.ImagePath = _fileHandlingService.ConvertFilePathForImage(mappedOffer.ImagePath);
                    return new ApiResponse<OfferDto>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = _mapper.Map<OfferDto>(mappedOffer)
                    };
                }
                return new ApiResponse<OfferDto>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "There was a problem while saving the offer.Please try again." }
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<OfferDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }

        public ApiResponse<OfferDto> EditOffer(OfferDto offer, string path, IFormFile file)
        {
            try
            {
                var offerInDb = _offerRepository.Get(offer.Id);
                offer.RestaurantId = offerInDb.RestaurantId;
                if (offerInDb == null)
                {
                    return new ApiResponse<OfferDto>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The offer does not exist" }
                    };
                }
                var image = offerInDb.Image;
                var imagePath = offerInDb.ImagePath;
                _mapper.Map(offer, offerInDb);
                if (file != null && file.Length > 0)
                {

                    var fileObject = _fileHandlingService.SaveFile(file, "Offers", path, new string[] { ".jpeg", ".png", ".jpg" });
                    if (fileObject == null)
                    {
                        return new ApiResponse<OfferDto>()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Errors = new List<string>() { "The file type is not correct" }
                        };
                    }
                    _fileHandlingService.DeleteFile(imagePath);
                    offerInDb.Image = fileObject.fileName;
                    offerInDb.ImagePath = fileObject.filePath;
                }
                if (offerInDb.ImagePath == null && offerInDb.Image == null)
                {
                    offerInDb.ImagePath = imagePath;
                    offerInDb.Image = image;
                }
                if (_offerRepository.Update(offerInDb))
                {

                    offerInDb.ImagePath = _fileHandlingService.ConvertFilePathForImage(offerInDb.ImagePath);
                    return new ApiResponse<OfferDto>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = _mapper.Map<OfferDto>(offerInDb)
                    };
                }
                return new ApiResponse<OfferDto>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Errors = new List<string>() { "The offer was not updated. Please try again." }
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<OfferDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }

    }
}
