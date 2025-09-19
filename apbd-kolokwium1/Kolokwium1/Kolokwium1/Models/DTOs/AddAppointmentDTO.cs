namespace Kolokwium1.Models.DTOs;

public class AddAppointmentDTO
{
    public int Id { get; set; }
    public int patientId { get; set; }
    public string pwz { get; set; }
    public ServicesDTO services { get; set; }
    
}

public class ServicesDTO
{
    public string serviceName { get; set; }
    public string serviceFee { get; set; }
}