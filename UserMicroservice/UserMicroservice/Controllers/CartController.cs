using Business.Services.Carts;
using Business.Services.MenuItems;
using Data.DTOs.Cart;
using Data.DTOs.Checkout;
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
        [HttpPut]
        public IActionResult UpdateCartState(CartCreateDto cart)
        {
            var response = _cartService.UpdateCartState(cart);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public IActionResult GetNumberOfItemsInCart(string id)
        {
            var response = _cartService.GetNumberOfItemsInCart(id);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public IActionResult CalculateCartTotalForCheckout(string id)
        {
            var response = _cartService.CalculateCartTotalForCheckout(id);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPost]
        public IActionResult ProcessPayment(CheckoutDto checkout)
        {
            var response = _cartService.CheckoutProcess(checkout);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
