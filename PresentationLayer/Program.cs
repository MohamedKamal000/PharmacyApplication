using System.Text;
using ApplicationLayer;
using ApplicationLayer.Categories_Handling;
using ApplicationLayer.Products_Handling;
using ApplicationLayer.Users_Handling;
using DomainLayer;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.RepositoryIntefraces;
using InfrastructureLayer;
using InfrastructureLayer.Logging;
using InfrastructureLayer.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PresentationLayer.Authentications;
using PresentationLayer.Extensions;
using PresentationLayer.middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.Configure<DataBaseOptions>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddProvider(new DataBaseLoggerProvider(
    new DataBaseOptions()
    {
        SqlConnection = builder.Configuration["ConnectionStrings:SqlConnection"]!,
        AdminBehaviour = builder.Configuration["DataBaseLogging:AdminBehaviour"]!,
        SystemBehaviour = builder.Configuration["DataBaseLogging:SystemBehaviour"]!
    }
));

// Admin123
// 01544444444
// helloworld1234

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>();

var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
builder.Services.AddSingleton(jwtOptions);

builder.Services.AddAuthentication()
    // .AddScheme<AuthenticationSchemeOptions, BearerAuthenticationHandler>("Bearer", null);
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
        };
    });


builder.Services.AddUtilties();
builder.Services.AddRepositories();
builder.Services.AddHandlers();

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
