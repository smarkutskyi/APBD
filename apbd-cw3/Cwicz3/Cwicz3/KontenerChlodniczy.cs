namespace Cwicz3;

public class KontenerChlodniczy : Kontener
{
    
    private string _rodzajProduktu { get;}
    
    private double _temperatura { get;}
    
    private Dictionary<string, double> listaLadunkow = new Dictionary<string, double >
    {
        {"Bananas", 13.3}, {"Chocolate", 18}, {"Fish", 2}, {"Meat", -15}, {"Ice cream", -18}, {"Frozen pizza", -30},
        {"Cheese", 7.2}, {"Sausages", 5}, {"Butter", 20.5}, {"Eggs", 19}
        
    };   
    
    
    
    public KontenerChlodniczy(double masaLadunku, double wysokosc, double wagaWlasna, double glebokosc, 
                                double maksymalnaLadownosc, string rodzajProduktu, double temperatura) 
        : base(masaLadunku, wysokosc, wagaWlasna, glebokosc, maksymalnaLadownosc)
    {
        if (!listaLadunkow.ContainsKey(rodzajProduktu))
        {
            throw new Exception("Nie ma takiego towaru na liście");
            
        }

        if (temperatura < listaLadunkow[rodzajProduktu] )
        {
            throw new Exception("Bardzi niska temperatura");
        }
            
        this._rodzajProduktu = rodzajProduktu;
        this._temperatura = temperatura;
    }

    public override string pobieramyTyp()
    {
        return "C";
    }
    
    
}