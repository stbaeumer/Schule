﻿using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class AbsSt : List<AbsencePerStudent>
{
    public AbsSt()
    {
    }

    public AbsSt(string dateiName, string dateiendung = "*.dat", string delimiter = "|")
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
            csv.Context.RegisterClassMap<AbsencePerStudentsMap>();
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            var records = csv.GetRecords<AbsencePerStudent>();
            this.AddRange(records);
        }
        Global.Ausgaben.Add(new Ausgabe(0, dateiPfad, this.Count().ToString()));
    }

    internal AbsSt Interessierende(List<string> interessierendeKlassen)
    {
        var x = this.Where(x => interessierendeKlassen.Contains(x.Klasse)).ToList();
        var xx = new AbsSt();
        xx.AddRange(x);
        Global.ZeileSchreiben(0, "interessierende SchuelerBasisdaten", x.Count().ToString(), null, null);
        return xx;
    }
}

public class AbsencePerStudentsMap : ClassMap<AbsencePerStudent>
{
    public AbsencePerStudentsMap()
    {
        Map(m => m.Name).Name("Schüler*innen");
        Map(m => m.ExterneId).Name("Externe Id");
        Map(m => m.Text).Name("Text");
        Map(m => m.Klasse).Name("Klasse");
        Map(m => m.Datum).Name("Datum");
        Map(m => m.Wochentag).Name("Wochentag");
        Map(m => m.Fehlstd).Name("Fehlstd.");
        Map(m => m.Fehlmin).Name("Fehlmin.");
        Map(m => m.Abwesenheitsgrund).Name("Abwesenheitsgrund");
        Map(m => m.ENr).Name("ENr");
        Map(m => m.Erledigt).Name("Erledigt");
        Map(m => m.AbwesenheitZaehlt).Name("Abwesenheit zählt");
        Map(m => m.Entschuldigungstext).Name("Entschuldigungstext");
        Map(m => m.Status).Name("Status");
        Map(m => m.Fehltage).Name("Fehltage");
    }
}