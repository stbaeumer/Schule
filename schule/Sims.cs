using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using static Global;

public class Simss : List<Sim>
{
    public string DateiPfad { get; private set; }
    public string DateiName { get; private set; }
    public string[] Hinweise { get; private set; }

    public Simss()
    {
    }

    public Simss(string dateiName, string dateiendung, string delimiter)
    {
        DateiPfad = Global.CheckFile(dateiName, dateiendung);

        Hinweise = new string[] {
                "Exportieren Sie die Datei aus SchILD, indem Sie:",
                "In SchILD den Pfad gehen: Datenaustausch > Schnittstelle > Export",
                "Die Datei auswählen.",
                "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() };

        if (DateiPfad == null){ return; }

        // Konfiguration für CsvReader: Header und Delimiter anpassen
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
            HasHeaderRecord = true,   // Gibt an, dass die CSV-Datei eine Kopfzeile hat
            Delimiter = delimiter
        };

        using (var reader = new StreamReader(DateiPfad))
        using (var csv = new CsvReader(reader, config))
        {            
            csv.Context.TypeConverterCache.AddConverter<string>(new TrimAndReplaceUnderscoreConverter());
            csv.Context.RegisterClassMap<SimsMap>();
            var records = csv.GetRecords<Sim>();
            this.AddRange(records);
        }
        Global.ZeileSchreiben(0, DateiPfad, this.Count().ToString(), null, null);
    }

    public Simss(string dateiPfad)
    {
        DateiPfad = dateiPfad;
    }

    public Zeilen GetSchuelerBasisdaten(Schülers schuelers)
    {
        Zeilen zeilen = new Zeilen();

        foreach (var sim in this)
        {
            var schueler = schuelers
                .Where(x => x.Nachname == sim.Familienname)
                .Where(x => x.Klasse == sim.Klasse4)
                .Where(x => x.Vorname == sim.Vorname).FirstOrDefault();

            if (schueler != null)
            {
                Zeile zeile = new Zeile();

                if (sim.Status2 == "2")
                {
                    zeile.Add(sim.Familienname);            // Nachname 
                    zeile.Add(sim.Vorname);                 // Vorname 
                    zeile.Add(sim.Gebdat16);                // Geburtsdatum
                    zeile.Add(sim.Geschlecht17);            // Geschlecht 
                    zeile.Add(sim.Status2);                 // Status
                    zeile.Add(sim.Plz14);                   // PLZ       
                    zeile.Add(sim.Ort15);                   // Ort
                    zeile.Add(schueler.Straße);             // Straße
                    zeile.Add("");                          // Aussiedler
                    zeile.Add(sim.Staatsang18);             // 1.Staatsang.

                    zeile.Add(sim.Religion19);              // Konfession
                    zeile.Add(sim.Religion19);              // StatistikKrz Konfession
                    zeile.Add(sim.Aufnahmedatum22);         // Aufnahmedatum
                    zeile.Add(sim.Reliabmeldung21);         // Abmeldedatum Religionsunterricht
                    zeile.Add(sim.Relianmeldung20);         // Anmeldedatum Religionsunterricht
                    zeile.Add(sim.Schulpflichterf47);       // Schulpflicht erf.
                    zeile.Add(sim.Reformpdg12);             // Reform - Pädagogik
                    zeile.Add("");                          // Nr.Stammschule (nur bei extern)
                    zeile.Add(sim.Bezugsjahr1);             // Bezugsjahr
                    zeile.Add("");                          // Abschnitt

                    zeile.Add("");                          // Jahrgang Muss identisch sein mit einer Bezeichnung aus Jahrgaenge.dat 
                    zeile.Add(sim.Klasse4);                 // Klasse
                    zeile.Add(sim.Gliederung5);             // Schulgliederung
                    zeile.Add(sim.Orgform8);                // OrgForm
                    zeile.Add(sim.Klassenart7);             // Klassenart
                    zeile.Add(sim.Fachklasse6);             // Fachklasse
                    zeile.Add("");                          // NochFrei
                    zeile.Add("");                          // VerpflichtungSprachförderkurs
                    zeile.Add("");                          // TeilnahmeSprachförderkurs
                    zeile.Add(sim.JahrEinschulung51);       // Einschulungsjahr       

                    zeile.Add(sim.GSEmpfehlung59);          // ÜbergangsempfJG5
                    zeile.Add(sim.JahrSchulwechsel52);      // JahrWechselS1
                    zeile.Add("");                          // 1.Schulform S1
                    zeile.Add(sim.JahrSchulwechsel52);      // Jahr Wechsel S2
                    zeile.Add(sim.Foerderschwerp10);        // Förderschwerpunkt
                    zeile.Add(sim.Foerderschwerpunkt263);   // 2.Förderschwerpunkt
                    zeile.Add(sim.Schwerstbehindert11);     // Schwerstbehinderung
                    zeile.Add("");                          // sim.Autist
                    zeile.Add(sim.Lsschulnummer27);         // LS Schulnr.  
                    zeile.Add(sim.LSSchulform26);           // LS Schulform

                    zeile.Add("");                          // Herkunft
                    zeile.Add(sim.LSSschulentl32);          // LS Entlassdatum
                    zeile.Add(sim.LSJahrgang33);            // LS Jahrgang
                    zeile.Add(sim.Lsversetz35);             // LS Versetzung      0 : Ja,  1: Nein, 2: Freiw.Rücktritt
                    zeile.Add(sim.Lsreformpdg31);           // LS Reformpädagogik
                    zeile.Add(sim.LSGliederung28);          // LS Gliederung
                    zeile.Add(sim.LSFachklasse29);          // LS Fachklasse
                    zeile.Add("");                          // LS Abschluss
                    zeile.Add(sim.Berufsabschluss65);       // Abschluss
                    zeile.Add("");                          // SchulnrNeueSchule

                    zeile.Add(sim.JahrZuzug50);             // Zuzugsjahr
                    zeile.Add("");                          // GeburtslandSchüler
                    zeile.Add(sim.GebLandMutter54);         // Geburtsland Mutter 
                    zeile.Add(sim.GebLandVater55);          // Geburtsland Vater 
                    zeile.Add(sim.Verkehrssprache57);       // Verkehrssprache 
                    zeile.Add("");                          // DauerKindergartenbesuch
                    zeile.Add("");                          // Kindergartenbesuch
                    zeile.Add("");                          // Anschlussförderung
                }
                zeilen.Add(zeile);
            }
        }
        return zeilen;
    }

