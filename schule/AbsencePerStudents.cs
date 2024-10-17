using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Global;

public class AbsencePerStudents : List<AbsencePerStudent>
{
    public AbsencePerStudents()
    {
    }

    public AbsencePerStudents(string dateiPfad, string dateiendungen, string delimiter)
    {
        var hinweise = new string[] {
                "Exportieren Sie die Datei aus SchILD, indem Sie:",
                "In SchILD den Pfad gehen: Datenaustausch > Schnittstelle > Export",
                "Die Datei auswählen.",
                "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() };

        List<object> result = Global.LiesDateien(dateiPfad, dateiendungen, hinweise, delimiter);

        foreach (var r in result)
        {
            AbsencePerStudent s = (AbsencePerStudent)r;
            this.Add(s);
        }
    }

    internal AbsencePerStudents Interessierende(List<string> interesserendeKlassen)
    {   
        var x = this.Where(x => interesserendeKlassen.Any(k => x.Klasse.Split('~').Contains(k))).ToList();

        var xx = new AbsencePerStudents();
        xx.AddRange(x);

        Global.ZeileSchreiben(0, "interessierende AbsencePerStudents", x.Count().ToString(), null, null);
        return xx;
    }
}

public class AbsencePerStudentsMap : ClassMap<AbsencePerStudent>
{
    public AbsencePerStudentsMap()
    {
        GlobalCsvMappings.AddMappings(this);
    }
}