using DocumentFormat.OpenXml.Office2013.Excel;

public class Menüeintrag
{
    public string Titel { get; set; }
    public string DateiPfad { get; private set; }
    public string Beschreibung { get; set; }
    public List<string> Kopfzeile { get; private set; }
    public SchBa SchBa { get; private set; }
    public Marks Marks { get; private set; }
    public ExpLe ExpLe { get; private set; }
    public StgrS StgrS { get; private set; }
    public Schülers Schülers { get; private set; }
    public SchAd SchAd { get; private set; }
    public Adres Adres { get; private set; }
    public AllAd AllAd { get; private set; }
    public Simss Simss { get; private set; }
    public List<object> ImportDateien { get; private set; }
    public bool DateienVorhanden { get; private set; }

    public Menüeintrag(string kennung, List<object> importdateien = null)
    {
        int count = 0;

        if (importdateien != null)
        {
            foreach (var item in importdateien)
            {
                if (item is IEnumerable<object> list)
                {
                    count += list.Count();
                }
            }
        }
        

        
        if (kennung == "SchuelerAdre")
        {
            Titel = "SchuelerAdressen";
            DateiPfad = "ImportFürSchILD\\SchuelerAdressen.dat";
            Beschreibung = "...";
            Kopfzeile = new List<string>() { "Nachname", "Vorname", "Geburtsdatum", "Adressart", "Name1", "Name2", "Straße", "PLZ", "Ort", "1. Tel.-Nr.", "2. Tel.-Nr.", "E-Mail", "Betreuer Nachname", "Betreuer Vorname", "Betreuer Anrede", "Betreuer Tel.-Nr.", "Betreuer E-Mail", "Betreuer Abteilung", "Vertragsbeginn", "Vertragsende", "Fax-Nr.", "Bemerkung", "Branche", "Zusatz 1", "Zusatz 2", "SchILD-Adress-ID", "externe Adress-ID" };
            ImportDateien = importdateien;
            DateienVorhanden = count > 0 ? true : false;

        }
        if (kennung == "SchuelerTele")
        {
            Titel = "SchuelerTelefonnummern";
            DateiPfad = "ImportFürSchILD\\SchuelerTelefonnummern.dat";
            Beschreibung = "...";
            Kopfzeile = new List<string>() { "Nachname", "Vorname", "Geburtsdatum", "Telefonnr.", "Art" };
            ImportDateien = importdateien;
            DateienVorhanden = count > 0 ? true : false;
        }
        
        if (kennung == "PDF-Kennwort")
        {
            Titel = "Kopien von PDF-Dateien auf dem Desktop mit Kennwort versehen";
            DateiPfad = "";
            Beschreibung = "Von allen PDF-Dateien auf dem Desktop werden verschlüsselte Kopien erstellt. Jeder Kopie wird das Suffix '-Kennwort' angehangen. Die Originale bleiben unberührt. Unterordner bleiben unberücksichtigt.";
            Kopfzeile = new List<string>();
        }
        if (kennung == "NFS365")
        {
            Titel = "Importdatei für Netman & 365 erstellen";
            DateiPfad = "ImportFürNfs";
            Beschreibung = "Es wird die Importdatei für Netman for schools und Office365 erstellt.";
            Kopfzeile = new List<string>() { "Lfd Nr", "Klasse: Schuljahr", "Klasse", "Schüler: Nachname", "Schüler: Vorname", "Schüler: Ort", "Schüler: Plz", "Schüler: Straße", "Schüler: Geschlecht", "Schüler: Telefon", "Schüler: Barcode", "Schüler: Geburtsdatum", "Klasse: Klassenleiter", "Betrieb: Briefadresse" };
            DateienVorhanden = count > 0 ? true: false;
            ImportDateien = importdateien;
        }
    }

