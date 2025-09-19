
using Cwicz3;

public class Program
{
    
    public static void Main(string[] args)
    {
        var statek1 = new Kontenerowiec(20, 40, 9000);
        var statek2 = new Kontenerowiec(35, 70, 12000);

        var kontener1 = stworzKontenerPlyny(1200, 8, 4, 4, 1800, false);
        var kontener2 = stworzKontenerGaz(1400, 10, 5, 5, 2500, 8);
        var kontener3 = stworzKontenerChlodniczy(1600, 13, 6, 6, 3500, "Apples", 7);

        zaladujKontenerNaStatek(statek1, kontener1);
        zaladujKontenerNaStatek(statek2, kontener2);
        zaladujKontenerNaStatek(statek1, kontener3);
        
        wypiszInformacjeOStatku(statek1);
        
        
        przeniesKontener(kontener3, statek1, statek2);
        
        wypiszInformacjeOStatku(statek1);
        wypiszInformacjeOStatku(statek2);
        

        usunKontenerZeStatku(statek1, kontener1);
        
        wypiszInformacjeOStatku(statek1);
        
    }
    
    public static Kontener stworzKontenerPlyny(double masaLadunku, double wysokosc, double wagaWlasna, double glebokosc, double maksymalnaLadownosc, bool niebezpieczny)
    {
        Kontener kontener = new KontenerPlyny(masaLadunku, wysokosc, wagaWlasna, glebokosc, maksymalnaLadownosc, niebezpieczny);
        
        return kontener;
    }

    
    public static Kontener stworzKontenerGaz(double masaLadunku, double wysokosc, double wagaWlasna, double glebokosc, double maksymalnaLadownosc, double cisnienie)
    {
        Kontener kontener = new KontenerNaGaz(masaLadunku, wysokosc, wagaWlasna, glebokosc, maksymalnaLadownosc, cisnienie);
        
        return kontener;
    }

    
    public static Kontener stworzKontenerChlodniczy(double masaLadunku, double wysokosc, double wagaWlasna, double glebokosc, double maksymalnaLadownosc, string rodzajProduktu, double temperatura)
    {
        Kontener kontener = new KontenerChlodniczy(masaLadunku, wysokosc, wagaWlasna, glebokosc, maksymalnaLadownosc, rodzajProduktu, temperatura);
        
        return kontener;
    }
    public void zaladujLadunek(Kontener kontener, double masaLadunku)
    {
        kontener.zaladowanieLadunku(masaLadunku);
        Console.WriteLine($"Zaladowano {masaLadunku} kg ladunku do kontenera {kontener._numerSeryjny}");
    }

    

    
    public static void zaladujKontenerNaStatek(Kontenerowiec statek, Kontener kontener)
    {
        if (!statek.kontenery.Contains(kontener))
        {
            statek.dodajKontener(kontener);
        }
    }

    public static void usunKontenerZeStatku(Kontenerowiec statek, Kontener kontener)
    {
        if (statek.kontenery.Contains(kontener))
        {
            statek.usunKontener(kontener);
        }
    }

    public static void przeniesKontener(Kontener kontener, Kontenerowiec statek1, Kontenerowiec statek2)
    {
        if (statek1.kontenery.Contains(kontener))
        {
            statek1.usunKontener(kontener);
            statek2.dodajKontener(kontener);
        }
    }
    
    public static void wypiszInformacjeOStatku(Kontenerowiec statek)
    {
        Console.WriteLine($"Informacje o statku:");
        Console.WriteLine($"Maksymalnaprędkość: {statek.maksPredkosc} węzłów");
        Console.WriteLine($"Liczba kontenerow na statku: {statek.kontenery.Count}");
        Console.WriteLine($"Wagasumy kontenerów: {statek.kontenery} ton");
        Console.WriteLine($"Szczególy ladunku na statku:");

        foreach (var kontener in statek.kontenery)
        {
            Console.WriteLine($"---Kontener {kontener._numerSeryjny}  Typ: {kontener.pobieramyTyp()}  Masa: {kontener._masaLadunku} kg");
        }
    }
    
}

