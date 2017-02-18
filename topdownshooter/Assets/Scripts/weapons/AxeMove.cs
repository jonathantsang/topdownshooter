using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeMove : MonoBehaviour {

	private Vector2 mousepos;
	private Vector2 currentpos;
	private Vector2 unitv;
	private Vector2 difference;
	private float starttime;
	private bool flipped;


	// In Collision with Enemy
	private int enemyHealth;

	// Player
	private GameObject player;
	private Vector2 playerpos;


	// Use this for initialization
	void Start () {

		// Find player for boomerang
		player = GameObject.FindGameObjectWithTag("player");
		playerpos = player.transform.position;

		// Calculate the time
		starttime = Time.time;
		// Calculate position to move to
		currentpos = gameObject.transform.position;
		// Convert mouse position
		mousepos = Input.mousePosition;
		mousepos = Camera.main.ScreenToWorldPoint (mousepos);
		// Calculate the unit vector
		difference = mousepos - currentpos;
		unitv = calcunitv ();

		flipped = false;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "enemy") {
			//Destroy (gameObject);
			Debug.Log("Hit Enemy");
		}  else if (col.gameObject.tag == "player") {
			if (flipped == true) {
				Debug.Log ("Boomerang came back");
				Destroy (gameObject);
			}
		}
		Debug.Log ("none");
	}


	// Update is called once per frame
	void Update () {

		// Move by the unitv (when not returning)
		transform.Translate (unitv);

		// Reverse at 5 seconds
		if (Time.time - starttime > 1 && flipped == false) {
			// Set forward unitv to (0,0)
			unitv = new Vector2 (0,0);
			homingBack ();
			flipped = true;
			// Reset the ignore collision
			Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), gameObject.GetComponent<CircleCollider2D>(), false);
		// If it is flipped, home backwards to player
		} else if (flipped == true) {
			homingBack ();
		// Destroy after 8
		} else if (Time.time - starttime > 8) {
			Destroy (gameObject);
		}
	}

	Vector2 calcunitv(){
		//difference = Camera.main.ScreenToWorldPoint(difference);
		float distance = Mathf.Sqrt (Mathf.Pow(difference.x, 2) + 
			Mathf.Pow(difference.y, 2));
		// Calculates a unit vector movement
		return (difference / distance) / 12;

	}
		
	void homingBack(){
		// Boomerang effect, recalculate the unitv
		playerpos = player.transform.position;
		// Move towards the player
		transform.position = Vector2.MoveTowards (transform.position, playerpos, 0.2f);

	}
}
