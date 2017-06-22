using System;
using System.Threading.Tasks;

namespace IncrementingIntegers.Common.Helpers
{
    public static class RetryHelper
    {
        public static async Task<T> RetryOnFault<T>(Func<Task<T>> function, Func<Exception, bool> retryIf, Func<Task> retryWhen, int maxTries)
        {
            for (int i = 0; i < maxTries; i++)
            {
                try
                {
                    return await function().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    if (!retryIf(ex))
                        throw;

                    if (i == maxTries - 1)
                        throw;
                }

                await retryWhen().ConfigureAwait(false);
            }

            return default(T);
        }
    }
}