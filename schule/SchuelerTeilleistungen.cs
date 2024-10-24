using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class ScTei : List<SchuelerTeilleistung>
{
    public ScTei(string dateiName, string dateiendung = "*.dat", string delimiter = "|")
    {
        DateiPfad = Global.CheckFile(dateiName, dateiendung);

        Hinweise = new string[] {
                "Exportieren Sie die Datei aus SchILD, indem Sie:",
                "In SchILD den Pfad gehen: Datenaustausch > Schnittstelle > Export",
                "Die Datei auswählen.",
                "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory()};

        if (DateiPfad == null) { return; }

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
            csv.Context.RegisterClassMap<SchuelerTeilleistungenMap>();
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            var records = csv.GetRecords<SchuelerTeilleistung>();
            this.AddRange(records);
        }
        Global.ZeileSchreiben(0, DateiPfad, this.Count().ToString(), null);
    }

    public string DateiPfad { get; private set; }
    public string[] Hinweise { get; }
}

public class SchuelerTeilleistungenMap : ClassMap<SchuelerTeilleistung>
{
    public SchuelerTeilleistungenMap()
    {
        Map(m => m.Nachname).Name("Nachname");
        Map(m => m.Vorname).Name("Vorname");
        Map(m => m.Geburtsdatum).Name("Geburtsdatum");
        Map(m => m.Jahr).Name("Jahr");
        Map(m => m.Abschnitt).Name("Abschnitt");
        Map(m => m.Fach).Name("Fach");
        Map(m => m.Datum).Name("Datum");
        Map(m => m.Teilleistung).Name("Teilleistung");
        Map(m => m.Note).Name("Note");
        Map(m => m.Bemerkung).Name("Bemerkung");
        Map(m => m.Lehrkraft).Name("Lehrkraft");
    }
}
