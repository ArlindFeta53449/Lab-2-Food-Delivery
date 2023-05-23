using Business.Services.Carts;
using Business.Services.MenuItems;
using Data.DTOs.Cart;
using Microsoft.AspNetCore.Mvc;

namespace UserMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CartController(ICartService cartService, IWebHostEnvironment webHostEnvironment)
        {
            _cartService = cartService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("{id}")]
        public IActionResult GetCartByUserId(string id)
        {
            var response = _cartService.GetCartByUserId(id);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPut]
        public IActionResult AddToCart(CartCreateDto cart)
        {
            var response = _cartService.AddToCart(cart);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpDelete]
        public IActionResult RemoveMenuItemFromCart(int cartId,int menuItemId) {
            var response = _cartService.RemoveMenuItemFromCart(cartId, menuItemId);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpDelete]
        public IActionResult RemoveOfferFromCart(int cartId, int offerId)
        {
            var response = _cartService.RemoveOfferFromCart(cartId, offerId);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
