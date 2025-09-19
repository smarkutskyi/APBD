namespace Cwiczenie11.Model.DTOs;

public class ReceptaDTO
{
    public DateOnly Data { get; set; }
    public DateOnly DueDate { get; set; }
    public PatientDTO PatientDto { get; set; }
    public List<MedicamentDTO> Medicaments { get; set; }
    public int IdDoctor { get; set; }
    
}

public class PatientDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
}

public class MedicamentDTO
{
    public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    public string Description { get; set; }
    
}