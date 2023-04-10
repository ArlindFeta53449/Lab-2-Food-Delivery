using Business.Services._01_Mailing;
using Business.Services.MenuItems;
using Business.Services.Menus;
using Business.Services.Offers;
using Business.Services.Restaurants;
using Business.Services.Roles;
using Business.Services.Token;
using Business.Services.Users;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories;
using Repositories.Repositories.Roles;
using Repositories.Repositories.Users;
using Repository.Repositories.Menus;
using Repository.Repositories.MenusItems;
using Repository.Repositories.Offers;
using Repository.Repositories.Restaurants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
builder.Services.AddScoped<IOfferRepository, OfferRepository>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService,MailService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
