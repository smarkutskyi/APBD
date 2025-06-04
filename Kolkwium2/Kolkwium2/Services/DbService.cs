using Microsoft.EntityFrameworkCore;
using PrzykladowyKolokwium2za2024.Data;

using PrzykladowyKolokwium2za2024.Models;

namespace PrzykladowyKolokwium2za2024.Services;

public class DbService : IDbService
{
     private readonly DatabaseContext _context;

     public DbService(DatabaseContext context)
     {
          _context = context;
     }

     

     
     
     
}