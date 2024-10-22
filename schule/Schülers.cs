using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.Metrics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

public class Schülers : List<Schueler>
{
    public List<int> AktSj { get; }
    public int Jahrgang { get; private set; }
    public int Abschnitt { get; set; }
    public List<string> InteressierendeKlassen { get; private set; }
    public Exception Fehler { get; private set; }
    public Schülers InteressierendeSchuelers { get; set; }
    public string Status { get; }
    public string Aufnahmedatum { get; private set; }
    public string ImportNachSchildPfad { get; private set; }
    public string[] Kopfzeile { get; private set; }

    public Schülers(string dateiName, string dateiendung = "*.dat", string delimiter = "|")
    {
        var dateiPfad = Global.CheckFile(dateiName, dateiendung);

        if (dateiPfad == null)
        {
            var hinweise = new string[] {
                "Exportieren Sie die Datei aus SchILD, indem Sie:",
                "In SchILD den Pfad gehen: Datenaustausch > Schnittstelle > Export",
                "Die Datei auswählen.",
                "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory()
            };
            Global.ZeileSchreiben(0, dateiName, "keine Datei gefunden", new Exception("keine Datei gefunden"), hinweise);
            return;
        }

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
            HasHeaderRecord = true,
            Delimiter = delimiter
        };

        using (var reader = new StreamReader(dateiPfad))
        using (var csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<SchuelersMap>();
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            var records = csv.GetRecords<Schueler>();
            
            this.AddRange(records);
            foreach (var item in this)
            {
                item.EmailSchulisch = item.GenerateMail();
            }
        }

