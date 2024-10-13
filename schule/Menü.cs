using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Menü : List<Menüeintrag>
{
    public int Eingabe { get; set; }

    public Menü()
    {
        Eingabe = 0;
        var m = new List<Menüeintrag>();

        m.Add(
        new Menüeintrag(
        "SchuelerTeilleistungen.dat erzeugen",
        "SchuelerTeilleistungen",
        @"ImportFürSchild\SchuelerTeilleistungen.dat",
        @"Beschreibung: Enthält die Teilleistungsdaten (Teilleistung und Noten) eines Lernabschnittes (=Halbjahr oder Quartal) der Schüler. ",
        new List<string> { "Nachname", "Vorname", "Geburtsdatum", "Jahr", "Abschnitt", "Fach", "Datum", "Teilleistung", "Note", "Bemerkung", "Lehrkraft" },
        new Schülers()));

        m.Add(
        new Menüeintrag(
        "Schuelerbasisdaten.dat erzeugen",
        "SchuelerBasisdaten",
        @"ImportFürSchild\SchuelerBasisdaten.dat",
        @"Beschreibung: Enthält die Basis-Stammdaten der Schüler, insbesondere solche, die für die Statistik relevant sind. Wenn die in den Dateien SchuelerZusatzdaten.dat, SchuelerLernabschnittsdaten.dat, SchuelerLeistungsdaten.dat, SchuelerSprachenfolgen.dat, SchuelerAbitur.dat, SchuelerTelefonnummern.dat, SchuelerErzieher.dat, SchuelerMerkmale.dat, SchuelerAdressen.dat enthaltenen Schüler schon in SchILD-NRW existieren (anhand von Nachname, Vorname und Geburtsdatum), so wird diese Datei beim Import nicht zwingend benötigt. Existieren die Schüler dagegen noch nicht, so muss diese Datei vorhanden sein. Siehe auch: https://wiki.svws.nrw.de/mediawiki/index.php?title=Schnittstellenbeschreibung",
        new List<string> { "Nachname", "Vorname", "Geburtsdatum", "Geschlecht", "Status", "PLZ", "Ort", "Straße", "Aussiedler", "1.Staatsang.| Konfession", "StatistikKrz Konfession", "Aufnahmedatum", "Abmeldedatum Religionsunterricht", "Anmeldedatum Religionsunterricht", "Schulpflicht erf.| Reform - Pädagogik", "Nr.Stammschule", "Jahr", "Abschnitt", "Jahrgang", "Klasse", "Schulgliederung", "OrgForm", "Klassenart", "Fachklasse", "Noch frei", "Verpflichtung Sprachförderkurs", "Teilnahme Sprachförderkurs", "Einschulungsjahr", "Übergangsempf.JG5", "Jahr Wechsel S1", "1.Schulform S1", "Jahr Wechsel S2", "Förderschwerpunkt", "2.Förderschwerpunkt", "Schwerstbehinderung", "Autist", "LS Schulnr.| LS Schulform", "Herkunft", "LS Entlassdatum", "LS Jahrgang", "LS Versetzung", "LS Reformpädagogik", "LS Gliederung", "LS Fachklasse", "LS Abschluss", "Abschluss", "Schulnr.neue Schule", "Zuzugsjahr", "Geburtsland Schüler", "Geburtsland Mutter", "Geburtsland Vater", "Verkehrssprache", "Dauer Kindergartenbesuch" },
        new Schülers()));

        



        //m.Add(
        //    new Menüeintrag(
        //        "Schülerleistungsdaten.dat erzeugen",
        //        "Schuelerleistungsdaten",
        //        @"Die Schülerleistungsdaten.dat wird erzeugt. Voraussetzung für einen erfolgreichen Import in SchILD sind die angelegten Fächer und die angelegten Lernabschnittsdaten. Schauen Sie im Importordner nach den beiden zuletzt geannten Dateien...",
        //        new Schülers(
        //            "",
        //            new string[] { }
        //        )
        //    )
        //);

        //m.Add(
        //    new Menüeintrag(
        //        "Import für Office 365 erzeugen",
        //        "import365",
        //        @"Beschreibung: Enthält die Teilleistungsdaten (Teilleistung und Noten) eines Lernabschnittes (=Halbjahr oder Quartal) der Schüler. ",                
        //        new Schülers(
        //            "",
        //            new string[] { }
        //        )
        //    )
        //);

        //m.Add(
        //    new Menüeintrag(
        //        "SchuelerAdressen.dat erzeugen",
        //        "SchuelerAdressen",
        //        @"Beschreibung: Enthält weitere Adressen (z.B. von Ausbildungsbetrieben) der Schüler. ",
        //        new Schülers(
        //            "",
        //            new string[] { "Nachname","Vorname","Geburtsdatum","Adressart","Name1","Name2","Straße","PLZ","Ort","1. Tel.-Nr.","2. Tel.-Nr.","E-Mail","Betreuer Nachname","Betreuer Vorname","Betreuer Anrede","Betreuer Tel.-Nr.","Betreuer E-Mail","Betreuer Abteilung","Vertragsbeginn","Vertragsende" }
        //        )
        //    )
        //);

        //m.Add(
        //    new Menüeintrag(
        //        "Import für GeeVoo erzeugen",
        //        "importGeevoo",
        //        @"Beschreibung: Enthält die Teilleistungsdaten (Teilleistung und Noten) eines Lernabschnittes (=Halbjahr oder Quartal) der Schüler. ",
        //        new Schülers(
        //            "",
        //            new string[] { }
        //        )
        //    )
        //);

        //m.Add(
        //    new Menüeintrag(
        //        "Import für Webuntis (inklusive Bilder)",
        //        "importWebuntis",
        //        @"Beschreibung: Enthält die Teilleistungsdaten (Teilleistung und Noten) eines Lernabschnittes (=Halbjahr oder Quartal) der Schüler. ",
        //        new Schülers(
        //            "",
        //            new string[] { }
        //        )
        //    )
        //);

        //m.Add(
        //    new Menüeintrag(
        //        "Schuelerbasisdaten.dat erzeugen",
        //        "Schuelerbasisdaten",
        //        @"Beschreibung: Enthält die Basis-Stammdaten der Schüler, insbesondere solche, die für die Statistik relevant sind. Wenn die in den Dateien SchuelerZusatzdaten.dat, SchuelerLernabschnittsdaten.dat, SchuelerLeistungsdaten.dat, SchuelerSprachenfolgen.dat, SchuelerAbitur.dat, SchuelerTelefonnummern.dat, SchuelerErzieher.dat, SchuelerMerkmale.dat, SchuelerAdressen.dat enthaltenen Schüler schon in SchILD-NRW existieren (anhand von Nachname, Vorname und Geburtsdatum), so wird diese Datei beim Import nicht zwingend benötigt. Existieren die Schüler dagegen noch nicht, so muss diese Datei vorhanden sein. Siehe auch: https://wiki.svws.nrw.de/mediawiki/index.php?title=Schnittstellenbeschreibung#SchuelerBasisdaten.dat ",
        //        new Schülers(
        //            @"ImportFürSchild\SchuelerBasisdaten.dat", 
        //            new string[] { "Nachname","Vorname","Geburtsdatum","Geschlecht","Status","PLZ","Ort","Straße","Aussiedler","1. Staatsang.","Konfession","StatistikKrz Konfession","Aufnahmedatum","Abmeldedatum Religionsunterricht","Anmeldedatum Religionsunterricht","Schulpflicht erf.","Reform-Pädagogik","Nr. Stammschule","Jahr","Abschnitt","Jahrgang","Klasse","Schulgliederung","OrgForm","Klassenart","Fachklasse","Noch frei","Verpflichtung Sprachförderkurs","Teilnahme Sprachförderkurs","Einschulungsjahr","Übergangsempf. JG5","Jahr Wechsel S1","1. Schulform S1","Jahr Wechsel S2","Förderschwerpunkt","2. Förderschwerpunkt","Schwerstbehinderung","Autist","LS Schulnr.","LS Schulform","Herkunft","LS Entlassdatum","LS Jahrgang","LS Versetzung","LS Reformpädagogik","LS Gliederung","LS Fachklasse","LS Abschluss","Abschluss","Schulnr. neue Schule","Zuzugsjahr","Geburtsland Schüler","Geburtsland Mutter","Geburtsland Vater","Verkehrssprache","Dauer Kindergartenbesuch","Ende Eingliederungsphase","Ende Anschlussförderung" }
        //        )
        //    )
        //);

        this.AddRange(m.OrderBy(c => c.Titel).ToList());
    }

    
}