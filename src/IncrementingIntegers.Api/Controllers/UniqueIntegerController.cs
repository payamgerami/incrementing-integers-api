using AutoMapper;
using IncrementingIntegers.Api.Authentication;
using IncrementingIntegers.Contract.V1.Requests;
using IncrementingIntegers.Contract.V1.Responses;
using IncrementingIntegers.Logic;
using IncrementingIntegers.Logic.Commands;
using IncrementingIntegers.Logic.Queries;
using IncrementingIntegers.Logic.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IncrementingIntegers.Api.Controllers
{
    [Route("api/v1/uniqueinteger")]
    [Authorize]
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
            NextIdCommand command = new NextIdCommand(User.GetFaccebookUserIdClaim());

            NextIdResult result = await _handlerFacade.Invoke<NextIdCommand, NextIdResult>(command);

            return _mapper.Map<NextIdResponse>(result);
        }

        [HttpGet]
        [Route("current")]
        public async Task<CurrentIdResponse> Current()
        {
            CurrentIdQuery query = new CurrentIdQuery(User.GetFaccebookUserIdClaim());

            CurrentIdResult result = await _handlerFacade.Invoke<CurrentIdQuery, CurrentIdResult>(query);

            return _mapper.Map<CurrentIdResponse>(result);
        }

        [HttpPut]
        [Route("reset")]
        public async Task Reset([FromBody] ResetIdRequest request)
        {
            ResetIdCommand command = new ResetIdCommand(User.GetFaccebookUserIdClaim(), request.Id);

            await _handlerFacade.Invoke<ResetIdCommand>(command);
        }
    }
}