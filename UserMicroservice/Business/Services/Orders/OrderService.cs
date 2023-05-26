using AutoMapper;
using Data.DTOs;
using Data.DTOs.Order;
using Data.Entities;
using Repository.Repositories.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Orders
{

    public class OrderService : IOrderService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrdersRepository ordersRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }

        public OrderDto GetOrderById(int id)
        {
            var order = _ordersRepository.Get(id);
            return _mapper.Map<OrderDto>(order);
        }

        public IEnumerable<OrderDto> GetAllOrders()
        {
            var orders = _ordersRepository.GetAll();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
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
        public IList<OrderForDisplayDto> GetAllOrdersWithOrderItems()
        {
            return _ordersRepository.getAllOrdersWithOrderItems();
            
        }
    }
}
