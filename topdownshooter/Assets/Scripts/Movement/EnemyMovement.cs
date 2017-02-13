using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	GameObject player;

	private GameObject GameManager;
	private GameController GC;

	// Data about enemy
	public int myHealth;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("player");

		// Find Game Manager Script
		GameManager = GameObject.FindGameObjectWithTag ("GM");
		GC = GameManager.GetComponent<GameController> ();

		// Initialize health
		myHealth = GC.waveDesign [GC.currRound].zombieHealth;
	}

	// When the enemy detects a bullet, it takes damage
	void OnCollisionEnter2D(Collision2D col) {
		// Check if the enemy collides with the player
		if (col.gameObject.tag == "player") {
			Debug.Log("live lost");
			if (GC.lives <= 1) {
				Debug.Log ("Game Over");
				// Display Game Over
			} else {
				// Lose a life
				GC.lives -= 1;
				// Restart the round
				GC.restartRound ();
				GC.spawnPlayer ();
			}
		}
		// Check if it touches a bullet
		if (col.gameObject.tag == "bullet") {
			myHealth -= 35;
		}
		// Check if the enemy health is 0
		if (myHealth <= 0) {
			GC.ZombiesDestroyed += 1;
			GC.Points += 40;
			Destroy (gameObject);
		}

	}


	// Update is called once per frame
	void Update () {
		float step = 1 * Time.deltaTime;
		// If player is not null, go towards the player
		if (player != null) {
			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, step);
		}
	}
}
