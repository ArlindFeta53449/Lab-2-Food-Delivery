using Business.Services.NotificationService;
using Data.Entities;
using Data.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Repository.NotificationsRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = "mongodb://127.0.0.1:27017";
var databaseName = "NotificationDatabase";
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<NotificationDatabaseSettings>(
                    builder.Configuration.GetSection(nameof(NotificationDatabaseSettings)));
builder.Services.AddSingleton<INotificationDatabaseSettings>(sp=>
sp.GetRequiredService<IOptions<NotificationDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
        new MongoClient(builder.Configuration.GetValue<string>("NotificationDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService,NotificationService>();
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
