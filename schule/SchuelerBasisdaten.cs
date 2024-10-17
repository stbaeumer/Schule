using DocumentFormat.OpenXml.Bibliography;
using PdfSharp.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class SchuelerBasisdaten : List<SchuelerBasisdatum>
{
    public SchuelerBasisdaten()
    {
    }

    public SchuelerBasisdaten(string dateiPfad, string dateiendungen)
    {
        var hinweise = new string[] {
                "Exportieren Sie die Datei aus SchILD, indem Sie:",
                "In SchILD den Pfad gehen: Datenaustausch > Schnittstelle > Export",
                "Die Datei auswählen.",
                "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() };

        List<object> result = Global.LiesDateien(dateiPfad, dateiendungen, hinweise);

        foreach (var r in result)
        {
            SchuelerBasisdatum s = (SchuelerBasisdatum)r;
            this.Add(s);
        }
    }

    

    public Zeilen GetZeilen(AbsencePerStudents absencePerStudents, Schülers interessierendeSchuelers, ExportLessons exportLessons, MarksPerLessons marksPerLessons, StudentgroupStudents studentgroupStudents)
    {
        Zeilen zeilen = new Zeilen();

        try
        {
            foreach (var schueler in interessierendeSchuelers)
            {
                SchuelerBasisdatum sB = this.Find(x => x.Vorname == schueler.Vorname && x.Nachname == schueler.Nachname && x.Klasse == schueler.Klasse);


                foreach (var zeile in (from zeile in exportLessons
                                       where zeile.klassen.Split('~').Contains(schueler.Klasse)
                                       select zeile).ToList())
                {
                    string note = schueler.GetNote(marksPerLessons);

                    if (zeile.klassen.Contains(schueler.Klasse))
                    {
                        if (zeile.studentgroup == "") // Klassenunterricht werden immer hinzugefügt
                        {
                            zeilen.Add(new Zeile(new List<string>()
                            {
                                sB.Nachname,
                                sB.Vorname,
                                sB.Geburtsdatum,
                                sB.Jahr,
                                sB.Abschnitt,
                                zeile.subject,
                                zeile.teacher,
                                "PUK",  // Pflichtunterricht im Klassenverband
                                "",     // Kein Kursname
                                note,
                                zeile.periods,
                                "", // ExterneSchulnr
                                "", // Zusatzkraft
                                "", // WochenstdZK
                                "", // Jahrgang
                                "", // Jahrgänge
                                schueler.GetFehlstd(absencePerStudents),
                                schueler.GetUnentFehlstd(absencePerStudents)
                            }));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.ReadKey();
        }
        return zeilen;
    }

    internal SchuelerBasisdaten Interessierende(List<string> interesserendeKlassen)
    {
        var x = this.Where(x => interesserendeKlassen.Contains(x.Klasse)).ToList();

        var xx = new SchuelerBasisdaten();
        xx.AddRange(x);
        Global.ZeileSchreiben(0, "interessierende ", x.Count().ToString(), null, null);

        return xx;
    }

    internal Menüeintrag Menüeintrag(int count)
    {
        if (count > 0)
        {
            return new Menüeintrag(
                "SchuelerBasisdaten.dat aus Atlantis-SIM.txt und Webuntis-Student_...csv erzeugen",
                @"ImportFürSchild\SchuelerBasisdaten.dat",
                @"Beschreibung: Enthält die Basis-Stammdaten der Schüler, insbesondere solche, die für die Statistik relevant sind.",
                new List<string> { "Nachname", "Vorname", "Geburtsdatum", "Geschlecht", "Status", "PLZ", "Ort", "Straße", "Aussiedler", "1. Staatsang.", "Konfession", "StatistikKrz Konfession", "Aufnahmedatum", "Abmeldedatum Religionsunterricht", "Anmeldedatum Religionsunterricht", "Schulpflicht erf.", "Reform-Pädagogik", "Nr. Stammschule", "Jahr", "Abschnitt", "Jahrgang", "Klasse", "Schulgliederung", "OrgForm", "Klassenart", "Fachklasse", "Noch frei", "Verpflichtung Sprachförderkurs", "Teilnahme Sprachförderkurs", "Einschulungsjahr", "Übergangsempf. JG5", "Jahr Wechsel S1", "1. Schulform S1", "Jahr Wechsel S2", "Förderschwerpunkt", "2. Förderschwerpunkt", "Schwerstbehinderung", "Autist", "LS Schulnr.", "LS Schulform", "Herkunft", "LS Entlassdatum", "LS Jahrgang", "LS Versetzung", "LS Reformpädagogik", "LS Gliederung", "LS Fachklasse", "LS Abschluss", "Abschluss", "Schulnr. neue Schule", "Zuzugsjahr", "Geburtsland Schüler", "Geburtsland Mutter", "Geburtsland Vater", "Verkehrssprache", "Dauer Kindergartenbesuch" });
        }
        return null;
    }

    internal Menüeintrag Menüeintrag()
    {
        return new Menüeintrag(
            "PDF-Zeugnis-Stapel in PDF-Einzeldateien umwandeln und sprechend benennen",
            @"PDF-Zeugnisse-Einzeln\Protokoll.txt",
            @"Beschreibung: PDF-Klassensätze aus Atlantis werden nach Name, Datum, Zeugnisart und Klasse durchsucht. Für jedes gefundene Zeugnis wird eine PDF-Datei mit sprechendem Namen erstellt.",
            new List<string> { "Nachname", "Vorname", "Geburtsdatum", "Klasse", "Zeugnisart", "Datum" }
            );
    }
}