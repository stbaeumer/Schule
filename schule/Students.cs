using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Global;

public class Students : List<Student>
{
    public Students()
    {
    }

    public Students(string dateiPfad, string dateiendungen, string delimiter)
    {
        var hinweise = new string[] {
                "Exportieren Sie die Datei aus SchILD, indem Sie:",
                "In SchILD den Pfad gehen: Datenaustausch > Schnittstelle > Export",
                "Die Datei auswählen.",
                "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() };

        List<object> result = Global.LiesDateien(dateiPfad, dateiendungen, hinweise, delimiter);

        foreach (var r in result)
        {
            Student s = (Student)r;
            this.Add(s);
        }
    }
}

public class StudentsMap : ClassMap<Student>
{
    public StudentsMap()
    {
        GlobalCsvMappings.AddMappings(this);
    }
}