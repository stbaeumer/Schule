public class PdfDateien : List<PdfDatei>
{
    public PdfDateien(string inputFolder, string outputFolder, Schülers schuelers, Datei datei)
    {
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        datei.Zeilen = new Zeilen();
        datei.Zeilen.Add(new Zeile(new List<string> { "Protokoll " + DateTime.Now.ToString() }));

        // Lese alle PDF-Dateien aus dem Ordner "PDF-Zeugnisse"
        foreach (var file in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            this.Add(new PdfDatei(file, schuelers, outputFolder, datei));
        }
    }
}