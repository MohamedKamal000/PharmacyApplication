using ApplicationLayer;
using ApplicationLayer.Categories_Handling;
using ApplicationLayer.Delivery_Handling;
using ApplicationLayer.Orders_Handling;
using ApplicationLayer.Products_Handling;
using ApplicationLayer.Users_Handling;
using DomainLayer;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.RepositoryIntefraces;
using InfrastructureLayer;
using InfrastructureLayer.Repositories;

namespace PresentationLayer.Extensions;

public static class ServicesCollectionsExtension
{

    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepository<User>, UserRepository>();
        serviceCollection.AddScoped<IProductRepository, MedicalProductsRepository>();
        serviceCollection.AddScoped<IDeliveryRepository,DeliveryRepository>();
        serviceCollection.AddScoped<IMedicalCategory,MedicalCategoryRepository>();
        serviceCollection.AddScoped<IOrdersRepository,OrdersRepository>();
        serviceCollection.AddScoped<ISubMedicalCategory,SubMedicalCategoryRepository>();
        return serviceCollection;
    }

    public static IServiceCollection AddHandlers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<UserLogin>();
        serviceCollection.AddScoped<UserHandler>();
        serviceCollection.AddScoped<ProductHandler>();
        serviceCollection.AddScoped<MainCategoryHandler>();
        serviceCollection.AddScoped<SubCategoryHandler>();
        serviceCollection.AddScoped<OrderHandler>();
        serviceCollection.AddScoped<DeliveryManHandler>();
        return serviceCollection;
    }

    public static IServiceCollection AddUtilties(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPasswordHasher, PasswordHasher>();        
        serviceCollection.AddSingleton<ITokenGenerator, JwtTokenGenerator>();
        return serviceCollection;
    }
}