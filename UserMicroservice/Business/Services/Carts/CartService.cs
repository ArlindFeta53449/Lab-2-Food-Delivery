using AutoMapper;
using Business.Services.FileHandling;
using Data.DTOs;
using Data.DTOs.Cart;
using Data.Entities;
using Repositories.Repositories.CartMenuItems;
using Repositories.Repositories.CartOffers;
using Repositories.Repositories.Carts;
using Repositories.Repositories.MenusItems;
using Repository.Repositories.MenusItems;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Carts
{
    public class CartService:ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly IFileHandlingService _fileHandlingService;
        private readonly ICartMenuItemRepository _cartMenuItemRepository;
        private readonly ICartOfferRepository _cartOfferRepository;

        public CartService(
            ICartRepository cartRepository,
            IMapper mapper,
            IFileHandlingService fileHandlingService,
            ICartMenuItemRepository cartMenuItemRepository,
            ICartOfferRepository cartOfferRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _fileHandlingService = fileHandlingService;
            _cartMenuItemRepository = cartMenuItemRepository;
            _cartOfferRepository = cartOfferRepository;
        }
        public ApiResponse<CartDto> GetCartByUserId(string userId)
        {
            try
            {
                var cart = _cartRepository.GetCartByUserId(userId);
                foreach(var menuItem in cart.MenuItems)
                {
                    menuItem.ImagePath = _fileHandlingService.ConvertFilePathForImage(menuItem.ImagePath);
                }
                foreach(var offer in cart.Offers)
                {
                    offer.ImagePath = _fileHandlingService.ConvertFilePathForImage(offer.ImagePath);
                }
                if (cart == null)
                {
                    return new ApiResponse<CartDto>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The user's cart was not found" }
                    };
                }
                return new ApiResponse<CartDto>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<CartDto>(cart)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<CartDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public ApiResponse<string> RemoveMenuItemFromCart(int cartId,int menuItemId)
        {
            try
            {
                var menuItem = _cartMenuItemRepository.GetMenuItemInCart(cartId, menuItemId);
                if (menuItem != null)
                {
                    _cartMenuItemRepository.Remove(menuItem);
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "The menu item was removed successfully"
                    };
                }
                return new ApiResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "The menu item does not exist" }
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
        public ApiResponse<string> RemoveOfferFromCart(int cartId,int offerItemId)
        {
            try
            {
                var offer = _cartOfferRepository.GetOfferInCart(cartId, offerItemId);
                if (offer != null)
                {
                    _cartOfferRepository.Remove(offer);
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "The offer was removed successfully"
                    };
                }
                return new ApiResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "The offer does not exist" }
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
        public ApiResponse<CartDto> AddToCart(CartCreateDto cart)
        {
            try
            {
                var cartInDb = _cartRepository.Get(cart.Id);
                if (cart == null)
                {
                    return new ApiResponse<CartDto>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The user's cart was not found" }
                    };
                }
                _mapper.Map(cart, cartInDb);
                
                if (_cartRepository.Update(cartInDb))
                {
                    var cartToReturn = _cartRepository.GetCartByUserId(cartInDb.UserId);
                    return new ApiResponse<CartDto>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data =cartToReturn
                    };
                }
                return new ApiResponse<CartDto>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Errors = new List<string>() { "The cart was not updated. Please try again." }
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<CartDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
    }
}
