using System.ComponentModel.DataAnnotations.Schema;

namespace Kolkwium2.Models;

[Table("Concert")]
public class Concert
{
    public int ConcertId { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public int AvailableTickets { get; set; }
    
    public ICollection<TicketConcert> TicketConcerts { get; set; }
}