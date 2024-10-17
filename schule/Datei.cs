using CsvHelper.Configuration;
using CsvHelper;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
//using iTextSharp.text.pdf.parser;
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
using CsvHelper.Delegates;

public class Datei
{
    public Exception Fehler { get; set; }
    public string DateiPfad { get; set; }
    public string[] Hinweise { get; set; }
    public string Titel { get; private set; }
    public List<string> Kopfzeile { get; private set; }
    public string Beschreibung { get; private set; }
    public string[] BenötigteDateien { get; set; }
    public string? Funktionsname { get; private set; }
    public List<string> Fehlermeldungen { get; set; }
    public List<string> Referenzmerkmale { get; set; }
    public new Reihenfolge Reihenfolge { get; set; }
    public Zeilen Zeilen { get; set; }
    public Aliasse Aliasse { get; internal set; }
    public List<string> Referenzspalten { get; internal set; }
    public bool NameStattVorUndNachname { get; set; }

    //public void SchuelerBasisdaten(List<Datei> dateien, Schülers schuelers)
    //{
    //    try
    //    {
    //        Reihenfolge = new Reihenfolge(this, dateien);
            
    //        // Die erste Datei ist die Masterdatei, die die Anzahl der auszugebenen Zeilen festlegt.

    //        Datei masterdatei = dateien[0];

    //        foreach (var zeileIn in masterdatei.Zeilen)
    //        {
    //            if (!zeileIn.IstKopfzeile) // Die Kopfzeile bleibt unberücksichtigt
    //            {
    //                var zeileOut = new Zeile();

    //                foreach (var reihenElement in Reihenfolge)
    //                {
    //                    if (reihenElement.QuellDateiIndex >= 0)
    //                    {
    //                        if (reihenElement.QuellDateiPfad == masterdatei.DateiPfad)
    //                        {
    //                            // Der Wert wird direkt aus der Masterdatei entnommen.
    //                            var wert = zeileIn.Zellen[reihenElement.QuellDateiIndex];
    //                            zeileOut.Zellen.Add(wert);
    //                        }
    //                        else
    //                        {
    //                            Datei slavedatei = dateien.Where(x => x.DateiPfad == reihenElement.QuellDateiPfad).FirstOrDefault();

    //                            List<int[]> ident = new List<int[]>();

    //                            foreach (var item in this.Referenzmerkmale)
    //                            {
    //                                int iMaster = masterdatei.GetSpaltenIndex(item);
    //                                int iSlave = slavedatei.GetSpaltenIndex(item);

    //                                if (iMaster >= 0 && iSlave >= 0)
    //                                {
    //                                    ident.Add(new int[] { iMaster, iSlave });
    //                                }
    //                            }

    //                            Zeile zeileQuelldatei = slavedatei.Zeilen.FirstOrDefault(x =>
    //                                                           zeileIn.Zellen[ident[0][0]] == x.Zellen[ident[0][1]] &&
    //                                                           zeileIn.Zellen[ident[1][0]] == x.Zellen[ident[1][1]] &&
    //                                                           zeileIn.Zellen[ident[2][0]] == x.Zellen[ident[2][1]]);

    //                            var wert = zeileQuelldatei.Zellen[reihenElement.QuellDateiIndex];
    //                            zeileOut.Zellen.Add(wert);
    //                        }
    //                    }
    //                    else
    //                    {
    //                        var wert = reihenElement.Wert;
    //                        zeileOut.Zellen.Add(wert);
    //                    }
    //                }
    //                this.Zeilen.Add(zeileOut);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Fehler = ex;
    //    }
    //    finally
    //    {
    //        Global.ZeileSchreiben(0, this.DateiPfad + " wird vorbereitet", this.Zeilen.Count().ToString(), Fehler, null);
    //    }
    //}

    //public void SchuelerTeilleistungen(List<Datei> dateien, Schülers schuelers)
    //{
    //    try
    //    {
    //        Reihenfolge = new Reihenfolge(this, dateien);

    //        // Die erste Datei ist die Masterdatei, die die Anzahl der auszugebenen Zeilen festlegt.

    //        Datei masterdatei = dateien[0];

    //        foreach (var zeileIn in masterdatei.Zeilen)
    //        {
    //            if (!zeileIn.IstKopfzeile) // Die Kopfzeile bleibt unberücksichtigt
    //            {
    //                var zeileOut = new Zeile();

