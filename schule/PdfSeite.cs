using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

internal class PdfSeite
{
    public string DateiName { get; }
    public int Seite { get; private set; }    
    public Schueler Schueler { get; }

    public PdfSeite(string dateiName, int seite, Schueler schueler)
    {
        DateiName = dateiName;
        Seite = seite;
        Schueler = schueler;
    }

    internal void Ausschneiden(string outputFolder)
    {
        // Extrahiere die Seite aus der PDF-Datei und speicher sie unter dem Dateinamen Klasse_Nachname_Vorname_Zeugnisdatum_Seite.pdf

        using (var reader = new PdfReader(DateiName))
        {
            using (var document = new Document())
            {
                string outputFileName = $"{Schueler.Klasse}_{Schueler.Nachname}_{Schueler.Vorname}_{Schueler.Zeugnisdatum}_{Seite}.pdf";
                string path = outputFolder + "\\" + outputFileName;
                using (var writer = new PdfCopy(document, new FileStream(path, FileMode.Create)))
                {
                    document.Open();
                    PdfImportedPage page = writer.GetImportedPage(reader, Seite);
                    writer.AddPage(page);
                }
            }
        }
        Global.ZeileSchreiben(0, DateiName, "Seite " + Seite + " extrahiert", null, null);
    }
}