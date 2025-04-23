using Cwiczenie8.Models.DTOs;

namespace Cwiczenie8.Services;

public interface ITripsServices
{
    Task<List<TripDTO>>GetTripsAsync();
}