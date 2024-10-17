public class ReihenElement
{
    public string ZielDateiSpaltenBezeichner { get; set; }
    public string QuellDateiPfad { get; set; }
    public int QuellDateiIndex { get; set; }
    public bool ReferenzMerkmal { get; set; }
    public string[] Alias { get; set; }
    public string QuellDateiSpaltenBezeichner { get; set; }
    public string Wert { get; set; }

    public ReihenElement(string zielDateiSpaltenBezeichner)
    {
        ZielDateiSpaltenBezeichner = zielDateiSpaltenBezeichner;
        Wert = "";
        QuellDateiIndex = -1; // Zu Beginn ist das Element -1 und nicht 0.
    }
}