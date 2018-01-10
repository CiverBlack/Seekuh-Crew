using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour {

	private TilesMasterClass[,] gridOfTiles;
	public GameObject PrefabTile; 
	public int size;

	// Use this for initialization
	void Start () {
		gridOfTiles = new TilesMasterClass[size, size];
		GenerateGrid ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GenerateGrid(){
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				Vector3 posToCreateTile = new Vector3 (i, 0, j);
				GameObject mostRecentTile = (GameObject)Instantiate (PrefabTile, posToCreateTile, Quaternion.Euler (0, 0, 0));
				mostRecentTile.transform.parent = this.gameObject.transform;
				mostRecentTile.name = "Tile(" + i + "/" + j + ")";
				gridOfTiles [i, j] = mostRecentTile.GetComponent<TilesMasterClass> ();
			}
		}
	}
}
