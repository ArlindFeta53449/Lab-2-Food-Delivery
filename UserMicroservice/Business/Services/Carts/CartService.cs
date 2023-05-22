using AutoMapper;
using Business.Services.FileHandling;
using Data.DTOs;
using Data.DTOs.Cart;
using Data.Entities;
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

        public CartService(ICartRepository cartRepository, IMapper mapper, IFileHandlingService fileHandlingService)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _fileHandlingService = fileHandlingService;
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
