using Application.Repository;
using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Infrastructure
{
    public static class RegisterDataAccess
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IGenericRepository<Venue>, GenericRepository<Venue>>();
            services.AddScoped<IGenericRepository<Review>, GenericRepository<Review>>();
            services.AddScoped<IGenericRepository<Tag>, GenericRepository<Tag>>();
            services.AddScoped<IGenericRepository<VenueTag>, GenericRepository<VenueTag>>();

            return services;
        }
    }
}
