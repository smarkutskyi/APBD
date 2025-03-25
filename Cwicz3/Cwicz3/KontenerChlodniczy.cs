namespace Cwicz3;

public class KontenerChlodniczy : Kontener
{
    
    
    public KontenerChlodniczy(double masaLadunku, double wysokosc, double wagaWlasna, double glebokosc, double maksymalnaLadownosc) 
        : base(masaLadunku, wysokosc, wagaWlasna, glebokosc, maksymalnaLadownosc)
    {
        
    }

    public override string pobieramyTyp()
    {
        return "C";
    }
}