using Data.DTOs.Cart;
using Data.DTOs.MenuItem;
using Data.DTOs.Offer;
using Data.DTOs.OrderOffer;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.GenericRepository;
using Repositories.Repositories.MenuItemOffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Carts
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {

        }
        public void CreateCart(string userId)
        {
            var cart = new Cart()
            {
                UserId = userId,
            };
            Context.Set<Cart>().Add(cart); 
            Context.SaveChanges();
        }
        public void DeleteCart(string userId)
        {
            var cart = Context.Set<Cart>().Where(x=>x.UserId == userId).FirstOrDefault();
            if(cart != null)
            {
                Context.Set<Cart>().Remove(cart);
                Context.SaveChanges();
            }
        }
        public CartForOrderDto GetCartDetailsForOrder(string userId)
        {
          var menuItems = Context.Set<CartMenuItem>()
          .Where(x => x.Cart.UserId == userId)
          .Include(x => x.MenuItem)
          .Select(x => new  MenuItemForOrderCreateDto()
          {
              MenuItemId = x.MenuItem.Id,
              Quantity = x.Quantity
          })
          .ToList();

         var offers = Context.Set<CartOffer>()
             .Where(x => x.Cart.UserId == userId)
             .Include(x => x.Offer)
             .Select(x => new OrderOfferCreateDto()
              {
                OfferId = x.Offer.Id,
                Quantity = x.Quantity
              })
             .ToList();
            return new CartForOrderDto()
            {
                Id = Context.Set<Cart>().Where(x => x.UserId == userId).Select(x => x.Id).FirstOrDefault(),
                UserId = userId,
                OrderMenuItems = menuItems,
                OrderOffers = offers
            };
        }
        public CartDto GetCartByUserId(string userId)
        {
            var menuItems = Context.Set<CartMenuItem>()
       .Where(x => x.Cart.UserId == userId)
       .Include(x => x.MenuItem)
       .Select(x => new MenuItemForCartDto()
       {
           Id = x.MenuItem.Id,
           Name = x.MenuItem.Name,
           Description = x.MenuItem.Description,
           Price = x.MenuItem.Price,
           ImagePath = x.MenuItem.ImagePath,
           Quantity = x.Quantity
       })
       .ToList();

            var offers = Context.Set<CartOffer>()
                .Where(x => x.Cart.UserId == userId)
                .Include(x => x.Offer)
                .Select(x => new OfferForCartDto()
                {
                    Id = x.Offer.Id,
                    Name = x.Offer.Name,
                    Description = x.Offer.Description,
                    Price = x.Offer.Price,
                    ImagePath = x.Offer.ImagePath,
                    Quantity = x.Quantity
                })
                .ToList();

            return new CartDto()
            {
                Id = Context.Set<Cart>().Where(x=>x.UserId == userId).Select(x => x.Id).FirstOrDefault(),
                UserId = userId,
                MenuItems = menuItems,
                Offers = offers
            };

        }
        
        
}
}
