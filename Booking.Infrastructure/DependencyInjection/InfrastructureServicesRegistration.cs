using Booking.Application.Abstractions.Contracts;
using Booking.Infrastructure.Contracts.AuthService;
using Booking.Infrastructure.Features;
using Booking.Infrastructure.Persistence;
using Booking.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Booking.Infrastructure.DependencyInjection
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            string connectionString,
            IConfiguration configuration)
        {
            services.AddDbContext<BookingDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:SecretKey"]!)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });
            
            return services;
        }
    }
}
