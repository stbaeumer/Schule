
using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.Json;

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

    internal static void DisplayHeader(string text = "h1", char unterstrich = '=')
    {
        if (text == "h1")
        {
            text = @"Schule.exe | https://github.com/stbaeumer/Schule | GPLv3 | Stefan Bäumer " + DateTime.Now.Year + @" | 10.10.2024";
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

        int punkte = gesamtbreite - linkerAbstand - linkeSeite.Length - rechteSeite.Length - 4;
        var mitte = " .".PadRight(Math.Max(3, punkte), '.') + " ";
        Console.WriteLine("".PadRight(linkerAbstand+2) + linkeSeite + mitte + rechteSeite);

        if (fehler != null)
        {
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
                            if (j==0)
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
            else
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine(fehler.ToString());
                Console.ReadKey();
            }
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
}