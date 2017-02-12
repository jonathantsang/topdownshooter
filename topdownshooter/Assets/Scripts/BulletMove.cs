using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {

	private Vector3 mousepos;
	private Vector3 currentpos;
	private Vector3 unitv;
	private Vector3 difference;
	private float starttime;


	// In Collision with Enemy
	private int enemyHealth;


	// Use this for initialization
	void Start () {
		// Calculate the time
		starttime = Time.time;
		// Calculate position to move to
		currentpos = gameObject.transform.position;
		// Convert mouse position
		mousepos = Input.mousePosition;
		mousepos = Camera.main.ScreenToWorldPoint (mousepos);
		// Calculate the unit vector
		difference = mousepos - currentpos;
		unitv = moveunitv ();
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "enemy") {
			//Destroy (gameObject);
			Debug.Log("Hit Enemy");
		} else if (col.gameObject.tag == "wallr") {
			Destroy (gameObject);
		}
	}



	// Update is called once per frame
	void Update () {
		transform.Translate (unitv);
		if(Time.time - starttime > 10){
			Destroy(gameObject);
		}
	}

	Vector2 moveunitv(){
		//difference = Camera.main.ScreenToWorldPoint(difference);
		float distance = Mathf.Sqrt (Mathf.Pow(difference.x, 2) + 
			Mathf.Pow(difference.y, 2));
		// Calculates a unit vector movement
		return (difference / distance) / 7;

	}
}
