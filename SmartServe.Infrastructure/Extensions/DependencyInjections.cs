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
            //services

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITechnicianService, TechnicianService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<CustomerHelper>();
            services.AddScoped<IServiceJobService, ServiceJobService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddHttpClient<IAIService, AIService>(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(60);
            });

            //repos
            services.AddScoped<IServiceJobRepository, ServiceJobRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITechnicianRepository, TechnicianRepository>();
            services.AddScoped<IVehichleRepository, VehicleRepository>();
            services.AddScoped<ICustomerRespository, CustomerRepository>();
            services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IAIRepository, AIRepository>();


            return services;
        }
    }
}
