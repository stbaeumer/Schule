using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Text;
using System.IO;
using static Global;

Console.WindowHeight = 40;
Global.DisplayHeader();
Global.Ausgaben = new Ausgaben();
SchAd schAd = new SchAd(@"ExportAusSchild\SchuelerAdressen");
Adres adres = new Adres(@"ExportAusSchild\Adressen");
Kurse kurse = new Kurse(@"ExportAusSchild\Kurse");
SchTe schTe = new SchTe(@"ExportAusSchild\SchuelerTelefonnummern");
SchBa schBa = new SchBa(@"ExportAusSchild\SchuelerBasisdaten");
Faech faech = new Faech(@"ExportAusSchild\Faecher");
ScLei scLei = new ScLei(@"ExportAusSchild\SchuelerLeistungsdaten");
ScLer scLer = new ScLer(@"ExportAusSchild\SchuelerLernabschnittsdaten");
ScTei scTei = new ScTei(@"ExportAusSchild\SchuelerTeilleistungen");
Marks marks = new Marks(@"ExportAusWebuntis\MarksPerLesson", "*.csv", "\t");
ExpLe expLe = new ExpLe(@"ExportAusWebuntis\ExportLessons", "*.csv", "\t");
StgrS stgrS = new StgrS(@"ExportAusWebuntis\StudentgroupStudents", "*.csv", "\t");
Studs studs = new Studs(@"ExportAusWebuntis\Student_", "*.csv", "\t");
AbsSt absSt = new AbsSt(@"ExportAusWebuntis\AbsencePerStudent", "*.csv", "\t");
Simss simss = new Simss(@"ExportAusAtlantis\sim.csv", "*.csv", ";");
AllAd allAd = new AllAd(@"ExportAusAtlantis\AlleAdressen.csv", "*.csv", ";");

// Schüler generieren
Schülers schus = new Schülers(schBa);
if (schus.Count == 0) { schus = new Schülers(studs); }

do
{   
    Menüeintrag menüauswahl = new Menü().Display(new List<Menüeintrag>() 
    {
         new Menüeintrag("SchuelerBasi", simss.Count),
         new Menüeintrag("WebuntisImpo", schBa.Count + schAd.Count + adres.Count),
         new Menüeintrag("SchuelerLeis", schBa.Count + expLe.Count),
         new Menüeintrag("SchuelerTeil", schBa.Count + marks.Count()),
         new Menüeintrag("SchuelerAdre", allAd.Count + schBa.Count()),
         new Menüeintrag("SchuelerTele", allAd.Count + schBa.Count()),
         new Menüeintrag("PDF-Kennwort"),
    });

    do
    {
        Datei zielDatei = new Datei(menüauswahl);

        if (menüauswahl.Titel.Contains("PDF-Dateien auf dem Desktop mit Kennwort"))
        {
            var pdfdateien = new PdfDateien();
            break;
        }

        var iSchuS = schus.Interessierende(zielDatei); if (iSchuS.Keine()) { break; }
        var iKlass = iSchuS.Select(x => x.Klasse).Distinct().ToList();
        var iSimss = simss.Interessierende(iKlass);
        var iSchBa = schBa.Interessierende(iKlass);
        var iExpLe = expLe.Interessierende(iKlass);
        var iAbsSt = absSt.Interessierende(iKlass);
        var iMarks = marks.Interessierende(iKlass);
        var iStgrS = stgrS.Interessierende(iKlass);
        var iSchAd = schAd.Interessierende(iSchuS);        
                
        if (zielDatei.Titel.StartsWith("SchuelerBasisdaten") && iSimss.Count() > 0)
            zielDatei.Zeilen.AddRange(simss.GetSchuelerBasisdaten(iSchuS));
        if (zielDatei.Titel.StartsWith("SchuelerAdressen") && iSimss.Count() > 0)
            zielDatei.Zeilen.AddRange(allAd.GetSchuelerAdressen(iSchuS));
        if (zielDatei.Titel.StartsWith("SchuelerTelefonn") && iSimss.Count() > 0)
            zielDatei.Zeilen.AddRange(allAd.GetSchuelerTelefonnummern(iSchuS));
        if (zielDatei.Titel.StartsWith("SchuelerLeistungsd") && iExpLe.Count() > 0)
            zielDatei.Zeilen.AddRange(schBa.GetLeistungsdaten(iAbsSt, iSchuS, iExpLe, iMarks, iStgrS));
        if (zielDatei.Titel.StartsWith("SchuelerTeilleistu") && iSchuS.Count() > 0)
            zielDatei.Zeilen.AddRange(schBa.GetSchuelerTeilleistung(iSchAd, iSchAd));

        //if (zielDatei.Titel.StartsWith("Kopien von PDF-Dat") && iSchuS.Count() > 0) { 
        //    var pdfdateien = new PdfDateien("PDF-Zeugnisse", "PDF-Zeugnisse-Einzeln", schus);}


        //Dateien vergleichen
        string vergleichsdateiDateiPfad = zielDatei.CheckFile();

        if (vergleichsdateiDateiPfad != null && vergleichsdateiDateiPfad.ToLower().Contains("export"))
        {
            Global.ZeileSchreiben(0, vergleichsdateiDateiPfad, "existiert", null);

            Datei vergleichsdatei = new Datei();// new Datei(vergleichsdateiDateiPfad);            

            Global.ZeileSchreiben(0, vergleichsdateiDateiPfad, vergleichsdatei.Zeilen.Count().ToString(), null);

            if (zielDatei.DateienVergleichen(vergleichsdatei)) { break; }
        }

        zielDatei.Erstellen();
        

    } while (true);
    
    Global.DisplayHeader();

} while (true);
