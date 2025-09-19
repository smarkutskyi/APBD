using Cwiczenie12.DTO_s;

namespace Cwiczenie12.Services;

public interface IDbService
{
    Task<object> GetTripsAsync(int page = 1, int pageSize = 10);

    Task DeleteClientAsync(int idClient);

    Task AddClientToTripAsync(int idTrip, AddClientDto clientDto);
}