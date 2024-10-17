using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Text;
using static Global;

Global.DisplayHeader();

using (var reader = new StreamReader("Alias.txt")) { GlobalCsvMappings.LoadMappingsFromFile("Alias.txt"); }

AbsencePerStudents absencePerStudents = new AbsencePerStudents(@"ExportAusWebuntis\AbsencePerStudent", "*.csv", "\t");
Students students = new Students(@"ExportAusWebuntis\Student_", "*.csv", "\t");
ExportLessons exportLessons = new ExportLessons(@"ExportAusWebuntis\ExportLessons", "*.csv", "\t");
MarksPerLessons marksPerLessons = new MarksPerLessons(@"ExportAusWebuntis\MarksPerLesson", "*.csv", "\t");
StudentgroupStudents studentgroupStudents = new StudentgroupStudents(@"ExportAusWebuntis\StudentgroupStudents", "*.csv", "\t");
Faecher faecher = new Faecher(@"ExportAusSchild\Faecher","*.dat");
SchuelerLeistungsdaten schuelerLeistungsdaten = new SchuelerLeistungsdaten(@"ExportAusSchild\SchuelerLeistungsdaten", "*.dat");
SchuelerLernabschnittsdaten schuelerLernabschnittsdaten = new SchuelerLernabschnittsdaten(@"ExportAusSchild\SchuelerLernabschnittsdaten", "*.dat");
SchuelerTeilleistungen schuelerTeilleistungen = new SchuelerTeilleistungen(@"ExportAusSchild\SchuelerTeilleistungen", "*.dat");
SchuelerBasisdaten schuelerBasisdaten = new SchuelerBasisdaten(@"ExportAusSchild\SchuelerBasisdaten", "*.dat");
Sims sims = new Sims(@"ExportAusAtlantis\sim.csv", "*.csv", ";");

do
{
    // Schüler generieren
    Schülers schuelers = new Schülers(schuelerBasisdaten);
    if (schuelers.Count == 0) { schuelers = new Schülers(students); }
    
    Datei zielDatei = new Menü().Display(new List<Menüeintrag>() 
    {
        schuelerBasisdaten.Menüeintrag(sims.Count()),
        schuelerLeistungsdaten.Menüeintrag(schuelerBasisdaten.Count(), exportLessons.Count()),
        schuelerTeilleistungen.Menüeintrag(schuelerBasisdaten.Count(), marksPerLessons.Count()),
        schuelerBasisdaten.Menüeintrag(),
    });
    
    do
    {
        Schülers iSuS = schuelers.GetIntessierendeSchuelers(zielDatei);
        if (iSuS.Keine()) { break; }
        var iKla = iSuS.Select(x => x.Klasse).Distinct().ToList();
        var iSim = sims.Interessierende(iKla);
        var iSba = schuelerBasisdaten.Interessierende(iKla);
        var iExL = exportLessons.Interessierende(iKla);
        var iApS = absencePerStudents.Interessierende(iKla);
        var iMpL = marksPerLessons.Interessierende(iKla);
        var iSgS = studentgroupStudents.Interessierende(iKla);

        // Alle Funktionen:

        if (zielDatei.DateiPfad.ToLower().Contains("basisdaten") && iSim.Count() > 0)
            zielDatei.Zeilen.AddRange(sims.GetZeilen(iSuS));
        
        if (zielDatei.DateiPfad.ToLower().Contains("leistungsdaten") && iExL.Count() > 0)
            zielDatei.Zeilen.AddRange(schuelerBasisdaten.GetZeilen(iApS, iSuS, iExL, iMpL, iSgS));

        //if (zielDatei.DateiPfad.ToLower().Contains("teilleistungen") && iMpL.Count() > 0)
            //zielDatei.Zeilen = schuelerBasisdaten.GetZeilen(iExL, iMpL, iSgS);




        // Dateien vergleichen
        //string vergleichsdateiDateiPfad = zielDatei.CheckFile();

        //if (vergleichsdateiDateiPfad != null && vergleichsdateiDateiPfad.ToLower().Contains("export"))
        //{
        //    Global.ZeileSchreiben(0, vergleichsdateiDateiPfad, "existiert", null);

        //    Datei vergleichsdatei = new Datei();// new Datei(vergleichsdateiDateiPfad);            
                                
        //    Global.ZeileSchreiben(0, vergleichsdateiDateiPfad, vergleichsdatei.Zeilen.Count().ToString(), null);

        //    if(zielDatei.DateienVergleichen(vergleichsdatei)) { break; }      
        //}

        zielDatei.Erstellen();
        

    } while (true);
    
    Global.DisplayHeader();

} while (true);