    //                foreach (var reihenElement in Reihenfolge)
    //                {
    //                    if (reihenElement.QuellDateiIndex >= 0)
    //                    {
    //                        if (reihenElement.QuellDateiPfad == masterdatei.DateiPfad)
    //                        {
    //                            // Der Wert wird direkt aus der Masterdatei entnommen.
    //                            var wert = zeileIn.Zellen[reihenElement.QuellDateiIndex];
    //                            zeileOut.Zellen.Add(wert);
    //                        }
    //                        else
    //                        {
    //                            Datei slavedatei = dateien.Where(x => x.DateiPfad == reihenElement.QuellDateiPfad).FirstOrDefault();

    //                            List<int[]> ident = new List<int[]>();

    //                            foreach (var item in this.Referenzmerkmale)
    //                            {
    //                                if (item == "name" && masterdatei.NameStattVorUndNachname)
    //                                {
    //                                    // Wenn es in der masterdatei keine drei identifizierenden Merkmale gibt,
    //                                    // aber der Name statt Vor- und Nachname existiert, dann wird überprüft,
    //                                    // ob Vor- und Nachname im Namen der Masterdatei enthalten sind.

    //                                    int iMaster = masterdatei.GetSpaltenIndex(item);
    //                                    int iSlaveVname = slavedatei.GetSpaltenIndex("Vorname");
    //                                    ident.Add(new int[] { iMaster, iSlaveVname });
    //                                    int iSlaveNname = slavedatei.GetSpaltenIndex("Nachname");
    //                                    ident.Add(new int[] { iMaster, iSlaveNname });
    //                                }
    //                                else
    //                                {
    //                                    int iMaster = masterdatei.GetSpaltenIndex(item);
    //                                    int iSlave = slavedatei.GetSpaltenIndex(item);

    //                                    if (iMaster >= 0 && iSlave >= 0)
    //                                    {
    //                                        ident.Add(new int[] { iMaster, iSlave });
    //                                    }
    //                                }
    //                            }

    //                            Zeile zeileQuelldatei = slavedatei.Zeilen.FirstOrDefault(x =>
    //                                                           zeileIn.Zellen[ident[0][0]] == x.Zellen[ident[0][1]] &&
    //                                                           zeileIn.Zellen[ident[1][0]] == x.Zellen[ident[1][1]] &&
    //                                                           zeileIn.Zellen[ident[2][0]] == x.Zellen[ident[2][1]]);

    //                            if (masterdatei.NameStattVorUndNachname)
    //                            {
    //                                // Wenn der Name statt Vor- und Nachname existiert, dann wird überprüft, ob Vor- und Nachname im Namen der Masterdatei enthalten sind.
    //                                zeileQuelldatei = slavedatei.Zeilen.FirstOrDefault(x =>
    //                                                           zeileIn.Zellen[ident[0][0]].Contains(x.Zellen[ident[0][1]]) &&
    //                                                           zeileIn.Zellen[ident[1][0]].Contains(x.Zellen[ident[1][1]]) &&
    //                                                           zeileIn.Zellen[ident[2][0]] == x.Zellen[ident[2][1]]);
    //                            }

    //                            var wert = zeileQuelldatei.Zellen[reihenElement.QuellDateiIndex];
    //                            zeileOut.Zellen.Add(wert);
    //                        }
    //                    }
    //                    else
    //                    {
    //                        var wert = reihenElement.Wert;
    //                        zeileOut.Zellen.Add(wert);
    //                    }
    //                }
    //                this.Zeilen.Add(zeileOut);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Fehler = ex;
    //    }
    //    finally
    //    {
    //        Global.ZeileSchreiben(0, this.DateiPfad + " wird vorbereitet", this.Zeilen.Count().ToString(), Fehler, null);
    //    }
    //}

