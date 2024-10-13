public class Reihenfolge : List<ReihenElement>
{
    public List<Datei> Dateien { get; }
    public Aliasse Aliasse { get; set; }
    public string Abschnitt { get; set; }
    public List<int> AktSj { get; set; }

    public Reihenfolge(Datei zielDatei, List<Datei> quellDateien)
    {
        Aliasse = zielDatei.Aliasse;
        Abschnitt = (DateTime.Now.Month > 2 && DateTime.Now.Month <= 9) ? "2" : "1";
        AktSj = [(DateTime.Now.Month >= 7 ? DateTime.Now.Year : DateTime.Now.Year - 1), (DateTime.Now.Month >= 7 ? DateTime.Now.Year + 1 : DateTime.Now.Year)];

        var kopfzeileExportdatei = zielDatei.Zeilen[0].Zellen;

        foreach (var k in kopfzeileExportdatei)
        {
            // Da die Spaltennamen nicht 1 zu 1 matchen, gibt es die alias-Liste. Finde die Alias-Wörter:

            var alias = Aliasse.Where(x => x.Contains(k.ToLower())).FirstOrDefault();

            if (alias != null)
            {
                // Wende die Alias-Wörter auf den Quelldatei-Kopf an, um den Index der Spalten zu finden:

                int i = -1;

                foreach (var quellDatei in quellDateien)
                {
                    var kopfzeileZielDatei = quellDatei.Zeilen[0].Zellen;
                    i = Array.FindIndex(kopfzeileZielDatei.ToArray(), element => alias.Select(x => x.ToLower()).Contains(element.ToLower()));

                    if (i >= 0)
                    {
                        var quellDateiSpaltenBezeichner = quellDatei.Zeilen[0].Zellen[i].ToString();
                        this.Add(new ReihenElement(k, quellDatei.DateiPfad, i, alias, quellDateiSpaltenBezeichner));
                        break;
                    }
                }
                if (i < 0)
                {
                    // Wenn keine Spalte in einer Quelldatei gefunden wurde, wird für bestimmte Spalten (Jahr und Abschnitt)
                    // der Wert für alle hinzugefügt.

                    if (k == "Jahr")
                    {
                        var jahr = AktSj[0];
                        this.Add(new ReihenElement(k, "", i, alias, "", jahr.ToString()));
                    }
                    if (k == "Abschnitt")
                    {   
                        this.Add(new ReihenElement(k, "", i, alias, "", Abschnitt));
                    }
                    if (k != "Jahr" && k != "Abschnitt")
                    {
                        this.Add(new ReihenElement(k, "", -1));
                    }
                }
            }
            else
            {
                Console.WriteLine("In der Datei zu generierenden Datei kann für den Spaltenname " + k + " keine Entsprechung gefunden werden. Bitte die Aliasse.xlsx um eine Zeile ergänzen.");
            }
        }
    }
}