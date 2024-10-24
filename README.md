# Schule.exe

**_Schule.exe_** bietet Unterstützung an Schnittstellen zwischen Programmen in der Schulverwaltung:

## 1. Migration von Atlantis nach SchILD NRW

Ein Wechsel von Atlantis nach SchILD NRW beginnt damit, dass alle Einstellungen und Stammdaten nach SchILD NRW übernommen werden. Fächer können direkt aus Untis exportiert und in SchILD importiert werden. Lehrkräfte können aus der Lehrer.txt übernommen werden. 

Zuletzt werden die Schülerdaten in einem Rutsch von Atlantis nach Schild übertragen. Ab diesem Moment ist Schild das Produktivsystem. Atlantis kann abgeschaltet werden.

Mit folgenden Schritten gelingt der Übertrag der Schüler*innen:

 1. Speichern Sie Schule.exe in den Ordner Ihrer Wahl.
 2. Starten Sie Schule.exe erstmalig. Es werden verschiedene Ordner angelegt.
 3. Erstellen Sie in Atlantis die Datei sim.csv, indem Sie
	1. in Atlantis auf ... klicken 
   	2. die Datei unter dem Namen sim.csv im Ordner Export aus Atlantis ablegen.
 4. Erstellen aus Atlantis die Datei Adressen.csv, indem Sie
   	1. die PSR-Datei hier herunterladen
   	2. die PSR-Datei über den Listegenerator aufrufen und die neue Datei unter dem Namen Adressen.csv im Ordner ExportAusAtlantis ablegen.
 5. Erstellen Sie in Webuntis die Datei student.csv, indem Sie
   	1. in Webuntis ...
   	2. die Datei unter dem Namen student.csv im Ordner ExportAusWebuntis abspeichern.
 6. Die Ordnerstruktur sieht jetzt so aus: 

```
Schule.exe
+---ExportAusAtlantis
|       sim.csv
|       Adressen.csv
|       
+---ExportAusWebuntis
|       Student_20240922_0952.csv 
|       
\---ImportFürSchild
```

 6. Starten Sie Schule.exe erneut. Wenn Schule.exe die geforderten Dateien finden kann, erweitert sich das Menü um den Punkt SchuelerBasisdaten.dat aus Atlantis-SIM.txt erstellen.

```
       1. SchuelerBasisdaten.dat aus Atlantis-SIM.txt und Webuntis-Student_...csv erzeugen
```

 8. Klicken Sie den Menüpunkt SchuelerBasisdaten.dat aus Atlantis-SIM.txt erstellen.
 9. Optional kann die Auswahl der Klassen eingeschränkt werden. Das ist für den Anfang bestimmt eine gute Entscheidung.
 10. Es werden mehrere Dateien erstellt. Die Dateien liegen nun im Ordner ImportFürSchILD.
 11. Starten Sie SchILD und gehen Sie den Pfad: Datenaustausch ...
 12. ...
 13. Wenn SchILD in der Importdatei Fehler findet, wird das angezeigt. I.d.R. findet aber dennoch ein Import der fehlerfreien Datensätze statt.
 14. Nach dem Neustart von SchILD sind die Schülerdaten eingetragen.



 -  
 - bei der Migration von _Atlantis_ nach _SchILD NRW_, indem Importdateien für _SchILD NRW_ erstellt werden.
 - bei der Haupterhebung, indem Leistungsdaten von _Webuntis_ nach _SchILD NRW_ übertragen werden. Dadurch kann die UVD direkt aus _SchILD NRW_ generiert werden.
 - bei Zeugniskonferenzen, indem Noten und Fehlzeiten von Webuntis nach SchILD übertragen werden.
 - bei Blauen Briefen, indem Noten von Webuntis nach SchILD übertragen werden.
 - beim Übertrag von Schüler*innen nach Webuntis (inkl. Fotos) 

## Voraussetzungen für den Einsatz von **_Schule.exe_**

1. Administrativer Zugang zu _Atlantis_
1. Administrativer Zugang zu _SchILD NRW_
1. Administrativer Zugang zu _Untis_
1. Administrativer Zugang zu _Webuntis_
1. In Untis-Kursen muss der Klassenname Bestandteil des Kursnamen (=Schülergruppe) sein. Beispiel:

![Kurse benennen](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/kurse.png?raw=true)

## Der erste Start

**_Schule.exe_** legt folgende Ordnerstruktur (ohne Dateien) im selben Ordner an, in dem auch die **_Schule.exe_** ausgeführt wird:

