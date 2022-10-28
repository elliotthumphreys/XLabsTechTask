using Application.Venues;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Application.Reviews;
using Application.Tags;

namespace Application.Infrastructure
{
    public static class RegisterApplication
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IReadVenue, VenueReader>()
                    .AddScoped<IReadReview, ReviewsReader>()
                    .AddScoped<IReadTag, TagReader>()
                    .AddScoped<IDeleteReview, ReviewDeleter>()
                    .AddScoped<IDeleteVenue, VenueDeleter>()
                    .AddScoped<IDeleteTag, TagDeleter>()
                    .AddScoped<ICreateVenue, VenueCreator>()
                    .AddScoped<ICreateReview, ReviewCreator>()
                    .AddScoped<ICreateTag, TagCreator>()
                    .AddScoped<IReadVenueTag, VenueTagReader>()
                    .AddScoped<IUpdateVenueTag, VenueTagUpdater>()
            ;

            services.AddAutoMapper(Assembly.Load(nameof(Application)));

            return services;
        }
    }
}