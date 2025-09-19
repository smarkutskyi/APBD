using System.Data;
using Cwiczenie8.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace Cwiczenie8.Services;

public class TripsServices : ITripsServices
{
    private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=APBD;Integrated Security=True;";
    
    
    public async Task<List<TripDTO>> GetTripsAsync()
    {
        
        var trips = new Dictionary<int, TripDTO>();
        
        var cmdText = @"select t.idTrip, t.Name as TripName, t.Description, t.DateFrom, t.DateTo, t.MaxPeople, c.Name as CountryName
                        from Trip t
                        join Country_Trip ct on t.IdTrip = ct.IdTrip
                        join Country c on ct.IdCountry = c.IdCountry";

        using (SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(cmdText, conn))
        {
            await conn.OpenAsync();

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while ( await reader.ReadAsync())
                {
                    
                    var indexTrip = (int) reader["idTrip"];

                    if (!trips.ContainsKey(indexTrip))
                    {
                        trips[indexTrip] = new TripDTO()
                        {
                            IdTrip = (int) reader["idTrip"],
                            Name = (string) reader["TripName"],
                            Description = (string) reader["Description"],
                            DateFrom = (DateTime) reader["DateFrom"],
                            DateTo = (DateTime) reader["DateTo"],
                            MaxPeople = (int) reader["MaxPeople"],
                            Couriers = new List<CountryDTO>()
                        };
                        
                    }
                    
                    
                    trips[indexTrip].Couriers.Add(new CountryDTO()
                    {
                        Name = (string) reader["CountryName"]
                    });
                }
            }
        }
        
        return trips.Values.ToList();
    }
}