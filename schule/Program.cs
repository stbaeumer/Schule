using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Text;

Global.Delimiter = ['|', ';', ',', '\t'];
Global.ReferenzMerkmale = new List<string>() { "Name", "Nachname", "Vorname", "Klasse", "Geburtsdatum" };
Global.DisplayHeader();
Aliasse aliasse = new Aliasse("Aliasse.xlsx");
Dateien dateien = new Dateien();

do
{
    Datei zielDatei = dateien.DisplayMenu(aliasse);
    Dateien InteressierendeDateien = new Dateien(dateien, zielDatei.BenötigteDateien, aliasse);

    Dateien sDateien = dateien.Interessierende(new string[] { @"ExportAusWebuntis\Student_" });    
    Schülers schuelers = new Schülers(sDateien, aliasse);
        
    do
    {
        Schülers interessierendeSchuelers = schuelers.GetIntessierendeSchuelers(zielDatei);
        if (interessierendeSchuelers.Keine()) { break; }
        InteressierendeDateien.InteressierendeZeilenFiltern(interessierendeSchuelers);
                
        object[] parameters = new object[] { InteressierendeDateien, interessierendeSchuelers };

        MethodInfo method = zielDatei.GetType().GetMethod(zielDatei.Funktionsname);
        method?.Invoke(zielDatei, parameters);

        string vergleichsdateiDateiPfad = zielDatei.CheckFile();

        if (vergleichsdateiDateiPfad != null && vergleichsdateiDateiPfad.ToLower().Contains("export"))
        {
            Global.ZeileSchreiben(0, vergleichsdateiDateiPfad, "existiert", null);

            Datei vergleichsdatei = new Datei(vergleichsdateiDateiPfad);
            vergleichsdatei.Zeilen.Add(new Zeile(zielDatei.Zeilen.Where(x => x.IstKopfzeile).FirstOrDefault().Zellen, true));
            vergleichsdatei.AddZeilen();
                    
            Global.ZeileSchreiben(0, vergleichsdateiDateiPfad, vergleichsdatei.Zeilen.Count().ToString(), null);

            if(zielDatei.DateienVergleichen(vergleichsdatei)) { break; }                
        }

        zielDatei.Erstellen();

    } while (true);
    
    Global.DisplayHeader();

} while (true);
