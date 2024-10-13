public class ReihenElement(string zielDateiSpaltenBezeichner, string dateiPfad, int index, string[] alias = null, string quellDateiSpaltenBezeichner = null, string wert = null)
{
    public string ZielDateiSpaltenBezeichner { get; set; } = zielDateiSpaltenBezeichner;
    public string QuellDateiPfad { get; set; } = dateiPfad;
    public int QuellDateiIndex { get; set; } = index;
    public string[] Alias { get; set; } = alias;
    public string QuellDateiSpaltenBezeichner { get; set; } = quellDateiSpaltenBezeichner;
    public string Wert { get; set; } = wert;
}