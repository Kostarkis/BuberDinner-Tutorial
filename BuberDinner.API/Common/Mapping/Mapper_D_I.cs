using Mapster;
using MapsterMapper;
using System.Reflection;

namespace BuberDinner.API.Common.Mapping
{
    public static class Mapper_D_I
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }
    }
}
