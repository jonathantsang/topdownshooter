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
		myHealth = GC.waveDesign [GC.CurrWave].zombieHealth;
	}

	// When the enemy detects a bullet, it takes damage
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "bullet") {
			myHealth -= 35;
		}
		if (myHealth <= 0) {
			GC.ZombiesDestroyed += 1;
			GC.Points += 40;
			Destroy (gameObject);
		}
	}


	// Update is called once per frame
	void Update () {
		float step = 1 * Time.deltaTime;
		transform.position = Vector2.MoveTowards (transform.position, player.transform.position, step);
	}
}
