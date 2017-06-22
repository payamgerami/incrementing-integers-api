using AutoMapper;
using IncrementingIntegers.Contract.V1.Requests;
using IncrementingIntegers.Contract.V1.Responses;
using IncrementingIntegers.Logic;
using IncrementingIntegers.Logic.Commands;
using IncrementingIntegers.Logic.Queries;
using IncrementingIntegers.Logic.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IncrementingIntegers.Api.Controllers
{
    [Route("api/v1/uniqueinteger")]
    public class UniqueIntegerController : Controller
    {
        private IHandlerFacade _handlerFacade;
        private IMapper _mapper;

        public UniqueIntegerController(IMapper mapper, IHandlerFacade handlerFacade)
        {
            _mapper = mapper;
            _handlerFacade = handlerFacade;
        }

        [HttpGet]
        [Route("next")]
        public async Task<NextIdResponse> Next()
        {
            string email = "test@test.com";
            NextIdCommand command = new NextIdCommand(email);

            NextIdResult result = await _handlerFacade.Invoke<NextIdCommand, NextIdResult>(command);

            return _mapper.Map<NextIdResponse>(result);
        }

        [HttpGet]
        [Route("current")]
        public async Task<CurrentIdResponse> Current()
        {
            string email = "test@test.com";
            CurrentIdQuery query = new CurrentIdQuery(email);

            CurrentIdResult result = await _handlerFacade.Invoke<CurrentIdQuery, CurrentIdResult>(query);

            return _mapper.Map<CurrentIdResponse>(result);
        }

        [HttpPut]
        [Route("reset")]
        public async Task Reset([FromBody] ResetIdRequest request)
        {
            string email = "test@test.com";
            ResetIdCommand command = new ResetIdCommand(email, request.Id);

            await _handlerFacade.Invoke<ResetIdCommand>(command);
        }
    }
}
