using eCommerce.OrdersMicroservice.BusinessLogicLayer.HttpClients;
using eCommerce.OrdersMicroservice.API.Middleware;
using eCommerce.OrdersMicroservice.BusinessLogicLayer;
using eCommerce.OrdersMicroservice.DataAccessLayer;

namespace OrderMicroservice.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add DAL and BLL services
            builder.Services.AddDataAccessLayer(builder.Configuration);
            builder.Services.AddBusinessLogicLayer(builder.Configuration);

            builder.Services.AddControllers();

            //Swagger
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

            builder.Services.AddHttpClient<UsersMicroserviceClient>(client =>
            {
                client.BaseAddress = new Uri($"http://{builder.Configuration["UsersMicroserviceName"]}:{builder.Configuration["UsersMicroservicePort"]}");
            });
            builder.Services.AddHttpClient<ProductsMicroserviceClient>(client =>
            {
                client.BaseAddress = new Uri($"http://{builder.Configuration["ProductsMicroserviceName"]}:{builder.Configuration["ProductsMicroservicePort"]}");
            });


            var app = builder.Build();

            app.UseExceptionHandlingMiddleware();
            app.UseRouting();

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors("AngularApp");

            //Auth
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            //Endpoints
            app.MapControllers();
            app.Run();
        }
    }
}
