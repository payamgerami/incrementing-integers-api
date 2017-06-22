using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IncrementingIntegers.Api.Controllers
{
    [Route("api/version")]
    public class VersionController
    {
        // GET api/version
        [HttpGet]
        public Task<string> Get()
        {
            return Task.FromResult("V1");
        }
    }
}