using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralHomeTile : TilesMasterClass {

	private int Level = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LevelUp(){
		if (Level == 1) {
			Level = 2;
		}
	}
	void LevelDown(){
		if (Level == 2) {
			Level = 1;
		}
	}
}
