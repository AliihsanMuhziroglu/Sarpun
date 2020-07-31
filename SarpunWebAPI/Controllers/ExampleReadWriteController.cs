using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SarpunWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExampleReadWriteController : ControllerBase
    {
        // GET: api/ExampleReadWrite
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return new string[] { "value1", "value2",userId };
        }

        // GET: api/ExampleReadWrite/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ExampleReadWrite
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/ExampleReadWrite/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
