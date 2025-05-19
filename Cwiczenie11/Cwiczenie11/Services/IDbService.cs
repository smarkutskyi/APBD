using Cwiczenie11.Model.DTOs;

namespace Cwiczenie11.Services;

public interface IDbService
{
    Task DodanieNowejReceptyAsync(ReceptaDTO receptaDTO);
    
}