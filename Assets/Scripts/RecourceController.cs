using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RecourceController : MonoBehaviour {

	[SerializeField] private int chalk, plankton, waterPolution, timer, chalkCoralNr, seeweedNr, filterCoralNr, score, smallFishNr, bigFishNr, smallHouseNr, bigHouseNr;
	public int defaultRecources, buildingRecources, cleanWaterPerBuilding;
	public int smallToBigFishRatio = 5;
	public Text waterPolutionText, chalkText, planktonText, scoreText;
	public int smallHouseCost, levelUpCost, filterCoralCost, chalkCoralCost, seeweedCost;
	public int timerMax = 60;
	public Image dirtWater; 



	// Use this for initialization
	void Start () {
		timer = 0;
		chalkCoralNr = 0;
		seeweedNr = 0;
		score = 0;
		smallFishNr = 1;
		bigFishNr = 0;
		smallHouseNr = 1;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer++;
		if (timer > timerMax) {
			timer = 0;
			chalk += defaultRecources + (buildingRecources * chalkCoralNr*(1-(waterPolution/100)));
			plankton += defaultRecources + (buildingRecources * seeweedNr*(1-(waterPolution/100)));
			waterPolution -= (cleanWaterPerBuilding * filterCoralNr*(1-(waterPolution/100)));
			if (waterPolution < 0)
				waterPolution = 0;
			score += ((chalkCoralNr + seeweedNr + filterCoralNr + smallFishNr + (bigFishNr * smallToBigFishRatio) + smallHouseNr + (bigHouseNr * 2))*(1-(waterPolution/100)));
		//	Debug.Log (Time.time);
		}
		waterPolutionText.text = waterPolution + "%";
		chalkText.text = chalk.ToString();
		planktonText.text = plankton.ToString();
		scoreText.text = score.ToString ();
		dirtWater.color = new Color(1,1,1,((float)waterPolution)/100);
	}

	public bool IncreaseSmallFishNr(){
		if (smallFishNr + (bigFishNr * smallToBigFishRatio) >= (smallHouseNr * smallToBigFishRatio) + (bigHouseNr * smallToBigFishRatio * 2)) {
			return false;
		} else {
			smallFishNr++;
			return true;
		}
	}
	public bool IncreaseBigFishNr(){
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

	public void LooseFish (int percent){
		smallFishNr *= percent / 100;
		bigFishNr *= percent / 100;
	}

	public void KillSmallFish(){
		smallFishNr--;
		if (smallFishNr < 0)
			smallFishNr = 0;
	}

	public void KillBigFish(){
		bigFishNr--;
		if (bigFishNr < 0)
			bigFishNr = 0;
	}

	public void SmallHouseDestroyed(){
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

	public void BigHouseDestroyed(){
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

	public bool buildSmallHouse(){
		if (bigHouseNr < 1||chalk < smallHouseCost)
			return false;
		bigHouseNr--;
		smallHouseNr += 2;
		chalk -= smallHouseCost;
		return true;
	}

	public bool levelUpSmallHouse(){
		if (smallHouseNr < 1||chalk < levelUpCost)
			return false;
		smallHouseNr--;
		bigHouseNr++;
		chalk -= levelUpCost;
		return true;
	}

	public void IncreaseWaterPolution(int amount){
		waterPolution += amount;
		if (waterPolution > 100) {
			GameOver ();
		}
	}

	public void ChalkCoralDestroyed(){
		chalkCoralNr--;
		if (chalkCoralNr < 0)
			chalkCoralNr = 0;
	}
	public void SeeweedDestroyed(){
		seeweedNr--;
		if (seeweedNr < 0)
			seeweedNr = 0;
	}
	public void FilterCoralDestroyed(){
		filterCoralNr--;
		if (filterCoralNr < 0)
			filterCoralNr = 0;
	}
	public bool ChalkCoralBuild(){
		if (chalk < chalkCoralCost)
			return false;
		chalkCoralNr++;
		chalk -= chalkCoralCost;
		return true;
	}
	public bool FilterCoralBuild(){
		if (chalk < filterCoralCost)
			return false;
		filterCoralNr++;
		chalk -= filterCoralCost;
		return true;
	}
	public bool SeeweedBuild(){
		if (chalk < seeweedCost)
			return false;
		seeweedNr++;
		chalk -= seeweedCost;
		return true;
	}
	public void GameOver(){
		//TODO: Scripten
	}
}
