using fin_manager.Models.Interfaces;
using fin_manager.Models;
using fin_manager.Services.Interfaces;
using fin_manager.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoConfiguration>
    (builder.Configuration.GetSection(nameof(MongoConfiguration)));

builder.Services.AddSingleton<IMongoConfiguration>
    (x => x.GetRequiredService<IOptions<MongoConfiguration>>().Value);

builder.Services.AddSingleton<IMongoClient>
    (new MongoClient(builder.Configuration.GetValue<string>("MongoConfiguration:ConnectionString")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
});

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
