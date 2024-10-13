using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Dateien : List<Datei>
{
    public Dateien()
    {
        var datbeschreibung = new string[] {
            "Exportieren Sie die Datei aus SchILD, indem Sie:", "In SchILD den Pfad gehen: Datenaustausch > Schnittstelle > Export", "Die Datei auswählen.", "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory()
        };

        this.Add(new Datei(@"ImportFürSchild\SchuelerBasisdaten.dat",        // Dateipfad
datbeschreibung,            // Hinweise zum Download
new List<string> { "Nachname", "Vorname", "Geburtsdatum", "Geschlecht", "Status", "PLZ", "Ort", "Straße", "Aussiedler", "1. Staatsang.", "Konfession", "StatistikKrz Konfession", "Aufnahmedatum", "Abmeldedatum Religionsunterricht", "Anmeldedatum Religionsunterricht", "Schulpflicht erf.", "Reform-Pädagogik", "Nr. Stammschule", "Jahr", "Abschnitt", "Jahrgang", "Klasse", "Schulgliederung", "OrgForm", "Klassenart", "Fachklasse", "Noch frei", "Verpflichtung Sprachförderkurs", "Teilnahme Sprachförderkurs", "Einschulungsjahr", "Übergangsempf. JG5", "Jahr Wechsel S1", "1. Schulform S1", "Jahr Wechsel S2", "Förderschwerpunkt", "2. Förderschwerpunkt", "Schwerstbehinderung", "Autist", "LS Schulnr.", "LS Schulform", "Herkunft", "LS Entlassdatum", "LS Jahrgang", "LS Versetzung", "LS Reformpädagogik", "LS Gliederung", "LS Fachklasse", "LS Abschluss", "Abschluss", "Schulnr. neue Schule", "Zuzugsjahr", "Geburtsland Schüler", "Geburtsland Mutter", "Geburtsland Vater", "Verkehrssprache", "Dauer Kindergartenbesuch" },       // Kopfzeile
"SchuelerBasisdaten.dat aus Atlantis-SIM.txt und Webuntis-Student_...csv erzeugen",  // Titel der Datei; wird im Menü angezeigt
@"Beschreibung: Enthält die Basis-Stammdaten der Schüler, insbesondere solche, die für die Statistik relevant sind.", // Beschreibung
new string[] { "Student_", "sim" },                     // andere benötigte Dateien. Die erste Datei bestimmt die Anzahl der Zeilen der herauskommenden Datei
"SchuelerBasisdaten"));                                 // Funktionsname (Case-Sensitive)

        this.Add(new Datei(
@"ImportFürSchild\SchuelerTeilleistungen.dat",                                                                                                      // Dateipfad
datbeschreibung,                                                                                                                        // Hinweise zum Download
new List<string> { "Nachname", "Vorname", "Geburtsdatum", "Jahr", "Abschnitt", "Fach", "Datum", "Teilleistung", "Note", "Bemerkung", "Lehrkraft" }, // Kopfzeile
"SchuelerTeilleistungen.dat erzeugen",          // Titel der Datei; wird im Menü angezeigt
@"Beschreibung: Enthält die Teilleistungsdaten (Teilleistung und Noten) eines Lernabschnittes (=Halbjahr oder Quartal) der Schüler. ",                 // Beschreibung
new string[] { "MarksPerLesson", "SchuelerTeilleistungen", "Student_" },      // andere benötigte Dateien. Die erste Datei bestimmt die Anzahl der Zeilen der herauskommenden Datei
"SchuelerTeilleistungen"));                     // Funktionsname (Case-Sensitive)

        this.Add(new Datei(
@"PDF-Zeugnisse-Einzeln\Protokoll.txt",  // Dateipfad
new string[] { "Fügen Sie dem Ordner namens PDF - Zeugnisse eine mehrere PDF - Dateien hinzu.","Jede Datei kann mehrere Seiten umfassen.Schule.exe durchsucht jede Datei nach Nachname, Vorname, Geburtsatum, Zeugnisdatum und Zeugnisart.","Für jede * n identifizierte Schüler *in wird eine separate PDF - Datei erstellt. Der Dateiname setzt sich Schülername, Klasse, Zeugnisdatum zusammen." },                                // Hinweise zum Download
new List<string> { "Quelldatei", "Zieldatei" }, // Kopfzeile
"PDF-Zeugnis-Stapel in PDF-Einzeldateien umwandeln und sprechend benennen",          // Titel der Datei; wird im Menü angezeigt
@"Beschreibung: PDF-Klassensätze aus Atlantis werden nach Name, Datum, Zeugnisart und Klasse durchsucht. Für jedes gefundene Zeugnis wird eine PDF-Datei mit sprechendem Namen erstellt.",                 // Beschreibung
new string[] { "Student_" },      // andere benötigte Dateien. Die erste Datei bestimmt die Anzahl der Zeilen der herauskommenden Datei
"PdfZeugnisse"));                     // Funktionsname (Case-Sensitive)

        this.Add(new Datei(
@"ImportFürWebuntis\import.csv",  // Dateipfad
new string[] { "Importieren Sie die Benutzer inklusive Fotos nach Webuntis" },                                // Hinweise zum Download
new List<string> { "Quelldatei", "Zieldatei" }, // Kopfzeile
"Benutzer und Stammdaten nach Webuntis importieren (inkl. Bilder)",          // Titel der Datei; wird im Menü angezeigt
@"Beschreibung: PDF-Klassensätze aus Atlantis werden nach Name, Datum, Zeugnisart und Klasse durchsucht. Für jedes gefundene Zeugnis wird eine PDF-Datei mit sprechendem Namen erstellt.",                 // Beschreibung
new string[] { "Student_" },      // andere benötigte Dateien. Die erste Datei bestimmt die Anzahl der Zeilen der herauskommenden Datei
"PdfZeugnisse"));                     // Funktionsname (Case-Sensitive)

        this.Add(new Datei(
@"ImportFürGeevoo\import.csv",  // Dateipfad
new string[] { "Importieren Sie die Benutzer und Bilder nach Geevoo" },                                // Hinweise zum Download
new List<string> { "Quelldatei", "Zieldatei" }, // Kopfzeile
"Importieren Sie die Benutzer und Bilder nach Geevoo",          // Titel der Datei; wird im Menü angezeigt
@"Beschreibung: PDF-Klassensätze aus Atlantis werden nach Name, Datum, Zeugnisart und Klasse durchsucht. Für jedes gefundene Zeugnis wird eine PDF-Datei mit sprechendem Namen erstellt.",                 // Beschreibung
new string[] { "Student_" },      // andere benötigte Dateien. Die erste Datei bestimmt die Anzahl der Zeilen der herauskommenden Datei
"PdfZeugnisse"));                     // Funktionsname (Case-Sensitive)

        this.Add(new Datei(
@"ExportAusWebuntis\Student_",
new string[] { "Exportieren Sie die Datei aus Webuntis, indem Sie als Administrator:", "Stammdaten > Schülerinnen", "\"Berichte\" auswählen", "Bei \"Schüler\" auf CSV klicken", "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() },
new List<string> { "name", "longName", "foreName", "gender", "birthDate", "klasse.name", "entryDate", "exitDate", "text", "id", "externKey", "medicalReportDuty", "schulpflicht", "majority", "address.email", "address.mobile", "address.phone", "address.city", "address.postCode", "address.street" }));

        this.Add(new Datei(
@"ExportAusAtlantis\sim",
new string[] { "Exportieren die Datei aus Atlantis, indem Sie als DBA:", "die sim über den Listengenerator > Liste gemäß Suchlauf", "nach CSV exportieren", "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() },
new List<string> { "Familienname", "Vorname", "Austritt", "Schüler le. Jahr da", "Schüler Berufswechsel", "Vorjahr C05 Aktjahr C06", "1 Bezugsjahr", "2 Status", "3 Lfdnr", "4 Klasse", "5 Gliederung", "6 Fachklasse", "7 Klassenart", "8 Orgform", "9 Aktjahrgang", "10 Foerderschwerp", "11 Schwerstbehindert", "12 Reformpdg", "13 Jva", "14 Plz", "15. Ort", "16 Gebdat", "17 Geschlecht", "18 Staatsang", "19 Religion", "20 Relianmeldung", "21 Reliabmeldung", "22 Aufnahmedatum", "23 Labk", "24 Ausbildort", "25 Betriebsort", "26 LSSchulform", "27 Lsschulnummer", "28 LSGliederung", "29 LSFachklasse", "30 Lsklassenart", "31 Lsreformpdg", "32 LSSschulentl", "33 LSJahrgang", "34 LSQual", "35 Lsversetz", "36 VOKlasse", "37 VOGliederung", "38 VOFachklasse", "39 VOOrgform", "40 VOKlassenart", "41 VOJahrgang", "42 VOFoerderschwerp", "43 VOSchwerstbehindert", "44 VOReformpdg", "45 EntlDatum", "46 Zeugnis", "47 Schulpflichterf", "48 Schulwechselform", "49 Versetzung", "50 Jahr Zuzug", "51 Jahr Einschulung", "52 Jahr Schulwechsel", "53 zugezogen", "54 Geb.Land (Mutter)", "55 Geb.Land (Vater)", "56 Elternteil zugezogen", "57 Verkehrssprache", "58 Einschulungsart", "59 GS-Empfehlung", "60 Massnahmetraeger", "61 Betreuung", "62 BKAZVO", "63 Förderschwerpunkt 2", "64 VOFörderschwerpunkt 2", "65 Berufsabschluss", "66 Produktname", "67 Produktversion", "68 Adressmerkmal", "69 Internatsplatz", "70 Koopklasse" }
));
                
        this.Add(new Datei(
@"ExportAusWebuntis\MarksPerLesson",
new string[] { "Exportieren Sie die Datei aus Webuntis, indem Sie als Administrator:", "Klassenbuch > Berichte klicken", "Alle Klassen auswählen und ggfs. den Zeitraum einschränken", "Unter \"Noten\" die Prüfungsart (-Alle-) auswählen", "Unter \"Noten\" den Haken bei Notennamen ausgeben _NICHT_ setzen", "Hinter \"Noten pro Schüler\" auf CSV klicken", "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() },
new List<string> { "Datum", "Name", "Klasse", "Fach", "Prüfungsart", "Note", "Bemerkung", "Benutzer", "Schlüssel (extern)", "Gesamtnote" }));


        //this.Add(new Datei(
        //    @"ExportAusSchild\SchuelerLeistungsdaten.dat",          // Pfad
        //    new string[] {                                          // Benötigt von Funktionen
        //        "SchuelerLeistungsdaten",
        //        "SchuelerLernabschnittsdaten"
        //    },
        //    datbeschreibung,                                        // Hinweise zum Download
        //    '|',                                                   // Delimiter
        //    new string[] {
        //    }                                                      // Kopfzeile
        //    )
        //);

        //this.Add(new Datei(
        //    @"ExportAusAtlantis\AlleAdressen",
        //    new string[] { "SchuelerBasisdaten" },
        //    new string[] { "Exportieren Sie die aus Atlantis, indem Sie:", "Auswertung > Listengenerator > alle Adressen", "Alle Felder auswählen", "Felder übernehmen und Liste generieren", "Daten lesen ohne Filter", "Export sichtbare Felder (CSV)", "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() },
        //    '\t', 
        //    new List<string> { "Textfeld: ihr/ihre (klein)", "Textfeld: Ihrem Sohn/Ihrer Tochter", "Textfeld: Ihren Sohn/Ihre Tochter", "Textfeld: Ihres Sohnes/Ihrer Tochter", "Textfeld: Junge/Mädchen", "Textfeld: Klassenleiter/Klassenleiterin", "Textfeld: leere Spalte 1", "Textfeld: leere Spalte 2", "Textfeld: leere Spalte 3", "Textfeld: m/r-Ergänzung für Dativ", "Textfeld: n-Ergänzung für Akkusativ", "Textfeld: n/r-Ergänzung für Genitiv", "Textfeld: Numerus Dativ (Span.: s/-)", "Textfeld: Numerus Nominativ (Span.: es/-)", "Textfeld: Numerus Verb (Span.: n/-)", "Textfeld: r-Ergänzung für Nominativ", "Textfeld: Schüler/in", "Textfeld: Sein/Ihr (groß)", "Textfeld: sein/ihr (klein)", "Textfeld: Seine/Ihre (groß)", "Textfeld: seine/ihre (klein)", "Textfeld: Seinem/Ihrem (groß)", "Textfeld: seinem/ihrem (klein)", "Textfeld: Seiner/Ihrer (groß)", "Textfeld: seiner/ihrer (klein)", "Textfeld: Seines/Ihres (groß)", "Textfeld: seines/ihres (klein)", "Textfeld: Sie/Ihr Sohn/Ihre Tochter", "Textfeld: Sie/Ihren Sohn/Ihre Tochter", "Textfeld: sind(vollj.)/ist(minderj.)", "Textfeld: Sohn/Tochter", "Textfeld: Volljährig/Debitor", "Textfeld: werden(vollj.)/wird(minderj.)", "Vorgangsnummer", "Vorlagensprache", "Zeiger auf Klassensatz", "Zeiger auf Laufbahnsatz", "Zeiger auf schulbez. Schülersatz", "Zeiger auf Schulensatz", "Zeiger auf Schülersatz", "Zeiger auf Stamm-ID Adresse", "Zeiger auf Stamm-ID Klasse", "Zeiger auf Stamm-ID Schüler", "Zeitstempel letzte Änderung", "Lfd Nr", "Erzb. 1: Briefadresse", "Strafkarte: Lehrername", "Strafkarte: Datum", "Strafkarte: Bemerkung", "Schüler: Zusatzname", "Schüler: Zeugnisname", "Schüler: Wohnt bei", "Schüler: wird 18 am", "Schüler: wird 16 am", "Schüler: Vorgang Schuljahr", "Schüler: Vorgang aktueller Satz (J/N)", "Schüler: Vorgang (Auflösung)", "Schüler: Vorgang", "Schüler: Volljährig (J/N)", "Schüler: Verkehrs-/Muttersprache (ISAS) (Auflösung)", "Schüler: Verkehrs-/Muttersprache (ISAS)", "Schüler: Muttersprache (Auflösung)", "Schüler: Muttersprache", "Schüler: Unterbringungsart (Auflösung)", "Schüler: Unterbringungsart", "Schüler: Typ Koop-Schüler (Auflösung)", "Schüler: Typ Koop-Schüler", "Schüler: Tutor", "Schüler: Telefon 2", "Schüler: Telefon", "Schüler: Telefax", "Schüler: Suchbegriff", "Schüler: Straße", "Schüler: Stimmlage", "Schüler: Status Funktion (Auflösung)", "Schüler: Status Funktion", "Schüler: Staat (ISAS) (Auflösung)", "Schüler: Staat (ISAS)", "Schüler: Staat (Auflösung)", "Schüler: Staat", "Schüler: Schülertyp manuell", "Schüler: Schülertyp Internat", "Schüler: Schülertyp Artikel", "Schüler: Nummer", "Schüler: Beziehungen (mit Zeilenumbruch)", "Schüler: Beziehungen (Komma getrennt)", "Schüler: Datum Eintritt Grundschule (yyyy-mm-dd)", "Schüler: Datum Eintritt Grundschule", "Schüler: Eintrittsdatum", "Schüler: Schule verlassen (yyyy-mm-dd)", "Schüler: Schule verlassen", "Schüler: Austrittsdatum (yyyy-mm-dd)", "Schüler: Schulart Abgangsschule (Auflösung)", "Schüler: Schulart Abgangsschule", "Schüler: Schulabschluss (Auflösung)", "Schüler: Schulabschluss", "Schüler: Schliessfachnummer", "Schüler: Rufname", "Schüler: Religionsunterricht (Auflösung)", "Schüler: Religionsunterricht", "Schüler: Profil (Auflösung)", "Schüler: Profil", "Schüler: Presserechte (Auflösung)", "Schüler: Presserechte", "Schüler: Plz", "Schüler: Ortsteil", "Schüler: Ort", "Schüler: Name Zimmertutor oder Haustutor", "Schüler: Name Zimmertutor", "Schüler: Name Haustutor", "Schüler: Name Elternteil", "Betrieb: Merker-Priorität", "Betrieb: Merker", "Schüler: Länderkennzeichen", "Schüler: Letzte Schule (Vorjahr, Auflösung)", "Schüler: Letzte Schule (Vorjahr)", "Schüler: Letzte Schule (Auflösung)", "Schüler: Letzte Schule", "Schüler: Laufbahn (Klasse, JG)", "Schüler: Laufbahn (Klasse, Bildungsgang)", "Schüler: Laufbahn (alle Schuljahre)", "Schüler: Koop Schulnummer (Auflösung)", "Schüler: Koop Schulnummer", "Schüler: Klassenleiter letzte Schule", "Schüler: Klasse letzte Schule", "Schüler: Kind-Nr.", "Schüler: Joker 4 (Zahl)", "Schüler: Joker 4 (Text)", "Schüler: Joker 4 (Schlüssel) (Auflösung)", "Schüler: Joker 4 (Schlüssel)", "Schüler: Joker 4 (Datum)", "Schüler: Joker 3 (Zahl)", "Schüler: Joker 3 (Text)", "Schüler: Joker 3 (Schlüssel) (Auflösung)", "Schüler: Joker 3 (Schlüssel)", "Schüler: Joker 3 (Datum)", "Schüler: Joker 2 (Zahl)", "Schüler: Joker 2 (Text)", "Schüler: Joker 2 (Schlüssel) (Auflösung)", "Schüler: Joker 2 (Schlüssel)", "Schüler: Joker 2 (Datum)", "Schüler: Joker 1 (Zahl)", "Schüler: Joker 1 (Text)", "Schüler: Joker 1 (BAM/BAF) (Auflösung)", "Schüler: Joker 1 (Schlüssel)", "Schüler: Joker 1 (Datum)", "Schüler: Jahrgang (Auflösung-W)", "Schüler: Jahrgang (Auflösung-M)", "Schüler: Jahrgang (Auflösung)", "Schüler: Jahrgang", "Schüler: IHK-Nummer", "Schüler: Homepage", "Schüler: Herkunftsschule (Auflösung)", "Schüler: Herkunftsschule", "Schüler: Gruppe Klasse (Auflösung)", "Klassengruppe", "Schüler: Geschwister zu Stichtag", "Schüler: Geschwister", "Schüler: Geschlecht (Auflösung)", "Schüler: Geschlecht", "Schüler: Gemeindekennzeichen", "Schüler: Geburtsort", "Schüler: Geburtsname", "Schüler: Geburtsdatum (yyyy-mm-dd)", "Schüler: Geburtsdatum", "Schüler: Förderungsnr", "Schüler: fehlende Unterlagen", "Schüler: Familienstand (Auflösung)", "Schüler: Familienstand", "Schüler: Familiennummer", "Schüler: Erziehungsberechtigt (Art) (Auflösung)", "Schüler: Erziehungsberechtigt (Art)", "Schüler: Empfehlung(n) (mit Zeilenumbruch)", "Schüler: Empfehlung(n) (Komma getrennt)", "Schüler: Eintrittsjahrgang", "Schüler: Eintrittsgrund (Auflösung)", "Schüler: Eintrittsgrund", "Schüler: Eintrittsdatum (yyyy-mm-dd)", "Schüler: Eingang der Abmeldung (yyyy-mm-dd)", "Schüler: Eingang der Abmeldung", "Schüler: E-Mail-Adresse", "Schüler: Datum le. Schulbesuch (yyyy-mm-dd)", "Schüler: Datum le. Schulbesuch", "Schüler: Datum letzter Schulabschluss (yyyy-mm-dd)", "Schüler: Datum letzter Schulabschluss", "Schüler: Datum des Ausb.Ende (yyyy-mm-dd)", "Schüler: Datum des Ausb.Ende", "Schüler: Datum des Ausb.Beginn (yyyy-mm-dd)", "Schüler: Datum des Ausb.Beginn", "Schüler: Datum der Aufnahme (yyyy-mm-dd)", "Schüler: Datum der Aufnahme", "Schüler: Chor (aktuell)", "Schüler: Briefadresse", "Schüler: Bildungsgang Eintritt (yyyy-mm-dd)", "Schüler: Bildungsgang Eintritt", "Schüler: Bildungsgang Austritt (yyyy-mm-dd)", "Schüler: Bildungsdatum Austritt", "Schüler: Bildungsgang (Auflösung)", "Schüler: Bildungsgang", "Schüler: Bild (Pfad + Dateiname)", "Schüler: Beschäftigungsdauer (Auflösung)", "Schüler: Beschäftigungsdauer", "Schüler: Beschäftigungsart (Auflösung)", "Schüler: Beschäftigungsart", "Schüler: Berufsnummer", "Schüler: Berufsbildungsgesetz (Auflösung)", "Schüler: Berufsbildungsgesetz", "Schüler: Berufsbezeichnung Standard", "Schüler: Berufliche Vorbildung (Auflösung)", "Schüler: Berufliche Vorbildung", "Schüler: Bemerkung (Ziel)", "Schüler: Bemerkung (Wohnplaninfo)", "Schüler: Bemerkung (MOTIV)", "Schüler: Bemerkung (Interesse)", "Schüler: Bemerkung (frei)", "Schüler: Bemerkung (DIAG)", "Schüler: Bemerkung (BG)", "Schüler: Bemerkung (Background)", "Schüler: Bemerkung (AUFH)", "Schüler: Bemerkung (AN)", "Schüler: Bekenntnis (Auflösung)", "Schüler: Bekenntnis", "Schüler: Behinderunge(n) (mit Zeilenumbruch)", "Schüler: Behinderunge(n) (Komma getrennt)", "Schüler: Barcode", "Schüler: Ausweisnummer", "Schüler: Ausweis_Aussteller", "Schüler: Austrittsjahrgang", "Schüler: Austrittsdatum voraussichtlich (yyyy-mm-dd)", "Schüler: Austrittsdatum voraussichtlich", "Schüler: Austrittsdatum", "Schüler: Austritt wohin Schule (Auflösung)", "Schüler: Austritt wohin Schule (Adresse)", "Schüler: Austritt wohin Schule", "Schüler: Aussiedler (Auflösung)", "Schüler: Aussiedler", "Schüler: Ausgetreten (J/N)", "Schüler: Ausbildungsziel (Auflösung)", "Schüler: Ausbildungsziel", "Art der Kommunikation (Auflösung)", "Art der Kommunikation", "Schüler: Anzahl BAFÖG", "Anschrift Eltern (J/N)", "Schüler: Anredetext", "Schüler: Anrede", "Schüler: Anlage Datum", "Schüler: Alter", "Schüler: Akquise: Wdvl.-Datum", "Schüler: Akquise: Erstellungsdatum", "Schüler: Adresse letzte Schule", "Schüler: Abgangsjahrgang (Auflösung)", "Schüler: Abgangsjahrgang", "Schüler  Ausweis gültig bis", "Schüler  Ausweis ausgestellt am", "Schuljahr: Datum Unterrichtsende (yyyy-mm-dd)", "Schuljahr: Datum Unterrichtsende", "Schuljahr: Datum Unterrichtsbeginn (yyyy-mm-dd)", "Schuljahr: Datum Unterrichtsbeginn", "Schuljahr: Endedatum (yyyy-mm-dd)", "Schuljahr: Endedatum", "Schuljahr: Beginndatum (yyyy-mm-dd)", "Schuljahr: Beginndatum", "Schule: Zeugniskopfzeile 3", "Schule: Zeugniskopfzeile 2", "Schule: Zeugniskopfzeile 1", "Schule: Telefon 2", "Schule: Telefon 1", "Schule: Telefax", "Schule: Straße", "Schule: Stellvertreter Zeugnisunterschrift", "Schule: Stellvertreter Zeugnistitel", "Schule: Stellvertreter", "Schule: Schulträger", "Schule: Schulnummer", "Schule: Schulname 3", "Schule: Schulname 2", "Schule: Schulname", "Schule: Schulleiter Zeugnisunterschrift", "Schule: Schulleiter Zeugnistitel", "Schule: Schulleiter", "Schule: Schulart (Auflösung)", "Schule: Plz", "Schule: Ortsteil", "Schule: Ort", "Schule: Homepage", "Schule: Gläubiger-Identifikationsnummer", "Schule: Genitiv Name", "Schule: E-Mail-Adresse", "Schule: Briefpapier-Steuerung", "Schule: Briefadresse", "Schule: Anredetext", "Schule: Anrede", "Schule: Amtliche Bezeichnung 3", "Schule: Amtliche Bezeichnung 2", "Schule: Amtliche Bezeichnung 1", "Oberstufe: P-Seminar (Auflösung)", "Oberstufe: P-Seminar", "Oberstufe: Pflichtsprache in der Oberstufe (Auflösung)", "Oberstufe: P-Seminar (Auflösung)", "Oberstufe: P-Seminar", "Oberstufe: notw. FS für Erwerb AHR voc (Auflösung)", "Oberstufe: notw. FS für Erwerb AHR bis (Auflösung)", "Oberstufe: notw. FS für Erwerb AHR (Auflösung)", "Oberstufe: Fremdsprache Niveau B Dauer (Auflösung)", "Oberstufe: Fremdsprache Niveau B (Auflösung)", "Oberstufe: Fremdsprache 5 Status", "Oberstufe: Fremdsprache 5 Jahrgang von", "Oberstufe: Fremdsprache 5 Jahrgang bis", "Oberstufe: Fremdsprache 5 GER-Niveau (Auflösung)", "Oberstufe: Fremdsprache 5 GER-Niveau", "Oberstufe: Fremdsprache 5 (Auflösung)", "Oberstufe: Fremdsprache 5", "Oberstufe: Fremdsprache 4 Status", "Oberstufe: Fremdsprache 4 Jahrgang von", "Oberstufe: Fremdsprache 4 Jahrgang bis", "Oberstufe: Fremdsprache 4 GER-Niveau (Auflösung)", "Oberstufe: Fremdsprache 4 GER-Niveau", "Oberstufe: Fremdsprache 4 (Auflösung)", "Oberstufe: Fremdsprache 4", "Oberstufe: Fremdsprache 3 Status", "Oberstufe: Fremdsprache 3 Jahrgang von", "Oberstufe: Fremdsprache 3 Jahrgang bis", "Oberstufe: Fremdsprache 3 GER-Niveau (Auflösung)", "Oberstufe: Fremdsprache 3 GER-Niveau", "Oberstufe: Fremdsprache 3 (Auflösung)", "Oberstufe: Fremdsprache 3", "Oberstufe: Fremdsprache 2 Status", "Oberstufe: Fremdsprache 2 Jahrgang von", "Oberstufe: Fremdsprache 2 Jahrgang bis", "Oberstufe: Fremdsprache 2 GER-Niveau (Auflösung)", "Oberstufe: Fremdsprache 2 GER-Niveau", "Oberstufe: Fremdsprache 2 (Auflösung)", "Oberstufe: Fremdsprache 2", "Oberstufe: Fremdsprache 1 Status", "Oberstufe: Fremdsprache 1 Jahrgang von", "Oberstufe: Fremdsprache 1 Jahrgang bis", "Oberstufe: Fremdsprache 1 GER-Niveau (Auflösung)", "Oberstufe: Fremdsprache 1 GER-Niveau", "Oberstufe: Fremdsprache 1 (Auflösung)", "Oberstufe: Fremdsprache 1", "Oberstufe: Ersatz-FS 2 in der Oberstufe (Auflösung)", "Oberstufe: Ersatz-FS 1 in der Oberstufe (Auflösung)", "Oberstufe: Angestrebte Hochschulreife (Auflösung)", "Oberstufe: Abi-Prüfungs-Nr.", "letzter Vorgang: Typ Auflösung", "letzter Vorgang: Typ", "letzter Vorgang: Sachbearbeiter", "letzter Vorgang: Erstellungsdatum", "landesspezifisch: Name 3", "landesspezifisch: Zeugnisname für Druck", "landesspezifisch: Zeugnisname", "landesspezifisch: Vorname Vater", "landesspezifisch: Vorname Mutter", "landesspezifisch: Vorname", "landesspezifisch: Straße", "landesspezifisch: Staat Vater (Auflösung)", "landesspezifisch: Staat Vater", "landesspezifisch: Staat Mutter (Auflösung)", "landesspezifisch: Staat Mutter", "landesspezifisch: Sitzplatz", "landesspezifisch: Rufname", "landesspezifisch: Postleitzahl", "landesspezifisch: Ortsteil", "landesspezifisch: Ort", "landesspezifisch: Nachname Vater", "landesspezifisch: Nachname Mutter", "landesspezifisch: Nachname", "landesspezifisch: Länderkennzeichen", "landesspezifisch: Laufbahn (Klasse, JG)", "landesspezifisch: Herkunft (Auflösung)", "landesspezifisch: Geschlecht (Auflösung)", "landesspezifisch: Geschlecht", "landesspezifisch: Geburtsurkunde-Nummer", "landesspezifisch: Geburtsurkunde-Datum", "landesspezifisch: Geburtsort", "landesspezifisch: Geburtsname", "landesspezifisch: Geburtsdatum", "landesspezifisch: Fachbefreiung (Auflösung)", "landesspezifisch: Fachbefreiung", "landesspezifisch: Eintrittsdatum", "landesspezifisch: Bürger-Nummer Vater", "landesspezifisch: Bürger-Nummer Mutter", "landesspezifisch: Bürger-Nummer", "landesspezifisch: Beruf Vater (Langtext)", "landesspezifisch: Beruf Mutter (Langtext)", "landesspezifisch: Bemerkung", "landesspezifisch: Bekenntnis Vater (Aufl)", "landesspezifisch: Bekenntnis Vater", "landesspezifisch: Bekenntnis Mutter (Aufl)", "landesspezifisch: Bekenntnis Mutter", "landesspezifisch: Bekenntnis (Auflösung)", "landesspezifisch: Bekenntnis", "landesspezifisch: Arbeitgeber Vater", "landesspezifisch: Arbeitgeber Mutter", "Klasse: Unterrichtsorganisation (Auflösung)", "Klasse: Unterrichtsorganisation", "Klasse: Unterricht Blockwochen", "Klasse: Stellvertreter", "Klasse: Status (Auflösung)", "Klasse: Status", "Klasse: Sprechstunde (Tag)", "Klasse: Sprechstunde (Stunde)", "Klasse: Sprechstunde (Raum)", "Klasse: Sprechstunde (Bemerkung)", "Klasse: Semesterende  (yyyy-mm-dd)", "Klasse: Semesterende", "Klasse: Semesterbeginn  (yyyy-mm-dd)", "Klasse: Semesterbeginn", "Klasse: Schwerpunkt (Auflösung)", "Klasse: Schwerpunkt", "Klasse: Schuljahr", "Klasse: Klassenleiter (Zeugnisunterschrift)", "Klasse: Klassenleiter (Zeugnistitel)", "Klasse: Klassenleiter (Vorname)", "Klasse: Klassenleiter (Rufname)", "Klasse: Klassenleiter (Nachname)", "Klasse: Klassenleiter (Dienstbezeichnung)", "Klasse: Klassenleiter (Anrede)", "Klasse: Klassenleiter", "Klasse: Klassenende (yyyy-mm-dd)", "Klasse: Klassenende", "Klasse: Klassenbezeichnung bzw. Zeugnisname", "Klasse: Klassenbezeichnung bzw. Untisname", "Klasse: Klassenbezeichnung bzw. Statistikname", "Klasse: Klassenbeginn (yyyy-mm-dd)", "Klasse: Klassenbeginn", "Klasse: Klassenart (Auflösung)", "Klasse: Klassenart", "Klasse: Klasse Langname", "Klasse: Jahrgang (Auflösung)", "Klasse: Jahrgang", "Klasse: Gliederungsplan (Auflösung)", "Klasse: Gliederungsplan", "Klasse: Blockzeiten mit Tagen", "Klasse: Blockzeiten", "Klasse: Berufsfeld (Auflösung)", "Klasse: Berufsfeld", "Klasse: Bereich (Auflösung)", "Klasse: Bereich", "ESR Referenznummer", "ESR Prüfziffer Referenznummer", "Erzb. 2: WWW", "Erzb. 2: Typ Adresse (Steuerung)", "Erzb. 2: Typ Adresse (Auflösung)", "Erzb. 2: Typ Adresse", "Erzb. 2: Titel", "Erzb. 2: Telefax", "Erzb. 2: Tel 2 Typ (Auflösung)", "Erzb. 2: Tel 2", "Erzb. 2: Tel 1 Typ (Auflösung)", "Erzb. 2: Tel 1", "Erzb. 2: Swift-(BIC)-Code", "Erzb. 2: Straße", "Erzb. 2: Staat Auflösung", "Erzb. 2: Staat (weitere) Auflösung", "Erzb. 2: Staat (weitere)", "Erzb. 2: Staat", "Erzb. 2: Sozialversicherungsnummer", "Erzb. 2: Sorgerecht (Auflösung", "Erzb. 2: Sorgerecht", "Erzb. 2: Sorgeberechtig (J/N)", "Erzb. 2: Postleitzahl", "Erzb. 2: Personalausweisnummer", "Erzb. 2: Ortsteil", "Erzb. 2: Ort", "Erzb. 2: Mitglied (Auflösung)", "Erzb. 2: Mitglied", "Erzb. 2: Länderkennzeichen", "Erzb. 2: Korrespondenzsprache", "Erzb. 2: Kontonummer", "Erzb. 2: IBAN", "Erzb. 2: Herkunftsland (Auflösung)", "Erzb. 2: Herkunftsland", "Erzb. 2: Haussprache (weitere) Auflösung", "Erzb. 2: Haussprache (weitere)", "Erzb. 2: Haussprache (Auflösung)", "Erzb. 2: Haussprache", "Erzb. 2: Gemeinde-Kennzeichen", "Erzb. 2: Geburtsort", "Erzb. 2: Geburtsdatum", "Erzb. 2: Familienstand (Auflösung)", "Erzb. 2: Familienstand", "Erzb. 2: Eintrittsdatum", "Erzb. 2: E-Mail", "Erzb. 2: Debitorennummer", "Erzb. 2: Beruf (Langtext) Erzb. 1", "Erzb. 2: Beruf (Auflösung)", "Erzb. 2: Beruf", "Erzb. 2: Bemerkung", "Erzb. 2: Bekenntnis (Auflösung)", "Erzb. 2: Bekenntnis", "Erzb. 2: Barcode", "Erzb. 2: Bankname", "Erzb. 2: Bankleitzahl", "Erzb. 2: Ausweisnummer", "Erzb. 2: Ausweis gültig bis", "Erzb. 2: Ausweis Aussteller", "Erzb. 2: Ausweis ausgestellt am", "Erzb. 2: Austrittsdatum", "Erzb. 2: Art der Kommunikation (Auflösung)", "Erzb. 2: Art der Kommunikation", "Erzb. 2: Arbeitsgeber", "Erzb. 2: Anredetext", "Erzb. 2: Anrede", "Erzb. 1: WWW", "Erzb. 1: Typ Adresse (Steuerung)", "Erzb. 1: Typ Adresse (Auflösung)", "Erzb. 1: Typ Adresse", "Erzb. 1: Titel", "Erzb. 1: Telefax", "Erzb. 1: Tel 2 Typ (Auflösung)", "Erzb. 1: Tel 2", "Erzb. 1: Tel 1 Typ (Auflösung)", "Erzb. 1: Tel 1", "Erzb. 1: Swift-(BIC)-Code", "Erzb. 1: Straße", "Erzb. 1: Staat Auflösung", "Erzb. 1: Staat (weitere) Auflösung", "Erzb. 1: Staat (weitere)", "Erzb. 1: Staat", "Erzb. 1: Sozialversicherungsnummer", "Erzb. 1: Sorgerecht (Auflösung", "Erzb. 1: Sorgerecht", "Erzb. 1: Sorgeberechtig (J/N)", "Erzb. 1: Postleitzahl", "Erzb. 1: Personalausweisnummer", "Erzb. 1: Ortsteil", "Erzb. 1: Ort", "Erzb. 1: Mitglied (Auflösung)", "Erzb. 1: Mitglied", "Erzb. 1: Länderkennzeichen", "Erzb. 1: Korrespondenzsprache", "Erzb. 1: Kontonummer", "Erzb. 1: IBAN", "Erzb. 1: Herkunftsland (Auflösung)", "Erzb. 1: Herkunftsland", "Erzb. 1: Haussprache (weitere) Auflösung", "Erzb. 1: Haussprache (weitere)", "Erzb. 1: Haussprache (Auflösung)", "Erzb. 1: Haussprache", "Erzb. 1: Gemeinde-Kennzeichen", "Erzb. 1: Geburtsort", "Erzb. 1: Geburtsdatum", "Erzb. 1: Familienstand (Auflösung)", "Erzb. 1: Familienstand", "Erzb. 1: Eintrittsdatum", "Erzb. 1: E-Mail", "Erzb. 1: Debitorennummer", "Erzb. 1: Beruf (Langtext) Erzb. 2", "Erzb. 1: Beruf (Auflösung)", "Erzb. 1: Beruf", "Erzb. 1: Bemerkung", "Erzb. 1: Bekenntnis (Auflösung)", "Erzb. 1: Bekenntnis", "Erzb. 1: Barcode", "Erzb. 1: Bankname", "Erzb. 1: Bankleitzahl", "Erzb. 1: Ausweisnummer", "Erzb. 1: Ausweis gültig bis", "Erzb. 1: Ausweis Aussteller", "Erzb. 1: Ausweis ausgestellt am", "Erzb. 1: Austrittsdatum", "Erzb. 1: Art der Kommunikation (Auflösung)", "Erzb. 1: Art der Kommunikation", "Erzb. 1: Arbeitsgeber", "Erzb. 1: Anredetext", "Erzb. 1: Anrede", "Adresstyp", "Schüler: Vorname", "Schüler: Nachname", "Schüler: Erziehungsberechtigt Art 2", "Schüler: Erziehungsberechtigt Art", "Klasse: Klassenbezeichnung", "Erzb. 2: Vorname", "Erzb. 2: Nachname", "Erzb. 2: Briefadresse", "Erzb. 1: Vorname", "Erzb. 1: Nachname", "Strafkarte: Massnahme", "Strafkarte: Massnahme (Typ)", "Strafkarte: Satznummer", "Textfeld: Auszubildende/r", "Textfeld: Dem/Der (groß)", "Textfeld: den/die (klein)", "Textfeld: Den/Die (groß)", "Textfeld: dem/der (klein)", "Textfeld: Der/Die (groß)", "Textfeld: der/die (klein)", "Textfeld: des Schülers/der Schülerin", "Textfeld: Des/Der (groß)", "Textfeld: des/der (klein)", "Textfeld: Er/Sie (groß)", "Textfeld: er/sie (klein)", "Textfeld: Geschl. Schüler Genitiv (Span.: del/de la)", "Textfeld: Geschl. Schüler Nominativ (Span.: o/a)", "Textfeld: haben(vollj.)/hat(minderj.)", "Textfeld: Herr/Frau", "Textfeld: Ihnen/Ihrem Sohn/Ihrer Tochter", "Textfeld: Ihr Sohn/Ihre Tochter", "Textfeld: Ihr/Ihre (groß)", }));



        //this.Add(new Datei(
        //    @"ExportAusSchild\SchuelerLernabschnittsdaten.dat",
        //    new string[] {
        //        @"ExportAusSchild\SchuelerLernabschnittsdaten.dat"
        //    },
        //    datbeschreibung,
        //    '|',
        //    new string[] { }
        //    )
        //);

        //this.Add(new Datei(
        //    @"ExportAusSchild\Faecher.dat",
        //    new string[] {
        //        @"ExportAusSchild\Faecher.dat"
        //    },
        //    datbeschreibung,
        //    '|',
        //    new string[] { }
        //    )
        //);

        //this.Add(new Datei(
        //    @"ExportAusWebuntis\ExportLessons",
        //    new string[] { "wird referenziert von diesen Funktionen" },
        //    new string[] {
        //        "Exportieren Sie die Datei aus Webuntis, indem Sie als Administrator:",
        //        "Administration > Export klicken",
        //        "Das CSV-Icon hinter Unterricht klicken",
        //        "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory()
        //    },
        //    '|',
        //    new string[] { }
        //    )
        //);

        //this.Add(new Datei(
        //    @"ExportAusWebuntis\StudentgroupStudents",
        //    new string[] { "wird referenziert von diesen Funktionen" },
        //    new string[] {
        //        "Exportieren Sie die Datei aus Webuntis, indem Sie als Administrator:",
        //        "Administration > Export klicken",
        //        "Zeitraum begrenzen, also die Woche der Zeugniskonferenz und vergange Abschnitte herauslassen",
        //        "Das CSV-Icon hinter Schülergruppen klicken",
        //        "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory()
        //    },
        //    '\t',
        //    new string[] { }
        //    )
        //);



        //this.Add(new Datei(
        //    @"ExportAusWebuntis\AbsencePerStudent",
        //    new string[] { "wird referenziert von diesen Funktionen" },
        //    new string[] {
        //        "wird referenziert von diesen Funktionen"
        //    },
        //    '\t',
        //    new string[] { }
        //    )
        //);

        //this.Add(new Datei(
        //    @"ExportAusUntis\GPU006",
        //    new string[] {
        //        "wird referenziert von diesen Funktionen"
        //    },
        //    new string[] {
        //        "Exportieren Sie die Datei aus Untis, indem Sie:",
        //        "Datei > Import/Export > Export TXT > Fächer klicken",
        //        "Trennzeichen: Semikolon, Textbegrenzung \", Encoding UTF8",
        //        "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory()
        //    },
        //    ';',
        //    new string[] {
        //    } //"InternKrz;Bezeichnung;;;;;;;;;;;;;;;;;;;;;"
        //    )
        //);

        //this.Add(new Datei(
        //    @"ExportAusWebuntis\AbsencePerStudent",
        //    new string[] {
        //        "wird referenziert von diesen Funktionen"
        //    },
        //    new string[] {
        //        "Exportieren Sie die Datei aus Webuntis, indem Sie als Administrator:",
        //        "Den Pafad gehen: Klassenbuch > Berichte klicken",
        //        "Alle Klassen oder einzelne Klassen auswählen.",
        //        "Unter \"Abwesenheiten\" Fehlzeiten pro Schüler*in auswählen",
        //        "\"pro Tag\" ",
        //        "Auf CSV klicken",
        //        "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory()
        //    },
        //    '\t',
        //    new string[] { }
        //    )
        //);

        //this.Add(new Datei(
        //    @"ExportAusAtlantis\AlleAdressen",
        //    new string[] {
        //        "SchuelerAdressen"
        //    },
        //    new string[] {
        //        "Exportieren Sie die aus Atlantis, indem Sie:",
        //        "Auswertung > Listengenerator > alle Adressen",
        //        "Alle Felder auswählen",
        //        "Felder übernehmen und Liste generieren",
        //        "Daten lesen ohne Filter",
        //        "Export sichtbare Felder (CSV)",
        //        "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory()
        //    },
        //    '\t',
        //    new string[] { }
        //    )
        //);

        //this.Add(new Datei(
        //    @"ExportAusAtlantis\sim",
        //    new string[] {
        //        "wird referenziert von diesen Funktionen"
        //    },
        //    new string[] {
        //        "Exportieren die Datei aus Atlantis, indem Sie als DBA:",
        //        "die sim über den Listengenerator > Liste gemäß Suchlauf",
        //        "nach CSV exportieren",
        //        "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory()
        //    },
        //    '|',
        //    new string[] { }
        //    )
        //);

        //this.Add(new Datei(
        //    @"ExportAusSchild\SchuelerBasisdaten.dat",
        //    new string[] {
        //        "wird referenziert von diesen Funktionen"
        //    },
        //    new string[] { "" },
        //    '|',
        //    new string[] { }
        //    )
        //);


        OrdnerAnlegen();
    }

    private void OrdnerAnlegen()
    {

        bool exportOrdnerErstellt = false;
        bool importOrdnerErstellt = false;

        foreach (var ordner in this.Select(x => Path.GetDirectoryName(x.DateiPfad)))
        {
            if (!Directory.Exists(ordner))
            {
                Directory.CreateDirectory(ordner);
                Global.ZeileSchreiben(0, ordner, "wurde erstellt", null);
                if (ordner.ToLower().Contains("export"))
                {
                    exportOrdnerErstellt = true;
                }
                if (ordner.ToLower().Contains("import"))
                {
                    importOrdnerErstellt = true;
                }
            }
        }
        if (importOrdnerErstellt)
        {
            Console.WriteLine("   Es wurden Ordner für den Import von Datein in Programme erstellt. Schule.exe füllt die Import-Ordner mit entsprechenden Dateien.");
        }
        if (exportOrdnerErstellt)
        {
            Console.WriteLine("   Es wurden Ordner für den Export von Dateien aus Programmen erstellt.");
            Console.WriteLine("    Folgen Sie den Anweisungen, um die Ordner passend zu füllen.");
        }
    }

    public Dateien(Schülers interessierendeSchuelers)
    {
        foreach (var datei in this)
        {
            datei.InteressierendeZeilenFiltern(interessierendeSchuelers);
        }
    }

    public Exception Fehler { get; private set; }
    public int Auswahl { get; private set; }

    internal void DoppelteZeilenAusDerZweitenDateiEntfernen(Datei untisFaecher, Datei schildFaecher, string dateiPfad, string meldung)
    {
        Datei exportDatei = new()
        {
            DateiPfad = dateiPfad
        };

        try
        {
            //// Vergleiche schildfaecher.Zeilen zeile für zeile mit untisfaecher.Zeilen
            //foreach (var untisZeile in untisFaecher.Zeilen)
            //{
            //    bool found = false;
            //    string[] schildZeileNeu = new string[schildFaecher.Kopfzeile.Length];
            //    // Gesucht werden zeilen, die in untisfaecher existieren, aber nicht in schildfaecher
            //    foreach (var schildZeile in schildFaecher.Zeilen)
            //    {
            //        bool match = true;

            //        // Es werden nur diejenigen Spalten einbezogen, deren Spaltenkopf in der Kopfzeile beider Dateien existiert
            //        for (int i = 0; i < untisFaecher.Kopfzeile.Length; i++)
            //        {
            //            string spaltenkopf = untisFaecher.Kopfzeile[i];
            //            var index = Array.IndexOf(schildFaecher.Kopfzeile, spaltenkopf);

            //            // Überprüfe, ob der Spaltenkopf in der Kopfzeile von schildfaecher existiert
            //            if (index != -1)
            //            {
            //                // Vergleiche den Wert in der aktuellen Spalte
            //                if (untisZeile[i] != schildZeile[index])
            //                {
            //                    schildZeileNeu[index] = untisZeile[i];
            //                    match = false;
            //                    //break;
            //                }
            //            }
            //        }

            //        // Wenn eine Übereinstimmung gefunden wurde, setze found auf true und beende die innere Schleife
            //        if (match)
            //        {
            //            found = true;
            //            break;
            //        }
            //    }

            //    // Wenn keine Übereinstimmung gefunden wurde, füge die Zeile zu fehlendeZeilen hinzu
            //    if (!found)
            //    {
            //        exportDatei.Zeilen.Add(schildZeileNeu.ToList());
            //    }
            //}
        }
        catch (Exception ex)
        {
            Fehler = ex;
        }
        finally
        {
            Global.ZeileSchreiben(0, meldung, exportDatei.Zeilen.Count.ToString(), Fehler);
        }
        exportDatei.Erstellen();
    }

    public Dateien(Dateien alleDateien, string[] benötigteDateien, Aliasse aliasse)
    {
        try
        {
            foreach (var benötigte in benötigteDateien)
            {
                foreach (var datei in alleDateien)
                {
                    if (datei.DateiPfad.ToLower().Contains("export")) // Nur Dateien, aus Exportordnern
                    {
                        if (Path.GetFileNameWithoutExtension(datei.DateiPfad).ToLower().StartsWith(benötigte.ToLower()))
                        {
                            datei.AddZeilen();

                            var klon = new Datei
                            {   
                                Zeilen = new Zeilen(),
                                Fehler = datei.Fehler,
                                DateiPfad = datei.DateiPfad,
                                Hinweise = (string[])datei.Hinweise.Clone(),
                            };
                            klon.Aliasse = aliasse;
                            klon.Zeilen = new Zeilen(datei.Zeilen.Select(z => new Zeile(z.Zellen.ToList(), z.IstKopfzeile)).ToList());

                            this.Add(klon);
                            Global.ZeileSchreiben(0, datei.DateiPfad, datei.Zeilen.Count().ToString(), Fehler);
                        }
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

        }
    }

    public Dateien(string v)
    {
    }

    internal Dateien InteressierendeZeilenFiltern(Schülers interessierendeSchuelers)
    {
        foreach (var datei in this)
        {
            datei.InteressierendeZeilenFiltern(interessierendeSchuelers);
        }
        return this;
    }

    internal void ZeilenHinzufuegen(string funktionsname)
    {
        foreach (Datei datei in this)
        {
            if (datei.Zeilen.Count() <= 1)
            {
                if (datei.BenötigteDateien.Contains(funktionsname))
                {
                    datei.AddZeilen();
                }
            }
        }
    }

    internal Dateien Interessierende(string[] dateiPfade)
    {
        Dateien dateien = new Dateien("");

        foreach (var dateiPfad in dateiPfade)
        {
            Datei datei = this.Where(x => x.DateiPfad.Contains(dateiPfad)).FirstOrDefault();
            dateien.Add(datei);
        }
        return dateien;
    }

    internal void AddZeilen()
    {
        foreach (var datei in this)
        {
            datei.AddZeilen();
        }
    }

    internal void Add(Dateien sDateien)
    {
        foreach (var datei in sDateien)
        {
            this.Add(datei);
        }
    }

    public Datei DisplayMenu(Aliasse aliasse)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appSettings.json", optional: false, reloadOnChange: true).Build();

        Console.WriteLine("");
        Console.WriteLine("  Bitte auswählen:");
        Console.WriteLine("");

        Auswahl = Convert.ToInt32(configuration["Auswahl"]);

        for (int i = 0; i < this.Where(x => !string.IsNullOrEmpty(x.Titel)).Count(); i++)
        {
            Console.WriteLine(" " + (i + 1).ToString().PadLeft(3) + ". " + this[i].Titel.PadRight(13));
        }

        bool wiederhole = true;
        do
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("    Ihre Auswahl [" + Auswahl + "] : ");
            Console.ResetColor();
            var eingabe = Console.ReadLine();

            if (eingabe == "ö")
            {
                Global.OpenCurrentFolder();
                wiederhole = true;
                continue;
            }
            if (eingabe == "x")
            {
                Global.OpenWebseite("");
                wiederhole = true;
                continue;
            }
            if (eingabe == "" && Auswahl.ToString() != "")
            {
                eingabe = Auswahl.ToString();
            }

            int nummer = 0;

            if (int.TryParse(eingabe, out nummer))
            {
                // Überprüfen, ob die Zahl im gültigen Bereich liegt
                if (nummer >= 1 && nummer <= this.Count)
                {
                    Auswahl = nummer;
                    Console.WriteLine($"     Sie haben die Zahl {Auswahl} eingegeben.");
                    Global.Speichern("Auswahl", Auswahl.ToString());
                    wiederhole = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("     Die Zahl muss zwischen 1 und " + this.Count + " liegen. Bitte versuchen Sie es erneut.");
                    Console.ResetColor();
                    wiederhole = true;
                    continue;
                }
            }
            else
            {
                if (!(eingabe == "" && Auswahl >= 1 && Auswahl <= this.Count))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("     Die Zahl muss zwischen 1 und " + this.Count + " liegen. Bitte versuchen Sie es erneut.");
                    Console.ResetColor();
                }
            }

        } while (wiederhole);

        Global.DisplayHeader();
        Global.DisplayHeader(this[Auswahl - 1].Titel, ' ');
        Global.DisplayCenteredBox(this[Auswahl - 1].Beschreibung, 90);
        this[Auswahl - 1].Aliasse = aliasse;
        return this.Where(x => !string.IsNullOrEmpty(x.Titel)).ToList()[Auswahl - 1];
    }
}