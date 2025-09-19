using Cwiczenie9.Models.DTOs;

namespace Cwiczenie9.Services;

public interface IWarehouseService
{
    Task<int> DodajemyProduktDoMagazynuAsync(WarehouseDTO warehouse);
    Task<int> dodajemyProduktDoMagazynuProceduraAsync(WarehouseDTO warehouse);

}