using Data.DTOs.Cart;
using Data.Entities;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Carts
{
    public interface ICartRepository : IRepository<Cart>
    {
        void CreateCart(string userId);
        void DeleteCart(string userId);
        CartDto GetCartByUserId(string userId);
        CartForOrderDto GetCartDetailsForOrder(string userId);
    }
}
