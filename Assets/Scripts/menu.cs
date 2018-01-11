using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour {
	public GameObject panelMenu;

	public void openMenu(){
		panelMenu.SetActive (true);
	}

	public void exitMenu(){
		panelMenu.SetActive (false);
	}

	public void beenden(){
		Application.Quit();
	}
}
