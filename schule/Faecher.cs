using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class Faech : List<Fach>
{
    public Faech(string dateiName, string dateiendung = "*.dat", string delimiter = "|")
    {
        var dateiPfad = Global.CheckFile(dateiName, dateiendung);

        if (dateiPfad == null)
        {
            var hinweise = new string[] {
                "Exportieren Sie die Datei aus SchILD, indem Sie:",
                "In SchILD den Pfad gehen: Datenaustausch > Schnittstelle > Export",
                "Die Datei auswählen.",
                "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory()
            };
            Global.ZeileSchreiben(0, dateiName, "keine Datei gefunden", new Exception("keine Datei gefunden"), hinweise);
            return;
        }

        // Konfiguration für CsvReader: Header und Delimiter anpassen
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
            HasHeaderRecord = true,   // Gibt an, dass die CSV-Datei eine Kopfzeile hat
            Delimiter = delimiter
        };

        using (var reader = new StreamReader(dateiPfad))
        using (var csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<FaecherMap>();
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            var records = csv.GetRecords<Fach>();
            this.AddRange(records);
        }
        Global.Ausgaben.Add(new Ausgabe(0, dateiPfad, this.Count().ToString()));
    }
}

public class FaecherMap : ClassMap<Fach>
{
    public FaecherMap()
    {
        Map(m => m.InternKrz).Name("InternKrz");
        Map(m => m.StatistikKrz).Name("StatistikKrz");
        Map(m => m.Bezeichnung).Name("Bezeichnung");
        Map(m => m.BezeichnungZeugnis).Name("BezeichnungZeugnis");
        Map(m => m.BezeichnungÜZeugnis).Name("BezeichnungÜZeugnis");
        Map(m => m.Unterrichtsprache).Name("Unterrichtsprache");
        Map(m => m.SortierungS1).Name("Sortierung S1");
        Map(m => m.SortierungS2).Name("Sortierung S2");
        Map(m => m.Gewichtung).Name("Gewichtung");
    }
}