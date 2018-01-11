using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Desaster : MonoBehaviour {
	public RecourceController recourceController;
	public GameObject panelMessage;
	public Text textMesage;
	public SelectionManager manager;
	public int minTimeTillDesaster, maxTimeTillDesaster, chanceDiver, chanceFisher, chanceOil;
	public int timeTillDesaster;
	private bool timerRunning=true;
	private int factor = 100;

	// Use this for initialization
	void Start () {
		timeTillDesaster = Random.Range (minTimeTillDesaster*factor, maxTimeTillDesaster*factor);
	}

	public void Restart(){
		timerRunning = true;
		recourceController.Restart ();
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
				Debug.Log (random);
				if (random < chanceDiver) {
					panelMessage.SetActive (true);
					textMesage.text = "Ein Taucher kommt vorbei und zerstört eine deiner Korallen.";
					manager.destroyRandom ();
				} else {
					if (random < chanceDiver + chanceFisher) {
						panelMessage.SetActive (true);
						textMesage.text = "Ein Fischerboot fängt einige deiner Fische.";
						recourceController.LooseFish (20);
					} else {
						if (random < chanceDiver + chanceFisher + chanceOil) {
							panelMessage.SetActive (true);
							textMesage.text = "Menschen verschmutzen die Ozeane und die Wasserqualität sinkt.";
							recourceController.IncreaseWaterPolution (40);
						}
					}
				}
			}
		}
	}
}
