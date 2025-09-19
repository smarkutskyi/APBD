using Cwiczenie11.Model.DTOs;
using Cwiczenie11.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenie11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public PrescriptionsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPrescription([FromBody] ReceptaDTO dto)
        {
            
            try
            {
                
                await _dbService.DodanieNowejReceptyAsync(dto); 
                return Created("", 
                    new { message = "Recepta została dodana." });
            }
            catch (NotFoundException ex)
            {
                
                return NotFound(new { error = ex.Message });
            }
            catch (ConflictException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { error = "Wystąpił nieoczekiwany błąd", details = ex.Message });
            }
        }
    }
        
    
}
