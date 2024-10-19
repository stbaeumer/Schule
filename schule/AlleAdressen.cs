using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class AllAd : List<AlleAdresse>
{
    public AllAd(string dateiName, string dateiendung = "*.dat", string delimiter = "|")
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

        // Konfiguration für CsvReader: Header und Delimiter anpassen
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
            HasHeaderRecord = true,   // Gibt an, dass die CSV-Datei eine Kopfzeile hat
            Delimiter = delimiter,
            BadDataFound = null
        };

        using (var reader = new StreamReader(dateiPfad))
        using (var csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<AlleAdressenMap>();
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            var records = csv.GetRecords<AlleAdresse>();
            this.AddRange(records);
        }
        Global.ZeileSchreiben(0, dateiPfad, this.Count().ToString(), null, null);
    }

    internal List<Zeile> GetSchuelerAdressen(Schülers iSchuS)
    {
        List<Zeile> z = new List<Zeile>();

        foreach (var schueler in iSchuS)
        {
            var ooo = this
                .Where(x => x.Nachname == schueler.Nachname
                    && x.SchülerVorname == schueler.Vorname
                    ).ToList();
                        
            Zeile zeile = new Zeile();

            foreach (var item in ooo)
            {
            zeile.Add(schueler.Nachname);           // Nachname 
            zeile.Add(schueler.Vorname);            // Vorname 
            zeile.Add(schueler.Geburtsdatum);       // Geburtsdatum
            zeile.Add("");                         //Adressart
            zeile.Add("");                         //Name1
            zeile.Add("");                         //Name2
            zeile.Add("");                         //Straße
            zeile.Add("");                         //PLZ
            zeile.Add("");                         //Ort
            zeile.Add("");                         //1. Tel.-Nr.
            zeile.Add("");                         //2. Tel.-Nr.
            zeile.Add("");                         //E-Mail
            zeile.Add("");                         //Betreuer Nachname
            zeile.Add("");                         //Betreuer Vorname
            zeile.Add("");                         //Betreuer Anrede
            zeile.Add("");                         //Betreuer Tel.-Nr.
            zeile.Add("");                         //Betreuer E-Mail
            zeile.Add("");                         //Betreuer Abteilung
            zeile.Add("");                         //Vertragsbeginn
            zeile.Add("");                         //Vertragsende
            zeile.Add("");                         //Fax-Nr.
            zeile.Add("");                         //Bemerkung
            zeile.Add("");                         //Branche
            zeile.Add("");                         //Zusatz 1
            zeile.Add("");                         //Zusatz 2
            zeile.Add("");                         //SchILD-Adress-ID
            zeile.Add("");                         //externe Adress-ID
            }
        }
        return z;
    }

    internal IEnumerable<Zeile> GetSchuelerTelefonnummern(Schülers iSchuS)
    {
        List<Zeile> z = new List<Zeile>();

        foreach (var schueler in iSchuS)
        {
            var ooo = this
                .Where(x => x.Nachname == schueler.Nachname
                    && x.SchülerVorname == schueler.Vorname
                    ).ToList();

            Zeile zeile = new Zeile();

            foreach (var item in ooo)
            {
                zeile.Add(schueler.Nachname);           // Nachname
                zeile.Add(schueler.Vorname);            // Vorname 
                zeile.Add(schueler.Geburtsdatum);       // Geburtsdatum
                zeile.Add("");                          // Telefonnr. 
                zeile.Add("");                          // Art 
            }
        }
        return z;
    }
}

