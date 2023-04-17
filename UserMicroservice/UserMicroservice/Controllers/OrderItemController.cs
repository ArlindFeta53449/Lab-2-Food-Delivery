using Business.Services.OrderItems;
using Data.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Menu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderitemService)
        {
            _orderItemService = orderitemService;
        }

        [HttpGet]
        public IActionResult GetAllOrderItem()
        {
            var orderitem = _orderItemService.GetAll();
            return Ok(orderitem);
        }


        [HttpGet("{id}")]
        public IActionResult GetOrderItemById(int id)
        {
            var orderitem = _orderItemService.GetOrderItem(id);
            return Ok(orderitem);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderItem(int id)
        {
            _orderItemService.DeleteOrderItem(id);
            return Ok("Order Item has been deleted succesfully! ");
        }

        [HttpPost]
        public IActionResult CreateOrderItem(OrderItemCreateDto orderitem)
        {
            var result = _orderItemService.CreateOrderItem(orderitem);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public IActionResult EditOrderItem(OrderItemDto orderitem)
        {
            var result = _orderItemService.EditOrderItem(orderitem);
            return Ok(result);
        }

    }
}
