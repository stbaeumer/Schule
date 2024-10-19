using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class StgrS : List<StudentgroupStudent>
{
    public StgrS()
    {
    }

    public StgrS(string dateiName, string dateiendung = "*.dat", string delimiter = "|")
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
            csv.Context.RegisterClassMap<StudentgroupStudentsMap>();
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            var records = csv.GetRecords<StudentgroupStudent>();
            this.AddRange(records);
        }
        Global.Ausgaben.Add(new Ausgabe(0, dateiPfad, this.Count().ToString()));
    }

    internal StgrS Interessierende(List<string> interessierendeKlassen)
    {
        var x = this.Where(x => interessierendeKlassen.Any(k => x.StudentgroupName.Split('~').Contains(k))).ToList();
        var xx = new StgrS();
        xx.AddRange(x);
        Global.ZeileSchreiben(0, "interessierende StudentgroupStudents", x.Count().ToString(), null, null);
        return xx;
    }
}

public class StudentgroupStudentsMap : ClassMap<StudentgroupStudent>
{
    public StudentgroupStudentsMap()
    {
        Map(m => m.StudentId).Name("studentId");
        Map(m => m.Name).Name("name");
        Map(m => m.Forename).Name("forename");
        Map(m => m.StudentgroupName).Name("studentgroup.name");
        Map(m => m.Subject).Name("subject");
        Map(m => m.StartDate).Name("startDate");
        Map(m => m.EndDate).Name("endDate");
    }
}
