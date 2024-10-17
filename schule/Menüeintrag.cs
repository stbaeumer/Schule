public class Menüeintrag
{
    public string Titel { get; set; }
    public string Funktionsname { get; set; }
    public string DateiPfad { get; private set; }
    public string Beschreibung { get; set; }
    public List<string> Kopfzeile { get; private set; }
    public object Objekt { get; set; }


    public Menüeintrag(string titel, string dateiPfad, string beschreibung, List<string> kopfzeile)
    {
        Titel = titel;
        DateiPfad = dateiPfad;
        Beschreibung = beschreibung;
        Kopfzeile = kopfzeile;
    }

    public Menüeintrag(string titel, string funktionsname, string dateiPfad, string beschreibung, List<string> kopfzeile, object objekt)
    {
        Titel = titel;
        Funktionsname = funktionsname;
        DateiPfad = dateiPfad;
        Beschreibung = beschreibung;
        Kopfzeile = kopfzeile;
        Objekt = objekt;        
    }
}