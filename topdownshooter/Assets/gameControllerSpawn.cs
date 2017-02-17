using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameControllerSpawn : MonoBehaviour {

	public GameObject GMtoInstantiate;

	// Use this for initialization
	void Start () {
		GameObject GM = GameObject.FindGameObjectWithTag ("GM");
		if (GM == null) {
			Debug.Log ("GM is null");
			Instantiate (GMtoInstantiate, transform.position, Quaternion.identity);
		} else {
			Debug.Log ("GM is not null");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
