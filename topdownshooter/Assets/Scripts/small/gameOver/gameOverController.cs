using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverController: MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag ("player");
		Destroy (player);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("r")){
			SceneManager.LoadScene ("Main");
		}
	}
}
