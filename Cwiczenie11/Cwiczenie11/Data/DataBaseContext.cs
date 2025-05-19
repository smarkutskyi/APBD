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
        base.OnModelCreating(modelBuilder);
    }
    
}