        Global.Ausgaben.Add(new Ausgabe(0, dateiPfad, this.Count().ToString()));
    }

    public class SchuelersMap : ClassMap<Schueler>
    {
        public SchuelersMap()
        {
            Map(m => m.AbschlussartAnEigenerSchuleStatistikKürzel).Name("Abschlussart an eigener Schule (Statistik-Kürzel)");
            Map(m => m.AktuellerAbschnitt).Name("Aktueller Abschnitt");
            Map(m => m.AktuellesHalbjahr).Name("Aktuelles Halbjahr");
            Map(m => m.AllgAdresse1TelNr).Name("Allg. Adresse: 1. Tel.-Nr.");
            Map(m => m.AllgAdresse2TelNr).Name("Allg. Adresse: 2. Tel.-Nr.");
            Map(m => m.AllgAdresseAdressart).Name("Allg. Adresse: Adressart");
            Map(m => m.AllgAdresseBemerkungen).Name("Allg. Adresse: Bemerkungen");
            Map(m => m.AllgAdresseBetreuerAbteilung).Name("Allg. Adresse: Betreuer Abteilung");
            Map(m => m.AllgAdresseBetreuerAnrede).Name("Allg. Adresse: Betreuer Anrede");
            Map(m => m.AllgAdresseBetreuerEmail).Name("Allg. Adresse: Betreuer E-Mail");
            Map(m => m.AllgAdresseBetreuerName).Name("Allg. Adresse: Betreuer Name");
            Map(m => m.AllgAdresseBetreuerTelefon).Name("Allg. Adresse: Betreuer Telefon");
            Map(m => m.AllgAdresseBetreuerTitel).Name("Allg. Adresse: Betreuer Titel");
            Map(m => m.AllgAdresseBetreuerVorname).Name("Allg. Adresse: Betreuer Vorname");
            Map(m => m.AllgAdresseBetreuungslehrer).Name("Allg. Adresse: Betreuungslehrer");
            Map(m => m.AllgAdresseBetreuungslehrerAnrede).Name("Allg. Adresse: Betreuungslehrer Anrede");
            Map(m => m.AllgAdresseBranche).Name("Allg. Adresse: Branche");
            Map(m => m.AllgAdresseBundesland).Name("Allg. Adresse: Bundesland");
            Map(m => m.AllgAdresseEmail).Name("Allg. Adresse: E-Mail");
            Map(m => m.AllgAdresseFaxNr).Name("Allg. Adresse: Fax-Nr.");
            Map(m => m.AllgAdresseKreis).Name("Allg. Adresse: Kreis");
            Map(m => m.AllgAdresseName1).Name("Allg. Adresse: Name1");
            Map(m => m.AllgAdresseName2).Name("Allg. Adresse: Name2");
            Map(m => m.AllgAdresseOrt).Name("Allg. Adresse: Ort");
            Map(m => m.AllgAdressePLZ).Name("Allg. Adresse: PLZ");
            Map(m => m.AllgAdresseSonstigeBetreuer).Name("Allg. Adresse: Sonstige Betreuer");
            Map(m => m.AllgAdresseStraße).Name("Allg. Adresse: Straße");
            Map(m => m.AllgAdresseVertragsbeginn).Name("Allg. Adresse: Vertragsbeginn");
            Map(m => m.AllgAdresseVertragsende).Name("Allg. Adresse: Vertragsende");
            Map(m => m.AllgAdresseZusatz1).Name("Allg. Adresse: Zusatz1");
            Map(m => m.AllgAdresseZusatz2).Name("Allg. Adresse: Zusatz2");
            Map(m => m.Anrede).Name("Anrede");
            Map(m => m.ASV).Name("ASV");
            Map(m => m.Aufnahmedatum).Name("Aufnahmedatum");
            Map(m => m.AufnehmendeSchuleName).Name("Aufnehmende Schule: Name");
            Map(m => m.AufnehmendeSchuleOrt).Name("Aufnehmende Schule: Ort");
            Map(m => m.AufnehmendeSchulePLZ).Name("Aufnehmende Schule: PLZ");
            Map(m => m.AufnehmendeSchuleSchulnr).Name("Aufnehmende Schule: Schulnr.");
            Map(m => m.AufnehmendeSchuleStraße).Name("Aufnehmende Schule: Straße");
            Map(m => m.Aussiedler).Name("Aussiedler");
            Map(m => m.Ausweisnummer).Name("Ausweisnummer");
            Map(m => m.BeginnDerBildungsganges).Name("Beginn d. Bildungsganges");
            Map(m => m.Bemerkungen).Name("Bemerkungen");
            Map(m => m.BerufsschulpflichtErfüllt).Name("Berufsschulpflicht erfüllt");
            Map(m => m.BesMerkmal).Name("bes. Merkmal");
            Map(m => m.BleibtAnSchule).Name("Bleibt an Schule?");
            Map(m => m.Briefanrede).Name("Briefanrede");
            Map(m => m.Bundesland).Name("Bundesland");
            Map(m => m.DatumLetztesAnschreiben).Name("Datum letzes Anschreiben");
            Map(m => m.DatumReligionsabmeldung).Name("Datum Religionsabmeldung");
            Map(m => m.DatumReligionsanmeldung).Name("Datum Religionsanmeldung");
            Map(m => m.DurchschnittsnoteText).Name("Durchschnittsnote (Text)");
            Map(m => m.DurchschnittsnoteZahl).Name("Durchschnittsnote (Zahl)");
            Map(m => m.EindeutigeNummerGUID).Name("eindeutige Nummer (GUID)");
            Map(m => m.EinschulungsartASD).Name("Einschulungsart (ASD)");
            Map(m => m.Einschulungsjahr).Name("Einschulungsjahr");
            Map(m => m.EmailPrivat).Name("E-Mail (privat)");
            Map(m => m.EmailSchulisch).Name("E-Mail schulisch)");
            Map(m => m.EndeDerAnschlussförderung).Name("Ende der Anschlussförderung");
            Map(m => m.EndeDerEingliederungsphase).Name("Ende der Eingliederungsphase");
            Map(m => m.Entlassdatum).Name("Entlassdatum");
            Map(m => m.Entlassjahrgang).Name("Entlassjahrgang");
            Map(m => m.EntlassjahrgangInterneBezeichnung).Name("Entlassjahrgang (interne Bezeichnung)");
            Map(m => m.ErhältBAFöG).Name("Erhält BAFöG");
            Map(m => m.ErsteSchulformInSekI).Name("Erste Schulform in Sek.I");
            Map(m => m.Erzieher1Anrede).Name("Erzieher 1: Anrede");
            Map(m => m.Erzieher1Briefanrede).Name("Erzieher 1: Briefanrede");
            Map(m => m.Erzieher1Nachname).Name("Erzieher 1: Nachname");
            Map(m => m.Erzieher1Titel).Name("Erzieher 1: Titel");
            Map(m => m.Erzieher1Vorname).Name("Erzieher 1: Vorname");
            Map(m => m.Erzieher2Anrede).Name("Erzieher 2: Anrede");
            Map(m => m.Erzieher2Briefanrede).Name("Erzieher 2: Briefanrede");
            Map(m => m.Erzieher2Nachname).Name("Erzieher 2: Nachname");
            Map(m => m.Erzieher2Titel).Name("Erzieher 2: Titel");
            Map(m => m.Erzieher2Vorname).Name("Erzieher 2: Vorname");
            Map(m => m.ErzieherArtKlartext).Name("Erzieher: Art (Klartext)");
            Map(m => m.ErzieherEmail).Name("Erzieher: E-Mail");
            Map(m => m.ErzieherErhältAnschreiben).Name("Erzieher: Erhält Anschreiben");
            Map(m => m.ErzieherOrt).Name("Erzieher: Ort");
            Map(m => m.ErzieherOrtsteil).Name("Erzieher: Ortsteil");
            Map(m => m.ErzieherPostleitzahl).Name("Erzieher: Postleitzahl");
            Map(m => m.ErzieherStraße).Name("Erzieher: Straße");
            Map(m => m.ExterneIDNummer).Name("Externe ID-Nummer");
            Map(m => m.FachklasseBezeichnung).Name("Fachklasse (Bezeichnung)");
            Map(m => m.FachklasseKürzel).Name("Fachklasse (Kürzel)");
            Map(m => m.Fahrschülerart).Name("Fahrschülerart");
            Map(m => m.FaxNr).Name("Fax-Nr.");
            Map(m => m.Förderschwerpunkt1).Name("Förderschwerpunkt 1");
            Map(m => m.Förderschwerpunkt2).Name("Förderschwerpunkt 2");
            Map(m => m.Geburtsdatum).Name("Geburtsdatum");
            Map(m => m.Geburtsland).Name("Geburtsland");
            Map(m => m.GeburtslandMutter).Name("Geburtsland Mutter");
            Map(m => m.GeburtslandVater).Name("Geburtsland Vater");
            Map(m => m.Geburtsname).Name("Geburtsname");
            Map(m => m.Geburtsort).Name("Geburtsort");
            Map(m => m.Gelöscht).Name("Gelöscht?");
            Map(m => m.Geschlecht).Name("Geschlecht");
            Map(m => m.Haltestelle).Name("Haltestelle");
            Map(m => m.Hausnummer).Name("Hausnummer");
            Map(m => m.HöchsterAllgAbschluss).Name("Höchster allg. Abschluss");
            Map(m => m.InterneIDNummer).Name("Interne ID-Nummer");
            Map(m => m.Jahrgang).Name("Jahrgang");
            Map(m => m.JahrgangInterneBezeichnung).Name("Jahrgang (interne Bezeichnung)");
            Map(m => m.Kindergarten).Name("Kindergarten");
            Map(m => m.Klasse).Name("Klasse");
            Map(m => m.Klassenlehrer).Name("Klassenlehrer");
            Map(m => m.KlassenlehrerAmtsbezeichnung).Name("Klassenlehrer: Amtsbezeichnung");
            Map(m => m.KlassenlehrerAnrede).Name("Klassenlehrer: Anrede");
            Map(m => m.KlassenlehrerName).Name("Klassenlehrer: Name");
            Map(m => m.KlassenlehrerTitel).Name("Klassenlehrer: Titel");
            Map(m => m.KlassenlehrerVorname).Name("Klassenlehrer: Vorname");
            Map(m => m.KonfessionKlartext).Name("Konfession (Klartext)");
            Map(m => m.Kreis).Name("Kreis");
            Map(m => m.LetzteSchuleAbschluss).Name("Letzte Schule: Abschluss");
            Map(m => m.LetzteSchuleAllgemeineHerkunft).Name("Letzte Schule: allgemeine Herkunft");
            Map(m => m.LetzteSchuleEntlassdatum).Name("Letzte Schule: Entlassdatum");
            Map(m => m.LetzteSchuleEntlassjahrgang).Name("Letzte Schule: Entlassjahrgang");
            Map(m => m.LetzteSchuleName).Name("Letzte Schule: Name");
            Map(m => m.LetzteSchuleOrt).Name("Letzte Schule: Ort");
            Map(m => m.LetzteSchulePLZ).Name("Letzte Schule: PLZ");
            Map(m => m.LetzteSchuleSchulform).Name("Letzte Schule: Schulform");
            Map(m => m.LetzteSchuleSchulnr).Name("Letzte Schule: Schulnr.");
            Map(m => m.LetzteSchuleStraße).Name("Letzte Schule: Straße");
            Map(m => m.LetzteSchuleVersetzungsvermerk).Name("Letzte Schule: Versetzungsvermerk");
            Map(m => m.LetzterBerufsbezAbschlussKürzel).Name("Letzter allg. Abschluss (Kürzel)");
            Map(m => m.LetzterBerufsbezAbschlussKürzel).Name("Letzter berufsbez. Abschluss (Kürzel)");
            Map(m => m.Markiert).Name("Markiert?");
            Map(m => m.MigrationshintergrundVorhanden).Name("Migrationshintergrund vorhanden");
            Map(m => m.Nachname).Name("Nachname");
            Map(m => m.Namenszusatz).Name("Namenszusatz");
            Map(m => m.Organisationsform).Name("Organisationsform");
            Map(m => m.Ortsname).Name("Ortsname");
            Map(m => m.Ortsteil).Name("Ortsteil");
            Map(m => m.Ortsteil).Name("Ortsteil");
            Map(m => m.ErzieherPostleitzahl).Name("Erzieher: Postleitzahl");
            Map(m => m.ErzieherStraße).Name("Erzieher: Straße");
            Map(m => m.ExterneIDNummer).Name("Externe ID-Nummer");
            Map(m => m.FachklasseBezeichnung).Name("Fachklasse (Bezeichnung)");
            Map(m => m.FachklasseKürzel).Name("Fachklasse (Kürzel)");
            Map(m => m.Fahrschülerart).Name("Fahrschülerart");
            Map(m => m.FaxNr).Name("Fax-Nr.");
            Map(m => m.Förderschwerpunkt1).Name("Förderschwerpunkt 1");
            Map(m => m.Förderschwerpunkt2).Name("Förderschwerpunkt 2");
            Map(m => m.Geburtsdatum).Name("Geburtsdatum");
            Map(m => m.Geburtsland).Name("Geburtsland");
            Map(m => m.GeburtslandMutter).Name("Geburtsland Mutter");
            Map(m => m.GeburtslandVater).Name("Geburtsland Vater");
            Map(m => m.Geburtsname).Name("Geburtsname");
            Map(m => m.Geburtsort).Name("Geburtsort");
            Map(m => m.Gelöscht).Name("Gelöscht?");
            Map(m => m.Geschlecht).Name("Geschlecht");
            Map(m => m.Haltestelle).Name("Haltestelle");
            Map(m => m.Hausnummer).Name("Hausnummer");
            Map(m => m.HöchsterAllgAbschluss).Name("Höchster allg. Abschluss");
            Map(m => m.InterneIDNummer).Name("Interne ID-Nummer");
            Map(m => m.Jahrgang).Name("Jahrgang");
            Map(m => m.JahrgangInterneBezeichnung).Name("Jahrgang (interne Bezeichnung)");
            Map(m => m.Kindergarten).Name("Kindergarten");
            Map(m => m.Klasse).Name("Klasse");
            Map(m => m.Klassenlehrer).Name("Klassenlehrer");
            Map(m => m.KlassenlehrerAmtsbezeichnung).Name("Klassenlehrer: Amtsbezeichnung");
            Map(m => m.KlassenlehrerAnrede).Name("Klassenlehrer: Anrede");
            Map(m => m.KlassenlehrerName).Name("Klassenlehrer: Name");
            Map(m => m.KlassenlehrerTitel).Name("Klassenlehrer: Titel");
            Map(m => m.KlassenlehrerVorname).Name("Klassenlehrer: Vorname");
            Map(m => m.KonfessionKlartext).Name("Konfession (Klartext)");
            Map(m => m.Kreis).Name("Kreis");
            Map(m => m.LetzteSchuleAbschluss).Name("Letzte Schule: Abschluss");
            Map(m => m.LetzteSchuleAllgemeineHerkunft).Name("Letzte Schule: allgemeine Herkunft");
            Map(m => m.LetzteSchuleEntlassdatum).Name("Letzte Schule: Entlassdatum");
            Map(m => m.LetzteSchuleEntlassjahrgang).Name("Letzte Schule: Entlassjahrgang");
            Map(m => m.LetzteSchuleName).Name("Letzte Schule: Name");
            Map(m => m.LetzteSchuleOrt).Name("Letzte Schule: Ort");
            Map(m => m.LetzteSchulePLZ).Name("Letzte Schule: PLZ");
            Map(m => m.LetzteSchuleSchulform).Name("Letzte Schule: Schulform");
            Map(m => m.LetzteSchuleSchulnr).Name("Letzte Schule: Schulnr.");
            Map(m => m.LetzteSchuleStraße).Name("Letzte Schule: Straße");
            Map(m => m.LetzteSchuleVersetzungsvermerk).Name("Letzte Schule: Versetzungsvermerk");
            Map(m => m.LetzterAllgAbschlussKürzel).Name("Letzter allg. Abschluss (Kürzel)");
            Map(m => m.LetzterBerufsbezAbschlussKürzel).Name("Letzter berufsbez. Abschluss (Kürzel)");
            Map(m => m.Markiert).Name("Markiert?");
            Map(m => m.MigrationshintergrundVorhanden).Name("Migrationshintergrund vorhanden");
            Map(m => m.Nachname).Name("Nachname");
            Map(m => m.Namenszusatz).Name("Namenszusatz");
            Map(m => m.Organisationsform).Name("Organisationsform");
            Map(m => m.Ortsname).Name("Ortsname");
            Map(m => m.Postleitzahl).Name("Postleitzahl");
            Map(m => m.Religionsabmeldung).Name("Religionsabmeldung");
            Map(m => m.Religionsanmeldung).Name("Religionsanmeldung");
            Map(m => m.Schulbesuchsjahre).Name("Schulbesuchsjahre");
            Map(m => m.Schulform).Name("Schulform");
            Map(m => m.SchulformFürSIMExport).Name("Schulform (f. SIM-Export)");
            Map(m => m.Schulgliederung).Name("Schulgliederung");
            Map(m => m.Schuljahr).Name("Schuljahr (z.B. 2008/2009)");
            Map(m => m.SchulNummer).Name("Schul-Nummer");
            Map(m => m.SchulpflichtErfüllt).Name("Schulpflicht erfüllt");
            Map(m => m.Schwerpunkt).Name("Schwerpunkt");
            Map(m => m.Schwerstbehinderung).Name("Schwerstbehinderung");
            Map(m => m.Sportbefreiung).Name("Sportbefreiung");
            Map(m => m.StaatsangehörigkeitKlartext).Name("Staatsangehörigkeit (Klartext)");
            Map(m => m.StaatsangehörigkeitKlartextAdjektiv).Name("Staatsangehörigkeit (Klartext, Adjektiv)");
            Map(m => m.StaatsangehörigkeitSchlüssel).Name("Staatsangehörigkeit (Schlüssel)");
            Map(m => m.Status).Name("Status");
            Map(m => m.Straße).Name("Straße");
            Map(m => m.Straßenname).Name("Straßenname");
            Map(m => m.TelefonNr).Name("Telefon-Nr.");
            Map(m => m.TelefonNummernAnschlussArt).Name("Telefon-Nummern: Anschluss-Art");
            Map(m => m.TelefonNummernBemerkung).Name("Telefon-Nummern: Bemerkung");
            Map(m => m.TelefonNummernTelefonNummer).Name("Telefon-Nummern: Telefon-Nummer");
            Map(m => m.Übergangsempfehlung).Name("Übergangsempfehlung");
            Map(m => m.VerkehrsspracheInDerFamilie).Name("Verkehrssprache in der Familie");
            Map(m => m.Versetzung).Name("Versetzung");
            Map(m => m.Volljährig).Name("Volljährig");
            Map(m => m.VoraussAbschlussdatum).Name("vorauss. Abschlussdatum");
            Map(m => m.Vorname).Name("Vorname");
            Map(m => m.Zuzugsjahr).Name("Zuzugsjahr");
        }
    }

    public Schülers()
    {
        InteressierendeKlassen = [];
        Abschnitt = (DateTime.Now.Month > 2 && DateTime.Now.Month <= 9) ? 2 : 1;

        AktSj = new List<int>() {
            (DateTime.Now.Month >= 7 ? DateTime.Now.Year : DateTime.Now.Year - 1),
            (DateTime.Now.Month >= 7 ? DateTime.Now.Year + 1 : DateTime.Now.Year)
            };
    }

    public Schülers(string importNachSchildPfad, string[] kopfzeile)
    {
        ImportNachSchildPfad = importNachSchildPfad;
        Kopfzeile = kopfzeile;
    }


    public void SchuelerAdressen()
    {
        //Schülers schuelers = new Schülers(Kopfzeile, ImportNachSchildPfad, Global.GetDat("ExportAusWebuntis\\Student_"));

        do
        {
            //Schülers interessierendeSchuelers = schuelers.GetIntessierendeSchuelers();

            //if (interessierendeSchuelers.Keine()) { break; }

            //Datei interessierendeAdressen = Global.GetDat(@"ExportAusAtlantis\AlleAdressen").InteressierendeZeilenFiltern(interessierendeSchuelers);

            ////Datei schuelerAdressen = interessierendeSchuelers.Erstelle(interessierendeAdressen,0);
            ////schuelerAdressen.Dat();

        } while (true);
    }

    public Datei SchuelerTeilleistungen(List<Datei> dateien)
    {
        return Erstelle(dateien);

    }

    private Datei Erstelle(List<Datei> dateien, int sortierIndexDerQuelldatei = 0)
    {
        Datei zielDatei = new()
        {
            DateiPfad = this.ImportNachSchildPfad,
        };
        return zielDatei;
    }


    private int GetIndex(List<string[]> indexs, string k)
    {
        // Suche alle Bergiffe, die in der Quelldatei die passende Spalte addressieren

        for (int i = 0; i < indexs.Count; i++)
        {
            if (indexs[i].Contains(k))
            {
                return i;
            }
        }
        return -1;
    }

    public string Gesamtpunkte2Gesamtnote(string gesamtpunkte)
    {
        gesamtpunkte = gesamtpunkte.Split('.')[0];

        if (gesamtpunkte == "0")
        {
            return "6";
        }
        if (gesamtpunkte == "1")
        {
            return "5";
        }
        if (gesamtpunkte == "2")
        {
            return "5";
        }
        if (gesamtpunkte == "3")
        {
            return "5";
        }
        if (gesamtpunkte == "4")
        {
            return "4";
        }
        if (gesamtpunkte == "5")
        {
            return "4";
        }
        if (gesamtpunkte == "6")
        {
            return "4";
        }
        if (gesamtpunkte == "7")
        {
            return "3";
        }
        if (gesamtpunkte == "8")
        {
            return "3";
        }
        if (gesamtpunkte == "9")
        {
            return "3";
        }
        if (gesamtpunkte == "10")
        {
            return "2";
        }
        if (gesamtpunkte == "11")
        {
            return "2";
        }
        if (gesamtpunkte == "12")
        {
            return "2";
        }
        if (gesamtpunkte == "13")
        {
            return "1";
        }
        if (gesamtpunkte == "14")
        {
            return "1";
        }
        if (gesamtpunkte == "15")
        {
            return "1";
        }
        if (gesamtpunkte == "84")
        {
            return "A";
        }
        if (gesamtpunkte == "99")
        {
            return "-";
        }
        return "";
    }

    public void Schuelerbasisdaten()
    {
        //Schülers schuelers = new Schülers(Kopfzeile, ImportNachSchildPfad, Global.GetDat(@"ExportAusAtlantis\SIM"));

        //do
        //{
        //    Schülers interessierendeSchuelers = schuelers.GetIntessierendeSchuelers();

        //    if (interessierendeSchuelers.Keine()) { break; }

        //    Datei schuelerbasisdaten = interessierendeSchuelers.ErstelleSchuelerbasisdaten();
        //    schuelerbasisdaten.Dat();

        //} while (true);
    }

    //public void Schuelerleistungsdaten()
    //{
    //    Schülers schuelers = new Schülers(Kopfzeile, ImportNachSchildPfad, Global.GetDat("ExportAusWebuntis\\Student_"));

    //    do
    //    {
    //        Schülers interessierendeSchuelers = [.. schuelers.GetIntessierendeSchuelers()];

    //        if (interessierendeSchuelers.Keine()) { break; }

    //        Datei interessierendeExportLessons = Global.GetDat(@"ExportLessons").InteressierendeZeilenFiltern(interessierendeSchuelers);
    //        Datei interessierendeUntisFaecher = new Datei();// Global.GetBereitsEingeleseneDatei(@"ExportAusUntis\GPU006").Filtern(interessierendeExportLessons);
    //        Datei interessierendeStudentgroupStudents = Global.GetDat(@"ExportAusWebuntis\StudentgroupStudents").InteressierendeZeilenFiltern(interessierendeSchuelers);
    //        Datei interessierendeMarksPerLesson = Global.GetDat(@"ExportAusWebuntis\MarksPerLesson").InteressierendeZeilenFiltern(interessierendeSchuelers);
    //        Datei interessierendeLernabschnittsdaten = Global.GetDat(@"ExportAusSchild\SchuelerLernabschnittsdaten").InteressierendeZeilenFiltern(interessierendeSchuelers);
    //        Datei interessierendeAbsencePerStudent = Global.GetDat(@"ExportAusWebuntis\AbsencePerStudent").InteressierendeZeilenFiltern(interessierendeSchuelers);

    //        Datei neueLeistungsdaten = interessierendeSchuelers.Leistungsdaten(
    //            interessierendeExportLessons,
    //            interessierendeStudentgroupStudents,
    //            interessierendeAbsencePerStudent,
    //            interessierendeMarksPerLesson,
    //            Global.GetDat("schuelerleistungsdaten").Kopfzeile,
    //            @"ImportFürSchild\Schülerleistungsdaten.dat");

    //        Global.Dateien.DoppelteZeilenAusDerZweitenDateiEntfernen(
    //            interessierendeUntisFaecher,              // An dieser Datei ...
    //            Global.GetDat("ExportAusSchild"),                            // diese Datei wird an der anderen verglichen
    //            @"ImportFürSchild\Faecher.dat",           // Diese Datei kommt heraus
    //            "In SchILD fehlen Fächer oder Fächer-Eigenschaften weichen ab");

    //        Global.Dateien.DoppelteZeilenAusDerZweitenDateiEntfernen(
    //            neueLeistungsdaten,                                                                     // An dieser Datei ...
    //            Global.GetDat("schuelerleistungsdaten"),                            // diese Datei wird an der anderen verglichen
    //            @"ImportFürSchild\Schuelerleistungsdateien.dat",                                        // Diese Datei kommt heraus
    //            "In SchILD fehlen Leistungsdaten oder Leistungsdaten weichen ab");

    //        Datei neueLernabschnitsdaten = interessierendeSchuelers.SchuelerLernabschnittsdaten(
    //            Global.GetDat("lernabschnittsdaten"),
    //            interessierendeExportLessons,
    //            interessierendeStudentgroupStudents,
    //            interessierendeAbsencePerStudent,
    //            interessierendeMarksPerLesson,
    //            Global.GetDat("schuelerleistungsdaten").Kopfzeile,
    //            @"ImportFürSchild\Lernabschnittsdaten.dat");

    //        Global.Dateien.DoppelteZeilenAusDerZweitenDateiEntfernen(
    //            neueLernabschnitsdaten,                     // An dieser Datei ...
    //            Global.GetDat("lernabschnittsdaten"),                // diese Datei wird an der anderen verglichen
    //            @"ImportFürSchild\Lernabschnittsdaten.dat", // Diese Datei kommt heraus
    //            "In SchILD fehlen Lernabschnittsdaten oder Lernabschnittsdaten weichen ab");


    //    } while (true);
    //}

    public bool Keine()
    {
        if (this.Count == 0) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Es wurde kein einziger Schüler ausgewählt. Erneute Auswahl mit ENTER.");
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.White;

            return true; 
        
        }
        return false;
    }

    //private Datei ErstelleSchuelerbasisdaten()
    //{
    //    Datei basisdaten = new()
    //    {
    //        DateiPfad = this.ImportNachSchildPfad,
    //        Kopfzeile = this.Kopfzeile
    //    };

    //    try
    //    {
    //        foreach (var schueler in this)
    //        {
    //            basisdaten.Zeilen.Add(new List<string>
    //            {
    //                // https://wiki.svws.nrw.de/mediawiki/index.php?title=Schnittstellenbeschreibung#SchuelerBasisdaten.dat

    //                schueler.Nachname,                              //Nachname
    //                schueler.Vorname,                               //Vorname
    //                schueler.Geburtsdatum,                          //Geburtsdatum
    //                schueler.Geschlecht == "3" ? "w" : "m",         //Geschlecht
    //                schueler.Status,                                //Status
    //                schueler.Plz,                                   //PLZ
    //                schueler.Wohnort,                               //Ort
    //                schueler.Straße,                                //Straße
    //                "",                                             //Aussiedler
    //                schueler.Staatsang,                             //1. Staatsang.
    //                schueler.Religion,                              //Konfession
    //                "",                                             //StatistikKrz Konfession
    //                schueler.Aufnahmedatum,                         //Aufnahmedatum
    //                schueler.Austrittsdatum,                        //Abmeldedatum Religionsunterricht
    //                schueler.Relianmeldung,                         //Anmeldedatum Religionsunterricht
    //                schueler.Schulpflichterf,                       //Schulpflicht erf.
    //                schueler.Reformpdg,                             //Reform-Pädagogik
    //                "",                                             //Nr. Stammschule
    //                "",                                             //Jahr
    //                ""  ,                                           //Abschnitt
    //                schueler.Jahrgang  ,                            //Jahrgang
    //                schueler.Klasse  ,                              //Klasse
    //                schueler.Gliederung  ,                          //Schulgliederung
    //                schueler.Orgform  ,                             //OrgForm
    //                schueler.Klassenart  ,                          //Klassenart
    //                schueler.Fachklasse  ,                          //Fachklasse
    //                ""  ,                                           //Noch frei
    //                ""  ,                                           //Verpflichtung Sprachförderkurs
    //                ""  ,                                           //Teilnahme Sprachförderkurs
    //                schueler.JahrEinschulung  ,                     //Einschulungsjahr
    //                ""  ,                                           //Übergangsempf. JG5
    //                ""  ,                                           //Jahr Wechsel S1
    //                ""  ,                                           //1. Schulform S1
    //                ""  ,                                           //Jahr Wechsel S2
    //                schueler.Foerderschwerp  ,                      //Förderschwerpunkt
    //                schueler.Förderschwerpunkt2  ,                  //2. Förderschwerpunkt
    //                schueler.Schwerstbehindert  ,                   //Schwerstbehinderung
    //                ""  ,                                           //Autist
    //                schueler.Lsschulnummer  ,                       //LS Schulnr.
    //                schueler.LSSchulform  ,                         //LS Schulform
    //                ""  ,                                           //Herkunft
    //                schueler.LSSschulentl  ,                        //LS Entlassdatum
    //                schueler.LSJahrgang  ,                          //LS Jahrgang
    //                schueler.Lsversetz  ,                           //LS Versetzung
    //                schueler.Lsreformpdg  ,                         //LS Reformpädagogik
    //                schueler.LSGliederung  ,                        //LS Gliederung
    //                schueler.LSFachklasse  ,                        //LS Fachklasse
    //                ""  ,                                           //LS Abschluss
    //                ""  ,                                           //Abschluss
    //                ""  ,                                           //Schulnr. neue Schule
    //                schueler.JahrZuzug  ,                           //Zuzugsjahr
    //                ""  ,                                           //Geburtsland Schüler
    //                schueler.GebLandMutter  ,                       //Geburtsland Mutter
    //                schueler.GebLandVater  ,                        //Geburtsland Vater
    //                schueler.Verkehrssprache  ,                     //Verkehrssprache
    //                ""  ,                                           //Dauer Kindergartenbesuch
    //                ""  ,                                           //Ende Eingliederungsphase
    //                ""  ,                                           //Ende Anschlussförderung
    //            });
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.ToString());
    //        Console.ReadKey();
    //    }
    //    return basisdaten;
    //}

    public Schülers(Dateien dateien, Aliasse aliasse)
    {
        //try
        //{
            foreach (var item in dateien[0].Zeilen)
            {
                this.Add(new Schueler());
            }

            List<string> schuelerEigenschaften = GetKopfzeile();
        List<string> kopfzeileImportdatei = new List<string>();// dateien[0].Zeilen;
            
            // Alle Eigenschaften der Schüler werden durchlaufen...

            for (int i = 0; i < schuelerEigenschaften.Count(); i++)
            {   
                var Aliasse = aliasse.Where(x => x.Contains(schuelerEigenschaften[i].ToLower())).FirstOrDefault();
                
                for (int y = 0; y < kopfzeileImportdatei.Count; y++)
                {
                    // Wenn einer der Aliasse passt, ...
                    if (Aliasse != null && Aliasse.Select(a => a.ToLower()).Contains(kopfzeileImportdatei[y].ToLower()))
                    {
                        // wird für jedes Schülerobjekt diese Spalte geschrieben.

                        for (int z = 0; z < dateien[0].Zeilen.Count(); z++)
                        {
                            string nameDerEigenschaft = schuelerEigenschaften[i];
                            string wertDerEigenschaft = "";
                            
                            var propertyInfo = typeof(Schueler).GetProperty(nameDerEigenschaft);

                            if (propertyInfo != null && propertyInfo.CanWrite)
                            {
                                propertyInfo.SetValue(this[z], wertDerEigenschaft);
                            }
                        }
                    }
                }
            }
        //}
        //catch (Exception ex)
        //{
        //    Fehler = ex;
        //}
        //finally
        //{
        //    Global.ZeileSchreiben(0, "Schüler*innen-Objekte angelegt", this.Count().ToString(), Fehler);
        //}
    }

    public Schülers(SchBa schuelerBasisdaten)
    {
        foreach (var schuelerBasisdatum in schuelerBasisdaten)
        {
            Schueler schueler = new Schueler();
            schueler.Nachname = schuelerBasisdatum.Nachname;
            schueler.Vorname = schuelerBasisdatum.Vorname;
            schueler.Klasse = schuelerBasisdatum.Klasse;
            schueler.Geburtsdatum = schuelerBasisdatum.Geburtsdatum;
            schueler.Straße = schuelerBasisdatum.Straße;
            schueler.GenerateMail();

            this.Add(schueler);
        }
        
        Global.Ausgaben.Add(new Ausgabe(0, "Schüler*innen aus schuelerBasisdaten", this.Count().ToString()));
    }

    public Schülers(Studs students)
    {
        foreach (var student in students)
        {
            Schueler schueler = new Schueler();
            schueler.Nachname = student.LongName;
            schueler.Vorname = student.ForeName;
            schueler.Klasse = student.KlasseName;
            schueler.Straße = student.AddressStreet;
            schueler.AllgAdressePLZ = student.AddressPostCode;
            schueler.AllgAdresseOrt = student.AddressCity;
            schueler.Geburtsdatum = student.BirthDate;
            this.Add(schueler);
        }
        Global.Ausgaben.Add(new Ausgabe(0, "Schüler*innen aus students_", this.Count().ToString()));
    }

    private List<string> GetKopfzeile()
    {
        List<string> kopfzeileSchuelers = new List<string>();
        foreach (PropertyInfo property in typeof(Schueler).GetProperties())
        {
            kopfzeileSchuelers.Add(property.Name);
        }
        return kopfzeileSchuelers;
    }

    //internal Datei Leistungsdaten(Datei exportLessons, Datei studentgroupStudents, Datei absencePerStudent, Datei marksPerLesson, string[] kopfzeile, string dateiPfad)
    //{
    //    Datei schuelerleistungsdaten = new Datei();
    //    schuelerleistungsdaten.DateiPfad = dateiPfad;
    //    //schuelerleistungsdaten.Kopfzeile = kopfzeile;

    //    try
    //    {
    //        foreach (var schueler in this)
    //        {
    //            schueler.GetFehlstd(absencePerStudent, AktSj, Abschnitt);
    //            schueler.GetUnentFehlstd(absencePerStudent, AktSj, Abschnitt);

    //            //foreach (var zeile in (from zeile in exportLessons.Zeilen
    //            //                       where zeile[Array.IndexOf(exportLessons.Kopfzeile, "klassen")].Split('~').Contains(schueler.Klasse)
    //            //                       select zeile).ToList())
    //            //{
    //            //    string note = schueler.GetNote(Array.IndexOf(marksPerLesson.Kopfzeile, "Gesamtnote"), marksPerLesson);

    //            //    if (zeile[Array.IndexOf(exportLessons.Kopfzeile, "klassen")].Contains(schueler.Klasse))
    //            //    {
    //            //        if (zeile[Array.IndexOf(exportLessons.Kopfzeile, "studentgroup")] == "") // Klassenunterricht werden immer hinzugefügt
    //            //        {
    //            //            schuelerleistungsdaten.Zeilen.Add(new List<string>
    //            //            {
    //            //                schueler.Nachname,
    //            //                schueler.Vorname,
    //            //                schueler.Geburtsdatum,
    //            //                AktSj[0].ToString(),
    //            //                Abschnitt.ToString(),
    //            //                zeile[Array.IndexOf(exportLessons.Kopfzeile, "subject")],
    //            //                zeile[Array.IndexOf(exportLessons.Kopfzeile, "teacher")],
    //            //                "PUK", // Pflichtunterricht im Klassenverband
    //            //                "", // Kein Kursname
    //            //                note,
    //            //                zeile[Array.IndexOf(exportLessons.Kopfzeile, "periods")],
    //            //                "", // ExterneSchulnr
    //            //                "", // Zusatzkraft
    //            //                "", // WochenstdZK
    //            //                "", // Jahrgang
    //            //                "", // Jahrgänge
    //            //                schueler.Fehlstd, // Fehlstd
    //            //                schueler.UnentschFehlstd // UnentschFehlstd
    //            //            });
    //            //        }
    //            //    }
    //            //}
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.ToString());
    //        Console.ReadKey();
    //    }
    //    return schuelerleistungsdaten;
    //}

    public Schülers Interessierende(Datei zielDatei)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appSettings.json", optional: false, reloadOnChange: true).Build();
        var klassen = configuration["Klassen"];

        InteressierendeKlassen = new List<string>();
        InteressierendeSchuelers = new Schülers();

        var linkeSeite = "Sie haben diese Klassen gewählt:";
        var rechteSeite = "keine";

        try
        {
            Console.WriteLine("");
            Console.WriteLine("   Bitte die interessierende Klasse oder die");
            Console.WriteLine("   interessierenden Klassen kommasepariert eingeben.");
            if (klassen == "")
            {
                Console.WriteLine("   Mit ENTER wählen Sie alle Klassen aus.");
            }
            else
            {
                Console.WriteLine("   Geben Sie 'alle' ein, um die Auswahl der Klassen nicht einzuschränken.");
            }
            
            Console.WriteLine("   Sie können auch nur den oder die Anfangsbuchstaben");
            Console.WriteLine("   kommaspariert eingeben. Abbruch mit q:");

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("      Ihre Auswahl [" + klassen + "] : ");
            Console.ResetColor();

            var x = Console.ReadLine().ToLower();

            if (x == "ö")
            {
                Global.OpenCurrentFolder();
            }
            if (x == "x")
            {
                Global.OpenWebseite("https://wiki.svws.nrw.de/mediawiki/index.php?title=Schnittstellenbeschreibung");
            }

            if ((x == "" && klassen == "") || x.ToLower() == "alle")
            {
                InteressierendeKlassen.AddRange((from s in this select s.Klasse).Distinct().ToList());
                rechteSeite = "alle Klassen";
                Global.Speichern("Klassen", "");
            }
            else
            {
                if (x == "" && klassen != "")
                {
                    x = klassen;
                }
                foreach (var klasse in (from s in this select s.Klasse).Distinct().ToList())
                {
                    foreach (var item in x.Trim().Split(','))
                    {
                        if (klasse.ToLower().StartsWith(item))
                        {
                            if (!InteressierendeKlassen.Select(x=>x.ToLower()).Contains(klasse.ToLower()))
                            {
                                InteressierendeKlassen.Add(klasse);
                            }
                        }
                    }
                }
                if (InteressierendeKlassen.Count > 0)
                {
                    Global.Speichern("Klassen", x);
                    rechteSeite = string.Join(",", InteressierendeKlassen);
                }
                else
                {
                    // Wenn q oder ein anderer Buchstabe getippt wurde, dem keine Klasse zugeordnet werden kann, werden 0 SuS zurückgegeben.
                    return new Schülers();
                }
            }

            InteressierendeSchuelers.AddRange((from t in this where InteressierendeKlassen.Contains(t.Klasse) select t).ToList());
        }
        catch (Exception ex)
        {
            Fehler = ex;
        }
        finally
        {
            Global.DisplayHeader();
            Global.DisplayHeader(zielDatei.Titel, '-');
            Console.WriteLine("");
            Global.ZeileSchreiben(0, linkeSeite, rechteSeite, Fehler);
            linkeSeite = (string.Join(",", InteressierendeKlassen.Order()));
            linkeSeite = linkeSeite.Substring(0, Math.Min(Console.WindowWidth / 2, linkeSeite.Length));
            
            if (InteressierendeKlassen.Count > 3)
            {
                linkeSeite = linkeSeite + " (" + InteressierendeKlassen.Count().ToString() + " Klassen)";
            }
            if (linkeSeite != "")
            {
                Global.ZeileSchreiben(0, linkeSeite, InteressierendeSchuelers.Count().ToString(), Fehler);
            }
        }

        return InteressierendeSchuelers;
    }

    //internal Datei SchuelerLernabschnittsdaten(Datei lernabschnittsdaten, Datei exportLessons, Datei studentgroupStudents, Datei absencePerStudent, Datei marksPerLesson, string[] kopfzeile, string v)
    //{
    //    Datei schuelerLernabschnittsdaten = new Datei();
    //    schuelerLernabschnittsdaten.DateiPfad = v;
    //    schuelerLernabschnittsdaten.Kopfzeile = kopfzeile;

    //    try
    //    {
    //        foreach (var schueler in this)
    //        {
    //            schueler.GetFehlstd(absencePerStudent, AktSj, Abschnitt);
    //            schueler.GetUnentFehlstd(absencePerStudent, AktSj, Abschnitt);

    //            schuelerLernabschnittsdaten.Zeilen.Add(new List<string>
    //            {
    //                schueler.Nachname,                              // Plichtfeld
    //                schueler.Vorname,                               // Plichtfeld
    //                schueler.Geburtsdatum,   // Plichtfeld
    //                AktSj[0].ToString(),                            // Plichtfeld
    //                Abschnitt.ToString(),                           // Plichtfeld
    //                "", // Jahrgang
    //                "", // Klasse
    //                "", // Schulgliederung
    //                "", // OrgForm
    //                "", // Klassenart
    //                "", // Fachklasse
    //                "", // Förderschwerpunkt
    //                "", // Förderschwerpunkt2
    //                "", // Schwerstbehinderung      // Plichtfeld
    //                "", // Wertung                  // Plichtfeld
    //                "", // Wiederholung             // Plichtfeld
    //                "", // Klassenlehrer
    //                "", // Versetzung
    //                "", // Abschluss
    //                "", // Schwerpunkt
    //                "", // Konferenzdatum
    //                "", // Zeugnisdatum
    //                schueler.Fehlstd, // SummeFehlstd
    //                schueler.UnentschFehlstd, // SummeFehlstd_unentschuldigt
    //                "", // allgBildenderAbschluss
    //                "", // berufsbezAbschluss
    //                "", // Zeugnisart
    //                "", // FehlstundenGrenzwert
    //                "", // DatumVon
    //                "" // DatumBis
    //            });
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.ToString());
    //        Console.ReadKey();
    //    }
    //    return schuelerLernabschnittsdaten;
    //}
}