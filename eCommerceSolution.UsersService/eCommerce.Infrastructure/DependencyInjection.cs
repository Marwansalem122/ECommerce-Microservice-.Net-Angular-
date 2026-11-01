

using eCommerce.Core.RespositoryContract;
using eCommerce.Infrastructure.DbContext;
using eCommerce.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {    //  infrastructure services often include database contexts, repositories,
             //  logging, caching,low level components etc.
            // Example: services.AddDbContext<MyDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
         
            services.AddTransient<IUserRepository, UserRespository>();
            services.AddTransient<DapperDbContext>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();


            return services;
        }
    }
}
