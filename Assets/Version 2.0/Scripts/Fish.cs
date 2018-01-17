using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

	public Coral building;
	public int workforce, space, progress, costPlankton, costFish;

	
	void FixedUpdate () {
		//TODO: Bewegt sich zufällig oder auf building zu
	}

	//Aply the Fishs workforce to the Coral it is curently building
	public void Work (){
		if (building != null) {
			building.Work (workforce);
		}
	}
}