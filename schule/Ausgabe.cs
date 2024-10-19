public class Ausgabe
{
    public Ausgabe(int abstandLinks, string links, string rechts)
    {
        AbstandLinks = abstandLinks;
        Links = links;
        Rechts = rechts;
        Global.ZeileSchreiben(abstandLinks, links, rechts, null, null);
    }

    public int AbstandLinks { get; private set; }
    public string Links { get; private set; }
    public string Rechts { get; private set; }
}