﻿using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class Marks : List<MarkPerLesson>
{
    public Marks()
    {
    }

    public Marks(string dateiName, string dateiendung = "*.dat", string delimiter = "|")
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
            csv.Context.RegisterClassMap<MarksPerLessonsMap>();
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            var records = csv.GetRecords<MarkPerLesson>();
            this.AddRange(records);
        }
        Global.Ausgaben.Add(new Ausgabe(0, dateiPfad, this.Count().ToString()));
    }

    internal Marks Interessierende(List<string> interessierendeKlassen)
    {
        var x = this.Where(x => interessierendeKlassen.Contains(x.Klasse)).ToList();
        var xx = new Marks();
        xx.AddRange(x);
        Global.ZeileSchreiben(0, "interessierende SchuelerBasisdaten", x.Count().ToString(), null, null);
        return xx;
    }
}

public class MarksPerLessonsMap : ClassMap<MarkPerLesson>
{
    public MarksPerLessonsMap()
    {
        Map(m => m.Datum).Name("Datum");
        Map(m => m.Name).Name("Name");
        Map(m => m.Klasse).Name("Klasse");
        Map(m => m.Fach).Name("Fach");
        Map(m => m.Prüfungsart).Name("Prüfungsart");
        Map(m => m.Note).Name("Note");
        Map(m => m.Bemerkung).Name("Bemerkung");
        Map(m => m.Benutzer).Name("Benutzer");
        Map(m => m.SchlüsselExtern).Name("Schlüssel (extern)");
        Map(m => m.Gesamtnote).Name("Gesamtnote");
    }
}
