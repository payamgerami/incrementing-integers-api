using AutoMapper;
using IncrementingIntegers.DataAccess.Entities;
using IncrementingIntegers.DataAccess.Repositories;
using IncrementingIntegers.Logic.Commands;
using IncrementingIntegers.Logic.Results;
using System;
using System.Threading.Tasks;

namespace IncrementingIntegers.Logic.Handlers
{
    public class NextIdCommandHandler : IHandler<NextIdCommand, NextIdResult>
    {
        private IMapper _mapper;
        private IUniquIntegerRepository _uniquIntegerRepository;

        public NextIdCommandHandler(IMapper mapper, IUniquIntegerRepository uniquIntegerRepository)
        {
            _mapper = mapper;
            _uniquIntegerRepository = uniquIntegerRepository;
        }

        public async Task<NextIdResult> DoWork(NextIdCommand command)
        {
            UniqueIntegerUser uniqueIntegerUser = await _uniquIntegerRepository.GetOrCreate(command.Email);
            return _mapper.Map<NextIdResult>(uniqueIntegerUser);
        }
    }
}
