namespace Cwicz3;

public class KontenerNaGaz : Kontener , IHazardNotifier
{
    private double _cisnienie { get;}
    
    public KontenerNaGaz(double masaLadunku, double wysokosc, double wagaWlasna, double glebokosc, double maksymalnaLadownosc, double cisnienie) 
        : base(masaLadunku, wysokosc, wagaWlasna, glebokosc, maksymalnaLadownosc)
    {
        this._cisnienie = cisnienie;
    }

    public override string pobieramyTyp()
    {
        return "G";
    }

    public override void oproznienieLadunku()
    {
        _masaLadunku = (_masaLadunku * 0.05);
        
    }

    public override void zaladowanieLadunku(double masaLadunku)
    {
        if (masaLadunku > _maksymalnaLadownosc)
        {
            powiadomienie("Za duża masa ladunku!");
            throw new OverfillException("Za duża masa ladunku!"); // błąd zwracamy tak jak napisano 
        }
        base.zaladowanieLadunku(masaLadunku);
    }

    public void powiadomienie(string powiadomien )
    {
        Console.WriteLine("Uwaga:" + powiadomienie +  " => Numer seryjny: " + _numerSeryjny);
    } 
    
    
}