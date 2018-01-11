using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RecourceController : MonoBehaviour {

	private int chalk, plankton, waterPolution, timer, chalkCoralNr, seeweedNr, filterCoralNr, score, smallFishNr, bigFishNr, smallHouseNr, bigHouseNr;
	public int defaultRecources, buildingRecources, cleanWaterPerBuilding;
	public int smallToBigFishRatio = 5;
	public Text waterPolutionText, chalkText, planktonText, scoreText;



	// Use this for initialization
	void Start () {
		timer = 0;
		chalkCoralNr = 0;
		seeweedNr = 0;
		score = 0;
		smallFishNr = 1;
		bigFishNr = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer++;
		if (timer > 60) {
			timer = 0;
			chalk += defaultRecources + (buildingRecources * chalkCoralNr*(1-(waterPolution/100)));
			plankton += defaultRecources + (buildingRecources * seeweedNr*(1-(waterPolution/100)));
			waterPolution -= (cleanWaterPerBuilding * filterCoralNr*(1-(waterPolution/100)));
			if (waterPolution < 0)
				waterPolution = 0;
			score += ((chalkCoralNr + seeweedNr + filterCoralNr + smallFishNr + (bigFishNr * smallToBigFishRatio) + smallHouseNr + (bigHouseNr * 2))*(1-(waterPolution/100)));
			Debug.Log (Time.time);
		}
		waterPolutionText.text = waterPolution + "%";
		chalkText.text = chalk.ToString();
		planktonText.text = plankton.ToString();
		scoreText.text = score.ToString ();
	}

	bool IncreaseSmallFishNr(){
		if (smallFishNr + (bigFishNr * smallToBigFishRatio) >= (smallHouseNr * smallToBigFishRatio) + (bigHouseNr * smallToBigFishRatio * 2)) {
			return false;
		} else {
			smallFishNr++;
			return true;
		}
	}
	bool IncreaseBigFishNr(){
		if (smallFishNr + (bigFishNr * smallToBigFishRatio)+ smallToBigFishRatio > (smallHouseNr * smallToBigFishRatio) + (bigHouseNr * smallToBigFishRatio * 2)) {
			return false;
		} else {
			if (smallFishNr >= smallToBigFishRatio) {
				smallFishNr -= smallToBigFishRatio;
				bigFishNr++;
				return true;
			} else {
				return false;
			}
		}
	}

	void LooseFish (int percent){
		smallFishNr *= percent / 100;
		bigFishNr *= percent / 100;
	}

	void KillSmallFish(){
		smallFishNr--;
		if (smallFishNr < 0)
			smallFishNr = 0;
	}

	void KillBigFish(){
		bigFishNr--;
		if (bigFishNr < 0)
			bigFishNr = 0;
	}

	void SmallHouseDestroyed(){
		smallHouseNr--;
		if (smallHouseNr < 0)
			smallHouseNr = 0;
		while (smallFishNr + (bigFishNr * smallToBigFishRatio) >= (smallHouseNr * smallToBigFishRatio) + (bigHouseNr * smallToBigFishRatio * 2) && smallFishNr > 0) {
			KillSmallFish ();
		}
		while (smallFishNr + (bigFishNr * smallToBigFishRatio) >= (smallHouseNr * smallToBigFishRatio) + (bigHouseNr * smallToBigFishRatio * 2) && bigFishNr > 0) {
			KillBigFish ();
		}
	}

	void BigHouseDestroyed(){
		bigHouseNr--;
		if (bigHouseNr < 0)
			bigHouseNr = 0;
		while (smallFishNr + (bigFishNr * smallToBigFishRatio) >= (smallHouseNr * smallToBigFishRatio) + (bigHouseNr * smallToBigFishRatio * 2) && smallFishNr > 0) {
			KillSmallFish ();
		}
		while (smallFishNr + (bigFishNr * smallToBigFishRatio) >= (smallHouseNr * smallToBigFishRatio) + (bigHouseNr * smallToBigFishRatio * 2) && bigFishNr > 0) {
			KillBigFish ();
		}
	}

	bool buildSmallHouse(){
		if (bigHouseNr < 1)
			return false;
		bigHouseNr--;
		smallHouseNr += 2;
		return true;
	}

	bool levelUpSmallHouse(){
		if (smallHouseNr < 1)
			return false;
		smallHouseNr--;
		bigHouseNr++;
		return true;
	}

	void IncreaseWaterPolution(int amount){
		waterPolution += amount;
		if (waterPolution > 100) {
			GameOver ();
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
	void GameOver(){
		//TODO: SCripten
	}
}
