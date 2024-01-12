using Kafka.Producer.Domain.Interface.Service;
using Kafka.Producer.Domain.Service;

namespace Kafka.API.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection ExecuteDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IProducerService, ProducerService>();
            return services;
        }
    }
}
