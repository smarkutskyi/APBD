using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kolkwium2.Models;

[Table("Ticket_Concert")]
public class TicketConcert
{
    [Key]
    public int TicketConcertId { get; set; }
    [ForeignKey("TicketId")]
    public int TicketId { get; set; }
    [ForeignKey("ConcertId")]
    public int ConcertId { get; set; }
    [Column(TypeName = "decimal")]
    [Precision(10,2)]
    public decimal Price { get; set; }
    
    public Concert Concert { get; set; }
    public Ticket Ticket { get; set; }
    
    public ICollection<PurchasedTicket> PurchasedTickets { get; set; }
}