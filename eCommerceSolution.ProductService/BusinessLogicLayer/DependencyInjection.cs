using eCommerce.BusinessLogicLayer.Services;
using eCommerce.BusinessLogicLayer.Mappers;
using eCommerce.BusinessLogicLayer.ServiceContract;
using eCommerce.BusinessLogicLayer.Validators;
using eCommerce.DataAccessLayer.Repositories;
using eCommerce.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;


namespace eCommerce.ProductService.BusinessLogicLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<ProductResponseRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductUpdateRequestValidator>();

            services.AddAutoMapper(cfg => { }, typeof(ProductAddRequestToProductProfile).Assembly);
            services.AddAutoMapper(cfg => { }, typeof(ProductToProductResponseProfile).Assembly);
            services.AddAutoMapper(cfg => { }, typeof(ProductUpdateRequestToProductProfile).Assembly);

            services.AddScoped<IProductService, ProductsService>();
            services.AddScoped<IProductsRepository, ProductsRepository>();

            return services;
        }
    }
}
