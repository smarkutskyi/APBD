using Kolokwium1.Models.DTOs;

namespace Kolokwium1.Services;

public interface IAppointmentService
{
    Task<HistoryAppointmentDTO> GetAppointmentsHistory(int userId);
    
    // Task AddAppointment(AddAppointmentDTO addAppointment);
}