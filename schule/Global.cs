
using CsvHelper.Configuration;
using CsvHelper;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;

public static class Global
{
    public static string DateiPfad { get; private set; }
    public static Dateien Dateien { get; internal set; }
    public static List<Datei> DateienNurInteressierendeSuS { get; private set; }
    public static string InteressierenderFunktionsname { get; internal set; }
    public static string InteressierenderTitel { get; internal set; }
    public static string InteressierendeBeschreibung { get; internal set; }
    public static string Abschnitt { get; set; }
    public static List<int> AktSj { get; set; }
    public static IEnumerable<char> Delimiter { get; set; }
    public static List<string> ReferenzMerkmale { get; set; }
    public static Ausgaben Ausgaben { get; set; }

    internal static void DisplayHeader(string text = "h1", char unterstrich = '=')
    {
        if (text == "h1")
        {
            text = @"Schule.exe | https://github.com/stbaeumer/Schule | GPLv3 | Stefan Bäumer " + DateTime.Now.Year + @" | 23.10.2024";
        }
        if (unterstrich == '=')
        {
            Console.Clear();
        }
        Console.ForegroundColor = ConsoleColor.White;

        int platzVorher = (Console.WindowWidth - text.Length) / 2;

        Console.WriteLine(text.PadLeft(platzVorher + text.Length));
        if (unterstrich != ' ')
        {
            Console.WriteLine("".PadRight(Console.WindowWidth, unterstrich));
        }
    }

    internal static Datei GetDat(string dateiPfad)
    {
        return Global.Dateien
            .Where(d => d.DateiPfad != null)
            .FirstOrDefault(d => d.DateiPfad.ToLower()
            .Contains(dateiPfad.ToLower()));
    }

    internal static void ZeileSchreiben(int linkerAbstand, string linkeSeite, string rechteSeite, Exception? fehler, string[] hinweise = null)
    {
        var gesamtbreite = Console.WindowWidth;

        if (fehler != null)
        {
            rechteSeite = fehler.Message;
        }

        int punkte = gesamtbreite - linkerAbstand - linkeSeite.Length - rechteSeite.Length - 5;
        var mitte = " .".PadRight(Math.Max(3, punkte), '.') + " ";
        Console.WriteLine("".PadRight(linkerAbstand + 2) + linkeSeite + mitte + rechteSeite);


        if (hinweise != null)
        {
            int leftPad = 5;

            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < hinweise.Length; i++)
            {
                // Text in Wörter aufteilen
                string[] wörter = hinweise[i].Split(' ');
                StringBuilder aktuelleZeile = new StringBuilder();
                List<string> zeilen = new List<string>();

                foreach (var wort in wörter)
                {
                    if (aktuelleZeile.Length + wort.Length + 1 > gesamtbreite - leftPad)
                    {
                        zeilen.Add(aktuelleZeile.ToString());
                        aktuelleZeile.Clear();
                        //aktuelleZeile.Append(' ', leftPad); // Einrücken um den Wert leftPad
                    }

                    if (aktuelleZeile.Length > 0)
                    {
                        aktuelleZeile.Append(" ");
                    }
                    aktuelleZeile.Append(wort);
                }

                if (aktuelleZeile.Length > 0)
                {
                    zeilen.Add(aktuelleZeile.ToString());
                }

                // Zeilen ausgeben

                if (i == 0)
                {
                    for (int j = 0; j < zeilen.Count(); j++)
                    {
                        Console.WriteLine("".PadRight(leftPad - 2, ' ') + zeilen[j]);
                    }
                }
                else
                {
                    for (int j = 0; j < zeilen.Count(); j++)
                    {
                        if (j == 0)
                        {
                            Console.WriteLine(i.ToString().PadLeft(leftPad, ' ') + ". " + zeilen[j]);
                        }
                        else
                        {
                            Console.WriteLine("".PadLeft(leftPad, ' ') + "  " + zeilen[j]);
                        }

                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    internal static void DisplayCenteredBox(string text, int prozent)
    {
        // Breite der Konsole und die Box-Breite berechnen
        int consoleWidth = Console.WindowWidth;
        int boxWidth = consoleWidth * prozent / 100;

        // Text in Zeilen aufteilen, falls \n vorhanden ist
        string[] lines = text.Split('\n');

        var wrappedLines = new List<string>();

        foreach (var line in lines)
        {
            // Text passend umbrechen
            string[] words = line.Split(' ');
            string currentLine = "";

            foreach (var word in words)
            {
                if (currentLine.Length + word.Length + 1 > boxWidth - 4) // 4 Zeichen für die Ränder abziehen
                {
                    wrappedLines.Add(currentLine);
                    currentLine = word;
                }
                else
                {
                    if (currentLine.Length > 0)
                        currentLine += " ";
                    currentLine += word;
                }
            }

            if (currentLine.Length > 0)
            {
                wrappedLines.Add(currentLine);
            }
        }

        // Zentrierung berechnen
        int leftPadding = (consoleWidth - boxWidth) / 2;

        // Horizontale Grenze für die Box
        string horizontalBorder = new string('─', boxWidth - 2);

        // Box zeichnen
        // Oberer Rand mit Padding
        Console.WriteLine(new string(' ', leftPadding) + "┌" + horizontalBorder + "┐");

        // Textzeilen in der Box
        foreach (var wrappedLine in wrappedLines)
        {
            // Leerzeichen auffüllen, damit die Zeile genau so breit ist wie die Box
            string paddedLine = wrappedLine.PadRight(boxWidth - 2);
            Console.WriteLine(new string(' ', leftPadding) + "│" + paddedLine + "│");
        }

        // Unterer Rand mit Padding
        Console.WriteLine(new string(' ', leftPadding) + "└" + horizontalBorder + "┘");
    }


    public static void Speichern(string key, string value)
    {
        // Aktuelle JSON-Daten lesen
        string jsonFilePath = "appSettings.json";
        var json = File.ReadAllText(jsonFilePath);
        var jsonDoc = JsonDocument.Parse(json);
        var jsonRoot = jsonDoc.RootElement;

        // Neuen Wert setzen
        using (var stream = new MemoryStream())
        {
            using (var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true }))
            {
                writer.WriteStartObject();
                foreach (var property in jsonRoot.EnumerateObject())
                {
                    if (property.NameEquals(key))
                    {
                        writer.WriteString(key, value);
                    }
                    else
                    {
                        property.WriteTo(writer);
                    }
                }
                writer.WriteEndObject();
            }

            // Neue JSON-Daten in die Datei schreiben
            File.WriteAllText(jsonFilePath, System.Text.Encoding.UTF8.GetString(stream.ToArray()));
        }
    }

    public static void OpenCurrentFolder()
    {
        Console.WriteLine("    Der Ordner " + Environment.CurrentDirectory + " wird geöffent.");
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = Environment.CurrentDirectory,
                UseShellExecute = true,
                Verb = "open"
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine("    Fehler beim Öffnen des Ordners:");
            Console.WriteLine(ex.Message);
        }
    }
    public static void OpenWebseite(string url)
    {
        Console.WriteLine("    Die Seite https://wiki.svws.nrw.de/mediawiki/index.php?title=Schnittstellenbeschreibung#" + url + " wird geöffnet.");

        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine("    Fehler beim Öffnen der Webseite:");
            Console.WriteLine(ex.Message);
        }
    }

