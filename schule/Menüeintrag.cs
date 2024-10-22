public class Menüeintrag
{
    public string Titel { get; set; }
    public string DateiPfad { get; private set; }
    public string Beschreibung { get; set; }
    public List<string> Kopfzeile { get; private set; }

    public Menüeintrag(string kennung, int count = 1)
    {
        if (count > 0)
        {
            if (kennung == "SchuelerBasi")
            {
                Titel = "SchuelerBasisdaten.dat aus Atlantis-SIM.txt erstellen";
                DateiPfad = @"ImportFürSchild\SchuelerBasisdaten.dat";
                Beschreibung = "Enthält die Basis-Stammdaten der Schüler, insbesondere solche, die für die Statistik relevant sind.";
                Kopfzeile = new List<string> { "Nachname", "Vorname", "Geburtsdatum", "Geschlecht", "Status", "PLZ", "Ort", "Straße", "Aussiedler", "1. Staatsang.", "Konfession", "StatistikKrz Konfession", "Aufnahmedatum", "Abmeldedatum Religionsunterricht", "Anmeldedatum Religionsunterricht", "Schulpflicht erf.", "Reform-Pädagogik", "Nr. Stammschule", "Jahr", "Abschnitt", "Jahrgang", "Klasse", "Schulgliederung", "OrgForm", "Klassenart", "Fachklasse", "Noch frei", "Verpflichtung Sprachförderkurs", "Teilnahme Sprachförderkurs", "Einschulungsjahr", "Übergangsempf. JG5", "Jahr Wechsel S1", "1. Schulform S1", "Jahr Wechsel S2", "Förderschwerpunkt", "2. Förderschwerpunkt", "Schwerstbehinderung", "Autist", "LS Schulnr.", "LS Schulform", "Herkunft", "LS Entlassdatum", "LS Jahrgang", "LS Versetzung", "LS Reformpädagogik", "LS Gliederung", "LS Fachklasse", "LS Abschluss", "Abschluss", "Schulnr. neue Schule", "Zuzugsjahr", "Geburtsland Schüler", "Geburtsland Mutter", "Geburtsland Vater", "Verkehrssprache", "Dauer Kindergartenbesuch", "Ende Eingliederungsphase", "Ende Anschlussförderung" };
            }

            if (kennung == "WebuntisImpo")
            {
                Titel = "Import für Webuntis aus Schild-Basisdaten erstellen (inkl. Fotos)";
                DateiPfad = @"ImportFürWebuntis\SchuelerBasisdaten.csv";
                Beschreibung = @"Enthält alle Schüler, ...";
                Kopfzeile = new List<string> { "Schlüssel", "E-Mail", "Familienname", "Vorname", "Klasse", "Kurzname", "Geschlecht", "Geburtsdatum", "Eintrittsdatum", "Austrittsdatum", "Telefon", "Mobil", "Strasse", "PLZ", "Ort", "ErzName", "ErzMobil", "ErzTelefon", "Volljährig", "BetriebName", "BetriebStrasse", "BetriebPlz", "BetriebOrt", "BetriebTelefon", "O365Identität", "Benutzername" };
            }
            if (kennung == "SchuelerAdre")
            {
                Titel = "SchuelerAdressen";
                DateiPfad = "ImportFürSchILD\\SchuelerAdressen.dat";
                Beschreibung = "...";
                Kopfzeile = new List<string>() { "Nachname", "Vorname", "Geburtsdatum", "Adressart", "Name1", "Name2", "Straße", "PLZ", "Ort", "1. Tel.-Nr.", "2. Tel.-Nr.", "E-Mail", "Betreuer Nachname", "Betreuer Vorname", "Betreuer Anrede", "Betreuer Tel.-Nr.", "Betreuer E-Mail", "Betreuer Abteilung", "Vertragsbeginn", "Vertragsende", "Fax-Nr.", "Bemerkung", "Branche", "Zusatz 1", "Zusatz 2", "SchILD-Adress-ID", "externe Adress-ID" };
            }
            if (kennung == "SchuelerTele")
            {
                Titel = "SchuelerTelefonnummern";
                DateiPfad = "ImportFürSchILD\\SchuelerTelefonnummern.dat";
                Beschreibung = "...";
                Kopfzeile = new List<string>() { "Nachname", "Vorname", "Geburtsdatum", "Telefonnr.", "Art" };
            }
            if (kennung == "SchuelerLeis")
            {
                Titel = "SchuelerLeistungsdaten.dat...";
                DateiPfad = @"ImportFürSchild\SchuelerLeistungsdaten.dat";
                Beschreibung = "Enthält die ...";
                Kopfzeile = new List<string> { "Nachname", "Vorname", "Geburtsdatum", "Jahr", "Abschnitt", "Fach", "Fachlehrer", "Kursart", "Kurs", "Note", "Abiturfach", "Wochenstd.", "Externe Schulnr.", "Zusatzkraft", "Wochenstd. ZK", "Jahrgang", "Jahrgänge", "Fehlstd.", "unentsch. Fehlstd." };
            }
            if (kennung == "SchuelerTeil")
            {
                Titel = "SchuelerTeilleistungen.dat...";
                DateiPfad = @"ImportFürSchild\SchuelerTeileistungen.dat";
                Beschreibung = "Enthält die ...";
                Kopfzeile = new List<string> { "Nachname", "Vorname", "Geburtsdatum", "Jahr", "Abschnitt", "Fach", "Datum", "Teilleistung", "Note", "Bemerkung", "Lehrkraft" };
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
            }
        }
    }
}