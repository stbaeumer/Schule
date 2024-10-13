using System.Text.RegularExpressions;
using System.Xml.Linq;

public class Schueler
{
    internal string zugezogen;

    public int Zeile { get; internal set; }
    public string MailPrefix { get; internal set; }
    public string Nachname { get; internal set; }
    public string Vorname { get; internal set; }
    public string Geschlecht { get; internal set; }
    public string Geburtsdatum { get; internal set; }
    public string Klasse { get; internal set; }
    public string Abschnitt { get; internal set; }
    public string Jahr { get; internal set; }
    public string Eintrittsdatum { get; internal set; }
    public string Austrittsdatum { get; internal set; }
    public string Mail { get; internal set; }
    public string Telefon { get; internal set; }
    public string Mobil { get; internal set; }
    public string Straße { get; internal set; }
    public string Wohnort { get; internal set; }
    public string Plz { get; internal set; }
    public string WebuntisId { get; internal set; }
    public string Fehlstd { get; internal set; }
    public string UnentschFehlstd { get; internal set; }
    public string Jahrgang { get; private set; }
    public string Austritt { get; internal set; }
    public string SchülerleJahrda { get; internal set; }
    public string SchülerBerufswechsel { get; internal set; }
    public string VorjahrC05AktjahrC06 { get; internal set; }
    public string Bezugsjahr { get; internal set; }
    public string Status { get; internal set; }
    public string Lfdnr { get; internal set; }
    public string Gliederung { get; internal set; }
    public string Fachklasse { get; internal set; }
    public string Klassenart { get; internal set; }
    public string Orgform { get; internal set; }
    public string Aktjahrgang { get; internal set; }
    public string Foerderschwerp { get; internal set; }
    public string Schwerstbehindert { get; internal set; }
    public string Reformpdg { get; internal set; }
    public string Jva { get; internal set; }
    public string Staatsang { get; internal set; }
    public string Religion { get; internal set; }
    public string Relianmeldung { get; internal set; }
    public string Reliabmeldung { get; internal set; }
    public string Aufnahmedatum { get; internal set; }
    public string Labk { get; internal set; }
    public string Ausbildort { get; internal set; }
    public string Betriebsort { get; internal set; }
    public string LSSchulform { get; internal set; }
    public string Lsschulnummer { get; internal set; }
    public string LSGliederung { get; internal set; }
    public string LSFachklasse { get; internal set; }
    public string Lsklassenart { get; internal set; }
    public string Lsreformpdg { get; internal set; }
    public string LSSschulentl { get; internal set; }
    public string LSJahrgang { get; internal set; }
    public string LSQual { get; internal set; }
    public string Lsversetz { get; internal set; }
    public string VOKlasse { get; internal set; }
    public string VOGliederung { get; internal set; }
    public string VOFachklasse { get; internal set; }
    public string VOOrgform { get; internal set; }
    public string VOKlassenart { get; internal set; }
    public string VOJahrgang { get; internal set; }
    public string VOFoerderschwerp { get; internal set; }
    public string VOSchwerstbehindert { get; internal set; }
    public string VOReformpdg { get; internal set; }
    public string EntlDatum { get; internal set; }
    public string Zeugnis { get; internal set; }
    public string Schulpflichterf { get; internal set; }
    public string Schulwechselform { get; internal set; }
    public string Versetzung { get; internal set; }
    public string JahrZuzug { get; internal set; }
    public string JahrEinschulung { get; internal set; }
    public string JahrSchulwechsel { get; internal set; }
    public string GebLandMutter { get; internal set; }
    public string GebLandVater { get; internal set; }
    public string ElternteilZugezogen { get; internal set; }
    public string Verkehrssprache { get; internal set; }
    public string Einschulungsart { get; internal set; }
    public string GSEmpfehlung { get; internal set; }
    public string Massnahmetraeger { get; internal set; }
    public string Betreuung { get; internal set; }
    public string BKAZVO { get; internal set; }
    public string Förderschwerpunkt2 { get; internal set; }
    public string VOFörderschwerpunkt2 { get; internal set; }
    public string Berufsabschluss { get; internal set; }
    public string Produktname { get; internal set; }
    public string Produktversion { get; internal set; }
    public string Adressmerkmal { get; internal set; }
    public string Internatsplatz { get; internal set; }
    public string Koopklasse { get; internal set; }
    public string SchildId { get; private set; }
    public string Zeugnisdatum { get; internal set; }
    public string Zeugnisart { get; internal set; }

    public void GetFehlstd(Datei absencesPerStudent, List<int> aktSj, int abschnitt)
    {
        try
        {
            //Fehlstd = (from a in absencesPerStudent.Zeilen
            //           where a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Schüler*innen")].Contains(Nachname)
            //           where a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Schüler*innen")].Contains(Vorname)
            //           select Convert.ToInt32(a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Fehlstd.")])).Sum().ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.ReadKey();
        }
    }

    public void GetUnentFehlstd(Datei absencesPerStudent, List<int> aktSj, int abschnitt)
    {
        try
        {
            //UnentschFehlstd = (from a in absencesPerStudent.Zeilen
            //                   where a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Schüler*innen")].Contains(Nachname)
            //                   where a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Schüler*innen")].Contains(Vorname)
            //                   where a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Status")].Contains("nicht entsch.")
            //                   select Convert.ToInt32(a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Fehlstd.")])).Sum().ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.ReadKey();
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
        return FBereinigen(Nachname) + FBereinigen(Vorname) + SchildId;
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
}