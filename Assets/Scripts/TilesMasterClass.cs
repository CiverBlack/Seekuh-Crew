using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesMasterClass : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Select(){
		this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
	}

	public void Deselect(){
		this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
	}
		
}
