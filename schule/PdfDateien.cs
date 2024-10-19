using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Configuration;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Security;
using System.Reflection.Metadata.Ecma335;

public class PdfDateien : List<PdfDatei>
{
    public PdfDateien()
    {
        try
        {
            var fileGroupPdf = (from f in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\", "*.pdf") where !f.Contains("-Kennwort") select f).ToList();

            Console.WriteLine("");
            foreach (var file in fileGroupPdf)
            {
                Global.ZeileSchreiben(0, file, "bereit zum Erstellen einer kennwortgeschützten Kopie", null, null);
            }

            var configuration = new ConfigurationBuilder().AddJsonFile("appSettings.json", optional: false, reloadOnChange: true).Build();
            var kennwort = configuration["Kennwort"];

            Console.WriteLine("");
            Console.WriteLine("   Bitte ein Kennwort wählen");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("      Ihr Kennwort [" + kennwort + "] : ");
            Console.WriteLine("");
            Console.ResetColor();

            var x = Console.ReadLine();

            if (x == "ö")
            {
                Global.OpenCurrentFolder();
            }
            if (x == "x")
            {
                Global.OpenWebseite("https://wiki.svws.nrw.de/mediawiki/index.php?title=Schnittstellenbeschreibung");
            }

            if ((x == "" && kennwort != ""))
            {
                Global.Speichern("Kennwort", kennwort);
            }
            if (x != "")
            {
                Global.Speichern("Kennwort", x);
            }

            string[] fileGroupJpg = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\", "*.png");

            foreach (string fileName in (from f in fileGroupJpg where !f.Contains("-Kennwort") select f).ToList())
            {
                Document document = new Document(new Rectangle(288f, 144f), 10, 10, 10, 10);
                document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());

                using (var stream = new FileStream(fileName + ".pdf", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    PdfWriter.GetInstance(document, stream);
                    document.Open();
                    using (var imageStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        var image = Image.GetInstance(imageStream);
                        image.SetAbsolutePosition(0, 0); // set the position to bottom left corner of pdf
                        image.ScaleAbsolute(iTextSharp.text.PageSize.A4.Height, iTextSharp.text.PageSize.A4.Width); // set the height and width of image to PDF page size
                        document.Add(image);
                    }
                    document.Close();
                }
            }

            foreach (string fileName in fileGroupPdf)
            {
                PdfSharp.Pdf.PdfDocument document = PdfSharp.Pdf.IO.PdfReader.Open(fileName);
                PdfSecuritySettings securitySettings = document.SecuritySettings;
                securitySettings.UserPassword = kennwort;
                securitySettings.OwnerPassword = kennwort;
                //securitySettings.PermitAccessibilityExtractContent = false;
                securitySettings.PermitAnnotations = false;
                securitySettings.PermitAssembleDocument = false;
                securitySettings.PermitExtractContent = false;
                securitySettings.PermitFormsFill = true;
                securitySettings.PermitFullQualityPrint = false;
                securitySettings.PermitModifyDocument = true;
                securitySettings.PermitPrint = false;


                var neueDatei = fileName.Replace(Path.GetFileNameWithoutExtension(fileName), Path.GetFileNameWithoutExtension(fileName) + "-Kennwort");

                document.Save(neueDatei);

                Global.ZeileSchreiben(0, neueDatei, "Kopie mit Kennwort erstellt", null, null);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("");
            Console.WriteLine("      Weiter mit ENTER");
            Console.WriteLine("");
            Console.ResetColor();

            Console.ReadKey();
        }
    }

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