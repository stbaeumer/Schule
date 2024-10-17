using static Global;
using System.Diagnostics;
using System.Text;

public class Aliasse : List<string[]>
{
    public List<string[]> Zeilen { get; private set; }
    public Exception Fehler { get; private set; }

    public Aliasse(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                this.AddRange(GetVorgabewerte());

                string[] hinweise = new string[] 
                {
                    "Die Alias-Datei wird erstellt, da sie nicht existiert.",
                    "Die Datei sorgt dafür, dass Spalten richtig eigelesen werden, wenn sie ansonsten ungültige Zeichen enthalten.",
                    "Die Datei kann händlisch ergänzt werden.",
                    "Jede Spalte muss exakt drei Einträge enthalten."
                };

                Global.ZeileSchreiben(0, "Die Alias-Datei namens " + filePath + " wird erstellt", "ok", new Exception("ok"), hinweise);

                using (FileStream fs = new FileStream(DateiPfad, FileMode.CreateNew))
                {
                    Encoding encoding = Encoding.UTF8;
                    using (StreamWriter writer = new StreamWriter(fs, encoding))
                    {
                        try
                        {
                            foreach (var zeile in this)
                            {   
                                writer.WriteLine(String.Join(',', zeile));
                            }
                        }
                        catch (Exception ex)
                        {
                            Fehler = ex;
                        }
                        finally
                        {
                            Global.ZeileSchreiben(0, "Aliasse", this.Count().ToString(), null);
                        }
                    }
                }
            }

            // Daten aus der Excel-Datei einlesen

