# CrystalQuest

Crystal Quest ist ein 2D Top-Down Weltraumactionspiel, das 1987 Patrick Buckland auf dem Mac veröffentlicht hat [82].
 
![GitHub Logo](/images/logo.png)
Format: ![Alt Text](url)
Abbildung 125: Crystal Quest by Patrick Buckland

Das Spiel besteht insgesamt aus 19 Wellen (Waves), die als Level aufzufassen sind. Der Spieler hat die Aufgabe, mit seinem Raumschiff alle Kristalle der Welle einzusammeln, um ein Tor zu öffnen, durch welches das nächste Level erreicht werden kann. KI-gesteuerte Gegner, die über Portale ins Spielgeschehen eingreifen, versuchen den Spieler mit unterschiedlichen Mitteln von seiner Aufgabe abzuhalten. Diese Gegner werden im fortschreitenden Spiel stärker und zahlreicher. Zusätzlich erscheinen mit jeder neuen Welle Minen im Spielbereich, die dem Spieler das Fliegen von einem Kristall zum nächsten und das Ausweichen vor den Gegnern und deren Geschossen erschwert.
Die Umsetzung des Crystal Quest in Unity und die daraus resultierenden Cross-Plattform Anwendungen dienen dem Zweck, die Möglichkeiten der Unity Engine an einem praktischen Beispiel zu erarbeiten. Es kommt dabei weniger auf die grafische Umsetzung der Inhalte an. Die Umsetzung und Realisierung des Spiels mit der Entwicklungsumgebung Unity stehen im Vordergrund und bilden den Schwerpunkt der Bachelorarbeit. Im Rahmen dieser Arbeit soll ein möglichst fertiges, lauffähiges Spiel, das dem 1987 von Patrick Buckland veröffentlichten Crystal Quest ähnelt, entwickelt werden, das auf unterschiedlichen Endgeräten spielbar ist.

## 7.1	Motivation und Ziel des Spiels
Ziel des Spiels ist es, die Gesamtpunktzahl (Highscore) anderer Spieler zu überbieten. Hierfür versucht der Spieler während jeder Wave mit seinem Flugobjekt möglichst schnell alle Kristalle im Level einzusammeln, da Bonuspunkte vergeben werden, wenn diese Aufgabe in einer gewissen Zeit erfüllt wird.
Um dem Spieler eine kleine Unterstützung zu bieten, damit dieser möglichst viele Level meistert und eine hohe Punktzahl erreichen kann, sind in unterschiedlichen Level einsammelbare Smart Bombs platziert, die der Spieler beim Herüberfliegen einsammeln kann.
Wenn der Spieler eine Smart Bomb aktiviert, werden vorhandene Gegenspieler sowie vorhandene Projektile zerstört.
7.2	KI-Gegner
Zwölf verschiedene KI-Gegner Typen versuchen den Spieler vom Erreichen des nächsten Levels abzuhalten. Sie unterscheiden sich im Bewegungsverhalten, in der Anzahl der Punkte, die der Spieler als Belohnung für deren Zerstörung erhält sowie in der Fähigkeit, Projektile abzufeuern, und besitzen ein unterschiedliches Maß an „Lebensenergie“.
In Folgenden ist eine Übersicht der wichtigsten Eigenschaften der verschiedenen Gegenspieler-Typen dargestellt: 
•	Bonus-Punkte, die der Spieler erhält, wenn er diesen Gegner zerstört
•	Zufälliges Bewegungsmuster, Angriffsbewegung - Bewegung auf kürzestem Weg zu Spieler, mehrere Bewegungsmuster 
•	Schussfrequenz und Projektiltyp
Abbildung 126 zeigt die Übersicht der verschiedenen Gegenspieler mit Eigenschaften
 
Abbildung 126: Übersicht der Gegenspieler mit Eigenschaften [82]

