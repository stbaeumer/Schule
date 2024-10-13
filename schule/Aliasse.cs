using static System.Runtime.InteropServices.JavaScript.JSType;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Spreadsheet;

public class Aliasse : List<string[]>
{
    public List<string[]> Zeilen { get; private set; }
    public Exception Fehler { get; private set; }

    public Aliasse(string filePath)
    {
        try
        {
            // Überprüfen, ob die Datei existiert
            if (!File.Exists(filePath))
            {
                Zeilen = GetVorgabewerte();

                // Leere Excel-Datei erstellen
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Alias");

                    for (int i = 0; i < Zeilen.Count; i++)
                    {
                        for (int j = 0; j < Zeilen[i].Length; j++)
                        {
                            worksheet.Cell(i + 1, j + 1).Value = Zeilen[i][j];
                        }
                    }
                    workbook.SaveAs(filePath);
                }

                string[] hinweise = new string[] { "Die Datei wird benötigt, um unterschiedliche Spaltenüberschriften aufeinander zu matchen.", "Öffnen Sie die Exceldatei, um alle Aliasse zu sichten", "Ergänzen Sie Aliasse, wenn Sie merken, dass Spalten in der Zieldatei ungewollt leer bleiben." };

                Global.ZeileSchreiben(0, "Die Excel-Datei namens " + filePath + " wird erstellt", "ok", new Exception("ok"), hinweise);
            }

            // Daten aus der Excel-Datei einlesen
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RowsUsed();

                foreach (var row in rows)
                {
                    var rowData = row.Cells().Select(cell => cell.Value.ToString().ToLower()).ToArray();
                    this.Add(rowData);
                }

                Global.ZeileSchreiben(0, "Aliasse aus der Excel-Datei eingelesen", "ok", null);
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

    private List<string[]> GetVorgabewerte()
    {
        List<string[]> vorgabewerte =
        [
            new[] { "1. tel.-nr.", "schüler: telefon", "address.phone", "Telefon" },
        new[] { "1.schulform s1","1.schulform s1" },
        new[] { "2. tel.-nr.", "schüler: telefon 2", "address.mobile", "mobil" },
        new[] { "2.förderschwerpunkt", "63 förderschwerpunkt 2", "2. förderschwerpunkt" },
        new[] { "abmeldedatum religionsunterricht" },
        new[] { "abschluss", "Art des Abschlusses an eigener Schule bei Verlassen der eigenen Schule" },
        new[] { "abschnitt" },
        new[] { "adressart" },
        new[] { "anmeldedatum religionsunterricht" },
        new[] { "aussiedler" },
        new[] { "austritt", "austrittsdatum", "exitDate" },
        new[] { "autist" },
        new[] { "68 adressmerkmal" },
        new[] { "24 ausbildort" },
        new[] { "22 aufnahmedatum", "entrydate", "aufnahmedatum", "eintrittsdatum" },
        new[] { "9 aktjahrgang" },
        new[] { "62 bkazvo" },
        new[] { "65 berufsabschluss" },
        new[] { "61 betreuung" },
        new[] { "25 betriebsort" },
        new[] { "bemerkung" },
        new[] { "betreuer abteilung" },
        new[] { "betreuer anrede" },
        new[] { "betreuer e-mail" },
        new[] { "betreuer nachname" },
        new[] { "betreuer tel.-nr." },
        new[] { "betreuer vorname" },
        new[] { "1 bezugsjahr" },
        new[] { "datum", "date" },
        new[] { "dauer kindergartenbesuch" },
        new[] { "e-mail", "schüler: e-mail-adresse", "address.email", "mail", "email", "mailadresse" },
        new[] { "einschulungsjahr", "51 jahr einschulung" },
        new[] { "45 entldatum", "exitdate" },
        new[] { "56 elternteil zugezogen" },
        new[] { "58 einschulungsart" },
        new[] { "erzb. 2: tel 1" },
        new[] { "erzb. 2: tel 2" },
        new[] { "erzb. 2: straße" },
        new[] { "erzb. 2: sorgeberechtig (j/n)" },
        new[] { "erzb. 2: postleitzahl" },
        new[] { "erzb. 2: ort" },
        new[] { "erzb. 2: e-mail" },
        new[] { "erzb. 2: anrede" },
        new[] { "erzb. 1: typ adresse (auflösung)" },
        new[] { "erzb. 1: typ adresse" },
        new[] { "erzb. 1: tel 1" },
        new[] { "erzb. 1: tel 2" },
        new[] { "erzb. 1: straße" },
        new[] { "erzb. 1: sorgeberechtig (j/n)" },
        new[] { "erzb. 1: postleitzahl" },
        new[] { "erzb. 1: ort" },
        new[] { "erzb. 1: e-mail" },
        new[] { "erzb. 1: anrede" },
        new[] { "erzb. 1: 2. staatsangehörigkeit" },
        new[] { "erzb. 1: 1. staatsangehörigkeit" },
        new[] { "erzb. 1: geburtsdatum" },
        new[] { "erzb. 1: anrede" },
        new[] { "erzb. 2: vorname" },
        new[] { "erzb. 2: nachname" },
        new[] { "erzb. 1: vorname" },
        new[] { "erzb. 1: nachname" },
        new[] { "fach", "subject" },
        new[] { "fehlstunden", "Fehlstd" },
        new[] { "UnentschFehlstd", "Unentsch.Fehlstd." },
        new[] { "fachklasse", "6 fachklasse" },
        new[] { "10 foerderschwerp", "förderschwerpunkt" },        
        new[] { "59 gs-empfehlung", "Übergangsempf. JG5" },
        new[] { "16 gebdat", "schüler: geburtsdatum (yyyy-mm-dd)", "birthdate", "geburtsdatum" },
        new[] { "geburtsdatum", "birthday", "birthdate" },
        new[] { "geburtsland mutter", "54 geb.land (mutter)" },
        new[] { "geburtsland schüler" },
        new[] { "geburtsland vater", "55 geb.land (vater)" },
        new[] { "geburtsort" },
        new[] { "geschlecht", "gender", "17 geschlecht" },
        new[] { "5 gliederung" },
        new[] { "herkunft" },
        new[] { "69 internatsplatz" },
        new[] { "jahr", "schüler: vorgang schuljahr" },
        new[] { "jahr wechsel s1" },
        new[] { "jahr wechsel s2" },
        new[] { "jahrgang" },
        new[] { "13 jva" },
        new[] { "50 jahr zuzug" },
        new[] { "52 jahr schulwechsel" },
        new[] { "7 klassenart" },
        new[] { "70 koopklasse" },
        new[] { "konfession" },
        new[] { "klasse", "4 klasse", "klasse: klassenbezeichnung", "klasse.name" },
        new[] { "klassenart" },
        new[] { "lehrkraft", "benutzer" },
        new[] { "23 labk" },
        new[] { "26 lsschulform", "ls schulform" },
        new[] { "27 lsschulnummer", "ls schulnr." },
        new[] { "28 lsgliederung" },
        new[] { "29 lsfachklasse" },
        new[] { "30 lsklassenart" },
        new[] { "31 lsreformpdg" },
        new[] { "32 lsschulentl" },
        new[] { "33 lsjahrgang" },
        new[] { "34 lsqual" },
        new[] { "35 lsversetz" },
        new[] { "ls abschluss" },
        new[] { "ls entlassdatum" },
        new[] { "ls fachklasse" },
        new[] { "ls gliederung" },
        new[] { "ls jahrgang" },
        new[] { "ls reformpädagogik" },        
        new[] { "ls versetzung" },
        new[] { "60 massnahmetraeger" },
        new[] { "nachname", "surname", "schüler: nachname", "longname", "familienname" },
        new[] { "name 1" },
        new[] { "name2" },
        new[] { "noch frei" },
        new[] { "note", "gesamtnote" },
        new[] { "nr.stammschule" },
        new[] { "8 orgform", "orgform" },
        new[] { "ort", "15. ort", "schüler: ort", "address.city", "Wohnort" },
        new[] { "schüler: ortsteil" },
        new[] { "66 produktname" },
        new[] { "67 produktversion" },
        new[] { "plz", "14 plz", "schüler: plz", "address.postcode" },
        new[] { "prüfungsart", "teilleistung" },
        new[] { "reform-pädagogik", "reform - pädagogik", "12 reformpdg" },
        new[] { "19 religion" },
        new[] { "20 relianmeldung" },
        new[] { "21 reliabmeldung" },
        new[] { "schulgliederung" },
        new[] { "schulpflicht erf.", "47 schulpflichterf" },
        new[] { "48 schulwechselform" },
        new[] { "11 schwerstbehindert", "Schwerstbehinderung" },
        new[] { "18 staatsang", "1.staatsang.", "schüler: staat (auflösung)", "1. staatsang." },
        new[] { "statistikkrz konfession" },
        new[] { "status", "2 status" },
        new[] { "straße", "schüler: straße", "address.street" },
        new[] { "schüler berufswechsel" },
        new[] { "schüler le. jahr da" },
        new[] { "teilnahme sprachförderkurs" },
        new[] { "übergangsempf.jg5" },
        new[] { "verkehrssprache" },
        new[] { "verpflichtung sprachförderkurs" },
        new[] { "vertragsbeginn" },
        new[] { "vertragsende" },
        new[] { "49 versetzung" },
        new[] { "36 voklasse" },
        new[] { "37 vogliederung" },
        new[] { "38 vofachklasse" },
        new[] { "39 voorgform" },
        new[] { "40 voklassenart" },
        new[] { "41 vojahrgang" },
        new[] { "42 vofoerderschwerp" },
        new[] { "64 voförderschwerpunkt 2" },
        new[] { "43 voschwerstbehindert" },
        new[] { "44 voreformpdg" },
        new[] { "57 verkehrssprache" },
        new[] { "vorjahr c05 aktjahr c06" },
        new[] { "vorname", "firstname", "givenname", "schüler: rufname", "schüler: vorname", "forename" },
        new[] { "zuzugsjahr" },
        new[] { "46 zeugnis" },
        new[] { "53 zugezogen" },
        new[] { "externKey" },
        new[] { "medicalReportDuty" },
        new[] { "schulpflicht" },
        new[] { "majority" },
        new[] { "name" },
        new[] { "Schulnummer der aufnehmenden Schule", "Schulnr.neue Schule", "Schulnr. neue Schule" }

        // externKey,medicalReportDuty,schulpflicht,majority,address.phone

        ];
        return vorgabewerte;
    }
}