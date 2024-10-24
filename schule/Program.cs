﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Text;
using System.IO;
using static Global;
using DocumentFormat.OpenXml.Office.MetaAttributes;
using DocumentFormat.OpenXml.Drawing;

Console.WindowHeight = 40;
Global.DisplayHeader();

Schülers schülers = new Schülers(@"ExportAusSchild\SchildSchuelerExport", "*.txt");

Simss simss = new Simss(@"ExportAusAtlantis\sim.csv", "*.csv", ";");
AllAd allAd = new AllAd(@"ExportAusAtlantis\Adressen.csv", "*.csv", ";");

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


if (schülers.Count == 0) { schülers = new Schülers(studs); }

do
{   
    Menüeintrag menüauswahl = new Menü().Display(new List<Menüeintrag>() 
    {
         new Menüeintrag("SchuelerBasi", allAd, simss),
         new Menüeintrag("Unterrichtsverteilung", expLe, stgrS, schülers),
         new Menüeintrag("WebuntisImpo", schBa, schAd, adres),
         new Menüeintrag("SchuelerTeil", schBa, marks),
         new Menüeintrag("Klassen", schBa ),
         new Menüeintrag("PDF-Kennwort"),
         new Menüeintrag("NFS365"),
    });

    if(menüauswahl.Display(simss, allAd, schBa, marks, expLe, schAd, adres, schülers, stgrS)) { break; }

    do
    {
        List<Datei> z = new List<Datei>() { new Datei(menüauswahl) };

        if (menüauswahl.Titel.Contains("PDF-Dateien auf dem Desktop mit Kennwort"))
        {
            var pdfdateien = new PdfDateien();
            break;
        }

        var iSchuS = schülers.Interessierende(z[0]); if (iSchuS.Keine()) { break; }
        var iKlass = iSchuS.Select(x => x.Klasse).Distinct().ToList();

        var iSimss = simss.Interessierende(iKlass);
        var iSchBa = schBa.Interessierende(iKlass);
        var iExpLe = expLe.Interessierende(iKlass);
        var iAbsSt = absSt.Interessierende(iKlass);
        var iMarks = marks.Interessierende(iKlass);
        var iStgrS = stgrS.Interessierende(iKlass);
        var iSchAd = schAd.Interessierende(iSchuS);
        var iAllAd = allAd.Interessierende(iKlass);
        var iAdres = adres.Interessierende(iSchuS);

        if (menüauswahl.Display(iSimss, iAllAd, iSchBa, iMarks, iExpLe, iSchAd, iAdres, iSchuS, iStgrS)) { break; }

        if (z[0].Titel.StartsWith("SchuelerBasisdaten") && iSimss.Count() > 0)
        {
            z[0].Zeilen.AddRange(simss.GetSchuelerBasisdaten(iSchuS));

            // SchuelerAdressen.dat
            // Beschreibung: Enthält weitere Adressen (z.B. von Ausbildungsbetrieben) der Schüler 
            // Hier werden nicht die Eltern erfasst
            var zieldatei = new Datei("ImportFürSchILD\\SchuelerAdressen.dat", "", new List<string>() { "Nachname", "Vorname", "Geburtsdatum", "Adressart", "Name1", "Name2", "Straße", "PLZ", "Ort", "1. Tel.-Nr.", "2. Tel.-Nr.", "E-Mail", "Betreuer Nachname", "Betreuer Vorname", "Betreuer Anrede", "Betreuer Tel.-Nr.", "Betreuer E-Mail", "Betreuer Abteilung", "Vertragsbeginn", "Vertragsende", "Fax-Nr.", "Bemerkung", "Branche", "Zusatz 1", "Zusatz 2", "SchILD-Adress-ID", "externe Adress-ID" });
            zieldatei.Zeilen.AddRange(allAd.GetSchuelerAdressen(iSchuS));
            z.Add(zieldatei);
            
            zieldatei = new Datei("ImportFürSchILD\\SchuelerTelefonnummern.dat", "", new List<string>() { "Nachname", "Vorname", "Geburtsdatum", "Telefonnr.", "Art" });
            zieldatei.Zeilen.AddRange(allAd.GetSchuelerTelefonnummern(iSchuS));
            z.Add(zieldatei);
            
            zieldatei = new Datei("ImportFürSchILD\\SchuelerZusatzdaten.dat", "", new List<string>() { "Nachname", "Vorname", "Geburtsdatum", "Namenszusatz", "Geburtsname", "Geburtsort", "Ortsteil", "Telefon-Nr.", "E-Mail", "2. Staatsang.", "Externe ID-Nr", "Sportbefreiung", "Fahrschülerart", "Haltestelle", "Einschulungsart", "Entlassdatum", "Entlassjahrgang", "Datum Schulwechsel", "Bemerkungen", "BKAZVO", "BeginnBildungsgang", "Anmeldedatum", "Bafög", "EP-Jahre", "Fax/Mobilnr", "Ausweisnummer", "schulische E-Mail" });
            zieldatei.Zeilen.AddRange(allAd.GetSchuelerZusatzdaten(iSchuS));            
            z.Add(zieldatei);

            zieldatei = new Datei("ImportFürSchILD\\SchuelerErzieher.dat", "", new List<string>() { "Nachname", "Vorname", "Geburtsdatum", "Erzieherart", "Anrede 1.Person", "Titel 1.Person", "Nachname 1.Person", "Vorname 1.Person", "Anrede 2.Person", "Titel 2.Person", "Nachname 2.Person", "Vorname 2.Person", "Straße", "PLZ", "Ort", "Ortsteil", "E-Mail", "Anschreiben" });
            zieldatei.Zeilen.AddRange(allAd.GetSchuelerErzieher(iSchuS));
            z.Add(zieldatei);
        }

        if (z[0].Titel.StartsWith("Unterrichtsverteilung") && iExpLe.Count() > 0) 
        {
            z[0].Zeilen.AddRange(iSchuS.GetLeistungsdaten(iAbsSt, iExpLe, iMarks, iStgrS));
        }
        
        if (z[0].Titel.StartsWith("SchuelerTeilleistu") && iSchuS.Count() > 0)
            z[0].Zeilen.AddRange(schBa.GetSchuelerTeilleistung(iSchAd, iSchAd));
        if (z[0].Titel.StartsWith("Kopien von PDF-Dateien") && iSchuS.Count() > 0)
        {
            var pdfdateien = new PdfDateien("PDF-Zeugnisse", "PDF-Zeugnisse-Einzeln", schülers);
        }

        if (z[0].Titel.StartsWith("Importdatei für Netman") && iSchuS.Count() > 0)
        {
            z[0].Zeilen.AddRange(schBa.GetNfsUnd365(iSchuS));
        }

        //Dateien vergleichen

        foreach (var zielDatei in z)
        {
            Datei vergleichsdatei = new Datei();            
            vergleichsdatei.DateiPfad = vergleichsdatei.CheckFile(zielDatei.DateiPfad.Replace("Export", "Import"));

            //if (vergleichsdateiDateiPfad != null && vergleichsdateiDateiPfad.ToLower().Contains("export"))
            //{
            //    Global.ZeileSchreiben(0, vergleichsdateiDateiPfad, "existiert", null);

            //    Datei vergleichsdatei = new Datei();// new Datei(vergleichsdateiDateiPfad);            

            //    Global.ZeileSchreiben(0, vergleichsdateiDateiPfad, vergleichsdatei.Zeilen.Count().ToString(), null);

            //    if (zielDatei.DateienVergleichen(vergleichsdatei)) { break; }
            //}
            zielDatei.Erstellen();
        }
    } while (true);
    
    Global.DisplayHeader();

} while (true);
