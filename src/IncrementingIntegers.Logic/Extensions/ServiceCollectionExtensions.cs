using IncrementingIntegers.Logic.Commands;
using IncrementingIntegers.Logic.Handlers;
using IncrementingIntegers.Logic.Queries;
using IncrementingIntegers.Logic.Results;
using Microsoft.Extensions.DependencyInjection;

namespace IncrementingIntegers.Logic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddLogicLayer(this IServiceCollection services)
        {
            services.AddSingleton<IHandlerFacade, HandlerFacade>();
            services.AddTransient<IHandler<NextIdCommand, NextIdResult>, NextIdCommandHandler>();
            services.AddTransient<IHandler<CurrentIdQuery, CurrentIdResult>, CurrentIdQueryHandler>();
            services.AddTransient<IHandler<ResetIdCommand>, ResetIdCommandHandler>();
        }
    }
}