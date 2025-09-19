using Kolkwium2.Models;

namespace Kolkwium2.DTOs;

public class CustomerPostDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    
    public List<PurchasPostDTO> Purchases { get; set; }
 }

public class PurchasPostDTO
{
    
    public TicketPostDTO Ticket { get; set; }
    public ConcertPostDTO Concert { get; set; }
    
}

public class TicketPostDTO
{
    public string Serial { get; set; }
    public int SeatNumber{ get; set; }
    public decimal Price { get; set; }
}

public class ConcertPostDTO
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
}