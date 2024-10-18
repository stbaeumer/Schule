using System.Reflection.Metadata.Ecma335;

public class PdfDateien : List<PdfDatei>
{
    public PdfDateien(string inputFolder, string outputFolder, Schülers schuelers)
    {
        Zeilen zeilen = new Zeilen();

        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        zeilen = new Zeilen();
        zeilen.Add(new Zeile(new List<string> { "Protokoll " + DateTime.Now.ToString() }));

        // Lese alle PDF-Dateien aus dem Ordner "PDF-Zeugnisse"
        foreach (var file in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            this.Add(new PdfDatei(file, schuelers, outputFolder));
        }
    }    
}