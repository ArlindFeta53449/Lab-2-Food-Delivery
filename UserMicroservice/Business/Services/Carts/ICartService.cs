using Data.DTOs.Cart;
using Data.DTOs.Checkout;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Carts
{
    public interface ICartService
    {
        ApiResponse<CartDto> GetCartByUserId(string userId);
        ApiResponse<CartDto> AddToCart(CartCreateDto cart);
        ApiResponse<string> RemoveOfferFromCart(int cartId, int offerItemId);
        ApiResponse<string> RemoveMenuItemFromCart(int cartId, int menuItemId);
        ApiResponse<CartDto> UpdateCartState(CartCreateDto cart);
        ApiResponse<int> GetNumberOfItemsInCart(string userId);
        ApiResponse<int> CalculateCartTotalForCheckout(string userId);
        ApiResponse<string> CheckoutProcess(CheckoutDto checkout);
    }
}
