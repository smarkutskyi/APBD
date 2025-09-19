namespace Cwiczenie11.Model.DTOs;

public class PacjentGetDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public List<ReceptaGetDTO> Prescriptions { get; set; }
    
}

public class ReceptaGetDTO
{
    public int IdPrescription { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    public List<MedicamentGetDTO> Medicaments { get; set; }
    public DoctorGetDTO Doctor { get; set; }
    
}

public class MedicamentGetDTO
{
    public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
}

public class DoctorGetDTO
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
