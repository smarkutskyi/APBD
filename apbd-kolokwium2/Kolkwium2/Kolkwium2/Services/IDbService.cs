


using Kolkwium2.DTOs;
using Kolkwium2.Models;


public interface IDbService
{
     Task<List<CustomerGetDTO>> GetCustomersAsync(int id);
     
     Task PostCustomerAsync (CustomerPostDTO customerPostDTO);
    
}