## 7.3	Programmierkonzept
In diesem Kapitel werden die Schlüsselkonzepte, welche bei der programmiertechnischen Realisierung von Crystal Quest unter Unity verwendet wurden, erörtert.
Das erste Konzept besteht in der nahtlosen Arbeit mit Unity und der Unity Engine. Das Spiel wurde mit dem Workflow im Unity Editor konstruiert. Die benötigten GameObjects wurden somit nicht per Scripts aufgebaut.
Anschließend wurden die Scripts entwickelt und die konstruierten GameObjects damit ausgestattet. Die Scripts arbeiten zum Teil komponentenweise. Sie sind geschlossene Systeme und unabhängig von anderen Scripts. Dies hat den Vorteil, dass sie sich flexibel an allen GameObjects des Spiels einsetzen lassen. Dieses Verhalten wird in der OOP oftmals durch Vererbung, Abstraktion und Interfaces erziehlt. Auf diese OOP-Prinzien wurde überwiegend verzichtet, um Code nicht wiederholend zu verfassen und Methoden nicht zu überschreiben. Das Überschreiben von Methoden könnte dazu führen, dass Datenfelder im Inspector des Scripts nicht mehr funktionieren, diese sich jedoch nach einmaliger Definition nicht aus dem Zugriff des Inspectors entfernen lassen.
Es wurde darauf geachtet, den Rahmen des Echtzeit Systems von Unity nicht durch zu rechenintensive Algorithmen und Suchmethoden der Unity-API zu belasten, womit eine Instabilität des Echtzeitsystems vermieden wird. Dies wurde im Kern durch Singletons und einem Nachrichtensystem (DomainEventManager) realisiert. So kann Kommunikation zwischen Scripts unterschiedlicher GameObjects weitestgehend ohne vorherige Referenzsuche stattfinden.
Ein weiterer Vorteil des Nachrichtensystems ist die Einhaltung des Dependency Inversion-Prinzips, was eine leichte Erweiterungsfähigkeit des Systems ermöglicht.
So besteht das Spiel Crystal Quest in der Realisierung aus einem übergeordnetem Nachrichtensystem und mehreren Systemen, die über das Nachrichtensystem arbeiten und darüber untereinander kommunizieren.
Speicherzugriffe und das Anlegen von neuen Objekten werden weitestgehend in den Zeitbereich verlagert, in der der Spieler sein Spielobjekt nicht bewegen kann und keine Bewegungen im Spiel sattfinden. In dieser Zeit wird in anderen Programmen meist ein Ladebildschirm oder ein Standbild angezeigt. Dies geht auf die Nutzung des Objekt Pooling-Konzepts zurück. In der Ladezeit werden alle auftauchenden Objekte erstellt und müssen somit nicht während der Spielzeit erzeugt werden. GameObjecs, die visuell nicht mehr sichtbar sind, werden nicht zerstört, sondern lediglich deaktiviert und recycelt, um das Echtzeitsystem durch die Ausführung des Garbage Collectors nicht instabil werden zu lassen.

Zusammenfassend sind die wichtigsten Konzepte in der folgenden Liste dargestellt:
•	Code mit einfachen Mitteln mit möglichst wenig Abhängigkeiten in Scenesimulation testbar
•	Code nicht mehrfach schreiben
•	Mit dem Inspector arbeiten: Flags und Fields im Inspector sollten nutzbar bleiben
•	Kein coupling  flexibilität (erweiterbar), wartbar 
•	Dependency inversion (n zu 1 statt 1 zu n Verbindung)  leicht erweiterbar
•	Kein Erzeugen der GameObjects in laufender Spielzeit, wenn Spieler Spielfigur steuert
•	Kleinstmögliche Anzahl von Suchvorgängen der UnityEngine wird angestrebt
•	Objekte werden wiederverwendet (Object Pooling)
•	Echtzeitsystem soll stabil gehalten werden
•	Suchvorgänge verhindern  Referenzen setzen / Singleton


## 7.4	Skript-Komponenten
Abbildung 127 zeigt eine Übersicht der in Crystal Quest zum Einsatz kommenden Scripts. Die Darstellung zeigt die GameObjects (schwarzer Kasten) als System und die darin arbeitenden Scripts des GameObjects. Zusätzlich ist die Kommunikation mit dem DomainEventManager eingetragen. Diese Abbildung dient der Übersicht und strebt keine umfassende Darstellung an.
 
