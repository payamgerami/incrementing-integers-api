using System;
using System.Threading.Tasks;

namespace IncrementingIntegers.Common.Helpers
{
    internal static class RetryHelper
    {
        public static async Task<T> RetryOnFault<T>(Func<Task<T>> function, int maxTries)
        {
            for (int i = 0; i < maxTries; i++)
            {
                try
                {
                    return await function().ConfigureAwait(false);
                }
                catch
                {
                    if (i == maxTries - 1)
                        throw;
                }
            }

            return default(T);
        }

        public static async Task<T> RetryOnFault<T>(Func<Task<T>> function, int maxTries, Func<Task> retryWhen)
        {
            for (int i = 0; i < maxTries; i++)
            {
                try
                {
                    return await function().ConfigureAwait(false);
                }
                catch
                {
                    if (i == maxTries - 1)
                        throw;
                }

                await retryWhen().ConfigureAwait(false);
            }

            return default(T);
        }

        //Usage
        //string row = await RetryOnFault(() => GetRow(partitionKey,rowKey), 3);
        //string row = await RetryOnFault(() => GetRow(partitionKey,rowKey), 3, () => Task.Delay(1000));
    }
}
