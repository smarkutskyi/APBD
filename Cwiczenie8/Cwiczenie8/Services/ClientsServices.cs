using System.Data.SqlTypes;
using Cwiczenie8.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace Cwiczenie8.Services;

public class ClientsServices : IClientsServices
{
    private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=APBD;Integrated Security=True;";

    

    public async Task<int?> tworzymyKlientaAsync(ClientDTO client)
    {
        await using SqlConnection con = new SqlConnection(_connectionString);
        await con.OpenAsync();
    
        
        var cmdText = @"SELECT 1 FROM Client WHERE Pesel = @Pesel";
        await using SqlCommand cmd = new SqlCommand(cmdText, con);
        
        cmd.Parameters.AddWithValue("@Pesel", client.Pesel);

        var czyIstnieje = await cmd.ExecuteScalarAsync();

        if (czyIstnieje != null)
        {
            return null; 
        }

        
        var cmdText2 = @"
        INSERT INTO Client (FirstName, LastName, Email, Telephone, Pesel)
        OUTPUT INSERTED.IdClient
        VALUES (@FirstName, @LastName, @Email, @Telephone, @Pesel)";
    
        
        cmd.CommandText = cmdText2;
    
        
        cmd.Parameters.Clear();
    
        
        cmd.Parameters.AddWithValue("@FirstName", client.FirstName);
        cmd.Parameters.AddWithValue("@LastName", client.LastName);
        cmd.Parameters.AddWithValue("@Email", client.Email);
        cmd.Parameters.AddWithValue("@Telephone", client.Telephone);
        cmd.Parameters.AddWithValue("@Pesel", client.Pesel);

        var id = await cmd.ExecuteScalarAsync();

        if (id == null)
        {
            return null; 
        }

        return (int) id;
    }
    
    public async Task<List<TripDTO>> PobieranieWycieczekKlientaAsync(int clientId)
    {
        await using SqlConnection con = new SqlConnection(_connectionString);
        await con.OpenAsync();

        
        var cmdText = "SELECT 1 FROM Client WHERE IdClient = @ClientId";
        
        await using SqlCommand cmd = new SqlCommand(cmdText, con);
        cmd.Parameters.AddWithValue("@ClientId", clientId);
        var clientExists = await cmd.ExecuteScalarAsync();

        if (clientExists == null)
        {
            return null; 
        }

        
        var cmdText2 = @"SELECT t.IdTrip, t.Name, t.Description, t.DateFrom, t.DateTo, t.MaxPeople, ct.RegisteredAt, ct.PaymentDate
                            FROM Trip t
                            JOIN Client_Trip ct ON t.IdTrip = ct.IdTrip
                            WHERE ct.IdClient = @ClientId";

        var trips = new List<TripDTO>();

        await using SqlCommand cmd2 = new SqlCommand(cmdText2, con);
        
        cmd2.Parameters.AddWithValue("@ClientId", clientId);

        await using SqlDataReader reader = await cmd2.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var trip = new TripDTO
            {
                IdTrip = (int) reader["IdTrip"],
                Name = (string) reader["Name"],
                Description = (string) reader["Description"],
                DateFrom = (DateTime) reader["DateFrom"],
                DateTo = (DateTime) reader["DateTo"],
                MaxPeople = (int) reader["MaxPeople"],
                RegisteredAt = (int) reader["RegisteredAt"],
                PaymentDate = (int) reader["PaymentDate"]
            };

            trips.Add(trip);
        }

        if (trips.Count == 0)
        {
            return null; 
        }

        return trips;
    }
    
    public async Task<bool?> UsunWycieczkiAsync(int clientId, int tripId) 
    {
        await using var con = new SqlConnection(_connectionString);
        await con.OpenAsync();

        var cmdText = @"SELECT 1 FROM Client WHERE IdClient = @ClientId";
        var checkClientCmd = new SqlCommand(cmdText, con);
        
        checkClientCmd.Parameters.AddWithValue("@ClientId", clientId);
        
        var clientExists = await checkClientCmd.ExecuteScalarAsync();

        if (clientExists == null)
        {
            return null;
        }

        var cmdText2 = @"SELECT 1 FROM Client_Trip WHERE IdClient = @ClientId AND IdTrip = @TripId";

        var checkRegistrationCmd = new SqlCommand(cmdText2, con);
        checkRegistrationCmd.Parameters.AddWithValue("@ClientId", clientId);
        checkRegistrationCmd.Parameters.AddWithValue("@TripId", tripId);


        var isRegistered = await checkRegistrationCmd.ExecuteScalarAsync();
        if (isRegistered == null)
        {
            return false;
        }

       
        var cmdText3 = @"DELETE FROM Client_Trip WHERE IdClient = @ClientId AND IdTrip = @TripId";
        var deleteCmd = new SqlCommand(cmdText3, con);
        
        deleteCmd.Parameters.AddWithValue("@ClientId", clientId);
        
        
        
        deleteCmd.Parameters.AddWithValue("@TripId", tripId);
        await deleteCmd.ExecuteNonQueryAsync();

        return true;
    }
    
    public async Task<string> ZapiszKlientaNaWycieczkeAsync(int clientId, int tripId)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();


        var cmdText = @"SELECT 1 FROM Client WHERE IdClient = @ClientId";

        using var checkClientCmd = new SqlCommand(cmdText, connection);
        checkClientCmd.Parameters.AddWithValue("@ClientId", clientId);



        var clientFound = await checkClientCmd.ExecuteScalarAsync();
        if (clientFound == null) return "Nie ma Klienta";

        var cmdText2 = @"SELECT MaxPeople FROM Trip WHERE IdTrip = @TripId";

        using var checkTripCmd = new SqlCommand(cmdText2, connection);
        checkTripCmd.Parameters.AddWithValue("@TripId", tripId);


        var maxSlotObj = await checkTripCmd.ExecuteScalarAsync();
        if (maxSlotObj == null) return "Nie ma wycieczki";

        int maxSlots = (int)maxSlotObj;

        var cmdText3 = @"SELECT COUNT(*) FROM Client_Trip WHERE IdTrip = @TripId";

        using var countCmd = new SqlCommand(cmdText3, connection);

        countCmd.Parameters.AddWithValue("@TripId", tripId);

        int currentRegistrations = (int)(await countCmd.ExecuteScalarAsync());

        if (currentRegistrations >= maxSlots)
        {
            return "Miejsca wycieczki Wypewnione";
        }


        var cmdText4 = @"SELECT 1 FROM Client_Trip WHERE IdClient = @ClientId AND IdTrip = @TripId";
        using var existingCmd = new SqlCommand(cmdText4, connection);
        existingCmd.Parameters.AddWithValue("@ClientId", clientId);
        existingCmd.Parameters.AddWithValue("@TripId", tripId);

        var existingRecord = await existingCmd.ExecuteScalarAsync();

        if (existingRecord != null)
        {
            return "Już zarejestrowany";
        }



        var cmdText5 = "INSERT INTO Client_Trip (IdClient, IdTrip, RegisteredAt, PaymentDate) VALUES (@ClientId, @TripId, @RegisteredAt, NULL)";

        using var insertCmd = new SqlCommand( cmdText5, connection);
        insertCmd.Parameters.AddWithValue("@ClientId", clientId);


        insertCmd.Parameters.AddWithValue("@TripId", tripId);


        insertCmd.Parameters.AddWithValue("@RegisteredAt", int.Parse(DateTime.Now.ToString("yyyyMMdd")));
        await insertCmd.ExecuteNonQueryAsync();

        return "Dobrze";
    }
    
    
}

