using Microsoft.EntityFrameworkCore;
using PrzykladowyKolokwium2za2024.Models;

namespace PrzykladowyKolokwium2za2024.Data;

public class DatabaseContext : DbContext
{
    
    
    
    protected DatabaseContext()
    {
        
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       base.OnModelCreating(modelBuilder);
        
        
    }
    
    
    
}