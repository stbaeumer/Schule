using Org.BouncyCastle.Math.EC;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Xml.Linq;

public class Schueler
{
    public string AbschlussartAnEigenerSchuleStatistikKürzel { get; set; }
    public string AktuellerAbschnitt { get; set; }
    public string AktuellesHalbjahr { get; set; }
    public string AllgAdresse1TelNr { get; set; }
    public string AllgAdresse2TelNr { get; set; }
    public string AllgAdresseAdressart { get; set; }
    public string AllgAdresseBemerkungen { get; set; }
    public string AllgAdresseBetreuerAbteilung { get; set; }
    public string AllgAdresseBetreuerAnrede { get; set; }
    public string AllgAdresseBetreuerEmail { get; set; }
    public string AllgAdresseBetreuerName { get; set; }
    public string AllgAdresseBetreuerTelefon { get; set; }
    public string AllgAdresseBetreuerTitel { get; set; }
    public string AllgAdresseBetreuerVorname { get; set; }
    public string AllgAdresseBetreuungslehrer { get; set; }
    public string AllgAdresseBetreuungslehrerAnrede { get; set; }
    public string AllgAdresseBranche { get; set; }
    public string AllgAdresseBundesland { get; set; }
    public string AllgAdresseEmail { get; set; }
    public string AllgAdresseFaxNr { get; set; }
    public string AllgAdresseKreis { get; set; }
    public string AllgAdresseName1 { get; set; }
    public string AllgAdresseName2 { get; set; }
    public string AllgAdresseOrt { get; set; }
    public string AllgAdressePLZ { get; set; }
    public string AllgAdresseSonstigeBetreuer { get; set; }
    public string AllgAdresseStraße { get; set; }
    public string AllgAdresseVertragsbeginn { get; set; }
    public string AllgAdresseVertragsende { get; set; }
    public string AllgAdresseZusatz1 { get; set; }
    public string AllgAdresseZusatz2 { get; set; }
    public string Anrede { get; set; }
    public string ASV { get; set; }
    public string Aufnahmedatum { get; set; }
    public string AufnehmendeSchuleName { get; set; }
    public string AufnehmendeSchuleOrt { get; set; }
    public string AufnehmendeSchulePLZ { get; set; }
    public string AufnehmendeSchuleSchulnr { get; set; }
    public string AufnehmendeSchuleStraße { get; set; }
    public string Aussiedler { get; set; }
    public string Ausweisnummer { get; set; }
    public string BeginnDerBildungsganges { get; set; }
    public string Bemerkungen { get; set; }
    public string BerufsschulpflichtErfüllt { get; set; }
    public string BesMerkmal { get; set; }
    public string BleibtAnSchule { get; set; }
    public string Briefanrede { get; set; }
    public string Bundesland { get; set; }
    public string DatumLetztesAnschreiben { get; set; }
    public string DatumReligionsabmeldung { get; set; }
    public string DatumReligionsanmeldung { get; set; }
    public string DurchschnittsnoteText { get; set; }
    public string DurchschnittsnoteZahl { get; set; }
    public string EindeutigeNummerGUID { get; set; }
    public string EinschulungsartASD { get; set; }
    public string Einschulungsjahr { get; set; }
    public string EmailPrivat { get; set; }
    public string EmailSchulisch { get; set; }
    public string EndeDerAnschlussförderung { get; set; }
    public string EndeDerEingliederungsphase { get; set; }
    public string Entlassdatum { get; set; }
    public string Entlassjahrgang { get; set; }
    public string EntlassjahrgangInterneBezeichnung { get; set; }
    public string ErhältBAFöG { get; set; }
    public string ErsteSchulformInSekI { get; set; }
    public string Erzieher1Anrede { get; set; }
    public string Erzieher1Briefanrede { get; set; }
    public string Erzieher1Nachname { get; set; }
    public string Erzieher1Titel { get; set; }
    public string Erzieher1Vorname { get; set; }
    public string Erzieher2Anrede { get; set; }
    public string Erzieher2Briefanrede { get; set; }
    public string Erzieher2Nachname { get; set; }
    public string Erzieher2Titel { get; set; }
    public string Erzieher2Vorname { get; set; }
    public string ErzieherArtKlartext { get; set; }
    public string ErzieherEmail { get; set; }
    public string ErzieherErhältAnschreiben { get; set; }
    public string ErzieherOrt { get; set; }
    public string ErzieherOrtsteil { get; set; }
    public string ErzieherPostleitzahl { get; set; }
    public string ErzieherStraße { get; set; }
    public string ExterneIDNummer { get; set; }
    public string FachklasseBezeichnung { get; set; }
    public string FachklasseKürzel { get; set; }
    public string Fahrschülerart { get; set; }
    public string FaxNr { get; set; }
    public string Förderschwerpunkt1 { get; set; }
    public string Förderschwerpunkt2 { get; set; }
    public string Geburtsdatum { get; set; }
    public string Geburtsland { get; set; }
    public string GeburtslandMutter { get; set; }
    public string GeburtslandVater { get; set; }
    public string Geburtsname { get; set; }
    public string Geburtsort { get; set; }
    public string Gelöscht { get; set; }
    public string Geschlecht { get; set; }
    public string Haltestelle { get; set; }
    public string Hausnummer { get; set; }
    public string HöchsterAllgAbschluss { get; set; }
    public string InterneIDNummer { get; set; }
    public string Jahrgang { get; set; }
    public string JahrgangInterneBezeichnung { get; set; }
    public string Kindergarten { get; set; }
    public string Klasse { get; set; }
    public string Klassenlehrer { get; set; }
    public string KlassenlehrerAmtsbezeichnung { get; set; }
    public string KlassenlehrerAnrede { get; set; }
    public string KlassenlehrerName { get; set; }
    public string KlassenlehrerTitel { get; set; }
    public string KlassenlehrerVorname { get; set; }
    public string KonfessionKlartext { get; set; }
    public string Kreis { get; set; }
    public string LetzteSchuleAbschluss { get; set; }
    public string LetzteSchuleAllgemeineHerkunft { get; set; }
    public string LetzteSchuleEntlassdatum { get; set; }
    public string LetzteSchuleEntlassjahrgang { get; set; }
    public string LetzteSchuleName { get; set; }
    public string LetzteSchuleOrt { get; set; }
    public string LetzteSchulePLZ { get; set; }
    public string LetzteSchuleSchulform { get; set; }
    public string LetzteSchuleSchulnr { get; set; }
    public string LetzteSchuleStraße { get; set; }
    public string LetzteSchuleVersetzungsvermerk { get; set; }
    public string LetzterAllgAbschlussKürzel { get; set; }
    public string LetzterBerufsbezAbschlussKürzel { get; set; }
    public string Markiert { get; set; }
    public string MigrationshintergrundVorhanden { get; set; }
    public string Nachname { get; set; }
    public string Namenszusatz { get; set; }
    public string Organisationsform { get; set; }
    public string Ortsname { get; set; }
    public string Ortsteil { get; set; }
    public string Postleitzahl { get; set; }
    public string Religionsabmeldung { get; set; }
    public string Religionsanmeldung { get; set; }
    public string Schulbesuchsjahre { get; set; }
    public string Schulform { get; set; }
    public string SchulformFürSIMExport { get; set; }
    public string Schulgliederung { get; set; }
    public string Schuljahr { get; set; }
    public string SchulNummer { get; set; }
    public string SchulpflichtErfüllt { get; set; }
    public string Schwerpunkt { get; set; }
    public string Schwerstbehinderung { get; set; }
    public string Sportbefreiung { get; set; }
    public string StaatsangehörigkeitKlartext { get; set; }
    public string StaatsangehörigkeitKlartextAdjektiv { get; set; }
    public string StaatsangehörigkeitSchlüssel { get; set; }
    public string Status { get; set; }
    public string Straße { get; set; }
    public string Straßenname { get; set; }
    public string TelefonNr { get; set; }
    public string TelefonNummernAnschlussArt { get; set; }
    public string TelefonNummernBemerkung { get; set; }
    public string TelefonNummernTelefonNummer { get; set; }
    public string Übergangsempfehlung { get; set; }
    public string VerkehrsspracheInDerFamilie { get; set; }
    public string Versetzung { get; set; }
    public string Volljährig { get; set; }
    public string VoraussAbschlussdatum { get; set; }
    public string Vorname { get; set; }
    public string Zuzugsjahr { get; set; }
    public string Zeugnisdatum { get; set; }


