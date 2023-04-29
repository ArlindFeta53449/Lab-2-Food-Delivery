using Business.Services.Orders;
using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Data.Entities;

namespace UserMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _orderService.GetAllOrdersWithOrderItems();
            return Ok(orders);
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        public IActionResult GetById(int id)
        {
            var order = _orderService.GetOrderById(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderCreateDto orderCreateDto)
        {
            var order = _orderService.CreateOrder(orderCreateDto);

            return Ok(order);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] OrderDto orderDto)
        {
            var result = _orderService.UpdateOrder(orderDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            _orderService.DeleteOrder(id);
            return Ok("Order Item has been deleted succesfully! ");
        }
    }
}
