using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kolkwium2.Models;

[Table("Purchased_Ticket")]
[PrimaryKey(nameof(TicketConcertId), nameof(CustomerId))]
public class PurchasedTicket
{
    [ForeignKey("TicketConcertId")]
    public int TicketConcertId { get; set; }
    [ForeignKey("CustomerId")]
    public int CustomerId { get; set; }
    public DateTime PurchaseDate { get; set; }
    
    public Customer Customer { get; set; }
    public TicketConcert TicketConcert { get; set; }
    
}