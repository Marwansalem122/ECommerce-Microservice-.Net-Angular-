

using eCommerce.DataAccessLayer.Repositories;
using eCommerce.DataAccessLayer.Context;
using eCommerce.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.ProductService.DataAccessLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services,IConfiguration config) {

            services.AddDbContext<ApplicationDbContext>(options => {
                options
                .UseMySQL(config.GetConnectionString("DefaultConnection")!);
            }); //DefaultConnection

            services.AddScoped<IProductsRepository, ProductsRepository>();

            return services;
        }
    }
}