```
Schule.exe
+---ExportAusSchild
|       Faecher.dat
|       SchuelerLeistungsdaten.dat
|       SchuelerLernabschnittsdaten.dat
|       weitere *.dat-Dateien aus SchILD
|       
+---ExportAusUntis
|       GPU006.TXT
|
+---ExportAusAtlantis
|       sim.csv
|
|       
+---ExportAusWebuntis
|       AbsencePerStudent_20240918_1230.csv
|       ExportLessons_20240918_1022.csv
|       MarksPerLesson_20240922_1603.csv
|       StudentgroupStudents_20240918_1022.csv
|       Student_20240922_0952.csv
|       
\---ImportFürSchild
        Faecher.dat
        Schuelerleistungsdateien.dat
        SchuelerLernabschnittsdaten.dat
```

Schule.exe wird ohne Installation in den Ordner der Wahl verschoben und dann doppelgeklickt. Alle interessierenden Dateienwerden eingelesen.

```
     Schule.exe | https://github.com/stbaeumer/Schule | GPLv3 | Stefan Bäumer 2024 | 10.10.2024
==================================================================================================
ExportAusWebuntis\AbsencePerStudent_20240918_1230.csv ....................................... 3082
ExportAusWebuntis\Student_20240922_0952.csv ................................................. 2220
ExportAusWebuntis\ExportLessons_20240918_1022.csv ........................................... 1043
ExportAusWebuntis\MarksPerLesson_20240922_1603.csv ............................................. 0
ExportAusWebuntis\StudentgroupStudents_20240918_1022.csv .................................... 3169
ExportAusSchild\Faecher.dat .................................................................. 339
ExportAusSchild\SchuelerLeistungsdaten.dat ..................................................... 0
ExportAusSchild\SchuelerLernabschnittsdaten.dat ................................................ 0
ExportAusSchild\SchuelerTeilleistungen.dat .....................`.............................. 16
ExportAusSchild\SchuelerBasisdaten.dat ......................................................... 0
ExportAusAtlantis\sim.csv ................................................................... 3162
Schüler*innen aus schuelerBasisdaten ........................................................... 0
Schüler*innen aus students_ ................................................................. 2220

  Bitte auswählen:

   1. SchuelerBasisdaten.dat aus Atlantis-SIM.txt und Webuntis-Student_...csv erzeugen
   2. PDF-Zeugnis-Stapel in PDF-Einzeldateien umwandeln und sprechend benennen

    Ihre Auswahl [1] :


```



Nicht alle gezeigten Dateien müssen vorhanden sein. **_Schule.exe_** gibt für jede Aufgabe Rückmeldung, welche Dateien fehlen. Die Zeitstempel in den Dateinamen sind Beispiele und können abweichen. Der erste Teil des Dateinamens (inklusive Unterstrich) darf nicht verändert werden, um die Dateien einlesen zu können.

Die Dateien im Ordner *ImportfürSchild* werden von **_Schule.exe_** erstellt.

## Migration von Atlantis nach SchILD NRW

### Schritt 1

Ein gutes Vorgehen ist, zuerst alle Stammdaten (mit Ausnahme der Schülerdaten), in einer frischen SchILD-Installation anzulegen. Die Schülerdaten werden erst zum tatsächlichen Zeitpunkt des Wechsels übertragen. So kann vermieden werden, dass Schülerdaten in zwei Programmen parallel bearbeitet werden müssen. Unabhängig davon macht es unbedingt Sinn, mit Kopien der SchILD-Datenbank den Import und den Umgang mit Schüler*innen-Daten (z.B. Zeugnisschreibung, Abschnittswechsel, ...) zu testen.

### Schritt 2

Wenn der Tag des Wechsels gekommen ist, erzeugt **_Schule.exe_** mehrere Dateien, die dann wiederum in SchILD importiert werden müssen.

Die Schnittstellen sind (hier)[[https://wiki.svws.nrw.de/mediawiki/index.php?title=Schnittstellenbeschreibung]] genau beschrieben. U.a. steht dort auch, welche Dateien in welcher Reihenfolge zu importieren sind.

Die Importe nach SchILD können problemlos mehrfach wiederholt werden. Es werden keine Duplikate erzeugt. Geänderte Werte werden aktualisiert. **_Schule.exe_** vergleicht die Dateien im ExportAusSchILD-Ordner mit den neuen Datensätzen und schreibt nur die neuen oder geänderten in die jeweilige *.dat-Datei.










![Schild](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/schild.png?raw=true)

Mit jedem Import werden neue Leistungsdatensätze angelegt und bestehende Datensätze aktualisiert.

### Wann im Schuljahr **_Schule.exe_** eingesetzt wird

Der Übertrag der Leistungsdaten ist mindestens 3x im Jahr sinnvoll:

1. Zur Haupterhebung, um die UVD direkt aus SchILD zu erstellen
2. Zu den Halbjahreszeugnissen, um die Zeugniskonferenzen vorzubereiten. 
3. Zu den Jahreszeugnissen, um die Zeugniskonferenzen vorzubereiten.

### Noten einsammeln

Vor den Zeugniskonferenzen kann **_Schule.exe_** wunderbar dafür eingesetzt werden, insbesondere Noten nach SchILD zu übertragen. Dazu müssen alle Lehrkräfte ihre Zeugnisnoten als *Gesamtnote* in Webuntis eintragen:

![Gesamtnoten](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/gesamtnoten.png?raw=true)
 
Abwesenheiten werden ebenfalls nach SchILD übertragen. Der Einsatz eines weiteren Programms zum Einsammeln der Noten ist nicht erforderlich.

## Vier Voraussetzungen für den Einsatz von **_Schule.exe_**

1. Administrativer Zugang zu Webuntis
2. Administrativer Zugang zu SchILD
3. Administrativer Zugang zu Untis
4. In Untis-Kursen muss der Klassenname Bestandteil des Kursnamen (=Schülergruppe) sein. Beispiel: 

![Kurse benennen](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/kurse.png?raw=true)

## Vorbereitungen

Folgende Ordnerstruktur muss im Download-Ordner angelegt werden. Die geforderten Dateien aus den entsprechenden Programmen müssen hineingelegt werden. Die Dateien im Ordner *ImportfürSchILD* werden von **_Schule.exe_** erstellt:

```
Download-Ordner
+---ExportAusSchild
|       Faecher.dat
|       SchuelerLeistungsdaten.dat
|       SchuelerLernabschnittsdaten.dat
|       
+---ExportAusUntis
|       GPU006.TXT
|       
+---ExportAusWebuntis
|       AbsencePerStudent_20240918_1230.csv
|       ExportLessons_20240918_1022.csv
|       MarksPerLesson_20240922_1603.csv
|       StudentgroupStudents_20240918_1022.csv
|       Student_20240922_0952.csv
|       
\---ImportFürSchild
        Faecher.dat
        Schuelerleistungsdateien.dat
        SchuelerLernabschnittsdaten.dat