    internal static void ProtokollzeileSchreiben(string v, string protokollDatei)
    {
        using (StreamWriter writer = new StreamWriter(protokollDatei))
        {
            writer.WriteLine(v);
        }
    }

    internal static List<object> LiesDateien(string dateiName, string dateiendung, string[] hinweise, string delimiter = "|")
    {
        var objekt = new List<object>();

        var dateiPfad = CheckFile(dateiName, dateiendung);

        if (dateiPfad == null)
        {
            Global.ZeileSchreiben(0, dateiName, "keine Datei gefunden", new Exception("keine Datei gefunden"), hinweise);
            return new List<object>();
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
            if (dateiName.ToLower().Contains("absenceperstudent"))
            {
                csv.Context.RegisterClassMap<AbsencePerStudentsMap>();
                objekt.AddRange(new List<AbsencePerStudent>(csv.GetRecords<AbsencePerStudent>()));
            }
            if (dateiName.ToLower().Contains("student_"))
            {
                csv.Context.RegisterClassMap<StudentsMap>();
                objekt.AddRange(new List<Student>(csv.GetRecords<Student>()));
            }
            if (dateiName.ToLower().Contains("exportlessons"))
            {
                csv.Context.RegisterClassMap<ExportLessonsMap>();
                objekt.AddRange(new List<ExportLesson>(csv.GetRecords<ExportLesson>()));
            }
            if (dateiName.ToLower().Contains("marksperlessons"))
            {
                //csv.Context.RegisterClassMap<MarksPerLessonMap>();
                //objekt.AddRange(new List<MarkPerLesson>(csv.GetRecords<MarkPerLesson>()));
            }
            if (dateiName.ToLower().Contains("studentgroupstudents"))
            {
                csv.Context.RegisterClassMap<StudentgroupStudentsMap>();
                objekt.AddRange(new List<StudentgroupStudent>(csv.GetRecords<StudentgroupStudent>()));
            }
            if (dateiName.ToLower().Contains("faecher"))
            {
                objekt.AddRange(new List<Fach>(csv.GetRecords<Fach>()));
            }
            if (dateiName.ToLower().Contains("schuelerbasisdaten"))
            {
                objekt.AddRange(new List<SchuelerBasisdatum>(csv.GetRecords<SchuelerBasisdatum>()));
            }
            if (dateiName.ToLower().Contains("schuelerleistungsdaten"))
            {
                objekt.AddRange(new List<SchuelerLeistungsdatum>(csv.GetRecords<SchuelerLeistungsdatum>()));
            }
            if (dateiName.ToLower().Contains("schuelerlernabschnittsdaten"))
            {
                objekt.AddRange(new List<SchuelerLernabschnittsdatum>(csv.GetRecords<SchuelerLernabschnittsdatum>()));
            }
            if (dateiName.ToLower().Contains("schuelerteilleistungen"))
            {
                objekt.AddRange(new List<SchuelerTeilleistung>(csv.GetRecords<SchuelerTeilleistung>()));
            }
            //if (dateiName.ToLower().Contains("schueleradressen"))
            //{
            //    csv.Context.RegisterClassMap<SchuelerAdressenMap>();
            //    objekt.AddRange(new List<SchuelerAdresse>(csv.GetRecords<SchuelerAdresse>()));
            //}
            //if (dateiName.ToLower().Contains("Adressen"))
            //{
            //    csv.Context.RegisterClassMap<SchuelerAdressenMap>();
            //    objekt.AddRange(new List<Adresse>(csv.GetRecords<Adresse>()));
            //}
            if (dateiName.ToLower().Contains("sim."))
            {
                csv.Context.RegisterClassMap<SimsMap>();
                objekt.AddRange(new List<Sim>(csv.GetRecords<Sim>()));
            }
        }

        Global.ZeileSchreiben(0, dateiPfad, objekt.Count().ToString(), null);

        return objekt;
    }


