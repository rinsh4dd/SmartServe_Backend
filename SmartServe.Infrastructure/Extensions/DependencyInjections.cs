using Microsoft.Extensions.DependencyInjection;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.Contracts.Services;
using SmartServe.Application.Services;
using SmartServe.Infrastructure.Repositories;
using SmartServe.Infrastructure.Services;

namespace SmartServe.Infrastructure.Extensions
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IDepartmentService, DepartmentService>();

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            return services;
        }
    }
}
