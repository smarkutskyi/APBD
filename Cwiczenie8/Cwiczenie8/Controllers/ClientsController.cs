using Cwiczenie8.Models.DTOs;
using Cwiczenie8.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenie8.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsServices _iClientsServices;

        public ClientsController(IClientsServices iClientsServices)
        {
            _iClientsServices = iClientsServices;
        }

        [HttpPost] // tworzymy klienta, nie wiem czy trzeba bylo ale pesel jest unikalny
        public async Task<IActionResult> PostClient([FromBody] ClientDTO client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var id = await _iClientsServices.tworzymyKlientaAsync(client);
                if (id == null)
                {
                    return Conflict($"Pesel {client.Pesel} już istnieje");

                }

                return Created(nameof(PostClient), id);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpGet("{id}/trips")] // zwraca dane o wycieczkach klienta
        public async Task<IActionResult> GetTripsForClient(int id)
        {
            try
            {
                var trips = await _iClientsServices.PobieranieWycieczekKlientaAsync(id);

                if (trips == null)
                {
                    return NotFound(new { message = "Nie znaleziono wycieczek dla danego klienta" });
                }

                return Ok(trips);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpDelete("{clientId}/trips/{tripId}")] // usuwamy klienta z wycieczki
        public async Task<IActionResult> DeleteClientTrip(int clientId, int tripId)
        {
            try
            {
                var result = await _iClientsServices.UsunWycieczkiAsync(clientId, tripId);

                if (result == null)
                    return NotFound($"Klient o ID {clientId} nie istnieje");

                if (result == false)
                    return NotFound($"Klient nie jest zapisany na wycieczkę o ID {tripId}");

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPut("{clientId}/trips/{tripId}")] // dodajemy klienta do wycieczki, uwaga na miejsca
        public async Task<IActionResult> ZarejestrujKlienta(int clientId, int tripId)
        {
            var wynik = await _iClientsServices.ZapiszKlientaNaWycieczkeAsync(clientId, tripId);

            if (wynik == "Nie ma wycieczki")
            {
                return NotFound($"Nie znaleziono wycieczki o {tripId}");
            }
                

            if (wynik == "Nie ma Klienta")
            {
                return NotFound($"Nie znaleziono klienta o  {clientId}");
            }


            if (wynik == "Miejsca wycieczki Wypewnione")
            {
                return BadRequest("Brakwolnych miejsc a tęwycieczkę");
            }

            if (wynik == "Już zarejestrowany")
            {
                return BadRequest("Klient jeż jest zapisany na tę wycieczk");
                
            }


            return CreatedAtAction(
                nameof(ZarejestrujKlienta), 
                new { clientId, tripId }, 
                "Klient został pomyślni zapisany na wycieczke"
            );
        }
    }
    
    
}

