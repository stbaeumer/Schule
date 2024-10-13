
public class Zeile
{
    public bool IstKopfzeile { get; set; }
    public new List<string> Zellen { get; set; }

    public Zeile(List<string> zellen, bool istKopfzeile = false)
    {
        Zellen = zellen;
        IstKopfzeile = istKopfzeile;
    }

    public Zeile()
    {
        Zellen = new List<string>();
        IstKopfzeile = false;
    }
}