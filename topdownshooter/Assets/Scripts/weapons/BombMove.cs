using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMove : MonoBehaviour {

	private float timer;
	private float counttime;

	// Use this for initialization
	void Start () {
		timer = 5f;
		counttime = Time.deltaTime;

	}
	
	// Update is called once per frame
	void Update () {
		counttime += Time.deltaTime;
		transform.localScale += Vector3.one * 0.01f;
		if (counttime > timer) {
			Destroy (gameObject);
		}
	}
}
