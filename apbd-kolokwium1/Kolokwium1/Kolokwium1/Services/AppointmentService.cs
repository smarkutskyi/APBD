using Kolokwium1.Models.DTOs;
using Microsoft.Data.SqlClient;
using PrzykladowyKolokwium1.Exeption;

namespace Kolokwium1.Services;

public class AppointmentService : IAppointmentService
{
    private readonly string _connectionString;

    public AppointmentService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Kolokwium1") ?? string.Empty;
    }

    public async Task<HistoryAppointmentDTO> GetAppointmentsHistory(int userId)
    {
        await using var con = new SqlConnection(_connectionString);
        await con.OpenAsync();
        
        var cmdText = @"select a.date, p.first_name, p.last_name, p.date_of_birth, d.doctor_id, d.PWZ, ase.service_fee, s.name as ServiceName
from Appointment a
join Doctor d on a.doctor_id = d.doctor_id
join Patient p on a.patient_id = p.patient_id
join Appointment_Service ase on a.appoitment_id = ase.appoitment_id
join Service s on ase.service_id = s.service_id
where a.appoitment_id = @id";
        
        
        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = cmdText;
        cmd.Parameters.AddWithValue("@id", userId);
        
        HistoryAppointmentDTO result = null;
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            if (result == null)
            {
                result = new HistoryAppointmentDTO()
                {
                    Date = (DateTime) reader["date"],
                    Doctor = new DoctorHistoryDTO()
                    {
                        DoctorId = (int) reader["doctor_id"],
                        PWZ = (string) reader["PWZ"]
                    },
                    Patient = new PatientHistoryDTO()
                    {
                        FirstName = (string) reader["first_name"],
                        LastName = (string) reader["last_name"],
                        DateOfBirth = (DateTime) reader["date_of_birth"]
                    },
                    AppointmentServices = new AppointmentServicesHistoryDTO()
                    {
                        name = (string) reader["ServiceName"],
                        serviceFee = (decimal) reader["service_fee"],
                    }
                };
                
            }
            
            
        }

        if (result == null)
        {
            throw new NotFoundException($"Nie ma wizyty z id : {userId}");
            
        }
        
        return result;
    }

//     public async Task AddAppointment(AddAppointmentDTO addAppointment)
//     {
//         await using SqlConnection con = new SqlConnection(_connectionString);
//         await using SqlCommand cmd = new SqlCommand();
//         cmd.Connection = con;
//         
//         await con.OpenAsync();
//
//         var transaction = await con.BeginTransactionAsync();
//         
//         cmd.Transaction = transaction as SqlTransaction;
//
//         try
//         {
//             cmd.Parameters.Clear();
//             var cmdText = @"Select first_name
// from Patient
// Where patient_id = @idPatient";
//             cmd.CommandText = cmdText;
//             cmd.Parameters.AddWithValue("@id", addAppointment.patientId);
//             
//             var czyIstniejePacjent = await cmd.ExecuteScalarAsync();
//
//             if (czyIstniejePacjent == null)
//             {
//                 throw new NotFoundException($"Nie ma takiego pacjenta o id: {addAppointment.patientId}");
//             }
//
//             cmd.Parameters.Clear();
//             var cmdText2 = @"Select PWZ
// from Doctor
// Where PWZ = @DoctorPWZ";
//             
//             cmd.CommandText = cmdText2;
//             cmd.Parameters.AddWithValue("@id", addAppointment.patientId);
//             
//             var czyIstniejDoctro = await cmd.ExecuteScalarAsync();
//
//             if (czyIstniejDoctro == null)
//             {
//                 throw new NotFoundException($"Nie ma takiego doktora o pwz: {addAppointment.pwz}");
//             }
//             
//             cmd.Parameters.Clear();
//             
//             var cmdText3 = @"Select 1
//               from Service
//             Where name = @ServiceName";
//             
//             cmd.CommandText = cmdText3;
//             cmd.Parameters.AddWithValue("@id", addAppointment.services.serviceName);
//             
//             var czyIstniejeServiceName = await cmd.ExecuteScalarAsync();
//
//             if (czyIstniejePacjent == null)
//             {
//                 throw new NotFoundException($"Nie ma takiego serwisu o nazwie: {addAppointment.services.serviceName}");
//             }
//             
//             cmd.Parameters.Clear();
//             var cmdText4 = @"Insert into Appointment (appointment_id,patient_id, pwz) 
//                             values (@appid, @patientId, @doctorId, @date)";
//             
//             cmd.CommandText = cmdText4;
//             cmd.Parameters.AddWithValue("@appid", addAppointment.Id);
//             cmd.Parameters.AddWithValue("@patientId", addAppointment.patientId);
//             cmd.Parameters.AddWithValue("@pwz", addAppointment.pwz);
//             
//             try
//             {
//                 await cmd.ExecuteNonQueryAsync();
//             }
//             catch (Exception e)
//             {
//                 
//                 throw new ConflictException($"");
//             }
//             
//
//             try
//             {
//
//             }
//             catch (Exception e)
//             {
//                 
//                 throw new ConflictException("Istnieja taka wizyta o id: {id}");
//             }
//             
//             
//
//
//
//
//
//
//         }
//         catch(Exception e)
//         {
//             transaction.Rollback();
//             throw;
//         }
//         
//     }
    
    
}