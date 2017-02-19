using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour {

	private GameObject player;

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
				SceneManager.LoadScene ("GameOver");
				Destroy (player);
				GC.cleanslate ();
			} else {
				// Lose a life
				GC.lives -= 1;
				// Restart the round
				GC.restartRound ();
			}
		}
		// Check if it touches a bullet
		if (col.gameObject.tag == "bullet") {
			myHealth -= 50;
			GC.Points += 35;
		} else if (col.gameObject.tag == "bulletshotgun"){
			myHealth -= 15;
			GC.Points += 15;
		} else if (col.gameObject.tag == "bulletuzi"){
			myHealth -= 5;
			GC.Points += 10;
		} else if (col.gameObject.tag == "RPG"){
			myHealth -= 100;
			GC.Points += 40;
		} else if (col.gameObject.tag == "bomb"){
			myHealth -= 80;
			GC.Points += 20;
		} else if (col.gameObject.tag == "axe"){
			myHealth -= 20;
			GC.Points += 30;
		} 
		// Check if the enemy health is 0
		if (myHealth <= 0) {
			GC.ZombiesDestroyed += 1;
			GC.Points += 100;
			Destroy (gameObject);
		}

	}


	// Update is called once per frame
	void Update () {
		float step = 1 * Time.deltaTime;
		// If player is not null, go towards the player
		if (player != null) {

			player = GameObject.FindGameObjectWithTag ("player");

			// Calculates a vector3 that the enemy moves to that is towards the player
			Vector3 newplace = Vector3.MoveTowards (transform.position, player.transform.position, step);
			// Set the current enemy position to the vector3
			transform.position = newplace;



//			// Player location
//			Vector3 playerLocation = player.transform.position;
//
//			// This is for debugging and draws a ray to show the vector
//			Debug.DrawRay (transform.position, playerLocation);
//
//			Vector3 upAxis = new Vector3(0,0,1);
//			//set mouses z to your targets
//			playerLocation.z = transform.position.z;
//			Debug.Log (playerLocation);
//			transform.LookAt(playerLocation, upAxis);
//			//zero out all rotations except the axis I want
//			transform.eulerAngles = new Vector3(0,0,-transform.eulerAngles.z);

		}
	}
}
