using AutoMapper;
using IncrementingIntegers.Common.Configurations;
using IncrementingIntegers.Common.Helpers;
using IncrementingIntegers.DataAccess.Entities;
using IncrementingIntegers.DataAccess.Repositories;
using IncrementingIntegers.Logic.Commands;
using IncrementingIntegers.Logic.Results;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Net;
using System.Threading.Tasks;

namespace IncrementingIntegers.Logic.Handlers
{
    public class NextIdCommandHandler : IHandler<NextIdCommand, NextIdResult>
    {
        private IMapper _mapper;
        private RetryOptions _retryOpptions;
        private IUniquIntegerRepository _uniquIntegerRepository;

        public NextIdCommandHandler(IMapper mapper, IOptions<RetryOptions> retryOpptions, IUniquIntegerRepository uniquIntegerRepository)
        {
            _mapper = mapper;
            _retryOpptions = retryOpptions.Value;
            _uniquIntegerRepository = uniquIntegerRepository;
        }

        public async Task<NextIdResult> DoWork(NextIdCommand command)
        {
            UniqueIntegerUserTableEntity tableEntity = await _uniquIntegerRepository.GetOrCreate(command.UserId);
            tableEntity.Id++;

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
                    tableEntity = await _uniquIntegerRepository.GetOrCreate(command.UserId);
                    tableEntity.Id++;
                },
                _retryOpptions.MaxRetry);

            return _mapper.Map<NextIdResult>(tableEntity);
        }
    }
}