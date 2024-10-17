using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class SchuelerTeilleistungen : List<SchuelerTeilleistung>
{
    public SchuelerTeilleistungen()
    {
    }

    public SchuelerTeilleistungen(string dateiPfad, string dateiendungen)
    {
        var hinweise = new string[] {
                "Exportieren Sie die Datei aus SchILD, indem Sie:",
                "In SchILD den Pfad gehen: Datenaustausch > Schnittstelle > Export",
                "Die Datei auswählen.",
                "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() };

        List<object> result = Global.LiesDateien(dateiPfad, dateiendungen, hinweise);

        foreach (var r in result)
        {
            SchuelerTeilleistung s = (SchuelerTeilleistung)r;
            this.Add(s);
        }
    }

    internal Menüeintrag Menüeintrag(int countSbd, int count)
    {
        if (countSbd > 0 && count > 0)
        {
            return new Menüeintrag(
                "SchuelerTeilleitungen.dat aus Webuntis-Dateien erzeugen",
                @"ImportFürSchild\SchuelerTeilleistungen.dat",
                @"Beschreibung: Enthält die Teilleistungsdaten (Teilleistung und Noten) eines Lernabschnittes (=Halbjahr oder Quartal) der Schüler. ",
                new List<string> { "Nachname", "Vorname", "Geburtsdatum", "Jahr", "Abschnitt", "Fach", "Datum", "Teilleistung", "Note", "Bemerkung", "Lehrkraft" });
        }
        return null;
    }
}