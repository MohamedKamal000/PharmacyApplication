using ApplicationLayer.Users_Handling;
using DomainLayer;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.RepositoryIntefraces;
using InfrastructureLayer;
using InfrastructureLayer.Logging;
using InfrastructureLayer.Repositories;
using PresentationLayer.middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.Configure<DataBaseOptions>(builder.Configuration.GetSection("DataBaseOptions"));

builder.Logging.ClearProviders();
builder.Logging.AddProvider(new DataBaseLoggerProvider(
    new DataBaseOptions()
    {
       SqlConnection = builder.Configuration["ConnectionStrings:SqlConnection"]!,
       AdminBehaviour = builder.Configuration["DataBaseLogging:AdminBehaviour"]!,
       SystemBehaviour = builder.Configuration["DataBaseLogging:SystemBehaviour"]!
    }
    ));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>();


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

app.MapControllers();

app.Run();