    public string GetFehlstd(AbsSt absencesPerStudent)
    {
        try
        {
            var fehlstd = (from a in absencesPerStudent
                           where a.Name.Contains(Nachname)
                           where a.Name.Contains(Vorname)
                           where a.Klasse.Contains(Klasse)
                           select Convert.ToDouble(a.Fehlstd)).Sum();

            if (fehlstd == 0)
            {
                return "";
            }
            
            return fehlstd.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.ReadKey();
            return null;
        }
    }

    public string GetUnentFehlstd(AbsSt absencesPerStudent)
    {
        try
        {
            var fehlstd = (from a in absencesPerStudent
                           where a.Name.Contains(Nachname)
                           where a.Name.Contains(Vorname)
                           where a.Klasse.Contains(Klasse)
                           select Convert.ToDouble(a.Fehlstd)).Sum();
            if (fehlstd == 0)
            {
                return "";
            }
            return fehlstd.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.ReadKey();
            return null;
        }
    }

    //internal string GetNote(int index, Datei marksPerLesson)
    //{
    //    try
    //    {
    //        var note = 1 (from zeile in marksPerLesson.Zeilen
    //                    where zeile[Array.IndexOf(marksPerLesson.Kopfzeile, "Name")].Contains(Vorname)
    //                    where zeile[Array.IndexOf(marksPerLesson.Kopfzeile, "Name")].Contains(Nachname)
    //                    where zeile[Array.IndexOf(marksPerLesson.Kopfzeile, "Klasse")].Contains(Klasse)
    //                    select zeile[Array.IndexOf(marksPerLesson.Kopfzeile, "Gesamtnote")]).Distinct().ToList();

