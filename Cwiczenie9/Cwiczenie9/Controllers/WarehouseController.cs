using Cwiczenie9.Models.DTOs;
using Cwiczenie9.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrzykladowyKolokwium1.Exeption;

namespace Cwiczenie9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseServiceservice;
        
        public WarehouseController(IWarehouseService warehouseServiceservice)
        
        {
            _warehouseServiceservice = warehouseServiceservice;
            
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> DodanieProduktuAsync([FromBody] WarehouseDTO warehouse)
        {
            
            if (warehouse == null)
                return BadRequest("Treść żądania jestpusta");

            if (warehouse.Amount <= 0)
                return BadRequest("Kwota musi być więkasza od 0");
            
            
            if (warehouse.CreatedAt == default)
                return BadRequest("CreatedAt musi być data i czas");

            
            try
            {
                var newId = await _warehouseServiceservice.DodajemyProduktDoMagazynuAsync(warehouse);
                return Ok(new { IdProductWarehouse = newId });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ConflictException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Unexpected server error", details = ex.Message });
            }
        }
        
        [HttpPost("addWithProcedure")]
        public async Task<IActionResult> AddProductToWarehouseViaProcedure([FromBody] WarehouseDTO warehouse)
        {
            try
            {
                var id = await _warehouseServiceservice.dodajemyProduktDoMagazynuProceduraAsync(warehouse);

                return Ok(new { IdProductWarehouse = id });
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ConflictException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
        
    }
    
    
    
}
