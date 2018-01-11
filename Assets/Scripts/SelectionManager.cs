using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour {

	TilesMasterClass selected = null;
	public RecourceController recourceController;
	public Button homeCoralButton, chalkCoralButton, seeweedButton, filterCoralButton, LevelUpButton, DestroyButton;
	public GameObject homeCoralLvl1, homeCoralLvl2, chalkCoral, seeweed, filterCoral, emptyTile;

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
		if (recourceController.buildSmallHouse ())
			replaceTile (homeCoralLvl1);
	}

	void levelUpClicked(){
		if (recourceController.levelUpSmallHouse()) 
			replaceTile (homeCoralLvl2);
	}
	void filterCoralButtonClicked(){
		if (recourceController.FilterCoralBuild()) 
			replaceTile (filterCoral);
	}
	void seeweedButtonClicked(){
		if (recourceController.SeeweedBuild()) 
			replaceTile (seeweed);
	}
	void chalkCoralButtonClicked(){
		if (recourceController.ChalkCoralBuild()) 
			replaceTile (chalkCoral);
	}
	void replaceTile(GameObject newTile){
		Vector3 position = selected.gameObject.transform.position;
		Destroy (selected.gameObject);
		GameObject mostRecentTile = (GameObject)Instantiate (newTile, position, Quaternion.Euler (0, 0, 0));
		mostRecentTile.transform.parent = this.gameObject.transform;
		mostRecentTile.name = newTile.name;
		selected = mostRecentTile.GetComponent<TilesMasterClass> ();
	}
	void destroyButtonClicked(){
		Debug.Log ("Destroy" + selected.name);
		if (selected.name.Equals ("HomeCoralLvl1")) {
			recourceController.SmallHouseDestroyed ();
			replaceTile (emptyTile);
		}
		if (selected.name.Equals ("ChalkCoral")) {
			recourceController.ChalkCoralDestroyed ();
			replaceTile (emptyTile);
		}
		if (selected.name.Equals ("FilterCoral")) {
			recourceController.FilterCoralDestroyed ();
			replaceTile (emptyTile);
		}
		if (selected.name.Equals ("Seeweed")) {
			recourceController.SeeweedDestroyed ();
			replaceTile (emptyTile);
		}
		if (selected.name.Equals ("HomeCoralLvl2")) {
			recourceController.BigHouseDestroyed ();
			replaceTile (emptyTile);
		}
	}
}
