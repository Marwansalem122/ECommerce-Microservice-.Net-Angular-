using eCommerce.API.Middlewares;
using eCommerce.DataAccessLayer.Context;
using eCommerce.ProductService.BusinessLogicLayer;
using eCommerce.ProductService.DataAccessLayer;
using ProductMicroService.API.APIEndPoints;
using System.Text.Json.Serialization;

namespace ProductMicroService.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDataAccessLayer(builder.Configuration);

            builder.Services.AddBusinessLogicLayer();
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AngularApp", builder =>
                {
                    builder
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            var app = builder.Build();

          

            app.UseExceptionHandlingMiddleware();
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors("AngularApp");

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.MapProductAPIEndPoints();
            app.Run();
        }
    }
}
