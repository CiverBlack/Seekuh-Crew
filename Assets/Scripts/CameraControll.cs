using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour {

	public float movementSpeed;
	public GameObject mainCamera;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Vertical") != 0) {
			transform.Translate (Vector3.forward * movementSpeed * Input.GetAxis ("Vertical"));
		}
		if (Input.GetAxis ("Horizontal") != 0) {
			transform.Translate (Vector3.right * movementSpeed * Input.GetAxis ("Horizontal"));
		}
		if (Input.GetKey (KeyCode.E)) {
			transform.Rotate(new Vector3(0,1,0) * movementSpeed);
		}
		if (Input.GetKey (KeyCode.Q)) {
			transform.Rotate(new Vector3(0,-1,0) * movementSpeed);
		}

	}
}
