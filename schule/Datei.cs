using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Datei
{
    public Exception Fehler { get; set; }
    public string DateiPfad { get; set; }
    public string[] Hinweise { get; set; }
    public string Titel { get; private set; }
    public string Beschreibung { get; private set; }
    public string[] BenötigteDateien { get; set; }
    public string? Funktionsname { get; private set; }
    public List<string> Fehlermeldungen { get; set; }
    public new Reihenfolge Reihenfolge { get; set; }
    public Zeilen Zeilen { get; set; }
    public Aliasse Aliasse { get; internal set; }

    public void SchuelerBasisdaten(List<Datei> dateien, Schülers schuelers)
    {
        try
        {
            Reihenfolge = new Reihenfolge(this, dateien);

            // Die erste Datei ist die Masterdatei, die die Anzahl der auszugebenen Zeilen festlegt.

            Datei masterdatei = dateien[0];

            foreach (var zeileIn in masterdatei.Zeilen)
            {
                if (!zeileIn.IstKopfzeile) // Die Kopfzeile bleibt unberücksichtigt
                {
                    var zeileOut = new Zeile();

                    foreach (var reihenElement in Reihenfolge)
                    {
                        if (reihenElement.QuellDateiIndex >= 0)
                        {
                            if (reihenElement.QuellDateiPfad == masterdatei.DateiPfad)
                            {
                                zeileOut.Zellen.Add(zeileIn.Zellen[reihenElement.QuellDateiIndex]);
                            }
                            else
                            {
                                // Es muss aus der verknüpften Tabelle die Zeile gefunden werden, in der die Identifikationsmerkmale übereinstimmen.
                                // In der MarksPerLesson sind Identifikationsmerkmale Name und Klasse

                                int iName = masterdatei.GetSpaltenIndex("name");
                                int iKlasse = masterdatei.GetSpaltenIndex("klasse");

                                string name = zeileIn.Zellen[1].ToString();
                                string klasse = zeileIn.Zellen[2].ToString();

                                Datei slavedatei = dateien.Where(x => x.DateiPfad.StartsWith(@"ExportAusWebuntis\Student_")).FirstOrDefault();

                                int iiNachname = slavedatei.GetSpaltenIndex("longname");
                                int iiVorname = slavedatei.GetSpaltenIndex("forename");
                                int iiKlasse = slavedatei.GetSpaltenIndex("klasse.name");

                                foreach (var zeile in slavedatei.Zeilen.Where(x => x.IstKopfzeile == false))
                                {
                                    if (name.Contains(zeile.Zellen[iiVorname]) && name.Contains(zeile.Zellen[iiNachname]) && zeile.Zellen[iiKlasse] == klasse)
                                    {
                                        zeileOut.Zellen.Add(zeile.Zellen[Convert.ToInt32(reihenElement.QuellDateiIndex)]);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Bei Jahr oder Abschnitt wird der Wert für alle Zeilen hinzugefügt.
                            if (reihenElement.Wert != null && reihenElement.Wert != "")
                            {
                                zeileOut.Zellen.Add(reihenElement.Wert);
                            }
                        }
                    }
                    this.Zeilen.Add(zeileOut);
                }
            }
        }
        catch (Exception ex)
        {
            Fehler = ex;
        }
        finally
        {
            Global.ZeileSchreiben(0, this.DateiPfad + " wird vorbereitet", this.Zeilen.Count().ToString(), Fehler, null);
        }
    }

    public void PdfZeugnisse(List<Datei> dateien, Schülers schuelers)
    {
        var pdfdateien = new PdfDateien("PDF-Zeugnisse", "PDF-Zeugnisse-Einzeln", schuelers, this);
    }

    public void SchuelerTeilleistungen(List<Datei> dateien, Schülers schuelers)
    {
        try
        {
            Reihenfolge = new Reihenfolge(this, dateien);

            // Die erste Datei ist die Masterdatei, die die Anzahl der auszugebenen Zeilen festlegt.

            Datei masterdatei = dateien[0];

            foreach (var zeileIn in masterdatei.Zeilen)
            {
                if (!zeileIn.IstKopfzeile) // Die Kopfzeile bleibt unberücksichtigt
                {
                    var zeileOut = new Zeile();

                    foreach (var reihenElement in Reihenfolge)
                    {
                        if (reihenElement.QuellDateiIndex >= 0)
                        {
                            if (reihenElement.QuellDateiPfad == masterdatei.DateiPfad)
                            {
                                zeileOut.Zellen.Add(zeileIn.Zellen[reihenElement.QuellDateiIndex]);
                            }
                            else
                            {
                                // Es muss aus der verknüpften Tabelle die Zeile gefunden werden, in der die Identifikationsmerkmale übereinstimmen.
                                // In der MarksPerLesson sind Identifikationsmerkmale Name und Klasse

                                int iName = masterdatei.GetSpaltenIndex("name");
                                int iKlasse = masterdatei.GetSpaltenIndex("klasse");

                                string name = zeileIn.Zellen[1].ToString();
                                string klasse = zeileIn.Zellen[2].ToString();

                                Datei slavedatei = dateien.Where(x => x.DateiPfad.StartsWith(@"ExportAusWebuntis\Student_")).FirstOrDefault();

                                int iiNachname = slavedatei.GetSpaltenIndex("longname");
                                int iiVorname = slavedatei.GetSpaltenIndex("forename");
                                int iiKlasse = slavedatei.GetSpaltenIndex("klasse.name");

                                foreach (var zeile in slavedatei.Zeilen.Where(x => x.IstKopfzeile == false))
                                {
                                    if (name.Contains(zeile.Zellen[iiVorname]) && name.Contains(zeile.Zellen[iiNachname]) && zeile.Zellen[iiKlasse] == klasse)
                                    {
                                        zeileOut.Zellen.Add(zeile.Zellen[Convert.ToInt32(reihenElement.QuellDateiIndex)]);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Bei Jahr oder Abschnitt wird der Wert für alle Zeilen hinzugefügt.
                            if (reihenElement.Wert != null && reihenElement.Wert != "")
                            {
                                zeileOut.Zellen.Add(reihenElement.Wert);
                            }
                        }
                    }
                    this.Zeilen.Add(zeileOut);
                }
            }
        }
        catch (Exception ex)
        {
            Fehler = ex;
        }
        finally
        {
            Global.ZeileSchreiben(0, this.DateiPfad + " wird vorbereitet", this.Zeilen.Count().ToString(), Fehler, null);
        }
    }

    public string CheckFile(string dateiPfad = "")
    {
        // Wenn kein Parameter übergeben wird, dann wird der Dateipfad der Instanz verwendet, wobei Import durch Export ersetzt wird.

        if (dateiPfad == "")
        {
            dateiPfad = DateiPfad.Replace("ImportFür", "ExportAus");
        }

        var extensions = new List<string> { "*.csv", "*.dat", "*.TXT" };

        if (!Path.Exists(Path.GetDirectoryName(dateiPfad)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dateiPfad));
        }

        var sourceFile = (from ext in extensions
                          from f in Directory.GetFiles(Path.GetDirectoryName(dateiPfad), ext, SearchOption.AllDirectories)
                          where Path.GetFileName(f).StartsWith(Path.GetFileName(dateiPfad))
                          orderby File.GetLastWriteTime(f)
                          select f).LastOrDefault();
        if (sourceFile == null)
        {
            sourceFile = dateiPfad;
            Fehler = new FileNotFoundException("nicht vorhanden.");
        }
        return sourceFile;
    }

    internal void Erstellen()
    {
        Encoding encoding = Encoding.UTF8;

        if (!Path.Exists(Path.GetDirectoryName(DateiPfad)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(DateiPfad));
        }

        try
        {
            while (File.Exists(DateiPfad))
            {
                DateTime lastWriteTime = File.GetLastWriteTime(DateiPfad);
                string timeStamp = lastWriteTime.ToString("yyyy-MM-dd_HH-mm-ss");
                try
                {
                    if (DateiPfad.EndsWith(".dat"))
                    {
                        File.Move(DateiPfad, DateiPfad.Replace(".dat", timeStamp + ".dat"));
                    }
                    if (DateiPfad.EndsWith(".txt"))
                    {
                        File.Move(DateiPfad, DateiPfad.Replace(".txt", timeStamp + ".txt"));
                    }
                }
                catch (Exception ex)
                {
                    Fehler = ex;
                }
                finally
                {
                    Global.ZeileSchreiben(3, "Datei " + DateiPfad + " existiert bereits und wird umbenannt", "ok", Fehler);
                }
            }

            using (FileStream fs = new FileStream(DateiPfad, FileMode.CreateNew))
            {
                using (StreamWriter writer = new StreamWriter(fs, encoding))
                {
                    try
                    {
                        foreach (var zeile in Zeilen)
                        {
                            if (!(Zeilen.IndexOf(zeile) == 1 && zeile.IstKopfzeile))
                            {
                                writer.WriteLine(string.Join("|", zeile.Zellen));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Fehler = ex;
                    }
                    finally
                    {
                        Global.ZeileSchreiben(3, "Datei " + DateiPfad + " Zeilen", Zeilen.Count().ToString(), Fehler);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Global.ZeileSchreiben(3, "Datei " + DateiPfad + " erstellt", "ok", Fehler);
                        Console.ForegroundColor = ConsoleColor.White;
                        Process.Start("notepad.exe", DateiPfad);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.ReadKey();
        }
    }

    public void InteressierendeZeilenFiltern(Schülers? schuelers, int? dieLetztenBleibenUnberücksichtigt = 0, int? maxFehlstundenProTag = 0)
    {
        try
        {
            // Wenn zu Nachname, Vorname etc. Spalten in der Datei vorhanden sind, dann muss der Nachname 
            // irgendeines Schülers in der Spalte vorkommen. Sonst wird die Zeile entfernt.

            int indexOfNachname = GetSpaltenIndex("nachname");
            int indexOfVorname = GetSpaltenIndex("vorname");
            int indexOfName = GetSpaltenIndex("name");
            int indexOfGeburtsdatum = GetSpaltenIndex("geburtsdatum");
            int indexOfKlasse = GetSpaltenIndex("klasse");
            int indexOfKlassen = GetSpaltenIndex("klasse");
            int indexOfstudentgroupName = GetSpaltenIndex("studentgroup.name");

            for (int ii = 0; ii < Zeilen.Count; ii++)
            {
                if (Zeilen[ii].IstKopfzeile == false)
                {
                    bool zeileEntfernen = false;

                    if (indexOfName > 0)
                    {
                        if (!(from i in schuelers where Zeilen[ii].Zellen[indexOfName].Contains(i.Nachname) where Zeilen[ii].Zellen[indexOfName].Contains(i.Vorname) select i).Any())
                        {
                            zeileEntfernen = true;
                        }
                    }
                    if (indexOfNachname >= 0)
                    {
                        if (!(from i in schuelers where i.Nachname == this.Zeilen[ii].Zellen[indexOfNachname].ToString() select i).Any())
                        {
                            zeileEntfernen = true;
                        }
                    }
                    if (indexOfVorname >= 0)
                    {
                        if (!(from i in schuelers where i.Vorname == Zeilen[ii].Zellen[indexOfVorname] select i).Any())
                        {
                            zeileEntfernen = true;
                        }
                    }

                    if (indexOfGeburtsdatum >= 0)
                    {
                        if (!(from i in schuelers where i.Geburtsdatum == Zeilen[ii].Zellen[indexOfGeburtsdatum] select i).Any())
                        {
                            zeileEntfernen = true;
                        }
                    }
                    if (indexOfKlasse >= 0)
                    {
                        if (!(from i in schuelers where i.Klasse == Zeilen[ii].Zellen[indexOfKlasse] select i).Any())
                        {
                            zeileEntfernen = true;
                        }
                    }
                    if (indexOfKlassen >= 0)
                    {
                        if (!(from i in schuelers where Zeilen[ii].Zellen[indexOfKlassen].Split('~').Contains(i.Klasse) select i).Any())
                        {
                            zeileEntfernen = true;
                        }
                    }
                    if (indexOfstudentgroupName >= 0)
                    {
                        if (!(from i in schuelers where Zeilen[ii].Zellen[indexOfstudentgroupName].Contains(i.Klasse) select i).Any())
                        {
                            zeileEntfernen = true;
                        }
                    }
                    if (zeileEntfernen) { Zeilen.RemoveAt(ii); ii--; }
                }
            }
        }
        catch (Exception ex)
        {
            Fehler = ex;
        }
        finally
        {
            Global.ZeileSchreiben(0, "Interessierende aus " + DateiPfad, (Zeilen.Count - 1).ToString(), Fehler);
        }
    }

    internal int GetSpaltenIndex(string eigenschaft)
    {
        Zeile kopfzeileDieserDatei = this.Zeilen.Where(x => x.IstKopfzeile).FirstOrDefault();

        var x = Aliasse.Where(x => x.Contains(eigenschaft.ToLower())).FirstOrDefault();

        if (x == null) { return -1; }

        foreach (var s in x)
        {
            if (kopfzeileDieserDatei.Zellen.Select(x => x.ToLower()).Contains(s))
            {
                return Array.IndexOf(kopfzeileDieserDatei.Zellen.Select(s => s.ToLower()).ToArray(), s);
            }
        }
        return -1;
    }

    internal string GetWert(Zeile zeile, string name)
    {
        var strings = Aliasse.Where(x => x.Contains(name.ToLower())).FirstOrDefault();

        if (strings == null)
        {
            if (!Fehlermeldungen.Contains(name))
            {
                Fehlermeldungen.Add(name);
            }

            return "";
        }
        var kopfzeile = this.Zeilen.Where(x => x.IstKopfzeile).FirstOrDefault();

        foreach (var s in strings)
        {
            if (kopfzeile.Zellen.Select(x=>x.ToLower()).Contains(s.ToLower()))
            {
                var index = Array.IndexOf(kopfzeile.Zellen.Select(z => z.ToLower()).ToArray(), s.ToLower());

                if (index >= 0)
                {
                    if (zeile.Zellen[index].GetType() == typeof(string))
                    {
                        return zeile.Zellen[index];
                    }
                }
            }
        }
        return "";
    }

    internal void HabenAlleZeilenDieselbeAnzahlSpalten()
    {
        //if ((from a in this.Zeilen select a.Count).Distinct().Count() > 1) {
        //    Fehler = new Exception("Die Zeilen haben nicht alle die gleiche Anzahl Spalten.");

        //    Hinweise = new string[] { "Prüfen Sie die Ausgabedatei sorgfältig, sofern es nicht schon vorher zu einem Fehler kommt.", "Öffnen Sie die Exportdatei gegebenenfalls in Excel, um herauszufinden, welche Zeilen betroffen sind.", "Prüfen Sie, ob der Delimiter (" + Delimiter + ") eventuell in einer Zelle eingetippt wurde." };
        //}        
    }

    internal bool DateienVergleichen(Datei vergleichsdatei)
    {
        // 1. Gib alle Zeilen zurück, die in this nicht enthalten sind.
        var nurInThis = this.Zeilen.Except(vergleichsdatei.Zeilen, new ZeilenComparer()).ToList();

        // 2. Gib alle Zeilen zurück, die in vergleichsdatei nicht enthalten sind.
        var nurInVergleichsdatei = vergleichsdatei.Zeilen.Except(this.Zeilen, new ZeilenComparer()).ToList();

        // 3. Gib alle Zeilen zurück, die in den Spalten Vorname, Nachname, Geburtsdatum und Klasse in beiden Dateien enthalten sind, aber unterschiedlich sind.

        if (nurInThis.Any() || nurInVergleichsdatei.Any())
        {
            Global.DisplayCenteredBox("Es gibt Unterschiede zwischen der Datei, die Sie aus SchILD exportiert haben (" + vergleichsdatei.DateiPfad + ") und derjenigen, die für den Import nach SchILD vorbereitet wird (" + this.DateiPfad + "):", 90);
            if (nurInThis.Any())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Global.DisplayCenteredBox(" Beispielsweise existieren " + nurInThis.Count() + " Zeilen bisher nicht in SchILD. D.h., die Zeilen werden beim Import neu angelegt werden. Beispiel:\n " + string.Join(", ", nurInThis[1].Zellen), 90);
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (nurInVergleichsdatei.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Global.DisplayCenteredBox(" Beispielsweise existieren laut " + vergleichsdatei.DateiPfad + " " + nurInVergleichsdatei.Count() + " Zeilen in SchILD, aber nicht in der neuen Datei. D.h., die Zeilen werden beim Import in SchILD gelöscht. Beispiel: \n" + string.Join(", ", nurInVergleichsdatei[1].Zellen), 90);
                Console.ForegroundColor = ConsoleColor.White;
            }
            do
            {
                Console.WriteLine("         Wollen Sie nur die veränderten Zeilen in der Datei");
                Console.WriteLine("         " + this.DateiPfad + " ausgeben lassen?");

                var configuration = new ConfigurationBuilder().AddJsonFile("appSettings.json", optional: false, reloadOnChange: true).Build();
                var vergleich = configuration["Vergleich"];


                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("            Ihre Auswahl (j/n) [" + vergleich + "] : ");
                Console.ResetColor();

                var eingabe = Console.ReadLine();

                if (eingabe == "q") { return true; }
                if (eingabe == "ö") { Global.OpenCurrentFolder(); }
                if (eingabe == "x") { Global.OpenWebseite("https://wiki.svws.nrw.de/mediawiki/index.php?title=Schnittstellenbeschreibung"); }
                if (eingabe == "n" || (eingabe == "" && vergleich == "n")){ Global.Speichern("Vergleich", "n"); return false; }
                if (eingabe == "j" || (eingabe == "" && vergleich == "j")) 
                {
                    Global.Speichern("Vergleich", "j");

                    // Um die Vergleichszeile identifizieren zu können, müssen mindestens drei der folgenden Spalten für die Identifizierung identisch sein:
                    List<string> identifizierendeMerkmale = new List<string>() { "Name", "Nachname", "Vorname", "Klasse", "Geburtsdatum"};
                                        
                    // ident sind die Indizies der identifizierenden Vergleichsspalten
                    List<int> ident = new List<int>();
                    foreach (var item in identifizierendeMerkmale)
                    {
                        string inn = GetWert(Zeilen[0], item);

                        if (inn != null && inn != "")
                        {   
                            ident.Add(GetSpaltenIndex(inn));
                        }
                    }

                    if (ident.Count() < 3)
                    {
                        Console.WriteLine("Ein Vergleich der Dateien ist nicht möglich, weil es weniger als drei identifizierende Merkmale gibt.");
                    }
                    else 
                    {
                        for (int i = 0; i < this.Zeilen.Count; i++)
                        {
                            // Prüfe, ob die Eigenschaftswerte in der Zeile this.Zeilen[i] und der vZeile übereinstimmen. Es werden nur die Eigenschaften ausgewertet, die in der List<string> konkret stehen
                            var zeileInVergleichsdatei = vergleichsdatei.Zeilen.Where(x =>
                                                            x.Zellen[ident[0]] == this.Zeilen[i].Zellen[ident[0]] &&
                                                            x.Zellen[ident[1]] == this.Zeilen[i].Zellen[ident[1]] &&
                                                            x.Zellen[ident[2]] == this.Zeilen[i].Zellen[ident[2]]).FirstOrDefault();

                            if (zeileInVergleichsdatei != null)
                            {
                                EntferneNichtIdentischeZeilen(zeileInVergleichsdatei);
                                // Wenn die Zeilen nicht identisch sind, lösche die Zeile aus der Liste this.Zeilen
                                Zeilen.Remove(this.Zeilen[i]); i--;
                                
                            }
                        }
                    }
                    return false;
                }
                
            } while (true);
        }
        return false;
    }

    public void EntferneNichtIdentischeZeilen(Zeile vZeile)
    {
        List<string> eigenschaften = this.Zeilen[0].Zellen.ToList();

        this.Zeilen.RemoveAll(aktuelleZeile =>
        {
            foreach (var eigenschaft in eigenschaften)
            {
                var index = Array.IndexOf(this.Zeilen[0].Zellen.ToArray(), eigenschaft);
                if (index == -1) continue;

                var wertAktuelleZeile = aktuelleZeile.Zellen[index].ToLower();
                var wertVZeile = vZeile.Zellen[index].ToLower();

                if (wertAktuelleZeile != wertVZeile)
                {
                    return true; // Zeile ist nicht identisch, also löschen
                }
            }
            return false; // Zeile ist identisch, also nicht löschen
        });
    }


    // Hilfsklasse zum Vergleichen von Zeilen
    class ZeilenComparer : IEqualityComparer<Zeile>
    {
        public bool Equals(Zeile x, Zeile y)
        {
            return x.Zellen.SequenceEqual(y.Zellen);
        }

        public int GetHashCode(Zeile obj)
        {
            return string.Join(",", obj.Zellen).GetHashCode();
        }
    }

    public void AddZeilen()
    {
        if (Fehler == null)
        {
            using (var reader = new StreamReader(DateiPfad))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    char delimiter = GetDelimiter(line);

                    var zellen = line
                        .Split(new[] { delimiter }, StringSplitOptions.None)
                        .Select(item => item.Trim().Replace("\"", "").Replace("_", ""))
                    .ToList();

                    if (Zeilen.Count > 0)
                    {
                        if (zellen.Count() < Zeilen[0].Zellen.Count())
                        {
                            for (int i = 0; i < Zeilen[0].Zellen.Count(); i++)
                            {
                                line = line + reader.ReadLine();
                                i = line.Split(delimiter, StringSplitOptions.None).Count();
                            }
                        }
                    }

                    // Wenn die Values der Zeile mit der Kopfzeile identisch sind, dann wird die Zeile nicht hinzugefügt.
                    if (Zeilen.Count == 0 || !Zeilen[0].Zellen.SequenceEqual(zellen))
                    {
                        Zeilen.Add(new Zeile(zellen));
                    }
                }
            }
        }
    }

    public char GetDelimiter(string line)
    {
        var listeDeliminiter = Global.Delimiter;

        if (listeDeliminiter == null || listeDeliminiter.Count() == 0)
        {
            throw new InvalidOperationException("Die ZeichenListe ist leer oder null.");
        }

        if (string.IsNullOrEmpty(line))
        {
            throw new ArgumentException("Der Eingabestring 'line' ist leer oder null.");
        }

        return listeDeliminiter
            .Select(c => new { Zeichen = c, Häufigkeit = line.Count(ch => ch == c) })
            .OrderByDescending(x => x.Häufigkeit)
            .First()
            .Zeichen;
    }

    public Datei()
    {
        Zeilen = new Zeilen();
    }

    public Datei(string dateiPfad)
    {
        Zeilen = new Zeilen();
        DateiPfad = dateiPfad;
    }

    public Datei(string dateiPfad, string[] hinweiseZumDownload, List<string> kopfzeile = null, string titel = null, string beschreibung = null, string[]? benötigteDateien = null, string? funktionsname = null)
    {
        try
        {
            if (dateiPfad.ToLower().Contains("export")) // Nur Export-Dateien müssen existieren
            {
                DateiPfad = CheckFile(dateiPfad); //Das kann ein Export oder Importpfad sein.
                if (DateiPfad != null)
                {
                    Global.ZeileSchreiben(0, dateiPfad, "vorhanden", Fehler, hinweiseZumDownload);
                }
            }
            else
            {   
                DateiPfad = dateiPfad;
            }
            
            Hinweise = hinweiseZumDownload;
            Zeilen = kopfzeile != null ? new Zeilen() { new Zeile(kopfzeile, true) } : new Zeilen();
            Titel = titel;
            Beschreibung = beschreibung;
            BenötigteDateien = benötigteDateien;
            Funktionsname = funktionsname;
            Fehlermeldungen = new List<string>();
        }
        catch (Exception ex)
        {
            Fehler = ex;
        }
        finally
        {   
        }
    }
}