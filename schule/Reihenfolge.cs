using DocumentFormat.OpenXml.Office2010.Word.Drawing;

public class Reihenfolge : List<ReihenElement>
{
    public List<Datei> Dateien { get; }
    public Aliasse Aliasse { get; set; }
    public string Abschnitt { get; set; }
    public List<int> AktSj { get; set; }
    public List<string> GeeigneteReferenztabellen { get; set; }
    public Exception Fehler { get; private set; }

    public Reihenfolge(Datei zielDatei, List<Datei> quellDateien)
    {
        GeeigneteReferenztabellen = new List<string>();
        Aliasse = zielDatei.Aliasse;
        Abschnitt = (DateTime.Now.Month > 2 && DateTime.Now.Month <= 9) ? "2" : "1";
        AktSj = [(DateTime.Now.Month >= 7 ? DateTime.Now.Year : DateTime.Now.Year - 1), (DateTime.Now.Month >= 7 ? DateTime.Now.Year + 1 : DateTime.Now.Year)];
        var kopfzeileZieldatei = zielDatei.Zeilen[0].Zellen;
        var zielDateiSpaltenOhneAlias = new List<string>() { "Die Datei " + zielDatei.DateiPfad + " wird erstellt. Im Folgenden werden diejenigen Spalten der Datei aufgelistet, denen keine Spalte zugeordnet werden kann. Beispiel: In einer eingelesenen Datei aus dem Export-Ordner heiße eine Spalte 'GebDat'. In der zu erstellenden Datei muss die Spalte aber 'Geburtsdatum' heißen. Genau diese Zuordnung muss in der Aliasse.xlsx abgebildet werden. Ansonsten bleibt die Spalte leer. Öffnen Sie die Aliasse.xlsx. Sie müssen die Tabelle so ergänzen, dass zu jedem Wert aus der Auflistung unten ein passender Wert in einer der Quelldateien vorkommt. Hinweise bekommen Sie im Internet, indem Sie jetzt 'x' und dann ENTER eintippen." };
        
        var fürAlleZugeordneteSpalten = new List<string>();
        
        try
        {            
            foreach (var k in kopfzeileZieldatei)
            {
                // Da die Spaltennamen nicht 1 zu 1 matchen, gibt es die alias-Liste.

                var alias = Aliasse.Where(x => x.Contains(k.ToLower())).FirstOrDefault();

                if (alias != null)
                {
                    // Wende die Alias-Wörter auf den Quelldatei-Kopf an, um den Index der Spalten zu finden:

                    int i = -1;

                    var reihenElement = new ReihenElement(k);

                    foreach (var quellDatei in quellDateien)
                    {
                        var kopfzeileZielDatei = quellDatei.Zeilen[0].Zellen;
                        i = Array.FindIndex(kopfzeileZielDatei.ToArray(), element => alias.Select(x => x.ToLower()).Contains(element.ToLower()));

                        if (i >= 0)
                        {
                            var quellDateiSpaltenBezeichner = quellDatei.Zeilen[0].Zellen[i].ToString();

                            // Referenzmarkmal bedeutet, dass über diese Spalte der Match mit weiteren Dateien erfolgt.
                            reihenElement.ReferenzMerkmal = Global.ReferenzMerkmale.Where(x => alias.Contains(x.ToLower())).Any();
                            reihenElement.Alias = alias;
                            reihenElement.QuellDateiIndex.Add(i);
                            reihenElement.QuellDateiPfad.Add(quellDatei.DateiPfad);
                            reihenElement.QuellDateiSpaltenBezeichner.Add(quellDateiSpaltenBezeichner);
                            
                            this.Add(reihenElement);
                        }
                    }
                    if (i < 0)
                    {
                        reihenElement.QuellDateiIndex.Add(i);
                        reihenElement.Alias = alias;

                        // Wenn keine Spalte in einer Quelldatei gefunden wurde, wird für bestimmte Spalten (Jahr und Abschnitt)
                        // der Wert für alle hinzugefügt.

                        if (k == "Jahr")
                        {
                            var jahr = AktSj[0];
                            reihenElement.Wert = jahr.ToString();
                            fürAlleZugeordneteSpalten.Add(k);   
                        }
                        if (k == "Abschnitt")
                        {
                            reihenElement.Wert = Abschnitt;
                            fürAlleZugeordneteSpalten.Add(k);
                        }

                        this.Add(reihenElement);
                    }
                }
                else
                {   
                    zielDateiSpaltenOhneAlias.Add(k);
                }
            }

            // Das sind alle gefundenen Spalten, die zur Identifizierung des Datensatzes geeignet sind.
            // Idealerweise werden drei oder mehr gefunden.
            var reihenfolgenelementeMitRef = this.Where(x => x.ReferenzMerkmal == true).Distinct().ToList();

            // Jeder DateiPfad, der dreimal oder öfter vorkommt, ist steht dafür, dass die Datei geeignet ist,
            // den verbundenen Datensatz sicher zu identifizieren. 

            List<string> aaa = new List<string>();

            for (int i = 0; i < reihenfolgenelementeMitRef.Count; i++)
            {
                // Soviele Quelldateien kommen infrage, um das 
                for (int j = 0; j < reihenfolgenelementeMitRef[i].QuellDateiPfad.Count(); j++)
                {
                    aaa.Add(reihenfolgenelementeMitRef[i].QuellDateiPfad[j]);
                }
            }

            foreach (var item in aaa.Distinct())
            {
                if ((from a in  aaa where a == item select a).Count() >= 3)
                {
                    // Diese Datei ist geeignet, um treffsicher Datensätze zu filtern
                    GeeigneteReferenztabellen.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            Fehler = ex;
        }
        finally
        {
            var anzahlSpalten = this.Count();
            
            Global.ZeileSchreiben(0, zielDatei.DateiPfad + ": Spalten werden zugeordnet", this.Count().ToString(), null);

            var anzahlNichtZugeordneteSpalten = this.Where(x => x.QuellDateiIndex[0] < 0).Where(x => x.Wert == "").ToList().Count();

            if (anzahlNichtZugeordneteSpalten > 0)
            {
                Global.ZeileSchreiben(1, "Es konnten Spalten nicht zugeordnet werden: ", anzahlNichtZugeordneteSpalten.ToString(), null);
            }

            if (fürAlleZugeordneteSpalten.Count() > 0)
            {
                Global.ZeileSchreiben(0, "Identische Werte für alle SuS: ", String.Join(',', fürAlleZugeordneteSpalten.Distinct()), null);
            }
            if (zielDateiSpaltenOhneAlias.Count > 0)
            {
                Global.ZeileSchreiben(0, "In der neuen Datei kann nicht jeder Spalte eine Entsprechung zugeordnet werden", (zielDateiSpaltenOhneAlias.Count() - 1).ToString(), new Exception((zielDateiSpaltenOhneAlias.Count() - 1).ToString()), zielDateiSpaltenOhneAlias.ToArray());
            }
        }
    }
}