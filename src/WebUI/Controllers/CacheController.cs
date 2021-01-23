using CleanArchitecture.Infrastructure.ApiConventions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Controllers
{
    public class CacheController : ApiControllerBase
    {
        private readonly IDistributedCache _cache;
        public CacheController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        [Route("get-set-cache")]
        [ApiConventionMethod(typeof(CleanArchitectureApiConventions), nameof(CleanArchitectureApiConventions.Get))]
        public async Task<IActionResult> GetSetCache()
        {
            var cachedName = await _cache.GetStringAsync("name");
            if (string.IsNullOrEmpty(cachedName))
            {
                cachedName = "Heisenberg";

                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(new System.TimeSpan(0, 0, 15));

                //and then, put them in cache
                _cache.SetString("name", cachedName, options);
            }
            return Ok(cachedName);
        }
    }
}
