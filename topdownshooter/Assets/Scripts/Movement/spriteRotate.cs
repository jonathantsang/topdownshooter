using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Rotate perpetually
		transform.Rotate(0, 0, 10);
	}
}
