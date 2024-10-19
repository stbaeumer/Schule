using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class Studs : List<Student>
{
    public Studs()
    {
    }

    public Studs(string dateiName, string dateiendung = "*.dat", string delimiter = "|")
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
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            csv.Context.RegisterClassMap<StudentsMap>();
            var records = csv.GetRecords<Student>();
            this.AddRange(records);
        }
        Global.Ausgaben.Add(new Ausgabe(0, dateiPfad, this.Count().ToString()));
    }

    internal Studs Interessierende(List<string> interesserendeKlassen)
    {
        var x = this.Where(x => interesserendeKlassen.Any(k => x.KlasseName.Split('~').Contains(k))).ToList();

        var xx = new Studs();
        xx.AddRange(x);

        Global.ZeileSchreiben(0, "interessierende Students", x.Count().ToString(), null, null);
        return xx;
    }
}

public class StudentsMap : ClassMap<Student>
{
    public StudentsMap()
    {
        Map(m => m.Name).Name("name");
        Map(m => m.LongName).Name("longName");
        Map(m => m.ForeName).Name("foreName");
        Map(m => m.Gender).Name("gender");
        Map(m => m.BirthDate).Name("birthDate");
        Map(m => m.KlasseName).Name("klasse.name");
        Map(m => m.EntryDate).Name("entryDate");
        Map(m => m.ExitDate).Name("exitDate");
        Map(m => m.Text).Name("text");
        Map(m => m.Id).Name("id");
        Map(m => m.ExternKey).Name("externKey");
        Map(m => m.MedicalReportDuty).Name("medicalReportDuty");
        Map(m => m.Schulpflicht).Name("schulpflicht");
        Map(m => m.Majority).Name("majority");
        Map(m => m.AddressEmail).Name("address.email");
        Map(m => m.AddressMobile).Name("address.mobile");
        Map(m => m.AddressPhone).Name("address.phone");
        Map(m => m.AddressCity).Name("address.city");
        Map(m => m.AddressPostCode).Name("address.postCode");
        Map(m => m.AddressStreet).Name("address.street");
    }
}
