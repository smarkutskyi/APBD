using Cwiczenie12.DTO_s;
using Cwiczenie12.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenie12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        
        private readonly IDbService _dbService;
        
        public TripsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _dbService.GetTripsAsync(page, pageSize);
                return Ok(result);
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
            
        }
        
        
        [HttpDelete("{idClient}")]
        public async Task<IActionResult> DeleteClient(int idClient)
        {
            try
            {
                await _dbService.DeleteClientAsync(idClient);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (ConflictException ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }
        
        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AddClientToTrip(int idTrip, [FromBody] AddClientDto clientDto)
        {
            try
            {
                await _dbService.AddClientToTripAsync(idTrip, clientDto);
                return CreatedAtAction(null, null); // lub 204 NoContent, jeśli nie ma lokalizacji zasobu
            }
            catch (NotFoundException ke)
            {
                return NotFound(new { error = ke.Message });
            }
            catch (ConflictException e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }
}
