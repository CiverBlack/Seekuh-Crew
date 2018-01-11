using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour {

	TilesMasterClass selected = null;
	public RecourceController recourceController;
	public Button homeCoralButton, chalkCoralButton, seeweedButton, filterCoralButton, LevelUpButton, DestroyButton;
	public GameObject homeCoralLvl1, homeCoralLvl2, chalkCoral, seeweed, filterCoral, emptyTile;
	private List<TilesMasterClass> Level2Houses = new List<TilesMasterClass>();
	private List<TilesMasterClass> AllBuildings = new List<TilesMasterClass>();



	void Start(){
		homeCoralButton.interactable = false;
		chalkCoralButton.interactable = false;
		seeweedButton.interactable = false;
		filterCoralButton.interactable = false;
		LevelUpButton.interactable = false;
		DestroyButton.interactable = false;
	}

	//Update is called once per frame
	void Update () {
		checkForLeftMouseClick ();
		checkSelected ();
	}
	void checkForLeftMouseClick(){
		if(Input.GetMouseButtonDown(0)){
			Debug.Log ("Left Mouse Button clicked");
			selectionRaycast ();
		}
	}

	void selectionRaycast(){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit) && hit.collider.gameObject.GetComponent<TilesMasterClass>()!= null) {
			Debug.Log ("Hit: " + hit.collider.gameObject.name);
			if (selected != null) {
				selected.Deselect();
			}
			selected = hit.collider.gameObject.GetComponent<TilesMasterClass>();
			selected.Select ();
		}
	}

	void checkSelected(){
		if (selected != null) {
			if(selected.name.Equals("EmptyTile")){
				homeCoralButton.interactable = true;
				chalkCoralButton.interactable = true;
				seeweedButton.interactable = true;
				filterCoralButton.interactable = true;
				LevelUpButton.interactable = false;
				DestroyButton.interactable = false;
			}
			if(selected.name.Equals("ChalkCoral")){
				homeCoralButton.interactable = false;
				chalkCoralButton.interactable = false;
				seeweedButton.interactable = false;
				filterCoralButton.interactable = false;
				LevelUpButton.interactable = false;
				DestroyButton.interactable = true;
			}
			if(selected.name.Equals("FilterCoral")){
				homeCoralButton.interactable = false;
				chalkCoralButton.interactable = false;
				seeweedButton.interactable = false;
				filterCoralButton.interactable = false;
				LevelUpButton.interactable = false;
				DestroyButton.interactable = true;
			}
			if(selected.name.Equals("HomeCoralLvl1")){
				homeCoralButton.interactable = false;
				chalkCoralButton.interactable = false;
				seeweedButton.interactable = false;
				filterCoralButton.interactable = false;
				LevelUpButton.interactable = true;
				DestroyButton.interactable = true;
			}
			if(selected.name.Equals("HomeCoralLvl2")){
				homeCoralButton.interactable = false;
				chalkCoralButton.interactable = false;
				seeweedButton.interactable = false;
				filterCoralButton.interactable = false;
				LevelUpButton.interactable = false;
				DestroyButton.interactable = true;
			}
			if(selected.name.Equals("Seeweed")){
				homeCoralButton.interactable = false;
				chalkCoralButton.interactable = false;
				seeweedButton.interactable = false;
				filterCoralButton.interactable = false;
				LevelUpButton.interactable = false;
				DestroyButton.interactable = true;
			}
		}
	}

	void homeCoralButtonClicked(){
		if (recourceController.buildSmallHouse ()) {
			AllBuildings.Remove(Level2Houses [0]);
			replaceTile (Level2Houses [0], homeCoralLvl1);
			Level2Houses.RemoveAt (0);
			replaceTile (homeCoralLvl1);
			AllBuildings.Add (selected);
		}
	}

	void levelUpClicked(){
		if (recourceController.levelUpSmallHouse ()) {
			replaceTile (homeCoralLvl2);
			Level2Houses.Add (selected);
			AllBuildings.Add (selected);
		}
	}
	void filterCoralButtonClicked(){
		if (recourceController.FilterCoralBuild()) 
			replaceTile (filterCoral);
		AllBuildings.Add (selected);
	}
	void seeweedButtonClicked(){
		if (recourceController.SeeweedBuild()) 
			replaceTile (seeweed);
		AllBuildings.Add (selected);
	}
	void chalkCoralButtonClicked(){
		if (recourceController.ChalkCoralBuild()) 
			replaceTile (chalkCoral);
		AllBuildings.Add (selected);
	}
	void replaceTile(GameObject newTile){
		Vector3 position = selected.gameObject.transform.position;
		Destroy (selected.gameObject);
		GameObject mostRecentTile = (GameObject)Instantiate (newTile, position, Quaternion.Euler (0, 0, 0));
		mostRecentTile.transform.parent = this.gameObject.transform;
		mostRecentTile.name = newTile.name;
		mostRecentTile.GetComponent<Collider>().gameObject.GetComponent<TilesMasterClass> ().Select ();
		selected = mostRecentTile.GetComponent<TilesMasterClass> ();
	}
	void replaceTile(TilesMasterClass oldTile, GameObject newTile){
		Vector3 position = oldTile.gameObject.transform.position;
		Destroy (oldTile.gameObject);
		GameObject mostRecentTile = (GameObject)Instantiate (newTile, position, Quaternion.Euler (0, 0, 0));
		mostRecentTile.transform.parent = this.gameObject.transform;
		mostRecentTile.name = newTile.name;
		mostRecentTile.GetComponent<Collider>().gameObject.GetComponent<TilesMasterClass> ().Deselect ();
	} 
	void destroyButtonClicked(){
		Debug.Log ("Destroy" + selected.name);
		if (selected.name.Equals ("HomeCoralLvl1")) {
			recourceController.SmallHouseDestroyed ();
			AllBuildings.Remove (selected);
			replaceTile (emptyTile);
		}
		if (selected.name.Equals ("ChalkCoral")) {
			recourceController.ChalkCoralDestroyed ();
			AllBuildings.Remove (selected);
			replaceTile (emptyTile);
		}
		if (selected.name.Equals ("FilterCoral")) {
			recourceController.FilterCoralDestroyed ();
			AllBuildings.Remove (selected);
			replaceTile (emptyTile);
		}
		if (selected.name.Equals ("Seeweed")) {
			recourceController.SeeweedDestroyed ();
			AllBuildings.Remove (selected);
			replaceTile (emptyTile);
		}
		if (selected.name.Equals ("HomeCoralLvl2")) {
			recourceController.BigHouseDestroyed ();
			AllBuildings.Remove (selected);
			replaceTile (emptyTile);
		}
	}
	public void destroyRandom(){
		if (AllBuildings.Count != 0) {
			int random = Random.Range (0, AllBuildings.Count - 1);
			replaceTile (AllBuildings [random], emptyTile);
			AllBuildings.Remove (AllBuildings [random]);
			switch (AllBuildings [random].name) {
			case "HomeCoralLvl1":
				recourceController.SmallHouseDestroyed ();
				break;

			case "HomeCoralLvl2":
				recourceController.BigHouseDestroyed ();
				break;

			case "ChalkCoral":
				recourceController.ChalkCoralDestroyed ();
				break;

			case "FilterCoral":
				recourceController.FilterCoralDestroyed ();
				break;

			case "Seeweed":
				recourceController.SeeweedDestroyed ();
				break;

			default :
				Debug.Log ("Es scheint etwas schiefgegangen zu sein");
				break;
			}
		}
	}
}
