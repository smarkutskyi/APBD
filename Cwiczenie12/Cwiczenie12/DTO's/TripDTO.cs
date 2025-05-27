namespace Cwiczenie12.DTO_s;

public class TripDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public List<string> Countries { get; set; }
    public List<ClientDTO> Clients { get; set; }
}

public class ClientDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class ResultTripDTO
{
    public string pageNum { get; set; }
    public string pageSize { get; set; }
    public string allPages { get; set; }
    public List<TripDTO> trips { get; set; }
}