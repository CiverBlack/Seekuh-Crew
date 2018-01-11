using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Fish : MonoBehaviour {

	EnumScript.FishType type;
	bool working = false;
	Field target;

	void FixedUpdate(){
		if (!working) {
			//TODO: Fish soll sich zufällig über die Karte bewegen
		}
	}

}
