using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour {

	private TilesMasterClass[,] gridOfTiles;
	public GameObject PrefabTile, HomeTile; 
	public int size;
	private int halfSize;

	// Use this for initialization
	void Start () {
		gridOfTiles = new TilesMasterClass[size, size];
		halfSize = size / 2;
		GenerateGrid ();
	}

	// Update is called once per frame
	void Update () {

	}

	void GenerateGrid(){
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				Vector3 posToCreateTile = new Vector3 (i-halfSize, 0.5f, j-halfSize);
				GameObject mostRecentTile = (GameObject)Instantiate (PrefabTile, posToCreateTile, Quaternion.Euler (0, 0, 0));
				mostRecentTile.transform.parent = this.gameObject.transform;
				mostRecentTile.name = PrefabTile.name;
				gridOfTiles [i, j] = mostRecentTile.GetComponent<TilesMasterClass> ();
			}
		}
		Vector3 position = gridOfTiles[halfSize,halfSize].gameObject.transform.position;
		Destroy(gridOfTiles[halfSize,halfSize].gameObject);
		GameObject newHomeTile = (GameObject)Instantiate (HomeTile, position, Quaternion.Euler (0, 0, 0));
		newHomeTile.transform.parent = this.gameObject.transform;
		newHomeTile.name = HomeTile.name;
		gridOfTiles[halfSize,halfSize] = newHomeTile.GetComponent<TilesMasterClass>();
	}
}
