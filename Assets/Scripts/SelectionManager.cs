using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour {

	TilesMasterClass selected = null;

	// Update is called once per frame
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
			Debug.Log(selected.name);
		}
	}
}