            using (var reader = new StreamReader(filePath)) { GlobalCsvMappings.LoadMappingsFromFile(filePath); }

        }
        catch (Exception ex)
        {
            Fehler = ex;
        }
        finally
        {
            
        }
    }

    private List<string[]> GetVorgabewerte()
    {
        List<string[]> vorgabewerte = new List<string[]>
    {
        new[] { "Fehlmin","Fehlmin.","Fehlmin." },
        new[] { "Name","Schüler*innen","Schüler" },
        new[] { "ExterneId","Externe Id","Externe Id" },
        new[] { "Fehlstd","Fehlstd.","Fehlst" },
        new[] { "Abwesenheitzaehlt","Abwesenheit zählt","Abwesenheit zählt" },
        new[] { "addressEmail","address.email","address.email" },
        new[] { "addressMobile","address.mobile","address.mobile" },
        new[] { "addressPhone","address.phone","address.phone" },
        new[] { "addressCity","address.city","address.city" },
        new[] { "addressPostCode","address.postCode","address.postCode" },
        new[] { "addressStreet","address.street","address.street" },
        new[] { "schluesselExtern","Schlüssel (extern)","Schlüssel (extern)" },
        new[] { "studentgroupName","studentgroup.name","studentgroup.name" },
        new[] { "Familienname","Familienname","Familienname" },
        new[] { "Austritt","Austritt","Austritt" },
        new[] { "klasseName","klasse.name","klasse.name" },
        new[] { "Vorname","Vorname","Vorname" },
        new[] { "SchuelerLeJahrDa","SchuelerLeJahrDa","Schüler le. Jahr da" },
        new[] { "SchuelerBerufswechsel","SchuelerBerufswechsel","Schüler Berufswechsel" },
        new[] { "VorjahrC05AktjahrC06","VorjahrC05AktjahrC06","Vorjahr C05 Aktjahr C06" },
        new[] { "Bezugsjahr1","Bezugsjahr1","1 Bezugsjahr" },
        new[] { "Status2","Status2","2 Status" },
        new[] { "Lfdnr3","Lfdnr3","3 Lfdnr" },
        new[] { "Klasse4","Klasse4","4 Klasse" },
        new[] { "Gliederung5","Gliederung5","5 Gliederung" },
        new[] { "Fachklasse6","Fachklasse6","6 Fachklasse" },
        new[] { "Klassenart7","Klassenart7","7 Klassenart" },
        new[] { "Orgform8","Orgform8","8 Orgform" },
        new[] { "Aktjahrgang9","Aktjahrgang9","9 Aktjahrgang" },
        new[] { "Foerderschwerp10","Foerderschwerp10","10 Foerderschwerp" },
        new[] { "Schwerstbehindert11","Schwerstbehindert11","11 Schwerstbehindert" },
        new[] { "Reformpdg12","Reformpdg12","12 Reformpdg" },
        new[] { "Jva13","Jva13","13 Jva" },
        new[] { "Plz14","Plz14","14 Plz" },
        new[] { "Ort15","Ort15","15. Ort" },
        new[] { "Gebdat16","Gebdat16","16 Gebdat" },
        new[] { "Geschlecht17","Geschlecht17","17 Geschlecht" },
        new[] { "Staatsang18","Staatsang18","18 Staatsang" },
        new[] { "Religion19","Religion19","19 Religion" },
        new[] { "Relianmeldung20","Relianmeldung20","20 Relianmeldung" },
        new[] { "Reliabmeldung21","Reliabmeldung21","21 Reliabmeldung" },
        new[] { "Aufnahmedatum22","Aufnahmedatum22","22 Aufnahmedatum" },
        new[] { "Labk23","Labk23","23 Labk" },
        new[] { "Ausbildort24","Ausbildort24","24 Ausbildort" },
        new[] { "Betriebsort25","Betriebsort25","25 Betriebsort" },
        new[] { "Lsschulform26","Lsschulform26","26 LSSchulform" },
        new[] { "Lsschulnummer27","Lsschulnummer27","27 Lsschulnummer" },
        new[] { "Lsgliederung28","Lsgliederung28","28 LSGliederung" },
        new[] { "Lsfacheklasse29","Lsfacheklasse29","29 LSFachklasse" },
        new[] { "Lsklassenart30","Lsklassenart30","30 Lsklassenart" },
        new[] { "Lsreformpdg31","Lsreformpdg31","31 Lsreformpdg" },
        new[] { "Lsschulentl32","Lsschulentl32","32 LSSschulentl" },
        new[] { "Lsjahrgang33","Lsjahrgang33","33 LSJahrgang" },
        new[] { "Lsqual34","Lsqual34","34 LSQual" },
        new[] { "Lsversetz35","Lsversetz35","35 Lsversetz" },
        new[] { "Voklasse36","Voklasse36","36 VOKlasse" },
        new[] { "Vogliederung37","Vogliederung37","37 VOGliederung" },
        new[] { "Vofachklasse38","Vofachklasse38","38 VOFachklasse" },
        new[] { "Voorgform39","Voorgform39","39 VOOrgform" },
        new[] { "Voklassenart40","Voklassenart40","40 VOKlassenart" },
        new[] { "Vojahrgang41","Vojahrgang41","41 VOJahrgang" },
        new[] { "Vofoerderschwerp42","Vofoerderschwerp42","42 VOFoerderschwerp" },
        new[] { "Voschwerstbehindert43","Voschwerstbehindert43","43 VOSchwerstbehindert" },
        new[] { "Voreformpdg44","Voreformpdg44","44 VOReformpdg" },
        new[] { "Entldatum45","Entldatum45","45 EntlDatum" },
        new[] { "Zeugnis46","Zeugnis46","46 Zeugnis" },
        new[] { "Schulpflichterf47","Schulpflichterf47","47 Schulpflichterf" },
        new[] { "Schulwechselform48","Schulwechselform48","48 Schulwechselform" },
        new[] { "Versetzung49","Versetzung49","49 Versetzung" },
        new[] { "JahrZuzug50","JahrZuzug50","50 Jahr Zuzug" },
        new[] { "JahrEinschulung51","JahrEinschulung51","51 Jahr Einschulung" },
        new[] { "JahrSchulwechsel52","JahrSchulwechsel52","52 Jahr Schulwechsel" },
        new[] { "Zugezogen53","Zugezogen53","53 zugezogen" },
        new[] { "GebLandMutter54","GebLandMutter54","54 Geb.Land (Mutter)" },
        new[] { "GebLandVater55","GebLandVater55","55 Geb.Land (Vater)" },
        new[] { "ElternteilZugezogen56","ElternteilZugezogen56","56 Elternteil zugezogen" },
        new[] { "Verkehrssprache57","Verkehrssprache57","57 Verkehrssprache" },
        new[] { "Einschulungsart58","Einschulungsart58","58 Einschulungsart" },
        new[] { "GsEmpfehlung59","GsEmpfehlung59","59 GS-Empfehlung" },
        new[] { "Massnahmetraeger60","Massnahmetraeger60","60 Massnahmetraeger" },
        new[] { "Betreuung61","Betreuung61","61 Betreuung" },
        new[] { "Bkazvo62","Bkazvo62","62 BKAZVO" },
        new[] { "Foerderschwerpunkt263","Foerderschwerpunkt263","63 Förderschwerpunkt 2" },
        new[] { "Vofoerderschwerpunkt264","Vofoerderschwerpunkt264","64 VOFörderschwerpunkt 2" },
        new[] { "Berufsabschluss65","Berufsabschluss65","65 Berufsabschluss" },
        new[] { "Produktname66","Produktname66","66 Produktname" },
        new[] { "Produktversion67","Produktversion67","67 Produktversion" },
        new[] { "Adressmerkmal68","Adressmerkmal68","68 Adressmerkmal" },
        new[] { "Internatsplatz69","Internatsplatz69","69 Internatsplatz" },
        new[] { "Koopklasse70","Koopklasse70","70 Koopklasse" }
    };
        return vorgabewerte;
    }
}