namespace Cwicz3;

public class KontenerNaGaz : Kontener
{   
    
    
    public KontenerNaGaz(double masaLadunku, double wysokosc, double wagaWlasna, double glebokosc, double maksymalnaLadownosc) 
        : base(masaLadunku, wysokosc, wagaWlasna, glebokosc, maksymalnaLadownosc)
    {
        
    }

    public override string pobieramyTyp()
    {
        return "G";
    }
}