using Data.Entities;
using Repositories;
using Repositories.Repositories.GenericRepository;

namespace Repository.Repositories.OrderItems
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