    public Menüeintrag(string kennung, AllAd allAd, Simss simss)
    {
        if (kennung == "SchuelerBasi")
        {
            Titel = "SchuelerBasisdaten.dat aus Atlantis-SIM.txt erstellen";
            DateiPfad = @"ImportFürSchild\SchuelerBasisdaten.dat";
            Beschreibung = "Enthält die Basis-Stammdaten der Schüler, insbesondere solche, die für die Statistik relevant sind. Zusätzlich zur SchuelerBasisdaten.dat werden weitere Dateien zu Adrssen, Telefonnummern und Betrieben erzeugt.";
            Kopfzeile = new List<string> { "Nachname", "Vorname", "Geburtsdatum", "Geschlecht", "Status", "PLZ", "Ort", "Straße", "Aussiedler", "1. Staatsang.", "Konfession", "StatistikKrz Konfession", "Aufnahmedatum", "Abmeldedatum Religionsunterricht", "Anmeldedatum Religionsunterricht", "Schulpflicht erf.", "Reform-Pädagogik", "Nr. Stammschule", "Jahr", "Abschnitt", "Jahrgang", "Klasse", "Schulgliederung", "OrgForm", "Klassenart", "Fachklasse", "Noch frei", "Verpflichtung Sprachförderkurs", "Teilnahme Sprachförderkurs", "Einschulungsjahr", "Übergangsempf. JG5", "Jahr Wechsel S1", "1. Schulform S1", "Jahr Wechsel S2", "Förderschwerpunkt", "2. Förderschwerpunkt", "Schwerstbehinderung", "Autist", "LS Schulnr.", "LS Schulform", "Herkunft", "LS Entlassdatum", "LS Jahrgang", "LS Versetzung", "LS Reformpädagogik", "LS Gliederung", "LS Fachklasse", "LS Abschluss", "Abschluss", "Schulnr. neue Schule", "Zuzugsjahr", "Geburtsland Schüler", "Geburtsland Mutter", "Geburtsland Vater", "Verkehrssprache", "Dauer Kindergartenbesuch", "Ende Eingliederungsphase", "Ende Anschlussförderung" };
            AllAd = allAd;
            Simss = simss;
        }
    }

    public Menüeintrag(string kennung, SchBa schBa, SchAd schAd, Adres adres)
    {
        if (kennung == "WebuntisImpo")
        {
            Titel = "Import für Webuntis aus Schild-Basisdaten erstellen (inkl. Fotos)";
            DateiPfad = @"ImportFürWebuntis\SchuelerBasisdaten.csv";
            Beschreibung = @"Enthält alle Schüler, ...";
            Kopfzeile = new List<string> { "Schlüssel", "E-Mail", "Familienname", "Vorname", "Klasse", "Kurzname", "Geschlecht", "Geburtsdatum", "Eintrittsdatum", "Austrittsdatum", "Telefon", "Mobil", "Strasse", "PLZ", "Ort", "ErzName", "ErzMobil", "ErzTelefon", "Volljährig", "BetriebName", "BetriebStrasse", "BetriebPlz", "BetriebOrt", "BetriebTelefon", "O365Identität", "Benutzername" };
            SchBa = schBa;
            SchAd = schAd;
            Adres = adres;
        }
    }

    public Menüeintrag(string kennung, ExpLe expLe, StgrS studentgroups, Schülers schülers)
    {
        if (kennung == "Unterrichtsverteilung")
        {
            Titel = "Unterrichtsverteilung (mit Noten) aus Webuntis-Dateien erzeugen";
            DateiPfad = @"ImportFürSchild\SchuelerLeistungsdaten.dat";
            Beschreibung = "Beschreibung: Enthält die Leistungsdaten (Fächer und Noten) eines Lernabschnittes (=Halbjahr oder Quartal) der Schüler. Zudem werden fehlende Fächer erstellt. Ergänzte Fächer müssen in SchILD nachbearbeitet werden. Fehlende Lernabschnitte werden ebenfalls erstellt. Grundsätzlich werden Lernabschnitte in SchILD angelegt.";
            Kopfzeile = new List<string> { "Nachname", "Vorname", "Geburtsdatum", "Jahr", "Abschnitt", "Fach", "Fachlehrer", "Kursart", "Kurs", "Note", "Abiturfach", "Wochenstd.", "Externe Schulnr.", "Zusatzkraft", "Wochenstd. ZK", "Jahrgang", "Jahrgänge", "Fehlstd.", "unentsch. Fehlstd." };
            ExpLe = expLe;
            StgrS = studentgroups;
            Schülers = schülers;
        }
    }

    public Menüeintrag(string kennung, SchBa schBa, Marks marks)
    {
        if (kennung == "SchuelerTeil")
        {
            Titel = "SchuelerTeilleistungen.dat...";
            DateiPfad = @"ImportFürSchild\SchuelerTeileistungen.dat";
            Beschreibung = "Enthält die ...";
            Kopfzeile = new List<string> { "Nachname", "Vorname", "Geburtsdatum", "Jahr", "Abschnitt", "Fach", "Datum", "Teilleistung", "Note", "Bemerkung", "Lehrkraft" };
            SchBa = schBa;
            Marks = marks;
        }
    }

    public Menüeintrag(string v, SchBa schBa)
    {
        SchBa = schBa;
    }

