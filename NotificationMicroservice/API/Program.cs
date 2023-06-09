using Business.Services.AsyncDataService;
using Business.Services.EventProcessing;
using Business.Services.NotificationHub;
using Business.Services.NotificationService;
using Business.Services.UserService;
using Data.Entities;
using Data.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Repository.NotificationsRepository;
using Repository.UserRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen();
builder.Services.Configure<NotificationDatabaseSettings>(
                    builder.Configuration.GetSection(nameof(NotificationDatabaseSettings)));
builder.Services.AddSingleton<INotificationDatabaseSettings>(sp=>
sp.GetRequiredService<IOptions<NotificationDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
        new MongoClient(builder.Configuration.GetValue<string>("NotificationDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService,NotificationService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INotificationHub, NotificationHub>();
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddHostedService<MessageBusSubscriber>();

builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins("http://localhost:3000","http://localhost:3001")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); 
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/notificationhub");
});

app.MapControllers();

app.Run();
