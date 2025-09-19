using Kolkwium2.DTOs;
using Kolkwium2.Models;
using Microsoft.EntityFrameworkCore;




namespace PrzykladowyKolokwium2za2024.Services;

public class DbService : IDbService
{
     private readonly DatabaseContext _context;

     public DbService(DatabaseContext context)
     {
          _context = context;
     }

     public async Task<List<CustomerGetDTO>> GetCustomersAsync(int id)
     {
          
          
          var customer =  _context.Customers.FirstOrDefault(e => e.CustomerId == id);

          if (customer == null)
          {
               throw new NotFoundException("Customer not found");
               
          }
          
          var result = await _context.Customers.Where(e => e.CustomerId == id)
               .Select(e => new CustomerGetDTO
               {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PhoneNumber = e.PhoneNumber,
                    Purchases = e.PurchasedTickets.Select(p => new PurchasGetDTO()
                    {
                         Date = p.PurchaseDate,
                         Price = p.TicketConcert.Price,
                         Ticket = new TicketGetDTO()
                         {
                              SeatNumber = p.TicketConcert.Ticket.SeatNumber,
                              Serial = p.TicketConcert.Ticket.SerialNumber
                         },
                         Concert = new ConcertGetDTO()
                         {
                              Name = p.TicketConcert.Concert.Name,
                              Date = p.TicketConcert.Concert.Date
                         }
                         
                         
                    }).ToList()
               }).ToListAsync();
          
          return result;
     }

     public async Task PostCustomerAsync(CustomerPostDTO customerPostDTO)
     {
          var client = _context.Customers.FirstOrDefault(e => e.CustomerId == customerPostDTO.Id);

          if (client != null)
          {
               throw new ConflictException("Customer already exists");
          }

          if (customerPostDTO.Purchases.Count() > 5)
          {
               throw new ConflictException("Purchase limit exceeded");
          }
          
          using var transaction = await _context.Database.BeginTransactionAsync();


          try
          {
               var customer = new Customer
               {
                    CustomerId = customerPostDTO.Id,
                    FirstName = customerPostDTO.FirstName,
                    LastName = customerPostDTO.LastName,
                    PhoneNumber = customerPostDTO.PhoneNumber,
               };
               
               _context.Customers.Add(customer);
               await _context.SaveChangesAsync();
               
               foreach (var item in customerPostDTO.Purchases)
               {
                    var ticket = new Ticket()
                    {
                         TicketId = _context.Tickets.Max(t => t.TicketId) + 1,
                         SeatNumber = item.Ticket.SeatNumber,
                         SerialNumber = item.Ticket.Serial,
                    };
               }
               
               


          }
          catch (Exception e)
          {
               await transaction.RollbackAsync();
               throw;
          }
          
          
          
     }

     

     
     
     
}