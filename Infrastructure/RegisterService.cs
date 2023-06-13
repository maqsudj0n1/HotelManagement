using Application.Abstractions;
using Application.Interfaces;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Interceptor;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class RegisterService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(x => x.UseNpgsql(configuration.GetConnectionString("DbConnection")));
            services.AddScoped<IEmployeeRepo, EmployeeService>();
            services.AddScoped<ICustomerRepo, CustomerService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRoomRepo, RoomService>();
            services.AddScoped<IRoomClassRepo, RoomsClasservice>();
            services.AddScoped<ITransactionRepo, TransactionService>();
            services.AddScoped<IReservationRepo, ReservationService>();
            services.AddScoped<IPaymentRepo, PaymentService>();
            return services;
        }
    }
}
