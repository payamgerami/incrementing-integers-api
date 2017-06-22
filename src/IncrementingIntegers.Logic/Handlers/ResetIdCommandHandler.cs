using IncrementingIntegers.DataAccess.Entities;
using IncrementingIntegers.DataAccess.Repositories;
using IncrementingIntegers.Logic.Commands;
using System;
using System.Threading.Tasks;

namespace IncrementingIntegers.Logic.Handlers
{
    public class ResetIdCommandHandler : IHandler<ResetIdCommand>
    {
        private IUniquIntegerRepository _uniquIntegerRepository;

        public ResetIdCommandHandler(IUniquIntegerRepository uniquIntegerRepository)
        {
            _uniquIntegerRepository = uniquIntegerRepository;
        }

        public async Task DoWork(ResetIdCommand command)
        {
            UniqueIntegerUser uniqueIntegerUser = await _uniquIntegerRepository.GetOrCreate(command.Email);
            uniqueIntegerUser.Id = command.Id;
            await _uniquIntegerRepository.Update(uniqueIntegerUser);
        }
    }
}