    internal Simss Interessierende(List<string> interesserendeKlassen)
    {
        var sims = new Simss(this.DateiPfad);

        foreach (var sim in this)
        {
            if (interesserendeKlassen.Contains(sim.Klasse4))
            {
                sims.Add(sim);
            }
        }

        return sims;
    }
}

public class SimsMap : ClassMap<Sim>
{
    public SimsMap()
    {
        Map(m => m.Familienname).Name(" Familienname");
        Map(m => m.Vorname).Name(" Vorname");
        Map(m => m.Austritt).Name(" Austritt");
        Map(m => m.SchülerLeJahrDa).Name("Schüler le. Jahr da");
        Map(m => m.SchülerBerufswechsel).Name("Schüler Berufswechsel");
        Map(m => m.VorjahrC05AktjahrC06).Name("Vorjahr C05 Aktjahr C06");
        Map(m => m.Bezugsjahr1).Name("1 Bezugsjahr");
        Map(m => m.Status2).Name("2 Status");
        Map(m => m.Lfdnr3).Name("3 Lfdnr");
        Map(m => m.Klasse4).Name("4 Klasse");
        Map(m => m.Gliederung5).Name("5 Gliederung");
        Map(m => m.Fachklasse6).Name("6 Fachklasse");
        Map(m => m.Klassenart7).Name("7 Klassenart");
        Map(m => m.Orgform8).Name("8 Orgform");
        Map(m => m.Aktjahrgang9).Name("9 Aktjahrgang");
        Map(m => m.Foerderschwerp10).Name("10 Foerderschwerp");
        Map(m => m.Schwerstbehindert11).Name("11 Schwerstbehindert");
        Map(m => m.Reformpdg12).Name("12 Reformpdg");
        Map(m => m.Jva13).Name("13 Jva");
        Map(m => m.Plz14).Name("14 Plz");
        Map(m => m.Ort15).Name("15. Ort");
        Map(m => m.Gebdat16).Name("16 Gebdat");
        Map(m => m.Geschlecht17).Name("17 Geschlecht");
        Map(m => m.Staatsang18).Name("18 Staatsang");
        Map(m => m.Religion19).Name("19 Religion");
        Map(m => m.Relianmeldung20).Name("20 Relianmeldung");
        Map(m => m.Reliabmeldung21).Name("21 Reliabmeldung");
        Map(m => m.Aufnahmedatum22).Name("22 Aufnahmedatum");
        Map(m => m.Labk23).Name("23 Labk");
        Map(m => m.Ausbildort24).Name("24 Ausbildort");
        Map(m => m.Betriebsort25).Name("25 Betriebsort");
        Map(m => m.LSSchulform26).Name("26 LSSchulform");
        Map(m => m.Lsschulnummer27).Name("27 Lsschulnummer");
        Map(m => m.LSGliederung28).Name("28 LSGliederung");
        Map(m => m.LSFachklasse29).Name("29 LSFachklasse");
        Map(m => m.Lsklassenart30).Name("30 Lsklassenart");
        Map(m => m.Lsreformpdg31).Name("31 Lsreformpdg");
        Map(m => m.LSSschulentl32).Name("32 LSSschulentl");
        Map(m => m.LSJahrgang33).Name("33 LSJahrgang");
        Map(m => m.LSQual34).Name("34 LSQual");
        Map(m => m.Lsversetz35).Name("35 Lsversetz");
        Map(m => m.VOKlasse36).Name("36 VOKlasse");
        Map(m => m.VOGliederung37).Name("37 VOGliederung");
        Map(m => m.VOFachklasse38).Name("38 VOFachklasse");
        Map(m => m.VOOrgform39).Name("39 VOOrgform");
        Map(m => m.VOKlassenart40).Name("40 VOKlassenart");
        Map(m => m.VOJahrgang41).Name("41 VOJahrgang");
        Map(m => m.VOFoerderschwerp42).Name("42 VOFoerderschwerp");
        Map(m => m.VOSchwerstbehindert43).Name("43 VOSchwerstbehindert");
        Map(m => m.VOReformpdg44).Name("44 VOReformpdg");
        Map(m => m.EntlDatum45).Name("45 EntlDatum");
        Map(m => m.Zeugnis46).Name("46 Zeugnis");
        Map(m => m.Schulpflichterf47).Name("47 Schulpflichterf");
        Map(m => m.Schulwechselform48).Name("48 Schulwechselform");
        Map(m => m.Versetzung49).Name("49 Versetzung");
        Map(m => m.JahrZuzug50).Name("50 Jahr Zuzug");
        Map(m => m.JahrEinschulung51).Name("51 Jahr Einschulung");
        Map(m => m.JahrSchulwechsel52).Name("52 Jahr Schulwechsel");
        Map(m => m.Zugezogen53).Name("53 zugezogen");
        Map(m => m.GebLandMutter54).Name("54 Geb.Land (Mutter)");
        Map(m => m.GebLandVater55).Name("55 Geb.Land (Vater)");
        Map(m => m.ElternteilZugezogen56).Name("56 Elternteil zugezogen");
        Map(m => m.Verkehrssprache57).Name("57 Verkehrssprache");
        Map(m => m.Einschulungsart58).Name("58 Einschulungsart");
        Map(m => m.GSEmpfehlung59).Name("59 GS-Empfehlung");
        Map(m => m.Massnahmetraeger60).Name("60 Massnahmetraeger");
        Map(m => m.Betreuung61).Name("61 Betreuung");
        Map(m => m.BKAZVO62).Name("62 BKAZVO");
        Map(m => m.Foerderschwerpunkt263).Name("63 Förderschwerpunkt 2");
        Map(m => m.VOFoerderschwerpunkt264).Name("64 VOFörderschwerpunkt 2");
        Map(m => m.Berufsabschluss65).Name("65 Berufsabschluss");
        Map(m => m.Produktname66).Name("66 Produktname");
        Map(m => m.Produktversion67).Name("67 Produktversion");
        Map(m => m.Adressmerkmal68).Name("68 Adressmerkmal");
        Map(m => m.Internatsplatz69).Name("69 Internatsplatz");
        Map(m => m.Koopklasse70).Name("70 Koopklasse");
    }
}