    internal bool Display(Simss simss, AllAd allAd, SchBa schBa, Marks marks, ExpLe expLe, SchAd schAd, Adres adres, Schülers schülers, StgrS stgrS)
    {   
        bool result = false;

        if (Simss != null)
        {
            if (simss.DateiPfad == null)
            {
                Global.ZeileSchreiben(0, "Sim.csv wird benötigt", simss.Count().ToString(), new Exception("aber nicht vorhanden"), simss.Hinweise );
                result = true;
            }
            else
            {
                Global.ZeileSchreiben(0, simss.DateiPfad, simss.Count().ToString(), null, null);
            }
        }
        if (AllAd != null)
        {
            if (allAd.DateiPfad == null)
            {
                Global.ZeileSchreiben(0, "Adressen.csv wird benötigt", allAd.Count().ToString(), new Exception("aber nicht vorhanden"), Simss.Hinweise);
                result = true;
            }
            else
            {
                Global.ZeileSchreiben(0, allAd.DateiPfad, allAd.Count().ToString(), null, null);
            }
        }
        if (SchBa != null)
        {
            if (schBa.DateiPfad == null)
            {
                Global.ZeileSchreiben(0, "Schuelerbasisdaten.dat wird benötigt", schBa.Count().ToString(), new Exception("aber nicht vorhanden"), schBa.Hinweise);
                result = true;
            }
            else
            {
                Global.ZeileSchreiben(0, schBa.DateiPfad, schBa.Count().ToString(), null, null);
            }
        }
        if (Marks != null)
        {
            if (marks.DateiPfad == null)
            {
                Global.ZeileSchreiben(0, "marksPerLesson.csv wird benötigt", marks.Count().ToString(), new Exception("aber nicht vorhanden"), marks.Hinweise);
                result = true;
            }
            else
            {
                Global.ZeileSchreiben(0, marks.DateiPfad, marks.Count().ToString(), null, null);
            }
        }
        if (ExpLe != null)
        {
            if (expLe.DateiPfad == null)
            {
                Global.ZeileSchreiben(0, "ExportLessons.csv wird benötigt", expLe.Count().ToString(), new Exception("aber nicht vorhanden"), expLe.Hinweise);
                result = true;
            }
            else
            {
                Global.ZeileSchreiben(0, expLe.DateiPfad, expLe.Count().ToString(), null, null);
            }
        }
        if (SchAd != null)
        {
            if (schAd.DateiPfad == null)
            {
                Global.ZeileSchreiben(0, "SchuelerAdressen.dat wird benötigt", schAd.Count().ToString(), new Exception("aber nicht vorhanden"), schAd.Hinweise);
                result = true;
            }
            else
            {
                Global.ZeileSchreiben(0, schAd.DateiPfad, schAd.Count().ToString(), null, null);
            }
        }
        if (Adres != null)
        {
            if (adres.DateiPfad == null)
            {
                Global.ZeileSchreiben(0, "Adressen.dat wird benötigt", adres.Count().ToString(), new Exception("aber nicht vorhanden"), adres.Hinweise);
                result = true;
            }
            else
            {
                Global.ZeileSchreiben(0, adres.DateiPfad, adres.Count().ToString(), null, null);
            }
        }
        if (Schülers != null)
        {
            if (schülers.DateiPfad == null)
            {
                Global.ZeileSchreiben(0, "SchildSchuelerExport wird benötigt", schülers.Count().ToString(), new Exception("aber nicht vorhanden"), schülers.Hinweise);
                result = true;
            }
            else
            {
                Global.ZeileSchreiben(0, schülers.DateiPfad, schülers.Count().ToString(), null, null);
            }
        }
        if (StgrS != null)
        {
            if (stgrS.DateiPfad == null)
            {
                Global.ZeileSchreiben(0, "StudentgroupStudents.csv wird benötigt", stgrS.Count().ToString(), new Exception("aber nicht vorhanden"), stgrS.Hinweise);
                result = true;
            }
            else
            {
                Global.ZeileSchreiben(0, stgrS.DateiPfad, stgrS.Count().ToString(), null, null);
            }
        }
        if (result == true)
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  Fehlende Dateien müssen zuerst nach Vorgabe erstellt und abgespeichert werden.");
            Console.ResetColor();
            Console.WriteLine("");
            Console.WriteLine("    ö      : öffnet den Zielordner");
            Console.WriteLine("    x      : öffnet die Webseite der App");
            Console.WriteLine("    Anykey : beendet die App");
            Console.WriteLine("");
            Console.Write("       Ihre Auswahl: ");
            Console.ResetColor();
            var eingabe = Console.ReadLine();

            if (eingabe == "ö")
            {
                Global.OpenCurrentFolder();                
            }
            if (eingabe == "x")
            {
                Global.OpenWebseite("https://github.com/stbaeumer/Schule");                
            }
        }
        return result;
    }
}