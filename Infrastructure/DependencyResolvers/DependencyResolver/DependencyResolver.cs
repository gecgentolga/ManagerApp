using Application.DataAccess;
using Application.IServices;
using Application.Services;
using Infrastructure.DataAccess.Concrete.EntityFramework;
using Infrastructure.DataAccess.ImportDataViaApi;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyResolvers.EFDependencyResolver;

public static class DependencyResolver
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // DbContext
        // Not: DbContext Program.cs'de ayrıca connection string ile kaydedilecek
        
        // Data Access Layer (Repository Pattern)
        services.AddScoped<IContractDal, EfContractDal>();
        services.AddScoped<ILeagueDal, EfLeagueDal>();
        services.AddScoped<IOfferDal, EfOfferDal>();
        services.AddScoped<IOwnedPlayerDal, EfOwnedPlayerDal>();
        services.AddScoped<IPlayerDal, EfPlayerDal>();
        services.AddScoped<ITeamDal, EfTeamDal>();
        services.AddScoped<IPlayerImport,PlayerImportHandler>();
        // Business Logic Services
        services.AddScoped<IOfferService, OfferService>();
        
        return services;
    }
}