using System.Data;
using Kolkwium2.Models;
using Microsoft.EntityFrameworkCore;




public class DatabaseContext : DbContext
{
    public DbSet<Concert> Concerts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<PurchasedTicket> PurchasedTickets { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketConcert> TicketConcerts { get; set; }
    
    
    protected DatabaseContext()
    {
        
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        
        modelBuilder.Entity<Concert>().HasData(new List<Concert>()
        {
            new Concert() { ConcertId = 1, Name = "Tralala", Date = DateTime.Parse("1980-01-01") },
            new Concert() { ConcertId = 2, Name = "Kolk", Date = DateTime.Parse("1980-01-02") },
            new Concert() { ConcertId = 3, Name = "RolingStone", Date = DateTime.Parse("1980-01-03") },
        });

        modelBuilder.Entity<Customer>().HasData(new List<Customer>()
        {
            new Customer() {CustomerId = 1, FirstName = "Petro", LastName = "Smarkutskyi", PhoneNumber = "987654321"},
            new Customer() {CustomerId = 2, FirstName = "Jane", LastName = "Doe", PhoneNumber = "987654321"},
            new Customer() {CustomerId = 3, FirstName = "Karol", LastName = "Karl"},
            
            
        });
        modelBuilder.Entity<Ticket>().HasData(new List<Ticket>()
        {
            new Ticket() {TicketId = 1, SerialNumber = "127836127bdq", SeatNumber = 12},
            new Ticket() {TicketId = 2, SerialNumber = "jdjia738723", SeatNumber = 7},
            new Ticket() {TicketId = 3, SerialNumber = "djkj127211", SeatNumber = 1}
            
        });

        modelBuilder.Entity<TicketConcert>().HasData(new List<TicketConcert>()
        {
            new TicketConcert(){ TicketConcertId = 1, ConcertId = 1, TicketId = 1, Price = 122},
            new TicketConcert() {TicketConcertId = 2, ConcertId = 2, TicketId = 2, Price = 7},
            new TicketConcert() {TicketConcertId = 3, ConcertId = 3, TicketId = 3, Price = 123}
        });
        
        modelBuilder.Entity<PurchasedTicket>().HasData(new List<PurchasedTicket>()
        {
            new PurchasedTicket() {TicketConcertId = 1, CustomerId = 1, PurchaseDate = DateTime.Parse("1980-01-01")},
            new PurchasedTicket() {TicketConcertId = 2, CustomerId = 2, PurchaseDate = DateTime.Parse("1980-01-02")},
            new PurchasedTicket() {TicketConcertId = 3, CustomerId = 3, PurchaseDate = DateTime.Parse("1980-01-03")},
        });

    }
    
    
    
}