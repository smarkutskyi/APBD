using Cwiczenie8.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenie8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        
        private readonly ITripsServices _tripsServices;

        public TripsController(ITripsServices tripsServices)
        {
            _tripsServices = tripsServices;
        }
        
        [HttpGet] // zwraca dane o podróżach i w jakich krajach ona była
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _tripsServices.GetTripsAsync();
            
            
            return Ok(trips);
        }
        
        
        
        
    }
}
