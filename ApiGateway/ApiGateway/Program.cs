using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddCacheManager<Guid>(x =>
{
    x.WithDictionaryHandle();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

// Configure CORS
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});
app.UseOcelot().Wait();
app.UseAuthorization();

app.MapControllers();

app.Run();
