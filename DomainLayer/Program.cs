using DataAccess;
using DataAccess.Interfaces;
using DomainLayer.middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnection, DapperContext>();
builder.Services.AddSingleton<ISystemTrackingLogger, DataBaseSystemTrackingLogger>();
builder.Services.AddScoped<IUserRepository<Users>, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<UsersLogin>();
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
