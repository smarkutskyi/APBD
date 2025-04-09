using Czwicz6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Czwicz6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var tests = Database.Tests;
            return tests.Any() ? Ok(tests) : new NoContentResult();
        }
        // Get api/tests/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var tests = Database.Tests.FirstOrDefault(x => x.Id == id);
            return Ok(tests);
        }

        // Post api/tests { "id":}
        [HttpPost]
        public IActionResult Add(Test test)
        {
            Database.Tests.Add(test);
            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult Update(Test test)
        {
            
        }
        
    }
}
