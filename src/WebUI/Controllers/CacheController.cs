using CleanArchitecture.Infrastructure.ApiConventions;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Controllers
{
    public class CacheController : ApiControllerBase
    {
        private readonly IRedisCacheClient _redisCacheClient;
        public CacheController(IRedisCacheClient redisCacheClient)
        {
            _redisCacheClient = redisCacheClient;
        }

        [HttpGet]
        [Route("get-set-cache")]
        [ApiConventionMethod(typeof(CleanArchitectureApiConventions), nameof(CleanArchitectureApiConventions.Get))]
        public async Task<IActionResult> GetSetCache()
        {
            await _redisCacheClient.GetDbFromConfiguration().AddAsync<string>("myName", "naveen", DateTimeOffset.Now.AddMinutes(2));
            return Ok(await _redisCacheClient.GetDbFromConfiguration().GetAsync<string>("myName"));
        }
    }
}
