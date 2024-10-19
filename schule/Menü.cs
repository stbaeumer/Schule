using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Menü : List<Menüeintrag>
{
    public int Eingabe { get; set; }
    public int Auswahl { get; private set; }
    public string DateiPfad { get; private set; }
    public string Titel { get; private set; }
    public List<string> Kopfzeile { get; private set; }

    public Menü()
    {
    }

    internal Menüeintrag Display(List<Menüeintrag> menüeintrags)
    {
        this.AddRange(menüeintrags.Where(x => x.Titel != "" && x.Titel != null));

        var configuration = new ConfigurationBuilder().AddJsonFile("appSettings.json", optional: false, reloadOnChange: true).Build();

        Console.WriteLine("");
        Console.WriteLine("  Bitte auswählen:");
        Console.WriteLine("");

        var xx =  this.IndexOf(this.Where(x => x.Titel == configuration["Auswahl"]).FirstOrDefault());

        Auswahl = xx != null ? Math.Max(1,xx + 1) : 1;

        for (int i = 0; i < this.Where(x => x != null && !string.IsNullOrEmpty(x.Titel)).Count(); i++)
        {
            Console.WriteLine(" " + (i + 1).ToString().PadLeft(3) + ". " + this[i].Titel.PadRight(13));
        }

        bool wiederhole = true;
        do
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("    Ihre Auswahl [" + Auswahl + "] : ");
            Console.ResetColor();
            var eingabe = Console.ReadLine();

            if (eingabe == "ö")
            {
                Global.OpenCurrentFolder();
                wiederhole = true;
                continue;
            }
            if (eingabe == "x")
            {
                Global.OpenWebseite("");
                wiederhole = true;
                continue;
            }
            if (eingabe == "" && Auswahl.ToString() != "")
            {
                eingabe = Auswahl.ToString();
            }

            int nummer = 0;

            if (int.TryParse(eingabe, out nummer))
            {
                // Überprüfen, ob die Zahl im gültigen Bereich liegt
                if (nummer >= 1 && nummer <= this.Count)
                {
                    Auswahl = nummer;
                    Console.WriteLine($"     Sie haben die Zahl {Auswahl} eingegeben.");

                    var x = this[Auswahl - 1].Titel;
                    Global.Speichern("Auswahl", x);
                    wiederhole = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("     Die Zahl muss zwischen 1 und " + this.Count + " liegen. Bitte versuchen Sie es erneut.");
                    Console.ResetColor();
                    wiederhole = true;
                    continue;
                }
            }
            else
            {
                if (!(eingabe == "" && Auswahl >= 1 && Auswahl <= this.Count))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("     Die Zahl muss zwischen 1 und " + this.Count + " liegen. Bitte versuchen Sie es erneut.");
                    Console.ResetColor();
                }
            }

        } while (wiederhole);

        Global.DisplayHeader();
        Global.DisplayHeader(this[Auswahl - 1].Titel, ' ');
        Global.DisplayCenteredBox(this[Auswahl - 1].Beschreibung, 90);
        return this[Auswahl - 1];
    }
}