Abbildung 127: Übersicht der Skriptkomponenten von Crystal Quest


## 7.5	DomainEventManager – Message System
Im folgenden Abschnitt wird das Nachrichtensystem (DomainEventManager) von Crystal Quest erörtert. Dieses ermöglicht die Kommunikation zwischen Skriptkomponenten. Die Kommunikation findet indirekt über einen Mittelsmann statt, womit es vom Kommunikationspartner unabhängig ist und erweiterbar bleibt [83].
Das Nachrichtensystem wird am Beispiel des Punkteystem von Crystal Quest erörtert, da die Punkte von mehreren unterschiedlichen GameObjects freigegeben und von mehreren Scripts aufgegriffen werden. Freigegeben werden Punkte durch das ScoreableObjectScript-Skript. Aufgegriffen wird die freigegebene Punktzahl vom ScoreManager-Skript. Dieses verwaltet die Punktzahl und kontrolliert, ob die nächste Punktzahlgrenze erreicht ist, um dem Spieler ein extra Leben zu reservieren. Angezeigt wird die aktuelle Punktzahl des ScoreManagers über das UIScoreScript.
Abbildung 128 zeigt das Klassendiagrammder beteiligten Scripts.
 
Abbildung 128: Klassendiagramm des Eventsystems am Beispiel des Punktesystems
Abbildung 129 zeigt den sequenziellen Kommunikationsablauf des Punktesystems über das Nachrichtensystem für den Fall, dass ein ScoreableObjectScript-Skript meldet, dass Punkte freigegeben wurden.
 
Abbildung 129: Sequenzdiagramm Punktesystem

Abbildung 130 zeigt die Übersicht und den Kommunikationsfluss des Punktesystems im Nachrichtensystem.
 
Abbildung 130:Übersicht des Punktesystems

### 7.5.1	EventManager

Der EventManager ist die zentrale Datenbank von Events. Dies erlaubt es den Event Listener unabhängig von dem Event Trigger zu gestalten. Dafür sind alle Listener und Trigger vom EventManager abhängig.
Der EventManager wird aus Gründen der Performance als Singleton realisiert.
```csharp
using UnityEngine;
using UnityEngine.Events; // UnityEvent
using System.Collections;
using System.Collections.Generic;  // Dictionary

[System.Serializable]   // FloatEvent kann im Inspektor gesetzt werden
public class FloatEvent : UnityEvent<float> { }

public class EventManager : MonoBehaviour
{
    // Datenbank: Event - Listener
    private Dictionary<string, UnityEvent> eventDictionary;      // Datenbank für UnityEvent
    private Dictionary<string, FloatEvent> floatEventDictionary; // Datenbank für UnityEvent<float>

    //Singleton
    private static EventManager eventManager;

    // (C# Getter)
    public static EventManager instance
    {
        get
        {
            // Lazy Singleton (wird erst bei erstem Zugriff initialisiert)
            if (eventManager == null) {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                    Debug.LogError("There needs to be one active EventManger in your scene.");
                else
                    // einmalige initialisierung
                    eventManager.Init();
            }

            return eventManager;
        }
    }

    void Init() {
        // UnityEvent Datenbank erzeugen
        eventDictionary = new Dictionary<string, UnityEvent>();
        // FloatEvent Datenbank erzeugen
        floatEventDictionary = new Dictionary<string, FloatEvent>();
    }

    // register Observer
    public static void StartGlobalListening(string globalEventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(globalEventName, out thisEvent))
        {
            // Event in Datenbank vorhanden, (Observer) Event Listener registrieren 
            thisEvent.AddListener(listener);
        }
        else
        {
            // Event noch nicht in Datenbank, erzeuge Eintrag und füge (Observer) Listener hinzu
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(globalEventName, thisEvent);
        }
    }
    
    // unregister Observer
    public static void StopGlobalListening(string globalEventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(globalEventName, out thisEvent))
            // Event existiert, entferne (Observer) Event Listener
            thisEvent.RemoveListener(listener);
    }

    // Trigger Event (execute Event Listener)
    public static void TriggerGlobalEvent(string globalEventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(globalEventName, out thisEvent)) {
            Debug.Log(globalEventName + " executed");
            thisEvent.Invoke();
        }
        else
            Debug.LogError(globalEventName + " not found");
    }

    #region float
    public static void StartGlobalListening(string globalEventName, UnityAction<float> listener) {
        FloatEvent thisEvent = null;
        if (instance.floatEventDictionary.TryGetValue(globalEventName, out thisEvent)) {
            thisEvent.AddListener(listener);
        }
        else {
            thisEvent = new FloatEvent();
            thisEvent.AddListener(listener);
            instance.floatEventDictionary.Add(globalEventName, thisEvent);
        }
    }

    public static void StopGlobalListening(string globalEventName, UnityAction<float> listener) {
        if (eventManager == null) return;
        FloatEvent thisEvent = null;
        if (instance.floatEventDictionary.TryGetValue(globalEventName, out thisEvent)) {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerGlobalEvent(string globalEventName, float value)
    {
        FloatEvent thisEvent = null;
        if (instance.floatEventDictionary.TryGetValue(globalEventName, out thisEvent))
        {
            thisEvent.Invoke(value);
        }
    }
    #endregion

}

```

