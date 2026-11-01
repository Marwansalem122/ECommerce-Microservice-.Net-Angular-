using eCommerce.API.Middlewares;
using eCommerce.API.Services;
using eCommerce.Core;
using eCommerce.Core.Mappers;
using eCommerce.Core.ServicesContract;
using eCommerce.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
namespace eCommerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add infrastructure services
            builder.Services.AddInfrastructure();

            // Add core services
            builder.Services.AddCore();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.AddAutoMapper(
                         cfg => { },
                         typeof(ApplicationUserMappingProfile).Assembly
                     );

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
            builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            var jwt = builder.Configuration.GetSection("Jwt");
            var signingKey = jwt["Key"]
                ?? throw new InvalidOperationException("JWT signing key is missing.");

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwt["Issuer"],

                        ValidateAudience = true,
                        ValidAudience = jwt["Audience"],

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(signingKey)),

                        ValidateLifetime = true
                    };
                });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.UseExceptionHandlingMiddleware();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors("AngularApp");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
