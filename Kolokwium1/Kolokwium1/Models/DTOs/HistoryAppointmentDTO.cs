namespace Kolokwium1.Models.DTOs;

public class HistoryAppointmentDTO
{
    public DateTime Date { get; set; }
    public DoctorHistoryDTO Doctor { get; set; }
    public PatientHistoryDTO Patient { get; set; }
    
    public AppointmentServicesHistoryDTO AppointmentServices { get; set; }
    
}

public class DoctorHistoryDTO
{
    public int DoctorId { get; set; }
    public string PWZ { get; set; }
}

public class PatientHistoryDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class AppointmentServicesHistoryDTO
{
    public decimal serviceFee { get; set; }
    public string name { get; set; }
    
}
