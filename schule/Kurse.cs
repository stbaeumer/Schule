﻿using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class Kurse : List<Kurs>
{
    public Kurse(string dateiName, string dateiendung = "*.dat", string delimiter = "|")
    {
        DateiPfad = Global.CheckFile(dateiName, dateiendung);

        Hinweise = new string[] {
                "Exportieren Sie die Datei aus SchILD, indem Sie:",
                "In SchILD den Pfad gehen: Datenaustausch > Schnittstelle > Export",
                "Die Datei auswählen.",
                "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory()};

        if (DateiPfad == null) { return; }

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
            csv.Context.RegisterClassMap<KurseMap>();
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            var records = csv.GetRecords<Kurs>();
            this.AddRange(records);
        }
        Global.ZeileSchreiben(0, DateiPfad, this.Count().ToString(), null);
    }

    public string DateiPfad { get; }
    public string[] Hinweise { get; }
}

public class KurseMap : ClassMap<Kurs>
{
    public KurseMap()
    {
        Map(m => m.KursBez).Name("KursBez");
        Map(m => m.Klasse).Name("Klasse");
        Map(m => m.Jahr).Name("Jahr");
        Map(m => m.Abschnitt).Name("Abschnitt");
        Map(m => m.Jahrgang).Name("Jahrgang");
        Map(m => m.Fach).Name("Fach");
        Map(m => m.Kursart).Name("Kursart");
        Map(m => m.Wochenstd).Name("Wochenstd.");
        Map(m => m.WochenstdKL).Name("Wochenstd. KL");
        Map(m => m.Kursleiter).Name("Kursleiter");
        Map(m => m.Epochenunterricht).Name("Epochenunterricht");
        Map(m => m.Schulnr).Name("Schulnr");
    }
}