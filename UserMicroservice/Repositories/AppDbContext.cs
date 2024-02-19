using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.EntityConfigurations;
using Repository.EntityConfiguration;

namespace Repositories
{
    public class AppDbContext : IdentityDbContext<User>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfigurations());
            modelBuilder.ApplyConfiguration(new OfferConfiguration());
            modelBuilder.ApplyConfiguration(new MenuItemConfiguration());
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new MenuItemOfferConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CartOfferConfiguration());
            modelBuilder.ApplyConfiguration(new CartMenuItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderMenuItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderOfferConfiguration());
            modelBuilder.ApplyConfiguration(new ParentConfiguration());
            modelBuilder.ApplyConfiguration(new ChildConfiguration());

            modelBuilder.ApplyConfiguration(new DirectorConfiguration());
            modelBuilder.ApplyConfiguration(new MovieConfiguration());

            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new ContractConfiguration());



        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<MenuItemOffer> MenuItemOffers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartMenuItem> CartMenuItems { get; set; }
        public DbSet<CartOffer> CartOffers{ get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<OrderMenuItem> OrderMenuItems{ get; set; }
        public DbSet<OrderOffer> OrderOffers { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Child> Children { get; set; }

        //public DbSet<Director> Directors { get; set; }
        //public DbSet<Movie> Movies { get; set; }

        //public DbSet<Employee> Employees { get; set; }
        //public DbSet<Contract> Contracts { get; set; }

       // public DbSet<InterviewNotes> InterviewsNotes { get; set; }
        //public DbSet<Interview> Interviews { get; set; }


    }
}
