using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecourceController : MonoBehaviour {

	private int chalk, plankton, waterPolution, timer, chalkCoralNr, seeweedNr, filterCoralNr;
	public int defaultRecources, buildingRecources, cleanWaterPerBuilding;



	// Use this for initialization
	void Start () {
		timer = 0;
		chalkCoralNr = 0;
		seeweedNr = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer++;
		if (timer > 60) {
			timer = 0;
			chalk += defaultRecources + (buildingRecources * chalkCoralNr);
			plankton += defaultRecources + (buildingRecources * seeweedNr);
			waterPolution -= (cleanWaterPerBuilding * filterCoralNr);
			if (waterPolution < 0)
				waterPolution = 0;
			Debug.Log (Time.time);
		}
	}
	void IncreaseWaterPolution(int amount){
		waterPolution += amount;
		if (waterPolution > 100) {
			//TODO Game over
		}
	}

	void ChalkCoralDestroyed(){
		chalkCoralNr--;
		if (chalkCoralNr < 0)
			chalkCoralNr = 0;
	}
	void SeeweedDestroyed(){
		seeweedNr--;
		if (seeweedNr < 0)
			seeweedNr = 0;
	}
	void FilterCoralDestroyed(){
		filterCoralNr--;
		if (filterCoralNr < 0)
			filterCoralNr = 0;
	}
	void ChalkCoralBuild(){
		chalkCoralNr++;
	}
	void FilterCoralBuild(){
		filterCoralNr++;

	}
	void SeeweedBuild(){
		seeweedNr++;
	}
}
