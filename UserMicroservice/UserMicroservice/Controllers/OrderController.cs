using Business.Services.Orders;
using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Data.Entities;

namespace UserMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var response = _orderService.GetAllOrders();
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPut]
        public IActionResult AcceptOrder(int orderId, string userId)
        {
            var response = _orderService.AcceptOrder(orderId, userId);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = _orderService.GetOrderById(id);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("{agentId}")]
        public IActionResult GetActiveOrderForAgent(string agentId) {
            var response = _orderService.GetActiveOrderForAgent(agentId);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPut("{orderId}/{orderStatus}")]
        public IActionResult UpdateOrderStatus(int orderId, int orderStatus)
        {
            var response = _orderService.UpdateOrderStatus(orderId,orderStatus);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("{orderId}/{distance}")]
        public IActionResult SendOrderStatusToCustomer(int orderId, float distance)
        {

            _orderService.SendOrderStatusToCustomer(orderId, distance);

            return Ok();
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


        [HttpGet]
        public IActionResult GetTopSellingOrders()
        {
            var response = _orderService.GetTopSellingOrders();
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