    //        if (note.Count > 1)
    //        {
    //            Console.WriteLine("Mehr als eine Note");
    //            Console.ReadKey();
    //        }
    //        if (note.Count == 0)
    //        {
    //            return "";
    //        }
    //        return note[0];
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.ToString());
    //        Console.ReadKey();
    //        return "";
    //    }
    //}

    public string generateKurzname()
    {
        return FBereinigen(Nachname) + FBereinigen(Vorname) + InterneIDNummer.ToString().PadLeft(6,'0');
    }

    string FBereinigen(string Textinput)
    {
        string Text = Textinput;

        Text = Text.ToLower();                          // Nur Kleinbuchstaben
        Text = FUmlauteBehandeln(Text);                 // Umlaute ersetzen


        Text = Regex.Replace(Text, "-", "_");           //  kein Minus-Zeichen
        Text = Regex.Replace(Text, ",", "_");           //  kein Komma            
        Text = Regex.Replace(Text, " ", "_");           //  kein Leerzeichen
                                                        // Text = Regex.Replace(Text, @"[^\w]", string.Empty);   // nur Buchstaben

        Text = Regex.Replace(Text, "[^a-z]", string.Empty);   // nur Buchstaben

        Text = Text.Substring(0, 1);  // Auf maximal 6 Zeichen begrenzen
        return Text;
    }

    string FUmlauteBehandeln(string Textinput)
    {
        string Text = Textinput;

        // deutsche Sonderzeichen
        Text = Regex.Replace(Text, "[æ|ä]", "ae");
        Text = Regex.Replace(Text, "[Æ|Ä]", "Ae");
        Text = Regex.Replace(Text, "[œ|ö]", "oe");
        Text = Regex.Replace(Text, "[Œ|Ö]", "Oe");
        Text = Regex.Replace(Text, "[ü]", "ue");
        Text = Regex.Replace(Text, "[Ü]", "Ue");
        Text = Regex.Replace(Text, "ß", "ss");

        // Sonderzeichen aus anderen Sprachen
        Text = Regex.Replace(Text, "[ã|à|â|á|å]", "a");
        Text = Regex.Replace(Text, "[Ã|À|Â|Á|Å]", "A");
        Text = Regex.Replace(Text, "[é|è|ê|ë]", "e");
        Text = Regex.Replace(Text, "[É|È|Ê|Ë]", "E");
        Text = Regex.Replace(Text, "[í|ì|î|ï]", "i");
        Text = Regex.Replace(Text, "[Í|Ì|Î|Ï]", "I");
        Text = Regex.Replace(Text, "[õ|ò|ó|ô]", "o");
        Text = Regex.Replace(Text, "[Õ|Ó|Ò|Ô]", "O");
        Text = Regex.Replace(Text, "[ù|ú|û|µ]", "u");
        Text = Regex.Replace(Text, "[Ú|Ù|Û]", "U");
        Text = Regex.Replace(Text, "[ý|ÿ]", "y");
        Text = Regex.Replace(Text, "[Ý]", "Y");
        Text = Regex.Replace(Text, "[ç|č]", "c");
        Text = Regex.Replace(Text, "[Ç|Č]", "C");
        Text = Regex.Replace(Text, "[Ð]", "D");
        Text = Regex.Replace(Text, "[ñ]", "n");
        Text = Regex.Replace(Text, "[Ñ]", "N");
        Text = Regex.Replace(Text, "[š]", "s");
        Text = Regex.Replace(Text, "[Š]", "S");

        return Text;
    }

    //public void GetFehlstd(Datei absencesPerStudent, List<int> aktSj, int abschnitt)
    //{
    //    try
    //    {
    //        Fehlstd = (from a in absencesPerStudent.Zeilen
    //                   where a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Schüler*innen")].Contains(Nachname)
    //                   where a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Schüler*innen")].Contains(Vorname)
    //                   select Convert.ToInt32(a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Fehlstd.")])).Sum();
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.ToString());
    //        Console.ReadKey();
    //    }
    //}

    //public void GetUnentFehlstd(Datei absencesPerStudent, List<int> aktSj, int abschnitt)
    //{
    //    try
    //    {
    //        UnentschFehlstd = (from a in absencesPerStudent.Zeilen
    //                           where a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Schüler*innen")].Contains(Nachname)
    //                           where a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Schüler*innen")].Contains(Vorname)
    //                           where a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Status")].Contains("nicht entsch.")
    //                           select Convert.ToInt32(a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Fehlstd.")])).Sum();
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.ToString());
    //        Console.ReadKey();
    //    }
    //}

    internal string GetNote(Marks marksPerLesson)
    {
        try
        {
            var note = from zeile in marksPerLesson
                       where zeile.Name.Contains(Vorname)
                       where zeile.Name.Contains(Nachname)
                       where zeile.Klasse.Contains(Klasse)
                       where zeile.Gesamtnote != ""
                       select zeile.Gesamtnote.ToList();

            if (note.Count() > 1)
            {
                Console.WriteLine("Mehr als eine Note");
                Console.ReadKey();
            }
            if (note.Count() == 0)
            {
                return "";
            }
            return note.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.ReadKey();
            return "";
        }
    }

    public string GenerateMail()
    {
        return Bereinigen(Nachname.ToLower().Substring(0,1)) + Bereinigen(Vorname.ToLower().Substring(0,1)) + InterneIDNummer.ToString().PadLeft(6,'0') + "@students.berufskolleg-borken.de";
    }

    public string UmlauteBehandeln(string Textinput)
    {
        string Text = Textinput;

        // deutsche Sonderzeichen
        Text = Regex.Replace(Text, "[æ|ä]", "ae");
        Text = Regex.Replace(Text, "[Æ|Ä]", "Ae");
        Text = Regex.Replace(Text, "[œ|ö]", "oe");
        Text = Regex.Replace(Text, "[Œ|Ö]", "Oe");
        Text = Regex.Replace(Text, "[ü]", "ue");
        Text = Regex.Replace(Text, "[Ü]", "Ue");
        Text = Regex.Replace(Text, "ß", "ss");

        // Sonderzeichen aus anderen Sprachen
        Text = Regex.Replace(Text, "[ã|à|â|á|å]", "a");
        Text = Regex.Replace(Text, "[Ã|À|Â|Á|Å]", "A");
        Text = Regex.Replace(Text, "[é|è|ê|ë]", "e");
        Text = Regex.Replace(Text, "[É|È|Ê|Ë]", "E");
        Text = Regex.Replace(Text, "[í|ì|î|ï]", "i");
        Text = Regex.Replace(Text, "[Í|Ì|Î|Ï]", "I");
        Text = Regex.Replace(Text, "[õ|ò|ó|ô]", "o");
        Text = Regex.Replace(Text, "[Õ|Ó|Ò|Ô]", "O");
        Text = Regex.Replace(Text, "[ù|ú|û|µ]", "u");
        Text = Regex.Replace(Text, "[Ú|Ù|Û]", "U");
        Text = Regex.Replace(Text, "[ý|ÿ]", "y");
        Text = Regex.Replace(Text, "[Ý]", "Y");
        Text = Regex.Replace(Text, "[ç|č]", "c");
        Text = Regex.Replace(Text, "[Ç|Č]", "C");
        Text = Regex.Replace(Text, "[Ð]", "D");
        Text = Regex.Replace(Text, "[ñ]", "n");
        Text = Regex.Replace(Text, "[Ñ]", "N");
        Text = Regex.Replace(Text, "[š]", "s");
        Text = Regex.Replace(Text, "[Š]", "S");

        return Text;
    }

    public string Bereinigen(string Textinput)
    {
        string Text = Textinput;

        Text = Text.ToLower();                          // Nur Kleinbuchstaben
        Text = UmlauteBehandeln(Text);                 // Umlaute ersetzen


        Text = Regex.Replace(Text, "-", "_");           //  kein Minus-Zeichen
        Text = Regex.Replace(Text, ",", "_");           //  kein Komma            
        Text = Regex.Replace(Text, " ", "_");           //  kein Leerzeichen
                                                        // Text = Regex.Replace(Text, @"[^\w]", string.Empty);   // nur Buchstaben

        Text = Regex.Replace(Text, "[^a-z]", string.Empty);   // nur Buchstaben

        Text = Text.Substring(0, Math.Min(6, Text.Length));  // Auf maximal 6 Zeichen begrenzen
        return Text;
    }
}