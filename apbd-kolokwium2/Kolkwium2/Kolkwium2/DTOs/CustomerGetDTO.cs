namespace Kolkwium2.DTOs;

public class CustomerGetDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    
    public List<PurchasGetDTO> Purchases { get; set; }
}

public class PurchasGetDTO
{
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    public TicketGetDTO Ticket { get; set; }
    public ConcertGetDTO Concert { get; set; }
    
}

public class TicketGetDTO
{
    public string Serial { get; set; }
    public int SeatNumber{ get; set; }
    
}

public class ConcertGetDTO
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
}