```

Die Zeitstempel in den Dateinamen sind Beispiele und können abweichen. Der erste Teil des Dateinamens (inklusive Unterstrich) darf nicht verändert werden, um die Dateien einlesen zu können.

### So wird es gemacht:

#### Exportieren Sie die Datei `Student_<...>.csv` aus Webuntis, indem Sie als Administrator:

1. Stammdaten &rarr; Schülerinnen
1. "Berichte" auswählen
1. Bei "Schüler" auf CSV klicken
1. Die Datei `Student_<...>.csv` im Ordner `ExportAusWebuntis` speichern

![Stammdaten](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/stammdaten.png?raw=true)

#### Exportieren Sie die Datei `MarksPerLesson_<...>.csv` aus Webuntis, indem Sie als Administrator:

1. Klassenbuch &rarr; Berichte klicken
1. Alle Klassen auswählen und ggfs. den Zeitraum einschränken
1. Unter "Noten" die Prüfungsart (-Alle-) auswählen
1. Unter "Noten" den Haken bei Notennamen ausgeben _NICHT_ setzen
1. Hinter "Noten pro Schüler" auf CSV klicken
1. Die Datei `MarksPerLesson<...>.csv` im Ordner `ExportAusWebuntis` speichern

![Noten](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/noten.png?raw=true)

#### Exportieren Sie die Datei `AbsencePerStudent_<...>.csv` aus Webuntis, indem Sie als Administrator:

1. Klassenbuch &rarr; Berichte klicken
1. Alle Klassen auswählen und als Zeitraum am besten das aktuelle Schuljahr wählen
1. Unter "Abwesenheiten" Fehlzeiten pro Schüler*in auswählen
1. pro Tag anhaken
1. Auf CSV klicken
1. Die Datei `AbsencePerStudent_<...>.csv` im Ordner `ExportAusWebuntis` speichern 

![Klassenbuch Berichte](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/abwesenheiten.png?raw=true)

#### Exportieren Sie die Datei `GPU006.TXT` aus Untis, indem Sie als Administrator:

1. Datei &rarr; Import/Export &rarr; Export TXT &rarr; Fächer klicken
1. Trennzeichen: Semikolon, Textbegrenzung: ", Encoding :UTF8
1. Die Datei `GPU006.TXT` im Ordner `ExportAusUntis` speichern 
	
![Untis Export](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/gpu.png?raw=true)

#### Exportieren Sie die Dateien `Faecher.dat`, `SchuelerLeistungsdaten.dat` und `SchuelerLernabschnittsdaten.dat` aus SchILD, indem Sie als Administrator:

1. Datenaustausch &rarr; Schnittstelle &rarr; Export klicken
1. Alle Dateien abhaken und dann die drei Dateien `Faecher.dat`, `SchuelerLeistungsdaten.dat` und `SchuelerLernabschnittsdaten.dat` auswählen
1. Das Ausgabeverzeichnis `ExportAusSchild` auswählen
1. Den Export starten

![Schild Export](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/exportausschild.png?raw=true)

## Herunterladen des Programms

Laden Sie alle Dateien aus dem ![exe-Ordner](https://github.com/stbaeumer/webuntis2schild/tree/main/exe) z.B. in den Download-Ordner herunter. Also: *webuntis2schild.exe*, *webuntis2schild.dll* usw. Eine Installation ist nicht notwendig. 
**_webuntis2schild.exe_** kann mit Doppelklick gestartet werden:

![Programm starten](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/exe.png?raw=true)


## Programmstart

**_webuntis2schild.exe_** kann mit Doppelklick gestartet werden. Es öffnet sich ein Terminalfenster, in dem die Ausführung des Programms angezeigt wird.
Nacheinander werden alle o.g. Dateien eingelesen.  

Nach dem Einlesen kann die Auswahl der Klassen eingeschränkt werden: 

![Programm starten](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/programmstart.png?raw=true)

Beispiele:

- `ENTER` ohne weitere Angaben sucht nach allen Klassen
- `5a,5b,5c` sucht nach Klassen 5a, 5b und 5c
- `5` sucht nach allen Klassen, die mit 5 beginnen
- `5a` sucht nach Klasse 5a


Im Anschluss werden die relevanten Dateien im Ordner `ExportFürSchild` abgelegt:

- `Faecher.dat` wird angelegt, sofern Fächer in Untis vorhanden sind, die in SchILD noch nicht existieren. Alternativ kann die Datei auch aus Untis exportiert werden. Prüfen Sie am besten zuerst die Fächer. Vervollständigen Sie in SchILD die Fächer.
- `SchuelerLernabschnittsdaten.dat` wird angelegt, sofern die Lernabschnitte in SchILD noch nicht existieren. Grundsätzlich sollten die Lernabschnitte bereits in SchILD angelegt sein.
- `SchuelerLeistungsdaten.dat` wird angelegt, sofern Leistungsdaten sich verändert haben oder der Leistungsdatensatz in SchILD noch nicht existiert. Der Import dieser Datei setzt voraus, dass die Fächer und Lernabschnitte in SchILD existieren bzw. in den entsprechenden Dateien `Faecher.dat` und `SchuelerLernabschnittsdaten.dat` für den Import bereit sind.

![Import für Schild](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/importfuerschild.png?raw=true)

## Übertrag der Zeugnisnoten und Abwesenheiten

**_Schule.exe_** so konzipiert, dass Lehrerinnen und Lehrer alle Zeugnisnoten als Gesamtnoten in Webuntis eintragen können. 
Zur Vorbereitung auf die Zeugniskonferenz lässt die Zeugnisschreibung **_Schule.exe_** nach dem Fristende für die Noteneingabe laufen und importiert somit die Zeugnisnoten nach SchILD. Diese Lösung kommt also ohne händisches Eintippen und ohne weitere Software (SchILD-App usw.).



## Fragen und Antworten

### Ist es gefährlich Daten über *.dat Dateien nach SchILD zu importieren? 
Nein. Die Schnittstellen von SchILD sind sehr gut dokumentiert. SchILD meldet sich, wenn der Import aus irgendeinem Grund nicht klappt.

### Ich habe Dateien manuell nachbearbeitet. Jetzt sind die Umlaute kaputt.
Haben Sie eine der Dateien evtl. nachträglich mit Excel geöffnet und bearbeitet? Öffnen Sie Dateien bitte nicht mit Excel, sondern -sofern notwendig- mit einem Editor, wie z.B. Notepad++. In jedem Fall müssen die Import-Dateien im UTF-8 Format vorliegen.

### Darf ich das Programm kostenlos nutzen?
Ja. **_Schule.exe_** steht unter der GPL-3.0 Lizenz kostenlos für jedermann zur Verfügung.

### Ist es nicht gefährlich, Programme aus dem Internet zu laden?
Ja. Es ist immer gefährlich, Programme aus dem Internet zu laden. **_Schule.exe_** ist jedoch quelloffen und kann von jedem eingesehen und geprüft werden.

### Man kann die Schuelerleistungsdaten.dat auch direkt aus Untis exportieren. 
Ja, das ist richtig. Ohne Untis-Kursmodul ist die `Schuelerleistungsdaten.dat` allerdings leer.




### Wie lese ich die Dateien in SchILD ein?

#### Schritt1:
![Import für Schild](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/import.png?raw=true)

#### Schritt2:
![Import für Schild](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/import2.png?raw=true)
