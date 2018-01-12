using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Desaster : MonoBehaviour {
	public RecourceController recourceController;
	public GameObject panelMessage;
	public GameObject tanker, diver, fisher;
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
		PauseApplication ();
		textMesage.text = "Rette das Riff:\nHauskoralle: " + recourceController.smallHouseCost + " Kalk und ableveln einer großen Hauskoralle\nHauskoralle aufleveln: " + recourceController.smallHouseCost + " Kalk\nKalkkoralle: " + recourceController.chalkCoralCost + " Kalk\nSeegrass: " + recourceController.seeweedCost + " Kalk\nFilterkoralle: " + recourceController.filterCoralCost + " Kalk\nPapageifisch: " + recourceController.smallFishCost + " Plankton\nTigerhai: " + recourceController.smallToBigFishRatio + " Papageifische";
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
					PauseApplication ();
					textMesage.text = "Ein Taucher kommt vorbei und zerstört eine deiner Korallen.";
					manager.destroyRandom ();
				} else {
					if (random < chanceDiver + chanceFisher) {
						panelMessage.SetActive (true);
						tanker.SetActive (false);
						diver.SetActive (false);
						fisher.SetActive (true);
						PauseApplication ();
						textMesage.text = "Ein Fischerboot fängt einige deiner Fische.";
						recourceController.LooseFish (20);
					} else {
						if (random < chanceDiver + chanceFisher + chanceOil) {
							panelMessage.SetActive (true);
							tanker.SetActive (true);
							diver.SetActive (false);
							fisher.SetActive (false);
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
