
using Cwicz3;

public class Program
{
    
    public static void Main(string[] args)
    {
        
    }
    
    public Kontener StworzKontenerPlyny(double masaLadunku, double wysokosc, double wagaWlasna, double glebokosc, double maksymalnaLadownosc, bool niebezpieczny)
    {
        Kontener kontener = new KontenerPlyny(masaLadunku, wysokosc, wagaWlasna, glebokosc, maksymalnaLadownosc, niebezpieczny);
        
        return kontener;
    }

    
    public Kontener StworzKontenerGaz(double masaLadunku, double wysokosc, double wagaWlasna, double glebokosc, double maksymalnaLadownosc, double cisnienie)
    {
        Kontener kontener = new KontenerNaGaz(masaLadunku, wysokosc, wagaWlasna, glebokosc, maksymalnaLadownosc, cisnienie);
        
        return kontener;
    }

    
    public Kontener StworzKontenerChlodniczy(double masaLadunku, double wysokosc, double wagaWlasna, double glebokosc, double maksymalnaLadownosc, string rodzajProduktu, double temperatura)
    {
        Kontener kontener = new KontenerChlodniczy(masaLadunku, wysokosc, wagaWlasna, glebokosc, maksymalnaLadownosc, rodzajProduktu, temperatura);
        
        return kontener;
    }
    
}

