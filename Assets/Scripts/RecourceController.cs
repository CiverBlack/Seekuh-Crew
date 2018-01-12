using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RecourceController : MonoBehaviour {

	public int chalk, plankton, waterPolution, timer, chalkCoralNr, seeweedNr, filterCoralNr, score, smallFishNr, bigFishNr, smallHouseNr, bigHouseNr;
	public int defaultRecources, buildingRecources, cleanWaterPerBuilding;
	public int smallToBigFishRatio = 5;
	public Text waterPolutionText, chalkText, planktonText, scoreText;
	public int smallHouseCost, levelUpCost, filterCoralCost, chalkCoralCost, seeweedCost, smallFishCost;
	public int timerMax = 60;
	public Image dirtWater; 
	private bool running = true;
	public GameObject panelGameOver;
	public Text textGameOver;
	public Desaster desaster;
	public GameObject smallFish, bigFish;
	List<GameObject> allSmallFish = new List<GameObject> ();
	List<GameObject> allBigFish = new List<GameObject> ();
	private int tankerNr = 1;
	public SelectionManager manager;

	// Use this for initialization
	void Start () {
		timer = 0;
		chalkCoralNr = 0;
		seeweedNr = 0;
		score = 0;
		smallFishNr = 1;
		bigFishNr = 0;
		smallHouseNr = 1;
		allSmallFish.Add (Instantiate (smallFish));
	}

	public void Stop(){
		running = false;
	}
	public void Restart(){
		running = true;
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Debug.Log ("Kleine Fische: " + smallFishNr);
		//Debug.Log ("Große Fische: " + bigFishNr);

		if (running){
			timer++;
			if (timer > timerMax) {
				timer = 0;
				waterPolution += tankerNr*cleanWaterPerBuilding;
				chalk += defaultRecources + (buildingRecources * chalkCoralNr*(1-(waterPolution/100)));
				plankton += (defaultRecources + (buildingRecources * seeweedNr*(1-(waterPolution/100))));
				waterPolution -= (cleanWaterPerBuilding * filterCoralNr*(1-(waterPolution/100)));
				if (waterPolution < 0)
					waterPolution = 0;
				if (waterPolution > 100)
					waterPolution = 100;
				score += ((chalkCoralNr + seeweedNr + filterCoralNr + smallFishNr + (bigFishNr * smallToBigFishRatio) + smallHouseNr + (bigHouseNr * 2))*(1-(waterPolution/100)));
			}
			waterPolutionText.text = waterPolution + "%";
			chalkText.text = chalk.ToString();
			planktonText.text = plankton.ToString();
			scoreText.text = score.ToString ();
			dirtWater.color = new Color(1,1,1,((float)waterPolution)/100);
			if (waterPolution >= 100 || bigHouseNr+smallHouseNr <= 0) {
				GameOver ();
			}
		}
	}

	public bool IncreaseSmallFishNr(){
		if (plankton < smallFishCost || smallFishNr + (bigFishNr * smallToBigFishRatio) >= (smallHouseNr * smallToBigFishRatio) + (bigHouseNr * smallToBigFishRatio * 2)) {
			return false;
		} else {
			smallFishNr++;
			plankton -= smallFishCost;
			allSmallFish.Add (Instantiate (smallFish));
			return true;
		}
	}
	public bool IncreaseBigFishNr(){
		if (smallFishNr + (bigFishNr * smallToBigFishRatio)+ smallToBigFishRatio > (smallHouseNr * smallToBigFishRatio) + (bigHouseNr * smallToBigFishRatio * 2)) {
			return false;
		} else {
			if (smallFishNr >= smallToBigFishRatio) {
				smallFishNr -= smallToBigFishRatio;
				allBigFish.Add (Instantiate (bigFish));
				List<GameObject> temp = new List<GameObject> (allSmallFish.GetRange(0,smallToBigFishRatio));
				allSmallFish.RemoveRange (0,smallToBigFishRatio);
				for (int i = temp.Count-1; i >=0; i--) {
					Destroy (temp [i]);
				}
				bigFishNr++;
				return true;
			} else {
				return false;
			}
		}
	}

	public void LooseFish (int percent){
		double percentDouble = (double)percent / 100.0;
		int killSmallFish = (int)(smallFishNr * percentDouble);
		for (int i = 0; i < killSmallFish; i++) {
			KillSmallFish ();
		}
		int killBigFish = (int)(bigFishNr * percentDouble);
		for (int i = 0; i < killBigFish; i++) {
			KillBigFish ();
		}
	}

	public void KillSmallFish(){
		if (smallFishNr > 0) {
			smallFishNr--;
			GameObject temp = allSmallFish [0];
			allSmallFish.RemoveAt (0);
			Destroy (temp);
		}
	}

	public void KillBigFish(){
		if (bigFishNr > 0) {
			bigFishNr--;
			GameObject temp = allBigFish [0];
			allBigFish.RemoveAt (0);
			Destroy (temp);
		}
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
		chalk -= levelUpCost;
		return true;
	}

	public void IncreaseWaterPolution(int amount){
		waterPolution += amount;
		if (waterPolution > 100)
			waterPolution = 100;
		tankerNr++;
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
		chalk -= chalkCoralCost;
		return true;
	}
	public bool FilterCoralBuild(){
		if (chalk < filterCoralCost)
			return false;
		chalk -= filterCoralCost;
		return true;
	}
	public bool SeeweedBuild(){
		if (chalk < seeweedCost)
			return false;
		chalk -= seeweedCost;
		return true;
	}
	public void LevelUpFinished(){
		smallHouseNr--;
		bigHouseNr++;
	}
	public void ChalkCoralFinished(){
		chalkCoralNr++;
	}
	public void FilterCoralFinished(){
		filterCoralNr++;
	}
	public void SeeweedFinished(){
		seeweedNr++;
	}
	public void GameOver(){
		manager.destroyAll ();
		panelGameOver.SetActive (true);
		textGameOver.text = "Das Korallenriff ist leider der Zerstörungswut der Menschen erlegen. Du hast " + score + " Punkte erreicht.";
		desaster.PauseApplication ();
	}
}
