using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class AbsSt : List<AbsencePerStudent>
{
    public string DateiPfad { get; private set; }
    public string[] Hinweise { get; }

    public AbsSt(string dateiPfad)
    {
        DateiPfad = dateiPfad;
    }

    public AbsSt(string dateiName, string dateiendung = "*.dat", string delimiter = "|")
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
            csv.Context.RegisterClassMap<AbsencePerStudentsMap>();
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            var records = csv.GetRecords<AbsencePerStudent>();
            this.AddRange(records);
        }
        Global.ZeileSchreiben(0, DateiPfad, this.Count().ToString(), null);
    }

    internal AbsSt Interessierende(List<string> interessierendeKlassen)
    {
        var x = this.Where(x => interessierendeKlassen.Contains(x.Klasse)).ToList();
        var xx = new AbsSt(this.DateiPfad);
        xx.AddRange(x);
        return xx;
    }
}

public class AbsencePerStudentsMap : ClassMap<AbsencePerStudent>
{
    public AbsencePerStudentsMap()
    {
        Map(m => m.Name).Name("Schüler*innen");
        Map(m => m.ExterneId).Name("Externe Id");
        Map(m => m.Text).Name("Text");
        Map(m => m.Klasse).Name("Klasse");
        Map(m => m.Datum).Name("Datum");
        Map(m => m.Wochentag).Name("Wochentag");
        Map(m => m.Fehlstd).Name("Fehlstd.");
        Map(m => m.Fehlmin).Name("Fehlmin.");
        Map(m => m.Abwesenheitsgrund).Name("Abwesenheitsgrund");
        Map(m => m.ENr).Name("ENr");
        Map(m => m.Erledigt).Name("Erledigt");
        Map(m => m.AbwesenheitZaehlt).Name("Abwesenheit zählt");
        Map(m => m.Entschuldigungstext).Name("Entschuldigungstext");
        Map(m => m.Status).Name("Status");
        Map(m => m.Fehltage).Name("Fehltage");
    }
}