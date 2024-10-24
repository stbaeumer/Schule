using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class SchBa : List<SchuelerBasisdatum>
{
    public string DateiPfad { get; private set; }
    public string[] Hinweise { get; }

    public SchBa(string dateiPfad) 
    {
        DateiPfad = dateiPfad;
    }

    public SchBa(string dateiName, string dateiendung = "*.dat", string delimiter = "|")
    {
        DateiPfad = Global.CheckFile(dateiName, dateiendung);

        Hinweise = new string[] {
                "Exportieren Sie die Datei aus Atlantis:",
                "Dialog zur Erstellung der SIM.txt aufrufen",
                "Liste gemäß Suchkriterien klicken",
                "Druck/Export > Export (sichtbare) Felder (CSV ;)",
                "Die Datei speichern unter sim.csv im Ordner: " + Directory.GetCurrentDirectory()};

        if (DateiPfad == null){ return; }

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
            HasHeaderRecord = true,
            Delimiter = delimiter
        };

        using (var reader = new StreamReader(DateiPfad))
        using (var csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<SchuelerBasisdatenMap>();
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            var records = csv.GetRecords<SchuelerBasisdatum>();
            this.AddRange(records);
        }
        Global.ZeileSchreiben(0, DateiPfad, this.Count().ToString(), null);
    }

    internal SchBa Interessierende(List<string> interessierendeKlassen)
    {
        var x = this.Where(x => interessierendeKlassen.Contains(x.Klasse)).ToList();
        var xx = new SchBa(this.DateiPfad);
        xx.AddRange(x);
        return xx;
    }

    internal IEnumerable<Zeile> GetSchuelerTeilleistung(SchAd iSchAd1, SchAd iSchAd2)
    {
        throw new NotImplementedException();
    }

    internal IEnumerable<Zeile> GetNfsUnd365(Schülers iSchuS)
    {
        throw new NotImplementedException();
    }
}


public class SchuelerBasisdatenMap : ClassMap<SchuelerBasisdatum>
{
    public SchuelerBasisdatenMap()
    {
        Map(m => m.Nachname).Name("Nachname");
        Map(m => m.Vorname).Name("Vorname");
        Map(m => m.Geburtsdatum).Name("Geburtsdatum");
        Map(m => m.Geschlecht).Name("Geschlecht");
        Map(m => m.Status).Name("Status");
        Map(m => m.PLZ).Name("PLZ");
        Map(m => m.Ort).Name("Ort");
        Map(m => m.Straße).Name("Straße");
        Map(m => m.Aussiedler).Name("Aussiedler");
        Map(m => m.Staatsangehörigkeit1).Name("1. Staatsang.");
        Map(m => m.Konfession).Name("Konfession");
        Map(m => m.StatistikKrzKonfession).Name("StatistikKrz Konfession");
        Map(m => m.Aufnahmedatum).Name("Aufnahmedatum");
        Map(m => m.AbmeldedatumReligionsunterricht).Name("Abmeldedatum Religionsunterricht");
        Map(m => m.AnmeldedatumReligionsunterricht).Name("Anmeldedatum Religionsunterricht");
        Map(m => m.SchulpflichtErfüllt).Name("Schulpflicht erf.");
        Map(m => m.ReformPädagogik).Name("Reform-Pädagogik");
        Map(m => m.NrStammschule).Name("Nr. Stammschule");
        Map(m => m.Jahr).Name("Jahr");
        Map(m => m.Abschnitt).Name("Abschnitt");
        Map(m => m.Jahrgang).Name("Jahrgang");
        Map(m => m.Klasse).Name("Klasse");
        Map(m => m.Schulgliederung).Name("Schulgliederung");
        Map(m => m.OrgForm).Name("OrgForm");
        Map(m => m.Klassenart).Name("Klassenart");
        Map(m => m.Fachklasse).Name("Fachklasse");
        Map(m => m.NochFrei).Name("Noch frei");
        Map(m => m.VerpflichtungSprachförderkurs).Name("Verpflichtung Sprachförderkurs");
        Map(m => m.TeilnahmeSprachförderkurs).Name("Teilnahme Sprachförderkurs");
        Map(m => m.Einschulungsjahr).Name("Einschulungsjahr");
        Map(m => m.ÜbergangsempfJG5).Name("Übergangsempf. JG5");
        Map(m => m.JahrWechselS1).Name("Jahr Wechsel S1");
        Map(m => m.SchulformS1).Name("1. Schulform S1");
        Map(m => m.JahrWechselS2).Name("Jahr Wechsel S2");
        Map(m => m.Förderschwerpunkt).Name("Förderschwerpunkt");
        Map(m => m.Förderschwerpunkt2).Name("2. Förderschwerpunkt");
        Map(m => m.Schwerstbehinderung).Name("Schwerstbehinderung");
        Map(m => m.Autist).Name("Autist");
        Map(m => m.LSSchulnr).Name("LS Schulnr.");
        Map(m => m.LSSchulform).Name("LS Schulform");
        Map(m => m.Herkunft).Name("Herkunft");
        Map(m => m.LSEntlassdatum).Name("LS Entlassdatum");
        Map(m => m.LSJahrgang).Name("LS Jahrgang");
        Map(m => m.LSVersetzung).Name("LS Versetzung");
        Map(m => m.LSReformpädagogik).Name("LS Reformpädagogik");
        Map(m => m.LSGliederung).Name("LS Gliederung");
        Map(m => m.LSFachklasse).Name("LS Fachklasse");
        Map(m => m.LSAbschluss).Name("LS Abschluss");
        Map(m => m.Abschluss).Name("Abschluss");
        Map(m => m.SchulnrNeueSchule).Name("Schulnr. neue Schule");
        Map(m => m.Zuzugsjahr).Name("Zuzugsjahr");
        Map(m => m.GeburtslandSchüler).Name("Geburtsland Schüler");
        Map(m => m.GeburtslandMutter).Name("Geburtsland Mutter");
        Map(m => m.GeburtslandVater).Name("Geburtsland Vater");
        Map(m => m.Verkehrssprache).Name("Verkehrssprache");
        Map(m => m.DauerKindergartenbesuch).Name("Dauer Kindergartenbesuch");
    }
}