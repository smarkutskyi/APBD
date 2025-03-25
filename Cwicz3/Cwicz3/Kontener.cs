namespace Cwicz3;

public abstract class Kontener
{
    private double _masaLadunku { get; set; }
    private double _wysokosc { get;}
    private double _wagaWlasna { get;}
    private double _glebokosc { get; }
    private String _numerSeryjny { get;}
    private double _maksymalnaLadownosc { get;}

    private static int index = 0;

    protected Kontener(double masaLadunku, double wysokosc, double wagaWlasna, double glebokosc, double maksymalnaLadownosc)
    {
        _numerSeryjny = 
        _masaLadunku = masaLadunku;
        _wysokosc = wysokosc;
        _wagaWlasna = wagaWlasna;
        _glebokosc = glebokosc;
        _maksymalnaLadownosc = maksymalnaLadownosc;
        
    }


    public void oproznienieLadunku()
    {
        _masaLadunku = 0;
    }

    public void zaladowanieLadunku(double masaLadunku)
    {
        if (masaLadunku > _maksymalnaLadownosc)
        {
            throw new OverfillException("Masa ladunku jest za duża!");
        }
        
        this._masaLadunku = masaLadunku;
        
    }

    public string tworzynyNumerSeryjny()
    {
        index++;
        return "KON-" + pobieramyTyp() + "-" + index.ToString();
    }

    public abstract string pobieramyTyp(); 
    
    





}