using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class ScLei : List<SchuelerLeistungsdatum>
{
    public ScLei(string dateiName, string dateiendung = "*.dat", string delimiter = "|")
    {
        DateiPfad = Global.CheckFile(dateiName, dateiendung);

        Hinweise = new string[] {
                "Exportieren Sie die Datei aus SchILD, indem Sie:",
                "In SchILD den Pfad gehen: Datenaustausch > Schnittstelle > Export",
                "Die Datei auswählen.",
                "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory()};

        if (DateiPfad == null){ return; }

        // Konfiguration für CsvReader: Header und Delimiter anpassen
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
            HasHeaderRecord = true,   // Gibt an, dass die CSV-Datei eine Kopfzeile hat
            Delimiter = delimiter
        };

        using (var reader = new StreamReader(DateiPfad))
        using (var csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<SchuelerLeistungsdatenMap>();
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            var records = csv.GetRecords<SchuelerLeistungsdatum>();
            this.AddRange(records);
        }
        Global.ZeileSchreiben(0, DateiPfad, this.Count().ToString(), null);
    }

    public string DateiPfad { get; private set; }
    public string[] Hinweise { get; }
}

public class SchuelerLeistungsdatenMap : ClassMap<SchuelerLeistungsdatum>
{
    public SchuelerLeistungsdatenMap()
    {
        Map(m => m.Nachname).Name("Nachname");
        Map(m => m.Vorname).Name("Vorname");
        Map(m => m.Geburtsdatum).Name("Geburtsdatum");
        Map(m => m.Jahr).Name("Jahr");
        Map(m => m.Abschnitt).Name("Abschnitt");
        Map(m => m.Fach).Name("Fach");
        Map(m => m.Fachlehrer).Name("Fachlehrer");
        Map(m => m.Kursart).Name("Kursart");
        Map(m => m.Kurs).Name("Kurs");
        Map(m => m.Note).Name("Note");
        Map(m => m.Abiturfach).Name("Abiturfach");
        Map(m => m.Wochenstd).Name("Wochenstd.");
        Map(m => m.ExterneSchulnr).Name("Externe Schulnr.");
        Map(m => m.Zusatzkraft).Name("Zusatzkraft");
        Map(m => m.WochenstdZK).Name("Wochenstd. ZK");
        Map(m => m.Jahrgang).Name("Jahrgang");
        Map(m => m.Jahrgänge).Name("Jahrgänge");
        Map(m => m.Fehlstd).Name("Fehlstd.");
        Map(m => m.UnentschFehlstd).Name("unentsch. Fehlstd.");
    }
}