
using eCommerce.OrdersMicroservice.DataAccessLayer.Repositories;
using eCommerce.OrdersMicroservice.DataAccessLayer.RepositoryContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace eCommerce.OrdersMicroservice.DataAccessLayer
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDataAccessLayer(
            this IServiceCollection services,
            IConfiguration config)
        {
            var connectionStringTemplate =
                config.GetConnectionString("MongoDB");

            if (string.IsNullOrWhiteSpace(connectionStringTemplate))
                throw new InvalidOperationException("MongoDB connection string is missing.");

            var mongoHost = config["MONGODB_HOST"];
            var mongoPort = config["MONGODB_PORT"];

            if (string.IsNullOrWhiteSpace(mongoHost))
                throw new InvalidOperationException("MONGO_HOST is missing.");

            if (string.IsNullOrWhiteSpace(mongoPort))
                throw new InvalidOperationException("MONGO_PORT is missing.");

            var connectionString = connectionStringTemplate
                .Replace("$MONGO_HOST", mongoHost)
                .Replace("$MONGO_PORT", mongoPort);

            services.AddSingleton<IMongoClient>(
                _ => new MongoClient(connectionString));

            services.AddSingleton<IMongoDatabase>(provider =>
            {
                var mongoClient = provider.GetRequiredService<IMongoClient>();
                return mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGODB_DATABASE"));
            });

            services.AddScoped<IOrdersRepository, OrdersRepository>();

            return services;
        }
    }
}
