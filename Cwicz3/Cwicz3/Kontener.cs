namespace Cwicz3;

public abstract class Kontener
{
    protected double _masaLadunku { get; set; }
    protected double _wysokosc { get;}
    protected double _wagaWlasna { get;}
    protected double _glebokosc { get; }
    protected string _numerSeryjny { get; }
    protected double _maksymalnaLadownosc { get;}

    private static int index = 0;

    protected Kontener(double masaLadunku, double wysokosc, double wagaWlasna, double glebokosc, double maksymalnaLadownosc)
    {
        _numerSeryjny = tworzynyNumerSeryjny();
        _masaLadunku = masaLadunku;
        _wysokosc = wysokosc;
        _wagaWlasna = wagaWlasna;
        _glebokosc = glebokosc;
        _maksymalnaLadownosc = maksymalnaLadownosc;
        
    }


    public virtual void oproznienieLadunku()
    {
        _masaLadunku = 0;
    }

    public virtual void zaladowanieLadunku(double masaLadunku)
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