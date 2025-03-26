using AutoMapper;
using Business.Services._01_Mailing;
using Business.Services.XAsyncDataService;
using Data.DTOs;
using Data.DTOs.MenuItem;
using Data.DTOs.Order;
using Data.DTOs.RabbitMQ;
using Data.Entities;
using Data.Enums;
using Microsoft.AspNetCore.Http;
using Repositories.Repositories.Users;
using Repository.Repositories.Orders;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Orders
{

    public class OrderService : IOrderService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;
        private readonly IMessageBusClient _messageBusClient;
        public OrderService(
            IOrdersRepository ordersRepository,
            IMapper mapper,
            IUserRepository userRepository,
            IMailService mailService,
            IMessageBusClient messageBusClient
            )
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _mailService = mailService;
            _messageBusClient = messageBusClient;
        }

        public ApiResponse<OrderForDisplayDto> GetOrderById(int id)
        {
           
            try
            {
                var order = _ordersRepository.GetOrderById(id);
                if (order == null)
                {
                    return new ApiResponse<OrderForDisplayDto>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The menu item was not found" }
                    };
                }
               
                return new ApiResponse<OrderForDisplayDto>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<OrderForDisplayDto>(order)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<OrderForDisplayDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }

        }
        public ApiResponse<OrderForDisplayDto> GetActiveOrderForAgent(string agentId)
        {

            try
            {
                var order = _ordersRepository.GetActiveOrderForAgent(agentId);
                if (order == null)
                {
                    return new ApiResponse<OrderForDisplayDto>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "You need to accept an order first" }
                    };
                }

                return new ApiResponse<OrderForDisplayDto>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<OrderForDisplayDto>(order)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<OrderForDisplayDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }

        }

        public ApiResponse<IList<OrderForDisplayDto>> GetAllOrders()
        {
            try
            {
                var orders = _ordersRepository.GetAllOrders();
                
                return new ApiResponse<IList<OrderForDisplayDto>>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = orders
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<IList<OrderForDisplayDto>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public ApiResponse<string> AcceptOrder(int orderId, string userId)
        {
            try
            {
                var order = _ordersRepository.Get(orderId);
                var user = _userRepository.GetUserById(userId);
                if(order != null && user != null && user.Role.Name == "Agent" && user.AgentHasOrder == false && order.IsDelivered ==false)
                {
                    order.AgentId = user.Id;
                    order.OrderStatus = OrderStatuses.OrderSelected;
                    user.AgentHasOrder = true;
                    if (_ordersRepository.Update(order) && _userRepository.Update(user))
                    {
                        var orderForEmail = _ordersRepository.GetOrderById(orderId);
                        var orderForNotification = _mapper.Map<OrderPublishedDto>(order);
                        orderForNotification.Event = "Order_Accepted";
                        _messageBusClient.PublishMessage<OrderPublishedDto>(orderForNotification);
                        _mailService.SendEmailToCustomerWhenOrderAcceptedAsync(user,orderForEmail);
                        return new ApiResponse<string>()
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "You have accepted the order"
                        };
                    }
                    else
                    {
                        return new ApiResponse<string>()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Errors = new List<string>() { "There was a problem with accepting the order. Please try again."}
                        };
                    }

                }
                return new ApiResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "You can not accept the order" }
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

        public ApiResponse<OrderForDisplayDto> UpdateOrderStatus(int orderId,int orderStatus)
        {
            try
            {
                var order = _ordersRepository.Get(orderId);
                if(order == null)
                {
                    return new ApiResponse<OrderForDisplayDto>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The order does not exist"}
                    };
                }
                order.OrderStatus = (OrderStatuses)orderStatus;
                var orderToReturn = _ordersRepository.GetOrderById(orderId);
                var user = _userRepository.GetUserById(order.UserId);
                switch (orderStatus)
                {
                    case 2:
                        _mailService.SendEmailToCustomerWhenOrderOnItsWayAsync(user, orderToReturn);
                        break;
                    case 3:
                        _mailService.SendEmailToCustomerWhenOrderHasArrivedAsync(user, orderToReturn);
                        break;
                    case 4:
                        var agent = _userRepository.GetUserById(order.AgentId);
                        if (agent != null)
                        {
                            agent.AgentHasOrder = false;
                            order.IsDelivered = true;
                            _userRepository.Update(agent);
                        }
                        break;

                    default:
                        break;
                }
                if (_ordersRepository.Update(order))
                {
                    var mappedOrder = _mapper.Map<OrderPublishedDto>(order);
                    mappedOrder.Event = "OrderStatus_Changed";
                    mappedOrder.OrderStatus = order.OrderStatus.ToString();
                    _messageBusClient.PublishOrderStatus(mappedOrder);
                    return new ApiResponse<OrderForDisplayDto>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = orderToReturn,
                        Message = "The order status is updated"
                    };
                }
                return new ApiResponse<OrderForDisplayDto>() {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "Something went wrong. Please try again"}
                
                };

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<OrderForDisplayDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public void SendOrderStatusToCustomer(int orderId,float distance) {
            var order = _ordersRepository.Get(orderId);
            
            if(order != null)
            {
                var customer = _userRepository.GetUserById(order.UserId);
                if(distance <=500 && distance > 20)
                {
                    order.Total = order.Total / 100;
                    _mailService.SendOrderStatusEmailToCustomerAsync(customer, order, distance);
                }
                if(distance <= 20) {
                    order.OrderStatus = OrderStatuses.OrderHasArrived;
                    if (_ordersRepository.Update(order))
                    {
                        order.Total = order.Total / 100;
                        _mailService.SendOrderStatusEmailToCustomerAsync(customer,order,distance);
                    }
                }
            }

        }
        public IEnumerable<OrderDto> GetOrdersByCustomerId(string userId)
        {
            var orders = _ordersRepository.Find(o => o.UserId == userId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public bool CreateOrder(OrderCreateDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
          return _ordersRepository.Add(order);
     
        }

        public bool UpdateOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            return _ordersRepository.Update(order);
        }

        public bool DeleteOrder(int id)
        {
            var order = _ordersRepository.Get(id);
            return _ordersRepository.Remove(order);
        }


        public ApiResponse<IList<OrderForDisplayDto>> GetTopSellingOrders()
        {
            try
            {
                var topSellingOrders = _ordersRepository.GetTopSellingOrders();

                return new ApiResponse<IList<OrderForDisplayDto>>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<IList<OrderForDisplayDto>>(topSellingOrders)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<IList<OrderForDisplayDto>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }


    }
}
