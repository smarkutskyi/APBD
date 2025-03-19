namespace Cwicz3;

public abstract class Kontener
{
    private double _masaLadunku { get; set; }
    private double _wysokosc { get; set; }
    private double _wagaWlasna { get; set; }
    private double _glebokosc { get; set; }
    private String _numerSeryjny { get; set; }
    private double _maksymalnaLadownosc { get; set; }


    public void oproznienieLadunku()
    {
        _masaLadunku = 0;
    }

    public void zaladowanieLadunku(double masaLadunku)
    {
        if (masaLadunku > _maksymalnaLadownosc)
        {
            //throw new OverfillException();
        }
        
        this._masaLadunku = masaLadunku;
        
    }
    
    
    
    
    
}