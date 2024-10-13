public class ReihenElement
{
    public string ZielDateiSpaltenBezeichner { get; set; }
    public List<string> QuellDateiPfad { get; set; }
    public List<int> QuellDateiIndex { get; set; }
    public bool ReferenzMerkmal { get; set; }
    public string[] Alias { get; set; }
    public List<string> QuellDateiSpaltenBezeichner { get; set; }
    public string Wert { get; set; }

    public ReihenElement(string zielDateiSpaltenBezeichner)
    {
        QuellDateiPfad = new List<string>();
        QuellDateiIndex = new List<int>();
        QuellDateiSpaltenBezeichner = new List<string>();
    }
}