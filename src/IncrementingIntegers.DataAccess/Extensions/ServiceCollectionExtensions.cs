using IncrementingIntegers.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IncrementingIntegers.DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataAccessLayer(this IServiceCollection services)
        {
            services.AddTransient<IUniquIntegerRepository, UniquIntegerRepository>();
        }
    }
}
