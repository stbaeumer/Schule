using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class Adres : List<Adresse>
{
    public Adres(string dateiPfad)
    {
        DateiPfad = dateiPfad;
    }

    public Adres(string dateiName, string dateiendung = "*.dat", string delimiter = "|")
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
            csv.Context.RegisterClassMap<AdressenMap>();
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            var records = csv.GetRecords<Adresse>();
            this.AddRange(records);
        }
        Global.ZeileSchreiben(0, DateiPfad, this.Count().ToString(), null);
    }

    public string DateiPfad { get; }
    public string[] Hinweise { get; }

    internal Adres Interessierende(Schülers interessierendeSuS)
    {
        var schuelerAdressen = new Adres(this.DateiPfad);
        var x = this.Where(t => interessierendeSuS.Any(s => t.Name1 == s.Nachname && t.Name2 == s.Vorname)).ToList();
        schuelerAdressen.AddRange(x);
        return schuelerAdressen;
    }
}

public class AdressenMap : ClassMap<Adresse>
{
    public AdressenMap()
    {
        Map(m => m.AdressArt).Name("Adress-Art");
        Map(m => m.Name1).Name("Name1");
        Map(m => m.Name2).Name("Name2");
        Map(m => m.Straße).Name("Straße");
        Map(m => m.PLZ).Name("PLZ");
        Map(m => m.Ort).Name("Ort");
        Map(m => m.Telefonnummer1).Name("Telefonnr. 1");
        Map(m => m.Telefonnummer2).Name("Telefonnr. 2");
        Map(m => m.Fax).Name("Fax");
        Map(m => m.Email).Name("E-Mail");
        Map(m => m.Branche).Name("Branche");
        Map(m => m.Bemerkungen).Name("Bemerkungen");
        Map(m => m.SchILDID).Name("SchILD-ID");
        Map(m => m.ExterneID).Name("Externe ID");        
    }
}