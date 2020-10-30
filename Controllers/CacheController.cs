using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace RedisExampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
      private readonly IDatabase _cache;

        public CacheController(IDatabase cache)
        {
            _cache = cache;
        }
        
        [HttpGet("{key}")]  
        public string Get(string key) {  
            var c = _cache.StringGet(key);
            return c;
        }  

        [HttpPost]  
        [Route("post")]
        public void Post([FromBody] KeyValuePair <string, string> keyValue) {  
             _cache.StringSet(keyValue.Key, keyValue.Value);  
        }
    }
}