public class AlleAdressenMap : ClassMap<AlleAdresse>
{
    public AlleAdressenMap()
    {
        Map(m => m.LfdNr).Name("Lfd Nr");
        Map(m => m.Nachname).Name("Nachname");
        Map(m => m.Name2DerAdresse).Name("Name 2 der Adresse");
        Map(m => m.Name3DerAdresse).Name("Name 3 der Adresse");
        Map(m => m.GebäudeOrt).Name("Gebäude: Ort");
        Map(m => m.GebäudePlz).Name("Gebäude: Plz");
        Map(m => m.GebäudeStraße).Name("Gebäude: Straße");
        Map(m => m.Abrechnungsmodus).Name("Abrechnungsmodus");
        Map(m => m.Abteilung).Name("Abteilung");
        Map(m => m.AdresseQuellKennzeichen).Name("Adresse Quell-Kennzeichen");
        Map(m => m.AdresseKategorien).Name("Adresse-Kategorien");
        Map(m => m.AdresseBerufLangtext).Name("Adresse: Beruf (Langtext)");
        Map(m => m.Adresstyp).Name("Adresstyp");
        Map(m => m.AkadTitel).Name("akad. Titel");
        Map(m => m.Aktiv).Name("aktiv");
        Map(m => m.AnredeAuflösung).Name("Anrede (Auflösung)");
        Map(m => m.Anredeform).Name("Anredeform");
        Map(m => m.Anredetext).Name("Anredetext");
        Map(m => m.Ansprechpartner).Name("Ansprechpartner");
        Map(m => m.AnzahlKopien).Name("Anzahl Kopien");
        Map(m => m.Arbeitgeber).Name("Arbeitgeber");
        Map(m => m.ArtDerKommunikation).Name("Art der Kommunikation");
        Map(m => m.ArtDerKommunikationAuflösung).Name("Art der Kommunikation (Auflösung)");
        Map(m => m.ArtDerLastschriftausführungSEPA).Name("Art der Lastschriftausführung (SEPA)");
        Map(m => m.Austrittsdatum).Name("Austrittsdatum");
        Map(m => m.Ausweisnummer).Name("Ausweisnummer");
        Map(m => m.Ausweisnummer2).Name("Ausweisnummer 2");
        Map(m => m.Ausweisnummer3).Name("Ausweisnummer 3");
        Map(m => m.Ausweisnummer4).Name("Ausweisnummer 4");
        Map(m => m.Belegdruck).Name("Belegdruck");
        Map(m => m.BemerkungZurAdresse).Name("Bemerkung zur Adresse");
        Map(m => m.Beruf).Name("Beruf");
        Map(m => m.Briefadresse).Name("Briefadresse");
        Map(m => m.DatumDerErteilungDesSEPAMandats).Name("Datum der Erteilung des SEPA-Mandats");
        Map(m => m.DatumErsteBuchung).Name("Datum erste Buchung");
        Map(m => m.DatumLetzteBuchung).Name("Datum letzte Buchung");
        Map(m => m.DebitorSaldo).Name("Debitor Saldo");
        Map(m => m.DebitorGruppe).Name("Debitor-Gruppe");
        Map(m => m.DebitorDebitornummer).Name("Debitor: Debitornummer");
        Map(m => m.EmailKomplett).Name("E-Mail (komplett)");
        Map(m => m.EmailVertraulich).Name("E-Mail (vertraulich)");
        Map(m => m.EmailAdresse).Name("E-Mail-Adresse");
        Map(m => m.EmailVersandAktiv).Name("E-Mail-Versand aktiv");
        Map(m => m.Eintrittsdatum).Name("Eintrittsdatum");
        Map(m => m.ExterneID).Name("Externe ID");
        Map(m => m.Familienstand).Name("Familienstand");
        Map(m => m.Geburtsdatum).Name("Geburtsdatum");
        Map(m => m.Gehalt).Name("Gehalt");
        Map(m => m.Gemeindekennzeichen).Name("Gemeindekennzeichen");
        Map(m => m.Hausnummer).Name("Hausnummer");
        Map(m => m.Herkunftsland).Name("Herkunftsland");
        Map(m => m.HomepageURL).Name("Homepage (URL)");
        Map(m => m.IBAN).Name("IBAN");
        Map(m => m.Kalenderjahr).Name("Kalenderjahr");
        Map(m => m.KennzeichenDerHauptadresse).Name("Kennzeichen der Hauptadresse");
        Map(m => m.Kontakt1JN).Name("Kontakt 1 (J/N)");
        Map(m => m.Kontakt2JN).Name("Kontakt 2 (J/N)");
        Map(m => m.Kontakt3JN).Name("Kontakt 3 (J/N)");
        Map(m => m.Kontoinhaber).Name("Kontoinhaber");
        Map(m => m.Korrespondenzsprache).Name("Korrespondenzsprache");
        Map(m => m.LehrerSuchbegriff).Name("Lehrer: Suchbegriff");
        Map(m => m.Länderkennzeichen).Name("Länderkennzeichen");
        Map(m => m.Mahnkennzeichen).Name("Mahnkennzeichen");
        Map(m => m.Mitgliedstyp).Name("Mitgliedstyp");
        Map(m => m.NameDesBankinstituts).Name("Name des Bankinstituts");
        Map(m => m.NummerFürSMSVersand).Name("Nummer für SMS-Versand");
        Map(m => m.NächstRechStellung).Name("nächst Rech.Stellung");
        Map(m => m.Ortsteil).Name("Ortsteil");
        Map(m => m.Personalausweisnummer).Name("Personalausweisnummer");
        Map(m => m.Postfach).Name("Postfach");
        Map(m => m.PostleitzahlDerPostfachadresse).Name("Postleitzahl der Postfachadresse");
        Map(m => m.Rechnungsrythmus).Name("Rechnungsrythmus");
        Map(m => m.SaldoAktuellZum).Name("Saldo (aktuell) zum");
        Map(m => m.SchuleAmtlicheBezeichnung1).Name("Schule: Amtliche Bezeichnung 1");
        Map(m => m.SchuleAmtlicheBezeichnung2).Name("Schule: Amtliche Bezeichnung 2");
        Map(m => m.SchuleAmtlicheBezeichnung3).Name("Schule: Amtliche Bezeichnung 3");
        Map(m => m.SchuleEmailAdresse).Name("Schule: E-Mail-Adresse");
        Map(m => m.SchuleGenitivName).Name("Schule: Genitiv Name");
        Map(m => m.SchuleGläubigerIdentifikationsnummer).Name("Schule: Gläubiger-Identifikationsnummer");
        Map(m => m.SchuleHomepage).Name("Schule: Homepage");
        Map(m => m.SchuleOrt).Name("Schule: Ort");
        Map(m => m.SchuleOrtsteil).Name("Schule: Ortsteil");
        Map(m => m.SchulePlz).Name("Schule: Plz");
        Map(m => m.SchuleSchulartAuflösung).Name("Schule: Schulart (Auflösung)");
        Map(m => m.SchuleSchulname).Name("Schule: Schulname");
        Map(m => m.SchuleSchulname2).Name("Schule: Schulname 2");
        Map(m => m.SchuleSchulname3).Name("Schule: Schulname 3");
        Map(m => m.SchuleSchulnummer).Name("Schule: Schulnummer");
        Map(m => m.SchuleStraße).Name("Schule: Straße");
        Map(m => m.SchuleTelefax).Name("Schule: Telefax");
        Map(m => m.SchuleTelefon1).Name("Schule: Telefon 1");
        Map(m => m.SchuleTelefon2).Name("Schule: Telefon 2");
        Map(m => m.SchuleZeugniskopfzeile1).Name("Schule: Zeugniskopfzeile 1");
        Map(m => m.SchuleZeugniskopfzeile2).Name("Schule: Zeugniskopfzeile 2");
        Map(m => m.SchuleZeugniskopfzeile3).Name("Schule: Zeugniskopfzeile 3");
        Map(m => m.SchülerAnrede).Name("Schüler: Anrede");
        Map(m => m.SchülerAusgetretenJN).Name("Schüler: Ausgetreten (J/N)");
        Map(m => m.SchülerBekenntnis).Name("Schüler: Bekenntnis");
        Map(m => m.SchülerGeschlecht).Name("Schüler: Geschlecht");
        Map(m => m.SchülerNachname).Name("Schüler: Nachname");
        Map(m => m.SchülerVolljährigJN).Name("Schüler: Volljährig (J/N)");
        Map(m => m.SchülerVorgangSchuljahr).Name("Schüler: Vorgang Schuljahr");
        Map(m => m.SchülerVorname).Name("Schüler: Vorname");
        Map(m => m.SchülerWird16Am).Name("Schüler: wird 16 am");
        Map(m => m.SchülerWird18Am).Name("Schüler: wird 18 am");
        Map(m => m.SorgeberechtigungJN).Name("Sorgeberechtigung (J/N)");
        Map(m => m.SorgeberechtigungTyp).Name("Sorgeberechtigung Typ");
        Map(m => m.SozialversNr).Name("Sozialvers.Nr.");
        Map(m => m.Staatsangehörigkeit).Name("Staatsangehörigkeit");
        Map(m => m.SwiftBICCode).Name("Swift-(BIC)-Code");
        Map(m => m.Telefax).Name("Telefax");
        Map(m => m.Telefon1).Name("Telefon 1");
        Map(m => m.Telefon1Typ).Name("Telefon 1 Typ");
        Map(m => m.Telefon2).Name("Telefon 2");
        Map(m => m.Telefon2Typ).Name("Telefon 2 Typ");
        Map(m => m.Telefon3).Name("Telefon 3");
        Map(m => m.Telefon3Typ).Name("Telefon 3 Typ");
        Map(m => m.Telefon4).Name("Telefon 4");
        Map(m => m.Telefon4Typ).Name("Telefon 4 Typ");
        Map(m => m.TextfeldAuszubildende).Name("Textfeld: Auszubildende/r");
        Map(m => m.TextfeldDemDerGroß).Name("Textfeld: Dem/Der (groß)");
        Map(m => m.TextfeldDemDerKlein).Name("Textfeld: dem/der (klein)");
        Map(m => m.TextfeldDenDieGroß).Name("Textfeld: Den/Die (groß)");
        Map(m => m.TextfeldDenDieKlein).Name("Textfeld: den/die (klein)");
        Map(m => m.TextfeldDerDieGroß).Name("Textfeld: Der/Die (groß)");
        Map(m => m.TextfeldDerDieKlein).Name("Textfeld: der/die (klein)");
        Map(m => m.TextfeldDesSchülersDerSchülerin).Name("Textfeld: des Schülers/der Schülerin");
        Map(m => m.TextfeldDesDerGroß).Name("Textfeld: Des/Der (groß)");
        Map(m => m.TextfeldDesDerKlein).Name("Textfeld: des/der (klein)");
        Map(m => m.TextfeldErSieGroß).Name("Textfeld: Er/Sie (groß)");
        Map(m => m.TextfeldErSieKlein).Name("Textfeld: er/sie (klein)");
        Map(m => m.TextfeldHerrFrau).Name("Textfeld: Herr/Frau");
        Map(m => m.TextfeldIhrSohnIhreTochter).Name("Textfeld: Ihr Sohn/Ihre Tochter");
        Map(m => m.TextfeldIhrIhreGroß).Name("Textfeld: Ihr/Ihre (groß)");
        Map(m => m.TextfeldIhrIhreKlein).Name("Textfeld: ihr/ihre (klein)");
        Map(m => m.TextfeldIhremSohnIhrerTochter).Name("Textfeld: Ihrem Sohn/Ihrer Tochter");
        Map(m => m.TextfeldIhrenSohnIhreTochter).Name("Textfeld: Ihren Sohn/Ihre Tochter");
        Map(m => m.TextfeldIhresSohnesIhrerTochter).Name("Textfeld: Ihres Sohnes/Ihrer Tochter");
        Map(m => m.TextfeldJungeMädchen).Name("Textfeld: Junge/Mädchen");
        Map(m => m.TextfeldSchülerIn).Name("Textfeld: Schüler/in");
        Map(m => m.TextfeldSeinIhrGroß).Name("Textfeld: Sein/Ihr (groß)");
        Map(m => m.TextfeldSeinIhrKlein).Name("Textfeld: sein/ihr (klein)");
        Map(m => m.TextfeldSeineIhreGroß).Name("Textfeld: Seine/Ihre (groß)");
        Map(m => m.TextfeldSeineIhreKlein).Name("Textfeld: seine/ihre (klein)");
        Map(m => m.TextfeldSeinemIhremGroß).Name("Textfeld: Seinem/Ihrem (groß)");
        Map(m => m.TextfeldSeinemIhremKlein).Name("Textfeld: seinem/ihrem (klein)");
        Map(m => m.TextfeldSeinerIhrerGroß).Name("Textfeld: Seiner/Ihrer (groß)");
        Map(m => m.TextfeldSeinerIhrerKlein).Name("Textfeld: seiner/ihrer (klein)");
        Map(m => m.TextfeldSeinesIhresGroß).Name("Textfeld: Seines/Ihres (groß)");
        Map(m => m.TextfeldSeinesIhresKlein).Name("Textfeld: seines/ihres (klein)");
        Map(m => m.TextfeldSohnTochter).Name("Textfeld: Sohn/Tochter");
        Map(m => m.SchülerTypDerAdresse).Name("Schüler: Typ der Adresse");
        Map(m => m.TypDTAUSVerfahren).Name("Typ DTAUS-Verfahren");
        Map(m => m.VerkehrsMuttersprache).Name("Verkehrs-/Muttersprache");
        Map(m => m.Vorlagensprache).Name("Vorlagensprache");
        Map(m => m.ZahlVerkehrGutschrift).Name("Zahl.-Verkehr (Gutschrift)");
        Map(m => m.ZahlungsplanErwuenscht).Name("Zahlungsplan erwünscht");
        Map(m => m.Zahlungsverkehr).Name("Zahlungsverkehr");
        Map(m => m.Zahlweise).Name("Zahlweise");
        Map(m => m.ZeigerAufAdressentabelle).Name("Zeiger auf Adressentabelle");
        Map(m => m.ZeigerAufBetriebesatz).Name("Zeiger auf Betriebesatz");
        Map(m => m.ZeigerAufGebaeudesatz).Name("Zeiger auf Gebäudesatz");
        Map(m => m.ZeigerAufHeimBT).Name("Zeiger auf Heim (BT)");
        Map(m => m.ZeigerAufLaufbahnsatz).Name("Zeiger auf Laufbahnsatz");
        Map(m => m.ZeigerAufLehrerstammsatz).Name("Zeiger auf Lehrerstammsatz");
        Map(m => m.ZeigerAufSchulensatz).Name("Zeiger auf Schulensatz");
        Map(m => m.ZeigerAufSchuelersatz).Name("Zeiger auf Schülersatz");
        Map(m => m.ZeigerAufStammIDAdresse).Name("Zeiger auf Stamm-ID Adresse");
        Map(m => m.ZeigerAufStammIDBetrieb).Name("Zeiger auf Stamm-ID Betrieb");
        Map(m => m.ZeigerAufStammIDLehrer).Name("Zeiger auf Stamm-ID Lehrer");
        Map(m => m.ZeigerAufStammIDSchule).Name("Zeiger auf Stamm-ID Schule");
        Map(m => m.ZeigerAufStammIDSchueler).Name("Zeiger auf Stamm-ID Schüler");
        Map(m => m.ZeigerAufVertragssatz).Name("Zeiger auf Vertragssatz");
        Map(m => m.ZeitstempelLetzteAenderung).Name("Zeitstempel letzte Änderung");
    }
}