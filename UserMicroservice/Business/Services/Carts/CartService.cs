using AutoMapper;
using Business.Services.FileHandling;
using Business.Services.Stripe;
using Data.DTOs;
using Data.DTOs.Cart;
using Data.DTOs.Checkout;
using Data.Entities;
using Microsoft.VisualBasic;
using Repositories.Repositories.CartMenuItems;
using Repositories.Repositories.CartOffers;
using Repositories.Repositories.Carts;
using Repositories.Repositories.MenusItems;
using Repositories.Repositories.Payments;
using Repositories.Repositories.Users;
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
        private readonly IUserRepository _userRepository;
        private readonly IStripeService _stripeService;
        private readonly IPaymentRepository _paymentRepository;

        public CartService(
            ICartRepository cartRepository,
            IMapper mapper,
            IFileHandlingService fileHandlingService,
            ICartMenuItemRepository cartMenuItemRepository,
            ICartOfferRepository cartOfferRepository,
            IUserRepository userRepository,
            IStripeService stripeService,
            IPaymentRepository paymentRepository
            )
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _fileHandlingService = fileHandlingService;
            _cartMenuItemRepository = cartMenuItemRepository;
            _cartOfferRepository = cartOfferRepository;
            _userRepository = userRepository;
            _stripeService = stripeService;
            _paymentRepository = paymentRepository;
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
                if (cart.CartMenuItems != null)
                {
                    foreach (var menuItem in cart.CartMenuItems)
                    {
                        var menuItemInCart = _cartMenuItemRepository.GetMenuItemInCart(cart.Id, menuItem.MenuItemId);
                        if (menuItemInCart != null)
                        {
                            menuItem.Id = menuItemInCart.Id;
                            menuItem.CartId = cart.Id;
                            menuItem.Quantity += menuItemInCart.Quantity;
                        }
                    }
                }

                if (cart.CartOffers != null)
                {
                    foreach (var offer in cart.CartOffers)
                    {
                        var offerInCart = _cartOfferRepository.GetOfferInCart(cart.Id, offer.OfferId);
                        if (offerInCart != null)
                        {
                            offer.Id = offerInCart.Id;
                            offer.Quantity += offerInCart.Quantity;
                            offer.CartId = cart.Id;

                        }
                    }
                }

                _mapper.Map(cart, cartInDb);
                
                if (_cartRepository.Update(cartInDb))
                {
                    var cartToReturn = _cartRepository.GetCartByUserId(cartInDb.UserId);
                    return new ApiResponse<CartDto>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data =cartToReturn,
                        Message = "The item was added to the cart."

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
        public ApiResponse<CartDto> UpdateCartState(CartCreateDto cart)
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
                var menuItemsInCart = _cartMenuItemRepository.GetMenuItemsInCartByCartId(cart.Id);
                if(menuItemsInCart != null)
                {
                    _cartMenuItemRepository.RemoveRange(menuItemsInCart);
                }
                var offersInCart = _cartOfferRepository.GetOffersInCartByCartId(cart.Id);
                if(offersInCart != null)
                {
                    _cartOfferRepository.RemoveRange(offersInCart);
                }
                _mapper.Map(cart, cartInDb);

                _cartRepository.Update(cartInDb);
                
                    var cartToReturn = _cartRepository.GetCartByUserId(cartInDb.UserId);
                    return new ApiResponse<CartDto>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = cartToReturn,

                    };
                
                /*return new ApiResponse<CartDto>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Errors = new List<string>() { "The cart was not updated. Please try again." }
                };*/
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
        public ApiResponse<int> GetNumberOfItemsInCart(string userId)
        {
            try
            {
                var numberOfMenuItems = _cartMenuItemRepository.GetNumberOfMenuItemsInCartByUserId(userId);
                var numberOfOffers = _cartOfferRepository.GetNumberOfOffersInCartByUserId(userId);

                return new ApiResponse<int>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = numberOfMenuItems + numberOfOffers,

                }; 
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<int>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public ApiResponse<int> CalculateCartTotalForCheckout(string userId)
        {
            try
            {
                if(userId != null)
                {
                    var menuItems = _cartMenuItemRepository.GetMenuItemsForTotalCalculation(userId);
                    var offers = _cartOfferRepository.GetOffersForTotalCalculation(userId);
                    var total = 0;

                    foreach (var menuItem in menuItems)
                    {
                        total += menuItem.Quantity * (int)menuItem.Price;
                    }
                    foreach (var offer in offers)
                    {
                        total += offer.Quantity * (int)offer.Price;
                    }
                    return new ApiResponse<int>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = total*100,

                    };
                }
                return new ApiResponse<int>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Errors = new List<string>() { "The user does not exist"},

                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<int>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }

        }
        public ApiResponse<string> CheckoutProcess(CheckoutDto checkout)
        {
            try
            {
                var user = _userRepository.GetUserById(checkout.userId);
                if (user != null && user.StripeCustomerId == null && checkout.StripeCustomer != null)
                {
                    checkout.StripeCustomer.Name = user.Name;
                    checkout.StripeCustomer.Email = user.Email;
                    user.StripeCustomerId = _stripeService.AddStripeCustomer(checkout.StripeCustomer);
                    _userRepository.Update(user);
                }
                checkout.PaymentIntent.StripeCustomerId = user.StripeCustomerId;
                if(checkout.StripeCustomer.CardToken != null)
                {
                    checkout.PaymentIntent.PaymentMethod = checkout.StripeCustomer.CardToken;
                }
                else
                {
                    checkout.PaymentIntent.PaymentMethod = _stripeService.GetCardTokenForCustomer(checkout.userId);
                }

                var payment = _stripeService.AddStripePaymentIntent(checkout.PaymentIntent);
                _paymentRepository.Add(payment);
                return new ApiResponse<string>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "The payment was processed"
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
    }
}
