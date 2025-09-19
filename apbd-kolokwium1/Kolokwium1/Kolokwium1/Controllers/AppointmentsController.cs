using Kolokwium1.Models.DTOs;
using Kolokwium1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrzykladowyKolokwium1.Exeption;

namespace Kolokwium1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            try
            {
                var historyAppointment =  await _appointmentService.GetAppointmentsHistory(id);
                
                
                return Ok(historyAppointment);
                
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            
            
            
            
            
        }
        
    }
}
