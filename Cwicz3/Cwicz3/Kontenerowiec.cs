namespace Cwicz3;

public class Kontenerowiec
{
    private List<Kontener> kontener { get; }
    
    private double maksPredkosc { get;}
    private int liczbaKontenerow { get; }
    private double wagaSumyKontenerow { get; }

    public Kontenerowiec(List<Kontener> kontener, double maksPredkosc, int liczbaKontenerow, double wagaSumyKontenerow)
    {
        this.kontener = kontener;
        this.maksPredkosc = maksPredkosc;
        this.liczbaKontenerow = liczbaKontenerow;
        this.wagaSumyKontenerow = wagaSumyKontenerow;
    }
    
    
}