### 7.5.2	Event Listener 
Der Event Listener muss sich zuerst an einem Event registrieren, bevor ihm mitgeteilt werden kann, dass ein Event eingetreten ist. Damit keine Speicherlecks entstehen, muss sich der Event Listener auch wieder am Event abmelden. Der An- und Abmeldevorgang wird über die OnEnable und OnDisable MonoBehaviour-Methoden ausgeführt (5.3) damit in der Zeit, in der das Skript aktiv ist, auf das Event reagiert werden kann. 
Im Fall des Punktesystems ist der ScoreManager der Event Listener und wartet auf Meldungen, dass Punkte freigegeben wurden.
Der Anmelde- und Abmeldeprozess ist im C# Listing 39 dargestellt. Die Aktion, die beim Eintreten des Events ausgeführt wird, ist in C# Listing 40 definiert.


```csharp
	void OnEnable () {
		EventManager.StartGlobalListening (EventNames.ScoredValue, OnScoring);
	}
	
	void OnDisable () {
		EventManager.StopGlobalListening (EventNames.ScoredValue, OnScoring);
	}
```
C# Listing 39: Register- / Deregisterprozess


```csharp
	void OnScoring (float scoreValue)
	{
		this.score.AddPoints (scoreValue);
                // UI-Listener informieren
		EventManager.TriggerGlobalEvent (EventNames.ScoreUpdate, scoreValue);

                // Bonusleben Berechnen
		if ( 0 <= (scoreValue - pointsForExtraLive * extraLiveStep))
		{
			extraLiveStep++;
                        // Bonusleben erhalten, informiere Lebensverwaltung (und UI)
			EventManager.TriggerGlobalEvent (EventNames.ExtraLifeGained);
			myAudioSource.PlayOneShot (extraLifeClip);
		}
	}
```
C# Listing 40: Event Action (UnityAction<float> bzw. Methode mit float-Parameter)
 
### 7.5.3	Event Trigger
Ein Event Trigger ist derjenige, der das initialisierte Event System aktiviert und die Kommunikation startet. In dem Punktesystem aktiviert das ScoreableObjectScript-Skript das Event „ScoredValue“ und übergibt dabei den Parameter „scoreValue“ an die Event Listener. Dies hat zu Folge, dass alle aktiven „ScoredValue“-Event Listener die Nachricht erhalten und auswerten können. Im dargestellten Beispiel erhält der ScoreManager die Punktzahl und addiert sie zu der Punktzahl, die der Spieler bereits gesammelt hat.
C# Listing 41 zeigt den Ausschnit des ScoreableObjectScript-Skripts, dass den Trigger-Vorgang aktiviert.

```csharp
    [SerializeField]
    float scoreValue;

    public void ReleaseScore()
    {
        EventManager.TriggerGlobalEvent(EventNames.ScoredValue, scoreValue);
    }
```
C# Listing 41: Event Trigger


