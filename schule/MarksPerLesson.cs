using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Global;

public class MarksPerLessons : List<MarkPerLesson>
{
    public MarksPerLessons()
    {
    }

    public MarksPerLessons(string dateiPfad, string dateiendungen, string delimiter)
    {
        var hinweise = new string[] {
                "Exportieren Sie die Datei aus SchILD, indem Sie:",
                "In SchILD den Pfad gehen: Datenaustausch > Schnittstelle > Export",
                "Die Datei auswählen.",
                "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() };

        List<object> result = Global.LiesDateien(dateiPfad, dateiendungen, hinweise, delimiter);

        foreach (var r in result)
        {
            MarkPerLesson s = (MarkPerLesson)r;
            this.Add(s);
        }
    }

    internal MarksPerLessons Interessierende(List<string> interesserendeKlassen)
    {
        var x = this.Where(x => interesserendeKlassen.Contains(x.klasse)).ToList();

        var xx = new MarksPerLessons();
        xx.AddRange(x);
        Global.ZeileSchreiben(0, "interessierende MarksPerLessons", x.Count().ToString(), null, null);
        return xx;
    }
}

public class MarksPerLessonMap : ClassMap<MarkPerLesson>
{
    public MarksPerLessonMap()
    {
        GlobalCsvMappings.AddMappings(this);
    }
}