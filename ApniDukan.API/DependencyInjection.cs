using ApniDukan.Application;
using ApniDukan.Domain;
using ApniDukan.Infrastrcture;

namespace ApniDukan.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiDI(this IServiceCollection services)
        {
            services
                .AddInfrastructureDI()
                .AddApplicationDI()
                .AddDomainDI();
            return services;
        }
    }
}