## 7.6	Spielablauf
Das Spiel besteht aus mehreren Waves. Die Waves bestehen wiederum aus einzelnen Wellenzuständen. In Abbildung 131 ist der Ablauf von Wave-Zuständen bildlich dargstellt.
Nach dem Ladevorgang der Spielszene wird durch das Skript „CrystalQuestWaveManager“ der Spielstart signalisiert. Dazu wird der Initialisierungsprozess „WaveInit“ der ersten Wave ausgeführt und anschließend das WaveStart Signal gemeldet.
 
Abbildung 131: Spielablauf (WaveManager)
In den folgenden Abbildungen dieses Kapitels werden die einzelnen Wave-Zustände detailliert beschrieben.

 
Abbildung 132: Event „WaveInit“

 
Abbildung 133: Event „WaveStart“
 
Abbildung 134: Event „WaveTaskComplete“
 
Abbildung 135: Event „WaveFailed“
 
Abbildung 136: Event „PortalReached“
 
Abbildung 137: Event „PlayerDied“
 
Abbildung 138: Event "PlayerWillRespawn"
 
Abbildung 139: Event "ReleaseScoreValue"
 
Abbildung 140: Sequenzdiagramm des "ReleasedScoreValue"-Events
 
Abbildung 141: Event "SmartBombTriggered"


## 7.7	Object Pooling
Object Pooling ist ein Konzept, das ebenfalls ein Entwurfsmuster der OOP darstellt. Ziel dieses Konzepts ist es Objekte zu recyclen, d.h. Objekte werden nicht zerstört und aus dem Speicher entfernt. Der Erzeugungs- und Zerstörungsprozess von Objekten hat den Nachteil, dass in einer verwalteten Programmumgebung wie C# und Java im Hintergrund ein Garbage Collector zerstörte und nicht mehr referenzierte Objekte sammelt und zu einem bestimmten Zeitpunkt entfernt. Zu diesem Zeitpunkt gehört dem Garbage Collector die komplette Rechenleistung und andere Vorgänge der Anwendung werden somit angehalten. In Echtzeitanwendungen wie Spielen kann dies zu massiven Einbrüchen der Verarbeitung der Frame Loop führen und eine unmittelbare Reaktion auf eine Eingabe nicht mehr gewährleisten. Dass der Garbage Collector nicht anspringen muss und damit das Echtzeitsystem stabil bleibt, kann somit auf Objekt Pooling zurückgegriffen werden, da dieses System dafür sorgt, dass Objekte nicht zerstört werden, sondern deaktiviert und bei Wiederbenötigen eines Objekts gleichen Typs wieder aktiviert und recyelt werden [84]–[86].
In Crystal Quest wurde dieses System für verschiedene Objekttypen realisiert. Die PoolManager, die den „Object Pool“ verwalten, sind unter anderem:
•	EnemyManager
•	CrystalManager
•	WaveMineManager
•	SmartBombItems
•	ExplosionManager
•	BurstShootingPooled
Object Pooling wird für Gegenspieler, Projektile, Explosionen, Kristalle, WaveMines und SmartBombItems eingesetzt. Es handelt sich hierbei bei allen um GameObjects, die nicht dauerhaft in der Scene aktiv sind.
Das Erzeugen von GameObjects kostet Rechenzeit. Die GameObject Pools werden daher zum Szenenbeginn, bzw. für Crystal Quest während der Welleninitialisierung, erzeugt. Während der Welleninitialisierung kann der Spieler sein Raumschiff nicht steuern und bemerkt daher nicht, dass durch den Erzeugungsvorgang die Spielsteuerung instabil war.
C# Listing 42 beschreibt den Object Pooler Manager, der nach der Szeneninitialisierung das im Attribut „pooledObject“ mehrfach erzeugt und deaktiviert in einer Liste speichert. Wird über die Schnittstelle „GetPooledObject“ ein GameObject angefordert, durchsucht der Pool Manager den Pool nach dem ersten deaktivierten GameObject. Ist kein deaktiviertes GameObject vorhanden, bestimmt das Attribut „willGrow“, ob der Pool Manager ein weiteres GameObject aus der Vorlage erzeugt, im Pool ablegt und zurückgibt oder ob er direkt eine null-Referenz zurückgibt.
C# Listing 43 zeigt einen Client des Pool Managers. Wenn die linke Maustaste gedrückt ist fragt dieser in jedem Frame-Zyklus nach einem „pooledObject“.


