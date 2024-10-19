using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class ExpLe : List<ExportLesson>
{
    public ExpLe()
    {
    }

    public ExpLe(string dateiName, string dateiendung = "*.dat", string delimiter = "|")
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
            csv.Context.RegisterClassMap<ExportLessonsMap>();
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            var records = csv.GetRecords<ExportLesson>();
            this.AddRange(records);
        }
        Global.Ausgaben.Add(new Ausgabe(0, dateiPfad, this.Count().ToString()));
    }

    internal ExpLe Interessierende(List<string> interessierendeKlassen)
    {
        var x = this.Where(x => interessierendeKlassen.Any(k => x.Klassen.Split('~').Contains(k))).ToList();
        var xx = new ExpLe();
        xx.AddRange(x);
        Global.ZeileSchreiben(0, "interessierende ", x.Count().ToString(), null, null);
        return xx;
    }
}

public class ExportLessonsMap : ClassMap<ExportLesson>
{
    public ExportLessonsMap()
    {
        Map(m => m.LessonId).Name("lessonId");
        Map(m => m.LessonNumber).Name("lessonNumber");
        Map(m => m.Subject).Name("subject");
        Map(m => m.Teacher).Name("teacher");
        Map(m => m.Klassen).Name("klassen");
        Map(m => m.Studentgroup).Name("studentgroup");
        Map(m => m.Periods).Name("periods");
        Map(m => m.StartDate).Name("startDate");
        Map(m => m.EndDate).Name("endDate");
        Map(m => m.Room).Name("room");
        Map(m => m.ForeignKey).Name("foreignKey");
    }
}
