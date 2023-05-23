using Business.Services._01_Mailing;
using Business.Services.MenuItems;
using Business.Services.Menus;
using Business.Services.Offers;
using Business.Services.Restaurants;
using Business.Services.Roles;
using Business.Services.Token;
using Business.Services.Users;
using Business.Services.OrderItems;
using Business.Services.Orders;

using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Repositories.Menus;
using Repositories.Repositories.MenusItems;
using Repositories.Repositories.Roles;
using Repositories.Repositories.Users;
using Repository.Repositories.MenusItems;
using Repository.Repositories.Offers;
using Repository.Repositories.Restaurants;
using Repository.Repositories.OrderItems;
using Repository.Repositories.Orders;
using Business.Services.Authentification;
using Business.Services.FileHandling;
using Repositories.Repositories.MenuItemOffers;
using Repositories.Repositories.Carts;
using Business.Services.Carts;
using Repositories.Repositories.CartMenuItems;
using Repositories.Repositories.CartOffers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<AppDbContext>(options =>
//{
//  options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//});

builder.Logging.ClearProviders();
builder.Logging.AddFile("C:\\Users\\lkrasniqi\\Desktop\\Lab-2-Food-Delivery\\UserMicroservice\\UserMicroservice\\Logs\\file.txt");
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IRolesRepository, RolesRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IMenusRepository, MenusRepository>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IMenusItemRepository, MenusItemRepository>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOfferRepository, OfferRepository>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IAuthentificationService, AuthentificationService>();
builder.Services.AddScoped<IFileHandlingService, FileHandlingService>();
builder.Services.AddScoped<IMenuItemOffersRepository, MenuItemOffersRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartMenuItemRepository, CartMenuItemRepository>();
builder.Services.AddScoped<ICartOfferRepository, CartOfferRepository>();



//builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService,MailService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000").AllowCredentials();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
