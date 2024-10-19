using CsvHelper;
using CsvHelper.Configuration;
using DocumentFormat.OpenXml.Math;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class SchAd : List<SchuelerAdresse>
{
    public SchAd()
    {
    }

    public SchAd(string dateiName, string dateiendung = "*.dat", string delimiter = "|")
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

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
            HasHeaderRecord = true,
            Delimiter = delimiter
        };

        using (var reader = new StreamReader(dateiPfad))
        using (var csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<SchuelerAdressenMap>();
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            var records = csv.GetRecords<SchuelerAdresse>();
            this.AddRange(records);
        }

        Global.Ausgaben.Add(new Ausgabe(0, dateiPfad, this.Count().ToString()));
    }

    internal SchAd Interessierende(Schülers interessierendeSuS)
    {
        var schuelerAdressen = new SchAd();

        var x = this.Where(t => interessierendeSuS.Any(s => t.Nachname == s.Nachname && t.Vorname == t.Vorname && t.Geburtsdatum == t.Geburtsdatum)).ToList();

        schuelerAdressen.AddRange(x);

        Global.ZeileSchreiben(0, "interessierende SchuelerAdressen", x.Count().ToString(), null, null);
        return schuelerAdressen;
    }
}

public class SchuelerAdressenMap : ClassMap<SchuelerAdresse>
{
    public SchuelerAdressenMap()
    {   
        Map(m => m.Nachname).Name("Nachname");
        Map(m => m.Vorname).Name("Vorname");
        Map(m => m.Geburtsdatum).Name("Geburtsdatum");
        Map(m => m.Adressart).Name("Adressart");
        Map(m => m.Name1).Name("Name1");
        Map(m => m.Name2).Name("Name2");
        Map(m => m.Straße).Name("Straße");
        Map(m => m.PLZ).Name("PLZ");
        Map(m => m.Ort).Name("Ort");
        Map(m => m.Telefonnummer1).Name("1. Tel.-Nr.");
        Map(m => m.Telefonnummer2).Name("2. Tel.-Nr.");
        Map(m => m.Email).Name("E-Mail");
        Map(m => m.BetreuerNachname).Name("Betreuer Nachname");
        Map(m => m.BetreuerVorname).Name("Betreuer Vorname");
        Map(m => m.BetreuerAnrede).Name("Betreuer Anrede");
        Map(m => m.BetreuerTelefonnummer).Name("Betreuer Tel.-Nr.");
        Map(m => m.BetreuerEmail).Name("Betreuer E-Mail");
        Map(m => m.BetreuerAbteilung).Name("Betreuer Abteilung");
        Map(m => m.Vertragsbeginn).Name("Vertragsbeginn");
        Map(m => m.Vertragsende).Name("Vertragsende");
    }
}
