using ApniDukan.Application.Validator.Auth;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ApniDukan.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<ShopOwnerRegisterRequestValidator>();
            return services;
        }
    }
}
