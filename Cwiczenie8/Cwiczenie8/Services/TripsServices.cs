using System.Data;
using Cwiczenie8.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace Cwiczenie8.Services;

public class TripsServices : ITripsServices
{
    private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=APBD;Integrated Security=True;";
    
    public async Task<List<TripDTO>> GetTripsAsync()
    {
        var trips = new List<TripDTO>();
        var cmdText = @"Select IdTrip, Name from Trip";

        using (SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(cmdText, conn))
        {
            await conn.OpenAsync();

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                int idTripOrdinal = reader.GetOrdinal("IdTrip");
                
                while (await reader.ReadAsync())
                {
                    trips.Add(new TripDTO()
                    {
                        IdTrip = reader.GetInt32(idTripOrdinal),
                        Name = reader.GetString(1),
                    });
                    
                }

                
                
            }

            
            
            
        }
        

        

        
        
        return trips;
    }
}