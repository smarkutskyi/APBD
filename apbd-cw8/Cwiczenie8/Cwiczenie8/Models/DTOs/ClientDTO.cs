using System.ComponentModel.DataAnnotations;

namespace Cwiczenie8.Models.DTOs;

public class ClientDTO
{
    public int IdClient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    [Required]
    [StringLength(11, MinimumLength = 11)]
    public string Pesel { get; set; }
}