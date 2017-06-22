using IncrementingIntegers.Common.Configurations;
using IncrementingIntegers.Common.Helpers;
using IncrementingIntegers.DataAccess.Entities;
using IncrementingIntegers.DataAccess.Repositories;
using IncrementingIntegers.Logic.Commands;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Net;
using System.Threading.Tasks;

namespace IncrementingIntegers.Logic.Handlers
{
    public class ResetIdCommandHandler : IHandler<ResetIdCommand>
    {
        private RetryOptions _retryOpptions;
        private IUniquIntegerRepository _uniquIntegerRepository;

        public ResetIdCommandHandler(IOptions<RetryOptions> retryOpptions, IUniquIntegerRepository uniquIntegerRepository)
        {
            _retryOpptions = retryOpptions.Value;
            _uniquIntegerRepository = uniquIntegerRepository;
        }

        public async Task DoWork(ResetIdCommand command)
        {
            UniqueIntegerUserTableEntity tableEntity = await _uniquIntegerRepository.GetOrCreate(command.Email);
            tableEntity.Id = command.Id;

            await RetryHelper.RetryOnFault<TableResult>(
                async () =>
                {
                    return await _uniquIntegerRepository.Update(tableEntity);
                },
                ex =>
                {
                    if (ex is StorageException storageException)
                    {
                        if (storageException.RequestInformation.HttpStatusCode == (int)HttpStatusCode.PreconditionFailed)
                        {
                            return true;
                        }
                    }
                    return false;
                },
                async () =>
                {
                    tableEntity = await _uniquIntegerRepository.GetOrCreate(command.Email);
                    tableEntity.Id = command.Id;
                },
                _retryOpptions.MaxRetry);
        }
    }
}
