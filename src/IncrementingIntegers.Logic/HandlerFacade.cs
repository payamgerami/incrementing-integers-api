using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace IncrementingIntegers.Logic
{
    public class HandlerFacade : IHandlerFacade
    {
        private IServiceScopeFactory ServiceScopeFactory { get; }

        public HandlerFacade(IServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
        }

        public async Task<TOutput> Invoke<TInput, TOutput>(TInput input)
        {
            using (IServiceScope scope = ServiceScopeFactory.CreateScope())
            {
                var handler = (IHandler<TInput, TOutput>)scope.ServiceProvider.GetService(typeof(IHandler<TInput, TOutput>));

                return await handler.DoWork(input);
            }
        }

        public async Task Invoke<TInput>(TInput input)
        {
            using (IServiceScope scope = ServiceScopeFactory.CreateScope())
            {
                var handler = (IHandler<TInput>)scope.ServiceProvider.GetService(typeof(IHandler<TInput>));

                await handler.DoWork(input);
            }
        }
    }
}