    public static string CheckFile(string dateiPfad, string endung)
    {
        if (!Path.Exists(Path.GetDirectoryName(dateiPfad)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dateiPfad));
        }

        var sourceFile = (from f in Directory.GetFiles(Path.GetDirectoryName(dateiPfad), endung, SearchOption.AllDirectories)
                          where Path.GetFileName(f).StartsWith(Path.GetFileName(dateiPfad))
                          orderby File.GetLastWriteTime(f)
                          select f).LastOrDefault();

        return sourceFile;
    }

    internal static List<object> ZeileSchreiben(List<object> absencePerStudents)
    {
        Global.ZeileSchreiben(0, absencePerStudents.ToString(), absencePerStudents.Count().ToString(), null);
        return absencePerStudents;
    }

    public static class GlobalCsvMappings
    {
        public static Dictionary<string, List<string>> AliasMappings = new Dictionary<string, List<string>>();

        // Diese Methode liest die Alias.csv ein
        public static void LoadMappingsFromFile(string aliasFilePath)
        {
            using (var reader = new StreamReader(aliasFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<CsvAlias>();
                foreach (var record in records)
                {
                    // Jede PropertyName wird mit allen entsprechenden CSV-Namen verknüpft
                    AliasMappings[record.PropertyName] = new List<string> { record.CSVName1, record.CSVName2 };
                }
            }
        }

        public static void AddMappings<T>(ClassMap<T> map)
        {
            var properties = typeof(T).GetProperties(); // Hole alle Eigenschaften der Klasse T

            foreach (var property in properties)
            {
                var propertyName = property.Name;

                // Prüfe, ob die Eigenschaft in der Alias.csv vorhanden ist
                if (AliasMappings.TryGetValue(propertyName, out var csvNames))
                {
                    // Wenn ein Mapping in Alias.csv gefunden wurde, verwende die dynamischen Namen
                    var mapExpression = GetPropertyMappingExpression<T>(propertyName);
                    if (mapExpression != null)
                    {
                        map.Map(mapExpression).Name(csvNames.ToArray());
                    }
                }
                else
                {
                    // Fallback: Verwende den Standardnamen der Eigenschaft, wenn kein Mapping gefunden wurde
                    map.Map(typeof(T), property).Name(propertyName);
                }
            }
        }



        // Dynamische Methode, um eine Eigenschaft basierend auf dem Namen zu mappen
        private static System.Linq.Expressions.Expression<Func<T, object>> GetPropertyMappingExpression<T>(string propertyName)
        {
            var param = System.Linq.Expressions.Expression.Parameter(typeof(T), "m");
            var property = typeof(T).GetProperty(propertyName);
            if (property == null) return null;

            var body = System.Linq.Expressions.Expression.Convert(
                System.Linq.Expressions.Expression.Property(param, property),
                typeof(object)
            );
            return System.Linq.Expressions.Expression.Lambda<Func<T, object>>(body, param);
        }
    }
    public class CsvAlias
    {
        public string PropertyName { get; set; }
        public string CSVName1 { get; set; }
        public string CSVName2 { get; set; }
    }
}
