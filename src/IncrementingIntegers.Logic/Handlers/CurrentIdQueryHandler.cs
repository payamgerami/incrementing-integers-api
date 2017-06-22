using AutoMapper;
using IncrementingIntegers.DataAccess.Entities;
using IncrementingIntegers.DataAccess.Repositories;
using IncrementingIntegers.Logic.Queries;
using IncrementingIntegers.Logic.Results;
using System.Threading.Tasks;

namespace IncrementingIntegers.Logic.Handlers
{
    public class CurrentIdQueryHandler : IHandler<CurrentIdQuery, CurrentIdResult>
    {
        private IMapper _mapper;
        private IUniquIntegerRepository _uniquIntegerRepository;

        public CurrentIdQueryHandler(IMapper mapper, IUniquIntegerRepository uniquIntegerRepository)
        {
            _mapper = mapper;
            _uniquIntegerRepository = uniquIntegerRepository;
        }

        public async Task<CurrentIdResult> DoWork(CurrentIdQuery query)
        {
            UniqueIntegerUserTableEntity tableEntity = await _uniquIntegerRepository.GetOrCreate(query.Email);

            return _mapper.Map<CurrentIdResult>(tableEntity);
        }
    }
}