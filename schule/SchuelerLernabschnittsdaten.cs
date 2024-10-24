using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class ScLer : List<SchuelerLernabschnittsdatum>
{
    public ScLer(string dateiName, string dateiendung = "*.dat", string delimiter = "|")
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
            csv.Context.RegisterClassMap<SchuelerLernabschnittsdatenMap>();
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            var records = csv.GetRecords<SchuelerLernabschnittsdatum>();
            this.AddRange(records);
        }
        Global.ZeileSchreiben(0, DateiPfad, this.Count().ToString(), null);
    }

    public string DateiPfad { get; private set; }
    public string[] Hinweise { get; }
}

public class SchuelerLernabschnittsdatenMap : ClassMap<SchuelerLernabschnittsdatum>
{
    public SchuelerLernabschnittsdatenMap()
    {
        Map(m => m.Nachname).Name("Nachname");
        Map(m => m.Vorname).Name("Vorname");
        Map(m => m.Geburtsdatum).Name("Geburtsdatum");
        Map(m => m.Jahr).Name("Jahr");
        Map(m => m.Abschnitt).Name("Abschnitt");
        Map(m => m.Jahrgang).Name("Jahrgang");
        Map(m => m.Klasse).Name("Klasse");
        Map(m => m.Schulgliederung).Name("Schulgliederung");
        Map(m => m.OrgForm).Name("OrgForm");
        Map(m => m.Klassenart).Name("Klassenart");
        Map(m => m.Fachklasse).Name("Fachklasse");
        Map(m => m.Förderschwerpunkt).Name("Förderschwerpunkt");
        Map(m => m.Förderschwerpunkt2).Name("2. Förderschwerpunkt");
        Map(m => m.Schwerstbehinderung).Name("Schwerstbehinderung");
        Map(m => m.Wertung).Name("Wertung");
        Map(m => m.Wiederholung).Name("Wiederholung");
        Map(m => m.Klassenlehrer).Name("Klassenlehrer");
        Map(m => m.Versetzung).Name("Versetzung");
        Map(m => m.Abschluss).Name("Abschluss");
        Map(m => m.Schwerpunkt).Name("Schwerpunkt");
        Map(m => m.Konferenzdatum).Name("Konferenzdatum");
        Map(m => m.Zeugnisdatum).Name("Zeugnisdatum");
        Map(m => m.SummeFehlstd).Name("SummeFehlstd");
        Map(m => m.SummeFehlstdUnentschuldigt).Name("SummeFehlstd_unentschuldigt");
        Map(m => m.AllgBildenderAbschluss).Name("allg.-bildender Abschluss");
        Map(m => m.BerufsbezAbschluss).Name("berufsbez. Abschluss");
        Map(m => m.Zeugnisart).Name("Zeugnisart");
        Map(m => m.FehlstundenGrenzwert).Name("Fehlstunden-Grenzwert");
    }
}