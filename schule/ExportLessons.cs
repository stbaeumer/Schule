using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class ExpLe : List<ExportLesson>
{
    public ExpLe(string dateiPfad)
    {
        DateiPfad = dateiPfad;
    }

    public ExpLe(string dateiName, string dateiendung = "*.dat", string delimiter = "|")
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
            csv.Context.RegisterClassMap<ExportLessonsMap>();
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            var records = csv.GetRecords<ExportLesson>();
            this.AddRange(records);
        }
        Global.ZeileSchreiben(0, DateiPfad, this.Count().ToString(), null);
    }

    public string DateiPfad { get; }
    public string[] Hinweise { get; }

    internal ExpLe Interessierende(List<string> interessierendeKlassen)
    {
        var x = this.Where(x => interessierendeKlassen.Any(k => x.Klassen.Split('~').Contains(k))).ToList();
        var xx = new ExpLe(this.DateiPfad);
        xx.AddRange(x);
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
