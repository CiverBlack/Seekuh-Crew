using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour {

	private Vector3 startPosition;
	private bool moving;
	public int flySpeed;
	public Transform trans;
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetAxis("Vertical") != null){
			trans.Translate(Vector3.forward * flySpeed * Input.GetAxis("Vertical"));
		}
		if(Input.GetAxis("Horizontal") != null){
			trans.Translate(Vector3.forward * flySpeed * Input.GetAxis("Horizontal"));
		}
		if(Input.GetMouseButtonDown(1)){
			if (!moving) {
				startPosition = Input.mousePosition;
				moving = true;
			} else {

			}
		}
	}
}
