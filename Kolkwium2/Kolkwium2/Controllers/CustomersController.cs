using Kolkwium2.DTOs;
using Kolkwium2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kolkwium2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IDbService _dbService;

        public CustomersController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("/{id}/purchases")]
        public async Task<IActionResult> GetCustomerPurchases(int id)
        {
            try
            {

                var result = await _dbService.GetCustomersAsync(id);
                return Ok(result);

            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
            
        }
/*
        [HttpPost]
        public Task<IActionResult> AddCustomers([FromBody] CustomerPostDTO customers)
        {
            try
            {
                return Ok();

            }
            catch (ConflictException e)
            {
                return Conflict(e.Message);
            }
        }
        */
    }
}
