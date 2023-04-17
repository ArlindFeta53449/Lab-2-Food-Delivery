using Data.Entities;
using Repositories;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Orders
{
    public class OrdersRepository : Repository<Order>, IOrdersRepository
    {
        public OrdersRepository(AppDbContext context) : base(context)
        {
        }
    }
}