## 7.8	WaveSystem
Das WaveSystem besteht im Kern aus einer Liste von Waves. Jede dieser Wellen bestimmt gewisse Eigenschaften. Dazu zählen die Anzahl der Kristalle, Minen und Smart Bombs, die erzeugt werden müssen oder das Zeitlimit, das der Spieler unterschreiten muss, um einen Bonus zu erhalten.  Zusätzlich enthält jede Wave die Angabe, welche Gegnertypen vorkommen können, wie viele Gegner zu einem Zeitpunkt existieren dürfen und wie hoch die Wahrscheinlichkeit ist, dass der Gegnertyp erzeugt wird.
Abbildung 142 zeigt die Wave-Eigenschaften am Beispiel der ersten Welle. 
Abbildung 145 zeigt das WaveSystem, das aus insgesammt 19 Wellen besteht.
Abbildung 146 zeigt den WaveSystem Editor, der die Zuordnung von Eigenschaften an die Wellen im WaveSystem durch einen Algorithmus erlaubt. Dies erleichtert das Setzen der Welleneigenschaften für alle Wellen, da nur so die Parameter des Algorithmus definiert und übernommen werden müssen. Dieser setzt die Eigenschaften der Wellen daraufhin automatisch und es muss nicht jede Welle einzeln manuell über den Inspector (Abbildung 142) definiert werden.
Der WaveSystem Editor ist eine Unity Editor-Erweiterung und dient der Demonstration der Flexibilität von Unity. Diese Erweiterung zeigt nur im Ansatz die Erweiterungsmöglichkeiten des Unity Editors.
 	 	 
Abbildung 142: 1. Wave
Abbildung 143: 7. Wave
Abbildung 144: 19. Wave


 	 
Abbildung 145: WaveSystem
Abbildung 146: WaveSystem Editor (Unity Editor Erweiterung)


## 7.9	Highscore - Persistente Daten 
Wenn der Spieler das Spielende erreicht, werden die aktuelle Punktzahl, die erreichte Welle und die benötigte Spielzeit auf dem Dateisystem als Highscoreliste gespeichert. Im Hauptmenü wird beim Starten der Anwendung die Highscoreliste vom Dateisystem geladen und beim Öffnen des Highscore-Menüs in sortierter Form eingeblendet.

## 7.10	Kollisionssystem
Da Interaktionen zwischen GameObjects überwiegend durch Kollisionen auftreten, wurde ein Script entwickelt, das flexibel genug ist, an allen GameObjects eingesetzt zu werden. Dazu wurden die GameObjects in die folgenden Hauptkategorien (Layer) eingeteilt:
•	Player: Raumschiff des Spielers
•	Player Projectile: Projektile des Spielers
•	Level: Spielbereichsabgrenzung
•	LevelStopper: Bewegte Objekte können damit im Spielbereich gehalten werden
•	Level Damage: Bereiche im Level, die zu einem Schaden am Raumschiff des Spielers führen 
•	Enemy: Raumschiffe der Gegenspieler
•	Enemy Projectile: Projektile der Gegenspieler
•	Collectables: Vom Spieler einsammelbare Objekte (Kristalle und SmartBombs)

Abbildung 147 zeigt die Interaktionsmöglichkeiten der Layer des Physiksystems und definiert damit welche Layer miteinander kollidieren können.
 
Abbildung 147: Interaktionsmöglichkeiten der Layer des Physiksystems

Abbildung 148 zeigt den sequenziellen Ablauf einer Kollision am Gegenspielertyp “Annoyer” im UML Sequenzdiagramm. Die schwarzen Endpunkte stellen Event-Signale dar.
 
Abbildung 148: Sequenzdiagram von Gegner Annoyer – Kollision mit Schaden führt zur Explosion

