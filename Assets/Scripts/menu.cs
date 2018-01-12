using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour {
	public GameObject panelMenu;
	public AudioSource sound, sound1;

	public void openMenu(){
		sound.Play ();
		panelMenu.SetActive (true);
	}

	public void exitMenu(){
		sound1.Play ();
		panelMenu.SetActive (false);
	}

	public void beenden(){
		Application.Quit();
	}
}
