using LibraryStore.App.Extensions;
using LibraryStore.Business.Interfaces;
using LibraryStore.Business.Notifications;
using LibraryStore.Business.Services;
using LibraryStore.Data.Context;
using LibraryStore.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace LibraryStore.App.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<LibraryStoreDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, CurrencyValidationAttributeAdapterProvider>();
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
