using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Global;

public class StudentgroupStudents : List<StudentgroupStudent>
{
    public StudentgroupStudents()
    {
    }

    public StudentgroupStudents(string dateiPfad, string dateiendungen, string delimiter)
    {
        var hinweise = new string[] {
                "Exportieren Sie die Datei aus SchILD, indem Sie:",
                "In SchILD den Pfad gehen: Datenaustausch > Schnittstelle > Export",
                "Die Datei auswählen.",
                "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() };

        List<object> result = Global.LiesDateien(dateiPfad, dateiendungen, hinweise, delimiter);

        foreach (var r in result)
        {
            StudentgroupStudent s = (StudentgroupStudent)r;
            this.Add(s);
        }
    }

    internal StudentgroupStudents Interessierende(List<string> interesserendeKlassen)
    {
        var x = this.Where(x => interesserendeKlassen.Any(k => x.studentgroupName.Split('~').Contains(k))).ToList();
        var xx = new StudentgroupStudents();
        xx.AddRange(x);

        
        Global.ZeileSchreiben(0, "interessierende StudentgroupStudents", x.Count().ToString(), null, null);
        return xx;
    }
}

public class StudentgroupStudentsMap : ClassMap<StudentgroupStudent>
{   public StudentgroupStudentsMap()
    {
        GlobalCsvMappings.AddMappings(this);
    }
}