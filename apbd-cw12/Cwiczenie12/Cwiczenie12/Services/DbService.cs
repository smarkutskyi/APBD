using Cwiczenie12.DTO_s;
using Cwiczenie12.Models;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenie12.Services;

public class DbService : IDbService
{
    private readonly ApbdContext _context;
    public DbService(ApbdContext context)
    {
        _context = context;
    }


    public async Task<object> GetTripsAsync(int page = 1, int pageSize = 10)
    {
        var totalTrips = await _context.Trips.CountAsync();
        var allPages = (int)Math.Ceiling(totalTrips / (double)pageSize);

        var trips = await _context.Trips
            .Include(t => t.ClientTrips).ThenInclude(ct => ct.IdClientNavigation)
            .Include(t => t.IdCountries).OrderByDescending(t => t.DateFrom)
            .Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking()
            .ToListAsync();

        var tripDtos = trips.Select(t => new TripDTO
        {
            Name = t.Name,
            Description = t.Description,
            DateFrom = t.DateFrom,
            DateTo = t.DateTo,
            MaxPeople = t.MaxPeople,
            
            
            Countries = t.IdCountries.Select(c => c.Name).ToList(),
            
            
            Clients = t.ClientTrips.Select(ct => new ClientDTO
            {
                FirstName = ct.IdClientNavigation.FirstName,
                LastName = ct.IdClientNavigation.LastName
            }).ToList()
            
            
        }).ToList();

        ResultTripDTO result= new ResultTripDTO()
        {
            pageNum = page.ToString(),
            pageSize = pageSize.ToString(),
            allPages = allPages.ToString(),
            trips = tripDtos
        };
        return result;
        
        
    }
    
    public async Task DeleteClientAsync(int idClient)
    {
        var client = await _context.Clients.FindAsync(idClient);
        if (client == null)
            throw new NotFoundException("Client not found.");

        var hasTrips = await _context.ClientTrips.AnyAsync(ct => ct.IdClient == idClient);
        if (hasTrips)
            throw new ConflictException("Cannot delete client with assigned trips.");

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }
    
    

    public async Task AddClientToTripAsync(int idTrip, AddClientDto clientDto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var existingClient = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == clientDto.Pesel);

            if (existingClient != null)
            {
                throw new ConflictException("Taki Pesel już jest zarejestrowany");
            }
                

            var trip = await _context.Trips.FirstOrDefaultAsync(t => t.IdTrip == idTrip);

            if (trip == null)
            {
                throw new NotFoundException("Podróży nie ma");
            }

            if (trip.DateFrom <= DateTime.UtcNow)
            {
                throw new ConflictException("Daty się kłucą");
            }
            

            var clientInTrip = await _context.ClientTrips
                    
                .Include(ct => ct.IdClientNavigation)
                .AnyAsync(ct => ct.IdTrip == idTrip && ct.IdClientNavigation.Pesel == clientDto.Pesel);

            if (clientInTrip)
            {
                throw new ConflictException("Klient już jest w tej podróże");
            }
                

            var newClient = new Client
            {
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                Email = clientDto.Email,
                Telephone = clientDto.Telephone,
                Pesel = clientDto.Pesel
            };

            await _context.Clients.AddAsync(newClient);
            await _context.SaveChangesAsync();
            

            var clientTrip = new ClientTrip
            {
                IdClient = newClient.IdClient,
                IdTrip = idTrip,
                RegisteredAt = DateTime.UtcNow,
                PaymentDate = clientDto.PaymentDate
            };

            await _context.ClientTrips.AddAsync(clientTrip);
            await _context.SaveChangesAsync();

            
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }


    
}