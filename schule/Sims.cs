using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Global;

public class Sims : List<Sim>
{
    public Sims()
    {
    }

    public Sims(string dateiPfad, string dateiendungen, string delimiter)
    {
        var hinweise = new string[] {
                "Exportieren Sie die Datei aus SchILD, indem Sie:",
                "In SchILD den Pfad gehen: Datenaustausch > Schnittstelle > Export",
                "Die Datei auswählen.",
                "Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() };

        List<object> result = Global.LiesDateien(dateiPfad, dateiendungen, hinweise, delimiter);

        foreach (var r in result)
        {
            Sim s = (Sim)r;
            this.Add(s);
        }
    }

    public Zeilen GetZeilen(Schülers schuelers)
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

                    zeile.Add(sim.GsEmpfehlung59);          // ÜbergangsempfJG5
                    zeile.Add(sim.JahrSchulwechsel52);      // JahrWechselS1
                    zeile.Add("");                           // 1.Schulform S1
                    zeile.Add(sim.JahrSchulwechsel52);      // Jahr Wechsel S2
                    zeile.Add(sim.Foerderschwerp10);        // Förderschwerpunkt
                    zeile.Add(sim.Foerderschwerpunkt263);   // 2.Förderschwerpunkt
                    zeile.Add(sim.Schwerstbehindert11);     // Schwerstbehinderung
                    zeile.Add("");                          // sim.Autist
                    zeile.Add(sim.Lsschulnummer27);         // LS Schulnr.  
                    zeile.Add(sim.Lsschulform26);           // LS Schulform

                    zeile.Add("");                          // Herkunft
                    zeile.Add(sim.Lsschulentl32);           // LS Entlassdatum
                    zeile.Add(sim.Lsjahrgang33);            // LS Jahrgang
                    zeile.Add(sim.Lsversetz35);             // LS Versetzung      0 : Ja,  1: Nein, 2: Freiw.Rücktritt
                    zeile.Add(sim.Lsreformpdg31);           // LS Reformpädagogik
                    zeile.Add(sim.Lsgliederung28);          // LS Gliederung
                    zeile.Add(sim.Lsfacheklasse29);         // LS Fachklasse
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

    internal Sims Interessierende(List<string> interesserendeKlassen)
    {
        var sims = new Sims();

        foreach (var sim in this)
        {
            if (interesserendeKlassen.Contains(sim.Klasse4))
            {
                sims.Add(sim);
            }
        }

        Global.ZeileSchreiben(0, "interessierende SIM", sims.Count.ToString(), null, null);

        return sims;
    }
}

public class SimsMap : ClassMap<Sim>
{
    public SimsMap()
    {
        GlobalCsvMappings.AddMappings(this);
    }
}