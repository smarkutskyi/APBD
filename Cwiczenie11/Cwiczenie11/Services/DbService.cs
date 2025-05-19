using Cwiczenie11.Data;
using Cwiczenie11.Model;
using Cwiczenie11.Model.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenie11.Services;

public class DbService : IDbService
{
    private readonly DataBaseContext _context;
    public DbService(DataBaseContext context)
    {
        _context = context;
    }
    public async Task DodanieNowejReceptyAsync(ReceptaDTO receptaDTO)
    {
        if (receptaDTO.Medicaments.Count > 10)
        {
            throw new ConflictException("Recepta może zawierać nie więcej niz 10 leków");
        }

        if (receptaDTO.Data > receptaDTO.DueDate)
        {
            throw new ConflictException("Due Data jest mniejsza od Data");
            
        }

        var doktor = await _context.Doctors.FindAsync(receptaDTO.IdDoctor);
        
        if (doktor == null)
        {
            throw new NotFoundException("Nie znaleziono doktora");
        }
        
        await using var trans = await _context.Database.BeginTransactionAsync();

        try
        {
            var patient = await _context.Patients.FindAsync(receptaDTO.PatientDto.IdPatient);
            if (patient == null)
            {
                patient = new Patient
                {
                    IdPatient = receptaDTO.PatientDto.IdPatient,
                    FirstName = receptaDTO.PatientDto.FirstName,
                    LastName = receptaDTO.PatientDto.LastName,
                    BirthDate = receptaDTO.PatientDto.BirthDate
                };
                await _context.Patients.AddAsync(patient);
                await _context.SaveChangesAsync();
            }

            foreach (var med in receptaDTO.Medicaments)
            {
                if (!await _context.Medicaments.AnyAsync(m => m.IdMedicament == med.IdMedicament))
                    throw new NotFoundException($"Lek o ID {med.IdMedicament} nie istnieje.");
            }

            var prescription = new Prescription
            {
                Date = receptaDTO.Data,
                DueDate = receptaDTO.DueDate,
                IdDoctor = receptaDTO.IdDoctor,
                IdPatient = patient.IdPatient,
                PrescriptionMedicaments = receptaDTO.Medicaments.Select(m => new PrescriptionMedicament
                {
                    IdMedicament = m.IdMedicament,
                    Dose = m.Dose,
                    Details = m.Description
                }).ToList()
            };

            await _context.Prescriptions.AddAsync(prescription);
            await _context.SaveChangesAsync();

            await trans.CommitAsync();
            
        } catch (Exception e)
        {
            await trans.RollbackAsync();
            throw;
        }


    }
}