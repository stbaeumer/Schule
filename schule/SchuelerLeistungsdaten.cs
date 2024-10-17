using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class SchuelerLeistungsdaten : List<SchuelerLeistungsdatum>
{
    public SchuelerLeistungsdaten(string dateiPfad, string dateiendungen)
    {
        var hinweise = new string[] {
                "Exportieren Sie die Datei aus SchILD, indem Sie:",
                "In SchILD den Pfad gehen: Datenaustausch > Schnittstelle > Export",
                "Die Datei auswählen.",
                "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() };

        List<object> result = Global.LiesDateien(dateiPfad, dateiendungen, hinweise);

        foreach (var r in result)
        {
            SchuelerLeistungsdatum s = (SchuelerLeistungsdatum)r;
            this.Add(s);
        }
    }

    internal Menüeintrag Menüeintrag(int countSbd, int count)
    {
        if (countSbd > 0 && count > 0)
        {
            return new Menüeintrag(
                "SchuelerLeistungsdaten.dat erzeugen aus Webuntis-Dateien erzeugen",
                @"ImportFürSchild\SchuelerLeistungsdaten.dat",
                @"Beschreibung: Enthält die Leistungsdaten (Fächer und Noten) eines Lernabschnittes (=Halbjahr oder Quartal) der Schüler.",
                new List<string> { "Nachname", "Vorname", "Geburtsdatum", "Jahr", "Abschnitt", "Fach", "Fachlehrer", "Kursart", "Kurs", "Note", "Abiturfach", "Wochenstd.", "Externe Schulnr.", "Zusatzkraft", "Wochenstd. ZK", "Jahrgang", "Jahrgänge", "Fehlstd.", "unentsch. Fehlstd." });
        }
        return null;
    } 
}