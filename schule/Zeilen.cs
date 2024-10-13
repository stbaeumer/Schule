using static System.Runtime.InteropServices.JavaScript.JSType;

public class Zeilen: List<Zeile>
{
    public Zeilen()
    {
    }

    public Zeilen(IEnumerable<Zeile> collection) : base(collection)
    {
    }



    //internal void InteressierendeFiltern(Schülers schuelers)
    //{
    //    try
    //    {   
    //        // Wenn zu Nachname, Vorname etc. Spalten in der Datei vorhanden sind, dann muss der Nachname 
    //        // irgendeines Schülers in der Spalte vorkommen. Sonst wird die Zeile entfernt.

    //        int indexOfNachname = GetIndex(Aliasse.Where(x => x.Contains("Nachname")).FirstOrDefault());
    //        int indexOfVorname = GetIndex(Aliasse.Where(x => x.Contains("Vorname")).FirstOrDefault());
    //        int indexOfGeburtsdatum = GetIndex(Aliasse.Where(x => x.Contains("Geburtsdatum")).FirstOrDefault());
    //        int indexOfKlasse = GetIndex(Aliasse.Where(x => x.Contains("Klasse")).FirstOrDefault());
    //        int indexOfKlassen = GetIndex(Aliasse.Where(x => x.Contains("Klasse")).FirstOrDefault());
    //        int indexOfstudentgroupName = GetIndex(Aliasse.Where(x => x.Contains("studentgroup.name")).FirstOrDefault());

    //        for (int ii = 0; ii < Count; ii++)
    //        {
    //            bool zeileEntfernen = false;

    //            // In der MarksPerLesson matchen Vor und Nachname auf den selben index.
    //            // Beide zusammen ergeben Name.
    //            if (indexOfVorname == indexOfNachname)
    //            {
    //                if (!(from i in schuelers where i.Nachname + " " + i.Vorname == interessierendeDatei.Zeilen[ii][indexOfNachname] select i).Any())
    //                {
    //                    zeileEntfernen = true;
    //                }
    //            }
    //            else
    //            {
    //                if (indexOfNachname >= 0)
    //                {
    //                    if (!(from i in schuelers where i.Nachname == interessierendeDatei.Zeilen[ii][indexOfNachname] select i).Any())
    //                    {
    //                        zeileEntfernen = true;
    //                    }
    //                }
    //                if (indexOfVorname >= 0)
    //                {
    //                    if (!(from i in schuelers where i.Vorname == interessierendeDatei.Zeilen[ii][indexOfVorname] select i).Any())
    //                    {
    //                        zeileEntfernen = true;
    //                    }
    //                }
    //            }

    //            if (indexOfGeburtsdatum >= 0)
    //            {
    //                if (!(from i in schuelers where i.Geburtsdatum == interessierendeDatei.Zeilen[ii][indexOfGeburtsdatum] select i).Any())
    //                {
    //                    zeileEntfernen = true;
    //                }
    //            }
    //            if (indexOfKlasse >= 0)
    //            {
    //                if (!(from i in schuelers where i.Klasse == interessierendeDatei.Zeilen[ii][indexOfKlasse] select i).Any())
    //                {
    //                    zeileEntfernen = true;
    //                }
    //            }
    //            if (indexOfKlassen >= 0)
    //            {
    //                if (!(from i in schuelers where interessierendeDatei.Zeilen[ii][indexOfKlassen].Split('~').Contains(i.Klasse) select i).Any())
    //                {
    //                    zeileEntfernen = true;
    //                }
    //            }
    //            if (indexOfstudentgroupName >= 0)
    //            {
    //                if (!(from i in schuelers where interessierendeDatei.Zeilen[ii][indexOfstudentgroupName].Contains(i.Klasse) select i).Any())
    //                {
    //                    zeileEntfernen = true;
    //                }
    //            }
    //            if (zeileEntfernen) { interessierendeDatei.Zeilen.RemoveAt(ii); ii--; }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Fehler = ex;
    //    }
    //    finally
    //    {
    //        Global.ZeileSchreiben(0, "Interessierende aus " + DateiPfad, Zeilen.Count.ToString(), Fehler);
    //    }

    //    return interessierendeDatei;
    //}
}