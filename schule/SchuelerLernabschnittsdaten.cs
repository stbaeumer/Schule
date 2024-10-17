using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class SchuelerLernabschnittsdaten : List<SchuelerLernabschnittsdatum>
{
    public SchuelerLernabschnittsdaten()
    {
    }

    public SchuelerLernabschnittsdaten(string dateiPfad, string dateiendungen)
    {
        var hinweise = new string[] {
                "Exportieren Sie die Datei aus SchILD, indem Sie:",
                "In SchILD den Pfad gehen: Datenaustausch > Schnittstelle > Export",
                "Die Datei auswählen.",
                "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() };

        List<object> result = Global.LiesDateien(dateiPfad, dateiendungen, hinweise);

        foreach (var r in result)
        {
            SchuelerLernabschnittsdatum s = (SchuelerLernabschnittsdatum)r;
            this.Add(s);
        }
    }
}