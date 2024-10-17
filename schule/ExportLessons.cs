using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Global;

public class ExportLessons : List<ExportLesson>
{
    public ExportLessons()
    {
    }

    public ExportLessons(string dateiPfad, string dateiendungen, string delimiter)
    {
        var hinweise = new string[] {
                "Exportieren Sie die Datei aus SchILD, indem Sie:",
                "In SchILD den Pfad gehen: Datenaustausch > Schnittstelle > Export",
                "Die Datei auswählen.",
                "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() };

        List<object> result = Global.LiesDateien(dateiPfad, dateiendungen, hinweise, delimiter);

        foreach (var r in result)
        {
            ExportLesson s = (ExportLesson)r;
            this.Add(s);
        }
    }

    internal ExportLessons Interessierende(List<string> interesserendeKlassen)
    {
        var x = this.Where(x => interesserendeKlassen.Any(k => x.klassen.Split('~').Contains(k))).ToList();

        var ex = new ExportLessons();
        ex.AddRange(x);
        
        Global.ZeileSchreiben(0, "interessierende ExportLessons", x.Count().ToString(), null, null);
        return ex;
    }
}


public class ExportLessonsMap : ClassMap<ExportLesson>
{
    public ExportLessonsMap()
    {
        GlobalCsvMappings.AddMappings(this);
    }
}