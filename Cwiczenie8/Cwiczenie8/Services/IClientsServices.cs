using Cwiczenie8.Models.DTOs;

namespace Cwiczenie8.Services;

public interface IClientsServices
{
     Task<int?> tworzymyKlientaAsync(ClientDTO client);
     
     Task<List<TripDTO>> PobieranieWycieczekKlientaAsync(int clientId);
     Task<bool?> UsunWycieczkiAsync(int clientId, int tripId);
     
     Task<string> ZapiszKlientaNaWycieczkeAsync(int clientId, int tripId);
}