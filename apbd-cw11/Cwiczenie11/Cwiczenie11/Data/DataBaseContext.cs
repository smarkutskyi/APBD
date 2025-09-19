using Cwiczenie11.Model;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenie11.Data;

public class DataBaseContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    public DataBaseContext()
    {
    }

    public DataBaseContext(DbContextOptions options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor()
            {
                IdDoctor = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Emali = "<EMAIL>"
            });

        modelBuilder.Entity<Medicament>().HasData(
            new Medicament()
            {
                IdMedicament = 1,
                Name = "Paracetamol",
                Description = "Paracetamol",
                Type = "Antybiotyczne"
            }, new Medicament()
            {
                IdMedicament = 2,
                Name = "Ibuprofen",
                Description = "Ibuprofen",
                Type = "Antybiotyczne"
            }, new Medicament()
            {
                IdMedicament = 3,
                Name = "Opium",
                Description = "Opium",
                Type = "Antybiotyczne"
            }, new Medicament()
            {
                IdMedicament = 4,
                Name = "APAP",
                Description = "Apap",
                Type = "Antybiotyczne"
            });

        modelBuilder.Entity<Patient>().HasData(
            new Patient()
            {
                IdPatient = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                BirthDate = new DateOnly(1990, 1, 1)
            },
            new Patient()
            {
                IdPatient = 2,
                FirstName = "Jan",
                LastName = "Nowak",
                BirthDate = new DateOnly(1990, 1, 1)
            });
        
        modelBuilder.Entity<Prescription>().HasData(
            new Prescription()
            {
                IdPrescription = 2,
                IdPatient = 1,
                IdDoctor = 1,
                Date = new DateOnly(2025, 5, 20),
                DueDate = new DateOnly(2025, 5, 20)
            },
            new Prescription()
            {
                IdPrescription = 3,
                IdPatient = 1,
                IdDoctor = 1,
                Date = new DateOnly(2025, 5, 20),
                DueDate = new DateOnly(2025, 5, 20)
            });
        
        modelBuilder.Entity<PrescriptionMedicament>().HasData(
            new PrescriptionMedicament()
            {
                IdPrescription = 2,
                IdMedicament = 1,
                Details = "Paracetamol",
                Dose = 7
            },
            new PrescriptionMedicament()
            {
                IdPrescription = 3,
                IdMedicament = 2,
                Details = "Ibuprofen",
                Dose = 7
            },
            new PrescriptionMedicament()
            {
                IdPrescription = 3,
                IdMedicament = 3,
                Details = "Opium",
                Dose = 3
            });
            
        base.OnModelCreating(modelBuilder);
    }
    
}