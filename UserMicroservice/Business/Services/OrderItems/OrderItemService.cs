using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Repository.Repositories.OrderItems;

namespace Business.Services.OrderItems
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        //constructor
        public OrderItemService(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }


        public OrderItemDto CreateOrderItem(OrderItemCreateDto orderitem)
        {
            var mappedOrderItem = _mapper.Map<OrderItem>(orderitem);
            _orderItemRepository.Add(mappedOrderItem);
            return _mapper.Map<OrderItemDto>(mappedOrderItem);
        }

        public void DeleteOrderItem(int id)
        {
            var orderitem = _orderItemRepository.Get(id);
            //if(orderitem == null)
         //   {
          //      throw new NotFoundException(nameof(orderitem), id);
          //  }
            _orderItemRepository.Remove(orderitem);
        }

        public OrderItemDto EditOrderItem(OrderItemDto orderitem)
        {
            var mappedOrderItem = _mapper.Map<OrderItem>(orderitem);
            _orderItemRepository.Update(mappedOrderItem);
            return _mapper.Map<OrderItemDto>(mappedOrderItem);
        }

        public IList<OrderItemDto> GetAll()
        {
            var orderItems = _orderItemRepository.GetAll();
            return _mapper.Map<IList<OrderItemDto>>(orderItems);
        }

        public OrderItemDto GetOrderItem(int id)
        {
            var orderitem = _orderItemRepository.Get(id);
           // if(orderitem == null)
          //  {
          //      throw new NotFoundException(nameof(orderitem), id);
           // }
            return _mapper.Map<OrderItemDto>(orderitem);
        }
    }
}
