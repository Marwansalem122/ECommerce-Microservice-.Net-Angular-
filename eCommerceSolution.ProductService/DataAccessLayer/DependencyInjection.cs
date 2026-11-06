

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

          var connectionStringTemplate =
        config.GetConnectionString("DefaultConnection");

    if (string.IsNullOrWhiteSpace(connectionStringTemplate))
        throw new InvalidOperationException(
            "DefaultConnection is missing.");

    var mysqlHost = config["MYSQL_HOST"];
    var mysqlPassword = config["MYSQL_PASSWORD"];

    if (string.IsNullOrWhiteSpace(mysqlHost))
        throw new InvalidOperationException("MYSQL_HOST is missing.");

    if (string.IsNullOrWhiteSpace(mysqlPassword))
        throw new InvalidOperationException("MYSQL_PASSWORD is missing.");

    var connectionString = connectionStringTemplate
        .Replace("$MYSQL_HOST", mysqlHost)
        .Replace("$MYSQL_PASSWORD", mysqlPassword);
    Console.WriteLine(
    $"MySQL Connection String: {connectionString}");
    services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseMySQL(connectionString);
    });

    services.AddScoped<IProductsRepository, ProductsRepository>();

            return services;
        }
    }
}
