using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCquit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Escape to quit
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}
}
