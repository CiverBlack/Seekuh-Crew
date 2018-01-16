using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Desaster : MonoBehaviour {
	public RecourceController recourceController;
	public GameObject panelMessage;
	public GameObject tanker, diver, fisher, seecow;
	public Text textMesage;
	public SelectionManager manager;
	public int minTimeTillDesaster, maxTimeTillDesaster, chanceDiver, chanceFisher, chanceOil;
	public int timeTillDesaster;
	public bool timerRunning=true;
	private int factor = 100;

	// Use this for initialization
	void Start () {
		timeTillDesaster = Random.Range (minTimeTillDesaster*factor, maxTimeTillDesaster*factor);
		panelMessage.SetActive (true);
		tanker.SetActive (false);
		diver.SetActive (false);
		fisher.SetActive (false);
		seecow.SetActive (true);
		PauseApplication ();
		textMesage.text = "\nWILKOMMEN BEI GREAT BARRIER MIEF\nIn diesem Spiel geht es darum zu verhindern das ein Riff volständig durch Menschen zerstört wird.\nDa wir noch keine Hilfen während des Spiels haben kommt hier nun eine Erklärung:\nOben links finden sich deine Recourcen Plankton und Kalk. Klak wird für das errichten neuer Korallen benötigt und mit Plankton kannst du Fische kaufen.\nOben mittig siehst du die Verschmutzungsanzeige. Sie zeigt an wie dreckig der Ozean ist. Du solltest sie stehts niedrig halten. Du verlierst wenn sie voll ist und auch vorher schrenkt sie die effektivität deiner Korallen und Fische ein.\nOben rechts ist das Menü.\nUnten links findest du die Korallen die du bauen kannst. Wähle einen leeren Bereich des Spielfeldes aus und klicke dann auf die Koralle die du erichten willst.\nGanz links ist die Hauskoralle. Sie kostet " + recourceController.smallHouseCost + " Kalk und kann bis zu 5 Papageienfische oder einen Tigerhai beherbergen.Bevor du eine neue bauen kannst musst du allerdings zuerst eine bestehende aufleveln.\nDann kommt die Kalkkoralle für " + recourceController.chalkCoralCost + " Kalk, allerdings erhöht sie auch deine Produktion an Kalk.\nEs folgt das Seegrass für " + recourceController.seeweedCost + " Kalk welches deine Planktonproduktion erhöht.\nAls letztes kommt die Filterkoralle für " + recourceController.filterCoralCost + " Kalk die Verschmutzung aus dem Wasser filtern kann.\nUnten in der Mitte sind zum einen der Papageifisch für " + recourceController.smallFishCost + " Plankton sowie der Tigerhai für " + recourceController.smallToBigFishRatio + " Papageifische beheimatet. Beide erhöhen die Geschwindigkeit in der deine Gebäude errichtet werden.\nDie Bombe lässt dich die von dir ausgewählte Koralle zerstören und mit dem Pfeil kannst du die ausgewählte Hauskoralle für " + recourceController.levelUpCost + " Kalk upgraden.\n Zuletzt findet sich unten links dein aktueller Score.\nAlle Korallen brauchen eine Weile um gebaut zu werden dafür wird eine Prozentzahl ihres Fortschritts angezeigt. Erst fertige Korallen haben einen Effekt in der Spielwelt.\nVIEL SPASS BEIM SPIELEN";
	}

	public void RestartApplication(){
		timerRunning = true;
		recourceController.Restart();
		manager.Restart ();
	}

	public void PauseApplication(){
		timerRunning = false;
		recourceController.Stop ();
		manager.Stop ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (timerRunning){
			timeTillDesaster--;
			if (timeTillDesaster < 0) {
				timerRunning = false;
				recourceController.Stop ();
				timeTillDesaster = Random.Range (minTimeTillDesaster*factor, maxTimeTillDesaster*factor);
				factor--;
				float random = Random.Range (0, chanceDiver + chanceFisher + chanceOil);
				if (random < chanceDiver) {
					panelMessage.SetActive (true);
					tanker.SetActive (false);
					diver.SetActive (true);
					fisher.SetActive (false);
					seecow.SetActive (false);
					PauseApplication ();
					textMesage.text = "Ein Taucher kommt vorbei und zerstört eine deiner Korallen.";
					manager.destroyRandom ();
				} else {
					if (random < chanceDiver + chanceFisher) {
						panelMessage.SetActive (true);
						tanker.SetActive (false);
						diver.SetActive (false);
						fisher.SetActive (true);
						seecow.SetActive (false);
						PauseApplication ();
						textMesage.text = "Ein Fischerboot fängt einige deiner Fische.";
						recourceController.LooseFish (20);
					} else {
						if (random < chanceDiver + chanceFisher + chanceOil) {
							panelMessage.SetActive (true);
							tanker.SetActive (true);
							diver.SetActive (false);
							fisher.SetActive (false);
							seecow.SetActive (false);
							PauseApplication ();
							textMesage.text = "Menschen verschmutzen die Ozeane und die Wasserqualität sinkt.";
							recourceController.IncreaseWaterPolution (40);
						}
					}
				}
			}
		}
	}
}
