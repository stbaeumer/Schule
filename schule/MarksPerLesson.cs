using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class Marks : List<MarkPerLesson>
{
    public Marks(string dateiPfad)
    {
        DateiPfad = dateiPfad;
    }

    public Marks(string dateiName, string dateiendung = "*.dat", string delimiter = "|")
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
            csv.Context.RegisterClassMap<MarksPerLessonsMap>();
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            var records = csv.GetRecords<MarkPerLesson>();
            this.AddRange(records);
        }
        Global.ZeileSchreiben(0, DateiPfad, this.Count().ToString(), null);
    }

    public string DateiPfad { get; private set; }
    public string[] Hinweise { get; }

    internal Marks Interessierende(List<string> interessierendeKlassen)
    {
        var x = this.Where(x => interessierendeKlassen.Contains(x.Klasse)).ToList();
        var xx = new Marks(this.DateiPfad);
        xx.AddRange(x);
        return xx;
    }
}

public class MarksPerLessonsMap : ClassMap<MarkPerLesson>
{
    public MarksPerLessonsMap()
    {
        Map(m => m.Datum).Name("Datum");
        Map(m => m.Name).Name("Name");
        Map(m => m.Klasse).Name("Klasse");
        Map(m => m.Fach).Name("Fach");
        Map(m => m.Prüfungsart).Name("Prüfungsart");
        Map(m => m.Note).Name("Note");
        Map(m => m.Bemerkung).Name("Bemerkung");
        Map(m => m.Benutzer).Name("Benutzer");
        Map(m => m.SchlüsselExtern).Name("Schlüssel (extern)");
        Map(m => m.Gesamtnote).Name("Gesamtnote");
    }
}
