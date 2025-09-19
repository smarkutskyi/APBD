namespace Cwicz3;

public class Kontenerowiec
{
    public List<Kontener> kontenery { get; }
    
    public double maksPredkosc { get;}
    public int liczbaKontenerow { get; }
    public double wagaSumyKontenerow { get; }

    public Kontenerowiec( double maksPredkosc, int liczbaKontenerow, double wagaSumyKontenerow)
    {
        this.kontenery = kontenery;
        this.maksPredkosc = maksPredkosc;
        this.liczbaKontenerow = liczbaKontenerow;
        this.wagaSumyKontenerow = wagaSumyKontenerow;
    }

    public void dodajKontener(Kontener kontener)
    {
        this.kontenery.Add(kontener);
    }

    public void usunKontener(Kontener kontener)
    {
        this.kontenery.Remove(kontener);
    }
    
    
}