    public void PdfZeugnisse(List<Datei> dateien, Schülers schuelers)
    {
        
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

    //internal void Erstellen()
    //{
    //    Encoding encoding = Encoding.UTF8;

    //    if (!Path.Exists(Path.GetDirectoryName(DateiPfad)))
    //    {
    //        Directory.CreateDirectory(Path.GetDirectoryName(DateiPfad));
    //    }

    //    try
    //    {
    //        while (File.Exists(DateiPfad))
    //        {
    //            DateTime lastWriteTime = File.GetLastWriteTime(DateiPfad);
    //            string timeStamp = lastWriteTime.ToString("yyyy-MM-dd_HH-mm-ss");
    //            try
    //            {
    //                if (DateiPfad.EndsWith(".dat"))
    //                {
    //                    File.Move(DateiPfad, DateiPfad.Replace(".dat", timeStamp + ".dat"));
    //                }
    //                if (DateiPfad.EndsWith(".txt"))
    //                {
    //                    File.Move(DateiPfad, DateiPfad.Replace(".txt", timeStamp + ".txt"));
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                Fehler = ex;
    //            }
    //            finally
    //            {
    //                Global.ZeileSchreiben(0, DateiPfad + " existiert bereits und wird umbenannt", "ok", Fehler);
    //            }
    //        }

    //        using (FileStream fs = new FileStream(DateiPfad, FileMode.CreateNew))
    //        {
    //            using (StreamWriter writer = new StreamWriter(fs, encoding))
    //            {
    //                try
    //                {
    //                    foreach (var zeile in Zeilen)
    //                    {
    //                        if (!(Zeilen.IndexOf(zeile) == 1 && zeile.IstKopfzeile))
    //                        {
    //                            writer.WriteLine(string.Join("|", zeile.Zellen));
    //                        }
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                    Fehler = ex;
    //                }
    //                finally
    //                {
    //                    Global.ZeileSchreiben(0, DateiPfad + " Zeilen", Zeilen.Count().ToString(), Fehler);
    //                    Console.ForegroundColor = ConsoleColor.Magenta;
    //                    Global.ZeileSchreiben(0, DateiPfad + " erstellt", "ok", Fehler);
    //                    Console.ForegroundColor = ConsoleColor.White;
    //                    Process.Start("notepad.exe", DateiPfad);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.ToString());
    //        Console.ReadKey();
    //    }
    //}

    //public void InteressierendeZeilenFiltern(Schülers interessierendeSchuelers, int? dieLetztenBleibenUnberücksichtigt = 0, int? maxFehlstundenProTag = 0)
    //{
    //    try
    //    {
    //        // Wenn zu Nachname, Vorname etc. Spalten in der Datei vorhanden sind, dann muss der Nachname 
    //        // irgendeines Schülers in der Spalte vorkommen. Sonst wird die Zeile entfernt.

    //        int indexOfNachname = GetSpaltenIndex("nachname");
    //        int indexOfVorname = GetSpaltenIndex("vorname");
    //        int indexOfName = GetSpaltenIndex("name");
    //        int indexOfGeburtsdatum = GetSpaltenIndex("geburtsdatum");
    //        int indexOfKlasse = GetSpaltenIndex("klasse");
    //        int indexOfKlassen = GetSpaltenIndex("klasse");
    //        int indexOfstudentgroupName = GetSpaltenIndex("studentgroup.name");

    //        // Von hinten nach vorne iterieren, um direkt löschen zu können
    //        for (int i = Zeilen.Count - 1; i >= 1; i--)
    //        {
    //            foreach (var item in this.Referenzspalten)
    //            {
    //                if (item.ToLower() == "nachname" && indexOfNachname >= 0
    //                    && !(interessierendeSchuelers.Where(x => x.Nachname == Zeilen[i].Zellen[indexOfNachname]).Any()))
    //                {   
    //                    Zeilen.RemoveAt(i);
    //                    //var sssss = Zeilen[i].Zellen[indexOfNachname];
    //                    break;
    //                }
    //                if (item.ToLower() == "vorname" && indexOfVorname >= 0
    //                    && !(interessierendeSchuelers.Where(x => x.Vorname == Zeilen[i].Zellen[indexOfVorname]).Any()))
    //                {
    //                    Zeilen.RemoveAt(i);
    //                    //var xxx = Zeilen[i].Zellen[indexOfVorname];
    //                    break;
    //                }
    //                if (item.ToLower() == "geburtsdatum" && indexOfGeburtsdatum >= 0
    //                    && !(interessierendeSchuelers.Where(x => x.Geburtsdatum == Zeilen[i].Zellen[indexOfGeburtsdatum]).Any()))
    //                {
    //                    //var xxx = Zeilen[i].Zellen[indexOfGeburtsdatum];
    //                    Zeilen.RemoveAt(i);                        
    //                    break;
    //                }
    //                if (item.ToLower() == "klasse" && indexOfKlasse >= 0
    //                    && !(interessierendeSchuelers.Where(x => x.Klasse == Zeilen[i].Zellen[indexOfKlasse]).Any()))
    //                {
    //                    //var sssss = Zeilen[i].Zellen[indexOfKlasse];
    //                    Zeilen.RemoveAt(i);
    //                    break;
    //                }
    //                if (indexOfNachname <0 && indexOfVorname <0 && item.ToLower() == "name" && indexOfName >= 0
    //                    && !(interessierendeSchuelers.Where(x => Zeilen[i].Zellen[indexOfName].Contains(x.Nachname) && Zeilen[i].Zellen[indexOfName].Contains(x.Vorname)).Any()))
    //                {
    //                    //  Zeilen.RemoveAt(i);
    //                    // break;
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Fehler = ex;
    //    }
    //    finally
    //    {
    //        Global.ZeileSchreiben(0, "Interessierende aus " + DateiPfad, (Zeilen.Count - 1).ToString(), Fehler);
    //    }
    //}

    //internal int GetSpaltenIndex(string eigenschaft, List<string> kopfzeile = null)
    //{

    //    Zeile kopfzeileDieserDatei = new Zeile();

    //    if (kopfzeile == null)
    //    {
    //        kopfzeileDieserDatei = this.Zeilen.Where(x => x.IstKopfzeile).FirstOrDefault();
    //    }
    //    else
    //    {
    //        kopfzeileDieserDatei = new Zeile(kopfzeile, true);
    //    }

        

    //    var x = Aliasse.Where(x => x.Contains(eigenschaft.ToLower())).FirstOrDefault();

    //    if (x == null) { return -1; }

    //    foreach (var s in x)
    //    {
    //        if (kopfzeileDieserDatei.Zellen.Select(x => x.ToLower()).Contains(s))
    //        {
    //            return Array.IndexOf(kopfzeileDieserDatei.Zellen.Select(s => s.ToLower()).ToArray(), s);
    //        }
    //    }
    //    return -1;
    //}

    //internal string GetWert(Zeile zeile, string name)
    //{
    //    var strings = Aliasse.Where(x => x.Contains(name.ToLower())).FirstOrDefault();

    //    if (strings == null)
    //    {
    //        if (!Fehlermeldungen.Contains(name))
    //        {
    //            Fehlermeldungen.Add(name);
    //        }

    //        return "";
    //    }
    //    var kopfzeile = this.Zeilen.Where(x => x.IstKopfzeile).FirstOrDefault();

    //    foreach (var s in strings)
    //    {
    //        if (kopfzeile.Zellen.Select(x=>x.ToLower()).Contains(s.ToLower()))
    //        {
    //            var index = Array.IndexOf(kopfzeile.Zellen.Select(z => z.ToLower()).ToArray(), s.ToLower());

    //            if (index >= 0)
    //            {
    //                if (zeile.Zellen[index].GetType() == typeof(string))
    //                {
    //                    return zeile.Zellen[index];
    //                }
    //            }
    //        }
    //    }
    //    return "";
    //}

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

        if ((nurInThis != null && nurInThis.Count > 1)  || (nurInVergleichsdatei != null && nurInVergleichsdatei.Count > 1))
        {
            Global.DisplayCenteredBox("Es gibt Unterschiede zwischen der Datei, die Sie aus SchILD exportiert haben (" + vergleichsdatei.DateiPfad + ") und derjenigen, die für den Import nach SchILD vorbereitet wird (" + this.DateiPfad + "):", 90);
            if (nurInThis.Count > 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Global.DisplayCenteredBox(" Beispielsweise existieren " + nurInThis.Count() + " Zeilen bisher nicht in SchILD. D.h., die Zeilen werden beim Import neu angelegt werden. Beispiel:\n " + string.Join(", ", nurInThis[1]), 90);
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (nurInVergleichsdatei.Count > 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                //Global.DisplayCenteredBox(" Beispielsweise existieren laut " + vergleichsdatei.DateiPfad + " " + nurInVergleichsdatei.Count() + " Zeilen in SchILD, aber nicht in der neuen Datei. D.h., die Zeilen werden beim Import in SchILD gelöscht. Beispiel: \n" + string.Join(", ", nurInVergleichsdatei[1].Zellen), 90);
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
                        //string inn = GetWert(Zeilen[0], item);

                        //if (inn != null && inn != "")
                        //{   
                        //    ident.Add(GetSpaltenIndex(inn));
                        //}
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
                            var zeileInVergleichsdatei = new Zeile();//vergleichsdatei.Zeilen.Where(x =>
                                                         //   x.Zellen[ident[0]] == this.Zeilen[i].Zellen[ident[0]] &&
                                                         //   x.Zellen[ident[1]] == this.Zeilen[i].Zellen[ident[1]] &&
                                                         //   x.Zellen[ident[2]] == this.Zeilen[i].Zellen[ident[2]]).FirstOrDefault();

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
                    Global.ZeileSchreiben(0, DateiPfad + " existiert bereits und wird umbenannt", "ok", Fehler);
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
                            string zeileString = "";

                            for (int i = 0; i < zeile.Count(); i++)
                            {
                                // Unterstriche in Zellen entfernen
                                zeile[i] = zeile[i] == null ? "" : zeile[i];
                                zeile[i] = zeile[i].Replace("_", "");
                                zeile[i] = zeile[i].TrimEnd();
                                zeile[i] = zeile[i].TrimStart();

                                zeileString += zeile[i] + "|";
                            }
                            writer.WriteLine(zeileString.TrimEnd('|'));
                        }
                    }
                    catch (Exception ex)
                    {
                        Fehler = ex;
                    }
                    finally
                    {
                        Global.ZeileSchreiben(0, DateiPfad + " Zeilen", Zeilen.Count().ToString(), Fehler);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Global.ZeileSchreiben(0, DateiPfad + "neu erstellt", "ok", Fehler);
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



    public void EntferneNichtIdentischeZeilen(Zeile vZeile)
    {
        List<string> eigenschaften = this.Zeilen[0].ToList();

        this.Zeilen.RemoveAll(aktuelleZeile =>
        {
            foreach (var eigenschaft in eigenschaften)
            {
                var index = Array.IndexOf(this.Zeilen[0].ToArray(), eigenschaft);
                if (index == -1) continue;

                var wertAktuelleZeile = aktuelleZeile[index].ToLower();
                var wertVZeile = vZeile[index].ToLower();

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
            return x.SequenceEqual(y);
        }

        public int GetHashCode(Zeile obj)
        {
            return string.Join(",", obj).GetHashCode();
        }
    }

    public char ErmitteleDelimiter(string dateiPfad)
    {
        var possibleDelimiters = new[] { ',', ';', '\t', '|', ' ' };
        var delimiterCounts = new Dictionary<char, int>();

        foreach (var delimiter in possibleDelimiters)
        {
            delimiterCounts[delimiter] = 0;
        }

        using (var reader = new StreamReader(dateiPfad))
        {
            // Lies die ersten paar Zeilen
            for (int i = 0; i < 5 && !reader.EndOfStream; i++)
            {
                var line = reader.ReadLine();
                if (line == null) continue;

                foreach (var delimiter in possibleDelimiters)
                {
                    delimiterCounts[delimiter] += line.Count(c => c == delimiter);
                }
            }
        }

        // Bestimme das Trennzeichen, das am häufigsten vorkommt
        return delimiterCounts.OrderByDescending(kvp => kvp.Value).First().Key;
    }

    public void AddZeilen()
    {
        List<int> anzahlSpalten = new List<int>() { this.Zeilen[0].Count };

        try
        {
            var delimiter = ErmitteleDelimiter(DateiPfad);

            if (Fehler == null)
            {
                using (var reader = new StreamReader(DateiPfad))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = delimiter.ToString(), // Setzt das Trennzeichen auf Semikolon
                    HasHeaderRecord = true, // Falls die CSV keine Kopfzeile hat
                }))
                {
                    // Jede Zeile als Liste von Zellen (Strings) lesen
                    
                    while (csv.Read())
                    {
                        // Eine Zeile einlesen und in eine Liste von Strings konvertieren
                        var Zellen = new List<string>();
                        
                        for (int i = 0; csv.TryGetField<string>(i, out var cell); i++)
                        {
                            Zellen.Add(cell.Replace("_",""));
                        }

                        // Zeile zur Matrix hinzufügen
                        Zeilen.Add(new Zeile(Zellen));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Fehler = ex;
        }
        finally
        {
            if (anzahlSpalten.Distinct().Count() > 1) 
            { Fehler = new Exception("Abweichende Spaltenanzahl: " + String.Join(',', anzahlSpalten));
                Global.ZeileSchreiben(0, DateiPfad, "", Fehler, new string[] { "Die Anzahl der Spalten weicht voneinader ab. Das Problem ist gravierend.", "Importieren Sie die Datei nach Excel oder Libreoffice, um zu prüfen, woran das liegt.", "Wenn die Spalten in Excel nicht abweichen, dann durchsuchen Sie Excel nach dem Delimiter. So erkennen Sie, ob der Delimiter in Zellen als Wert eingetragen ist.", "Korrigieren Sie den Fehler z.B. mit Suchen & Ersetzen.", "Sie können auch mit einem anderen Delimiter neu speichern.", "Am Ende unbedingt prüfen, ob die Umlaute richtig angezeigt werden. Am besten als UTF8 exportieren.", "Achten Sie darauf, dass Datumsangeben wie folgt aussehen: dd.mm.yyyy" });
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
        Referenzspalten = new List<string>();
    }

    public Datei(string dateiPfad, string titel, List<string> kopfzeile)
    {
        DateiPfad = dateiPfad;
        Titel = titel;
        Kopfzeile = kopfzeile;
        Zeilen = new Zeilen();
        Zeilen.Add(new Zeile(kopfzeile));
    }

    public Datei(Aliasse aliasse, List<string> referenzmerkmale, string dateiPfad, string[] hinweiseZumDownload, List<string> kopfzeile = null, string titel = null, string beschreibung = null, string[]? benötigteDateien = null, string? funktionsname = null)
    {
        try
        {
            if (dateiPfad.ToLower().Contains("export")) // Nur Export-Dateien müssen existieren
            {
                DateiPfad = CheckFile(dateiPfad); //Das kann ein Export oder Importpfad sein.
                if (DateiPfad != null)
                {
                    DateTime creationDate = File.GetCreationTime(DateiPfad);
                    Global.ZeileSchreiben(0, dateiPfad, "erstellt: " + creationDate.ToShortDateString(), Fehler, hinweiseZumDownload);
                }
            }
            else
            {   
                DateiPfad = dateiPfad;
            }

            Aliasse = aliasse;
            Hinweise = hinweiseZumDownload;

            // Wenn es eine Spalte namens Name gibt, aber keine Spalte für Vor und Nachname, dann wird angenommen, dass Vor- und Nachname in der Spalte 
            // Name stecken.

            var iNachname = 0;//GetSpaltenIndex("nachname", kopfzeile);
            var iVorname = 0;//GetSpaltenIndex("vorname", kopfzeile);
            var iName = 0;//GetSpaltenIndex("name",kopfzeile);

            var neueKopfzeile = new Zeile();

            if (iNachname < 0 && iVorname < 0 && iName >= 0)
            {
                NameStattVorUndNachname = true;
            }

            Zeilen = kopfzeile != null ? new Zeilen() { new Zeile(kopfzeile, true) } : new Zeilen();
            Titel = titel;
            Beschreibung = beschreibung;
            BenötigteDateien = benötigteDateien;
            Funktionsname = funktionsname;
            Fehlermeldungen = new List<string>();
            Referenzmerkmale = referenzmerkmale;
            Referenzspalten = GetReferenzspalten(referenzmerkmale);
        }
        catch (Exception ex)
        {
            Fehler = ex;
        }
        finally
        {   
        }
    }

    public Datei(Menüeintrag menü)
    {
        DateiPfad = menü.DateiPfad;
        Titel = menü.Titel;
        Kopfzeile = menü.Kopfzeile;
        Zeilen = new Zeilen();
        Zeilen.Add(new Zeile(Kopfzeile));
    }

    public List<string> GetReferenzspalten(List<string> referenzmerkmale)
    {
        List<string> xxx = new List<string>();

        foreach (var spalteDerKopfzeile in this.Zeilen.FirstOrDefault())
        {
            var x = Aliasse.Where(x => x.Contains(spalteDerKopfzeile.ToLower())).FirstOrDefault();

            if (x != null)
            {
                foreach (var item in x)
                {
                    if (item != null && referenzmerkmale.Contains(item.ToLower()))
                    {
                        if (!xxx.Contains(item))
                        {
                            xxx.Add(item);
                        }
                    }
                }
            }
        }
        return xxx;
    }
}