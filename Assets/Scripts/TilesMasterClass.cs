using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesMasterClass : MonoBehaviour {

	public int prozess = 0;
	public List<FishBehavior> workers;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (prozess >= 100) {
			for (int i = 0; i < workers.Count; i++) {
				workers [i].SetFree ();
			}
			workers.Clear ();
		}
	}

	public void Select(){
		this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
	}

	public void Deselect(){
		this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
	}
		
}
