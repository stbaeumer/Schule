
public class Zeile : List<string>
{ 
    public Zeile(List<string> zellen, bool istKopfzeile = false)
    {
        // Durchlaufe alle Zellen. 

        // Wenn ein Zelleninhalt aus 7 oder 8 Ziffern besteht

        // und wenn die letzten vier Ziffern zusammen eine Zahl zwischen 1950 und dem heutigen Jahr bilden



        
    }

    public Zeile(List<string> zellen)
    {
        this.AddRange(zellen);
    }

    public Zeile()
    {
        
    }
}