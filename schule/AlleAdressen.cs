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
                .Where(x => x.SchülerNachname == schueler.Nachname
                    && x.SchülerVorname == schueler.Vorname
                    && x.SchülerGeburtsdatum == schueler.Geburtsdatum
                    ).ToList();

            foreach (var item in ooo)
            {
                // Für alle mit diesem Schüler verbundene Betriebe

                foreach (var betriebName in ooo.Select(x => x.BetriebName1).Distinct())
                {
                    var betrieb = ooo.Where(x => x.BetriebName1 == betriebName).FirstOrDefault();

                    Zeile zeile = new Zeile();
                    zeile.Add(schueler.Nachname);           // Nachname 
                    zeile.Add(schueler.Vorname);            // Vorname 
                    zeile.Add(schueler.Geburtsdatum);       // Geburtsdatum
                    zeile.Add("Betrieb");               //Adressart Als Klartext, z.B. "Betrieb" 
                    zeile.Add(item.BetriebName1);                         //Name1
                    zeile.Add(item.BetriebName2);                         //Name2
                    zeile.Add(item.BetriebStraße);                         //Straße
                    zeile.Add(item.BetriebPlz);                         //PLZ
                    zeile.Add(item.BetriebOrt);                         //Ort
                    zeile.Add(item.BetriebTelefon1);                         //1. Tel.-Nr.
                    zeile.Add(item.BetriebTelefon2);                         //2. Tel.-Nr.
                    zeile.Add(item.BetriebEmailAdresse);                         //E-Mail
                    zeile.Add(item.BetriebAnsprechpartner);                         //Betreuer Nachname
                    zeile.Add("");                         //Betreuer Vorname
                    zeile.Add(item.BetriebAnrede);                         //Betreuer Anrede
                    zeile.Add("");                         //Betreuer Tel.-Nr.
                    zeile.Add("");                         //Betreuer E-Mail
                    zeile.Add("");                         //Betreuer Abteilung
                    zeile.Add("");                         //Vertragsbeginn
                    zeile.Add("");                         //Vertragsende
                    zeile.Add("");                         //Fax-Nr.
                    zeile.Add("Betrieb-Nr.:" + item.BetriebBetriebenummer); //Bemerkung
                    zeile.Add("");                                          //Branche
                    zeile.Add("");                         //Zusatz 1
                    zeile.Add("");                         //Zusatz 2
                    zeile.Add("");                         //SchILD-Adress-ID
                    zeile.Add("");                         //externe Adress-ID
                    z.Add(zeile);
                }
            }
        }
        return z;
    }

    internal IEnumerable<Zeile> GetSchuelerErzieher(Schülers iSchuS)
    {
        List<Zeile> z = new List<Zeile>();

        foreach (var schueler in iSchuS)
        {
            var adressenDesSuS = this.Where(x => schueler.Nachname == x.SchülerNachname && schueler.Vorname == x.SchülerVorname && schueler.Geburtsdatum == x.SchülerGeburtsdatum).ToList();

            Zeile zeile = new Zeile();

            zeile.Add(schueler.Nachname);           // Nachname
            zeile.Add(schueler.Vorname);            // Vorname 
            zeile.Add(schueler.Geburtsdatum);       // Geburtsdatum
            zeile.Add("Eltern");
            foreach (var item in adressenDesSuS)
            {
                var x = item.AdresseTypAdresse == "0" ? "Schüler/-in" : item.AdresseTypAdresse == "V" ? "Vater" : item.AdresseTypAdresse == "M" ? "Mutter" : "Betrieb";               //Adressart Als Klartext, z.B. "Betrieb" 

                if (x == "Mutter")
                {
                    zeile.Add(x);                             // Erzieherart (not available in the provided class)
                    zeile.Add(item.SchülerAnrede == "F" ? "Frau" : "Herr");            // Anrede 1.Person
                    zeile.Add(item.AdresseTitel);             // Titel 1.Person
                    zeile.Add(item.AdresseName1);             // Nachname 1.Person
                    zeile.Add(item.AdresseName2);             // Vorname 1.Person
                    zeile.Add(item.AdresseStraße);            // Straße
                    zeile.Add(item.AdressePlz);               // PLZ
                    zeile.Add(item.AdresseOrt);               // Ort
                    zeile.Add(item.BetriebOrtsteil);          // Ortsteil
                    zeile.Add(item.AdresseEmail);    
                }
                if(x== "Vater")
                {
                    zeile.Add(item.SchülerAnrede == "H" ? "Herr" : "Frau");            // Anrede 2.Person
                    zeile.Add(item.AdresseTitel);             // Titel 2.Person
                    zeile.Add(item.AdresseName1);          // Nachname 2.Person
                    zeile.Add(item.AdresseName2);           // Vorname 2.Person
                }
            }
            z.Add(zeile);
        }
        return z;
    }

    internal IEnumerable<Zeile> GetSchuelerTelefonnummern(Schülers iSchuS)
    {
        List<Zeile> z = new List<Zeile>();

        foreach (var schueler in iSchuS)
        {
            var ooo = this
                .Where(x => x.SchülerNachname == schueler.Nachname
                    && x.SchülerVorname == schueler.Vorname
                    && x.SchülerGeburtsdatum == schueler.Geburtsdatum
                    ).ToList();

            foreach (var item in ooo)
            {
                Zeile zeile = new Zeile();

                var x = item.AdresseTypAdresse == "0" ? "Schüler/-in" : item.AdresseTypAdresse == "V" ? "Vater" : item.AdresseTypAdresse == "M" ? "Mutter" : "Betrieb";               //Adressart Als Klartext, z.B. "Betrieb" 

                zeile.Add(schueler.Nachname);           // Nachname
                zeile.Add(schueler.Vorname);            // Vorname 
                zeile.Add(schueler.Geburtsdatum);       // Geburtsdatum
                zeile.Add(item.AdresseTelefon1);        // Telefonnr. 
                zeile.Add(x);                           // Art Beschreibung der Telefonnr, z.B. "Eltern", "Handy Schüler", "Büro Vater", "Handy Mutter" usw. 
                if (item.AdresseTelefon1 != "")
                {
                    z.Add(zeile);
                }
                
                if (item.AdresseTelefon2 != null && item.AdresseTelefon2 != "")
                {
                    zeile = new Zeile();
                    zeile.Add(schueler.Nachname);           // Nachname
                    zeile.Add(schueler.Vorname);            // Vorname 
                    zeile.Add(schueler.Geburtsdatum);       // Geburtsdatum
                    zeile.Add(item.AdresseTelefon2);                          // Telefonnr. 
                                                                              // Art Beschreibung der Telefonnr, z.B. "Eltern", "Handy Schüler", "Büro Vater", "Handy Mutter" usw. 
                    zeile.Add(x);
                    if (item.AdresseTelefon2 != "")
                    {
                        z.Add(zeile);
                    }
                }
            }
        }
        return z;
    }

    internal IEnumerable<Zeile> GetSchuelerZusatzdaten(Schülers iSchuS)
    {
        List<Zeile> z = new List<Zeile>();

        foreach (var schueler in iSchuS)
        {
            var ooo = this
                .Where(x => x.SchülerNachname == schueler.Nachname
                    && x.SchülerVorname == schueler.Vorname
                    && x.SchülerGeburtsdatum == schueler.Geburtsdatum
                    ).ToList();

            foreach (var item in ooo)
            {
                Zeile zeile = new Zeile();
                zeile.Add(schueler.Nachname);             // Nachname
                zeile.Add(schueler.Vorname);              // Vorname 
                zeile.Add(schueler.Geburtsdatum);         // Geburtsdatum
                zeile.Add(item.AdresseTelefon1);          // Nachname|Vorname|Geburtsdatum|Namenszusatz|Geburtsname|Geburtsort|Ortsteil|
                zeile.Add(schueler.Geburtsname);          // zeile.Add(item.SchülerGeburtsname);       // Geburtsname
                zeile.Add(item.SchülerGeburtsort);        // Geburtsort
                zeile.Add(schueler.Ortsteil);             // Ortsteil (not available in the provided class)
                zeile.Add(item.AdresseTelefon1);          // Telefon-Nr.
                zeile.Add(item.AdresseEmail);             // E-Mail
                zeile.Add(item.SchülerStaat);             // 2. Staatsang.
                zeile.Add(item.AdresseExterneID);         // Externe ID-Nr
                zeile.Add("");                            // Sportbefreiung (not available in the provided class)
                zeile.Add("");                            // Fahrschülerart (not available in the provided class)
                zeile.Add("");                            // Haltestelle (not available in the provided class)
                zeile.Add("");                            // Einschulungsart (not available in the provided class)
                zeile.Add(schueler.Entlassdatum);         // Entlassdatum (not available in the provided class)
                zeile.Add("");                            // Entlassjahrgang (not available in the provided class)
                zeile.Add("");                            // Datum Schulwechsel (not available in the provided class)
                zeile.Add("");                            // Bemerkungen (not available in the provided class)
                zeile.Add("");                            // BKAZVO (not available in the provided class)
                zeile.Add("");                            // BeginnBildungsgang (not available in the provided class)
                zeile.Add("");                            // Anmeldedatum (not available in the provided class)
                zeile.Add("");                            // Bafög (not available in the provided class)
                zeile.Add("");                            // EP-Jahre (not available in the provided class)
                zeile.Add(item.AdresseTelefon2);          // Fax/Mobilnr
                zeile.Add(item.AdresseAusweisnummer);     // Ausweisnummer
                zeile.Add(schueler.EmailSchulisch);       // schulische E-Mail
                z.Add(zeile);
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
        Map(m => m.AdresseName1).Name("Adresse: Name 1");
        Map(m => m.AdresseName2).Name("Adresse: Name 2");
        Map(m => m.KlasseKlassenbezeichnung).Name("Klasse: Klassenbezeichnung");
        Map(m => m.SchülerNachname).Name("Schüler: Nachname");
        Map(m => m.SchülerVorname).Name("Schüler: Vorname");
        Map(m => m.AdresseAusweisnummer).Name("Adresse: Ausweisnummer");
        Map(m => m.AdresseDatumAustritt).Name("Adresse: Datum Austritt");
        Map(m => m.AdresseDatumEintritt).Name("Adresse: Datum Eintritt");
        Map(m => m.AdresseDatumGeburt).Name("Adresse: Datum Geburt");
        Map(m => m.AdresseEmail).Name("Adresse: E-Mail");
        Map(m => m.AdresseEmailKomplett).Name("Adresse: E-Mail (komplett)");
        Map(m => m.AdresseFamilienstand).Name("Adresse: Familienstand");
        Map(m => m.AdresseExterneID).Name("Adresse: externe ID");
        Map(m => m.AdresseOrt).Name("Adresse: Ort");
        Map(m => m.AdressePlz).Name("Adresse: Plz");
        Map(m => m.AdressePostfachPlz).Name("Adresse: Postfach Plz");
        Map(m => m.AdressePostfach).Name("Adresse: Postfach");
        Map(m => m.AdresseSorgeberechtigtJN).Name("Adresse: Sorgeberechtigt (J/N)");
        Map(m => m.AdresseSprache).Name("Adresse: Sprache");
        Map(m => m.AdresseStaat).Name("Adresse: Staat");
        Map(m => m.AdresseStraße).Name("Adresse: Straße");
        Map(m => m.AdresseTelefon1).Name("Adresse: Telefon 1");
        Map(m => m.AdresseTelefon2).Name("Adresse: Telefon 2");
        Map(m => m.AdresseTelefon3).Name("Adresse: Telefon 3");
        Map(m => m.AdresseTitel).Name("Adresse: Titel");
        Map(m => m.AdresseTypAdresse).Name("Adresse: Typ Adresse");
        Map(m => m.AdresseTypMitglied).Name("Adresse: Typ Mitgleid");
        Map(m => m.AdresseTypTelefon1).Name("Adresse: Typ Telefon 1");
        Map(m => m.AdresseTypTelefon2).Name("Adresse: Typ Telefon 2");
        Map(m => m.AdresseTypTelefon3).Name("Adresse: Typ Telefon 3");
        Map(m => m.BetriebAnrede).Name("Betrieb: Anrede");
        Map(m => m.BetriebAnredetext).Name("Betrieb: Anredetext");
        Map(m => m.BetriebAnsprechpartner).Name("Betrieb: Ansprechpartner");
        Map(m => m.BetriebAusbilderAbwVomAnsprPartner).Name("Betrieb: Ausbilder (abw. vom Anspr.Partner)");
        Map(m => m.BetriebBetriebenummer).Name("Betrieb: Betriebenummer");
        Map(m => m.BetriebEmailAdresse).Name("Betrieb: E-Mail-Adresse");
        Map(m => m.BetriebBriefadresse).Name("Betrieb: Briefadresse");
        Map(m => m.BetriebName1).Name("Betrieb: Name 1");
        Map(m => m.BetriebName2).Name("Betrieb: Name 2");
        Map(m => m.BetriebOrt).Name("Betrieb: Ort");
        Map(m => m.BetriebOrtsteil).Name("Betrieb: Ortsteil");
        Map(m => m.BetriebPlz).Name("Betrieb: Plz");
        Map(m => m.BetriebStraße).Name("Betrieb: Straße");
        Map(m => m.BetriebTelefon1).Name("Betrieb: Telefon 1");
        Map(m => m.BetriebTelefon2).Name("Betrieb: Telefon 2");
        Map(m => m.KlasseBereich).Name("Klasse: Bereich");
        Map(m => m.SchülerGeburtsdatum).Name("Schüler: Geburtsdatum");
        Map(m => m.SchülerGeburtsname).Name("Schüler: Geburtsname");
        Map(m => m.SchülerGeburtsort).Name("Schüler: Geburtsort");
        Map(m => m.SchülerGeschlecht).Name("Schüler: Geschlecht");
        Map(m => m.SchülerGeschlechtAuflösung).Name("Schüler: Geschlecht (Auflösung)");
        Map(m => m.SchülerNameElternteil).Name("Schüler: Name Elternteil");
        Map(m => m.SchülerNummer).Name("Schüler: Nummer");
        Map(m => m.SchülerStaat).Name("Schüler: Staat");
        Map(m => m.SchülerStaatAuflösung).Name("Schüler: Staat (Auflösung)");
        Map(m => m.SchülerMuttersprache).Name("Schüler: Muttersprache");
        Map(m => m.SchülerMutterspracheAuflösung).Name("Schüler: Muttersprache (Auflösung)");
        Map(m => m.SchülerVolljährigJN).Name("Schüler: Volljährig (J/N)");
        Map(m => m.SchülerVorgangSchuljahr).Name("Schüler: Vorgang Schuljahr");        
        Map(m => m.SchülerAnrede).Name("Adresse: Anrede");
        Map(m => m.SchülerAnredeAuflösung).Name("Adresse: Anrede (Auflösung)");
        Map(m => m.SchülerAnredeAnredetext).Name("Adresse: Anredetext");
    }
}