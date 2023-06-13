using Application.Mappings;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class RegisterService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapProfile));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            

            return services;
        }
    }
}
