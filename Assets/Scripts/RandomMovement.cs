using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour {

	NavMeshAgent navMeshAgent;
	NavMeshPath path;
	public float timeForNewPath;
	bool inCoRoutine;
	bool validPath;
	Vector3 target;



	// Use this for initialization
	void Start () {
		navMeshAgent = GetComponent<NavMeshAgent>();
		path = new NavMeshPath();
	}

	// Update is called once per frame
	void Update () {
		if (!inCoRoutine)
			StartCoroutine (DoSomething ());
	}

	Vector3 getNewRandomPosition () {
		float x = Random.Range (-5, 5);
		float z = Random.Range (-5, 5);

		Vector3 pos = new Vector3(x, 0, z);
		return pos;
	
	}

	IEnumerator DoSomething () {
		inCoRoutine = true;
		yield return new WaitForSeconds (timeForNewPath);
		GetNewPath();
		validPath = navMeshAgent.CalculatePath (target, path);
		if (!validPath)
			//Debug.Log ("Found an invalid Path");

		while (!validPath) {
			yield return new WaitForSeconds (0.01f);
			GetNewPath ();
			validPath = navMeshAgent.CalculatePath (target, path);
		}
		inCoRoutine = false;

	}

	void GetNewPath() {
		target = getNewRandomPosition ();
		navMeshAgent.SetDestination (target);
	}
	

}
