namespace Cwicz3;

public class KontenerPlyny : Kontener, IHazardNotifier
{

    private bool niebezpieczny; 

    public KontenerPlyny(double masaLadunku, double wysokosc, double wagaWlasna, double glebokosc, double maksymalnaLadownosc, bool niebezpieczny) 
        : base(masaLadunku, wysokosc, wagaWlasna, glebokosc, maksymalnaLadownosc)
    {
        this.niebezpieczny = niebezpieczny;
    }

    public override string pobieramyTyp()
    {
        return "L";
    }

    public void powiadomienie(string powiadomienie)
    {
        Console.WriteLine("Uwaga:" + powiadomienie +  " => Numer seryjny: " + _numerSeryjny);
    }

    public override void zaladowanieLadunku(double masaLadunku)
    {
        
        if (niebezpieczny)
        {
            if (masaLadunku >= (_maksymalnaLadownosc * 0.5) )
            {
                powiadomienie("Za duża masa ladunku dla niebiezpicznego towaru!");
                
                throw new OverfillException("Za duża masa");
            }
        } else
        {
            if (masaLadunku >= (_maksymalnaLadownosc * 0.9))
            {
                powiadomienie("Za duża masa ladunku!");

                throw new OverfillException("Za duża masa");
            }
        }
        
        base.zaladowanieLadunku(masaLadunku); // można po poprostu przypisać odrazu do pola klasy 
        
        
        
    }
    
    
    
    
    
}