using DocumentFormat.OpenXml.Spreadsheet;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;
using System.Text.RegularExpressions;

public class PdfDatei
{
    public string DateiName { get; private set; }
    internal List<PdfSeite> Seiten { get; private set; }
    public Schueler Schueler { get; private set; }
    public string Zeugnisart { get; private set; }
    public string Zeugnisdatum { get; private set; }

    public PdfDatei(string dateiName, Schülers schuelers, string outputFolder, Datei datei)
    {
        DateiName = dateiName;
        Seiten = new List<PdfSeite>();

        using (var pdfReader = new PdfReader(dateiName))
        {
            Global.ZeileSchreiben(0, dateiName, "wird ausgelesen:",null,null);
            datei.Zeilen.Add(new Zeile(new List<string> { dateiName }));

            for (int i = 1; i <= pdfReader.NumberOfPages; i++)
            {
                StringBuilder text = new StringBuilder();
                text.Append(PdfTextExtractor.GetTextFromPage(pdfReader, i));

                string pageText = text.ToString();

                foreach (var schueler in schuelers)
                {
                    if (pageText.Contains(schueler.Nachname, StringComparison.OrdinalIgnoreCase) &&
                        pageText.Contains(schueler.Vorname, StringComparison.OrdinalIgnoreCase) &&                        
                        pageText.Contains(schueler.Geburtsdatum, StringComparison.OrdinalIgnoreCase))
                    {
                        PdfSeite seite = new PdfSeite(dateiName, i+1, schueler);
                        
                        // Wenn Zeugnis als Wort oder Namensbestandteil in der Seite vorkommt, dann gib das Wort zurück.
                        var match = Regex.Match(pageText, @"\b\w*Zeugnis\w*\b", RegexOptions.IgnoreCase);
                        if (match.Success)
                        {   
                            Zeugnisart = match.Value == null ? "" : match.Value;
                        }

                        // Suche nach einem weiteren Datum (neben dem Geburtsdatum). Das Datum wird der Eigenschaft Zeugnisdatum zugewiesen
                        var dateMatches = Regex.Matches(pageText, @"\b\d{2}\.\d{2}\.\d{4}\b");
                        foreach (Match dateMatch in dateMatches)
                        {
                            if (!dateMatch.Value.Equals(schueler.Geburtsdatum))
                            {
                                Zeugnisdatum = dateMatch.Value == null ? "" : dateMatch.Value;
                                break;
                            }
                        }
                        Global.ZeileSchreiben(0, schueler.Nachname.PadRight(20) + Zeugnisart.PadRight(14) + Zeugnisdatum, "ok", null, null);
                        datei.Zeilen.Add(new Zeile(new List<string> { schueler.Nachname, schueler.Vorname , Zeugnisart, Zeugnisdatum }));
                        seite.Ausschneiden(outputFolder);

                        Seiten.Add(seite);
                    }
                }
            }
        }
    }
}