using System.Threading.Tasks;

namespace IncrementingIntegers.Logic
{
    public interface IHandlerFacade
    {
        Task<TOutput> Invoke<TInput, TOutput>(TInput input);

        Task Invoke<TInput>(TInput input);
    }
}
