using ApplicationLayer.Users_Handling;
using DomainLayer;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.RepositoryIntefraces;
using InfrastructureLayer;
using InfrastructureLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using PresentationLayer.middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddSingleton<ISystemTrackingLogger, DataBaseSystemTrackingLogger>();
builder.Services.AddScoped<IUserRepository<Users>, UserRepository>();
builder.Services.AddScoped<IProductRepository, MedicalProductsRepository>();
builder.Services.AddScoped<IDeliveryRepository,DeliveryRepository>();
builder.Services.AddScoped<IMedicalCategory,MedicalCategoryRepository>();
builder.Services.AddScoped<IOrdersRepository,OrdersRepository>();
builder.Services.AddScoped<ISubMedicalCategory,SubMedicalCategoryRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<UserLogin>();
builder.Services.AddScoped<UserHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<RequestTimeMeasurementMiddleWare>();
app.UseMiddleware<GlobalErrorHandlerMiddleWare>();

app.MapControllers();

app.Run();
