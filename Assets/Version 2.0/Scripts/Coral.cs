using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coral : MonoBehaviour {

	public int productionChalkPerLevel = 0, productionPlanktonPerLevel = 0, reductionPolutionPerLevel = 0, costChalk = 1, maxLevel = 1;
	private int progress = 0, level = 1;
	public bool dead = false;
	public GameObject go;

	//Sets all Children inactive exept the third.
	void Start (){
		for (int i = 0; i < ((maxLevel * 2) + 2); i++) {
			go.transform.GetChild (i).gameObject.SetActive (false);
		}
		go.transform.GetChild (2).gameObject.SetActive (true);
	}

	//Sets every Frame the shown Progress to the value in progress or sets the TextMesh inactive if progress is 100.
	void Update () {
		if (progress < 100) {
			go.transform.GetChild (1).gameObject.SetActive (true);
			go.transform.GetChild (1).GetComponent<TextMesh> ().text = progress + "%";
		} else {
			go.transform.GetChild (1).gameObject.SetActive (false);
		}
	}

	//registers Work done by a Fish
	public bool Work (int amount){
		progress += amount;
		if (progress > 100) {
			progress = 100;
		}
		if (progress == 100) {
			return true;
		}
		return false;
	}

	//Levels the Coral Up if posible
	public bool LevelUp (){
		if (level < maxLevel) {
			level++;
			progress = 0;
			return true;
		} else {
			return false;
		}
	}

	//returns if the Coral is finished or not
	public bool finished (){
		if (progress == 100) {
			return true;
		} else {
			return false;
		}
	}

	//returns  the amount of Chalk produced per Cicle
	public int produceChalk (){
		return productionChalkPerLevel * level;
	}

	//returns  the amount of Plankton produced per Cicle
	public int producePlankton (){
		return productionPlanktonPerLevel * level;
	}

	//returns  the amount of Polution reduced per Cicle
	public int reducePolution (){
		return reductionPolutionPerLevel * level;
	}
}
