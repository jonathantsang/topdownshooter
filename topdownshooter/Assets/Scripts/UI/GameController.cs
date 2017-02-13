using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Controller deals with enemy spawning in different waves of enemies
public class GameController : MonoBehaviour {

	// Main Storage Data
	public int currRound;
	public int ZombiesDestroyed;
	public int Points;
	public int lives;
	public int finalLevel;
	public WaveData[] waveDesign;

	// Spawning Enemies
	private GameObject enemy;
	private GameObject[] spawnPoints;
	private int pickrandom;
	private int spawnnumber;
	private int localZombCounter;

	// Timing
	private double timeInterval;
	private float lastSpawnTime;
	private double spawnInterval;

	// Initial Spawn
	public GameObject player;

	void Start(){
		spawnPlayer ();

		lastSpawnTime = Time.time;

		// Find objects needed for first level
		spawnPoints = GameObject.FindGameObjectsWithTag ("spawn");

		// Find enemy prefab
		enemy = GameObject.FindGameObjectWithTag("enemy");

		// Initialize to start wave 0
		currRound = 0;
		ZombiesDestroyed = 0;
		lives = 3;

		// Find the number of enemies to spawn
		spawnnumber = waveDesign [currRound].amtZombies;
		spawnInterval = waveDesign [currRound].spawnInterval;

		// Counter initialized
		localZombCounter = spawnnumber;

		// Final Level prevents overflow (hard-coded since using a list not an array)
		finalLevel = 5;
	}

	void Update(){
		// Update spawner for level
		timeInterval = Time.time - lastSpawnTime;
		newRound (currRound);
		// Check for new level
		checknewlevel();
		checkdead ();

		// Escape to quit
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	// For the new level spawn the new enemies, is in Update function, so is continuously called
	public void newRound(int level){
		// For each one, find a random spawn location and instantiate the enemy
		if (localZombCounter > 0) {
			timeInterval = Time.time - lastSpawnTime;
			if (timeInterval > spawnInterval) {
				pickrandom = (int) (Time.deltaTime + Random.Range(1,200)) % spawnPoints.Length;

				// These two need to be redefined because after scene is reloaded, they are destroyed
				// Find enemy prefab
				enemy = GameObject.FindGameObjectWithTag("enemy");
				// Find objects needed for first level
				spawnPoints = GameObject.FindGameObjectsWithTag ("spawn");

				// Select the placement AFTER the redefinition of the list of spawnPoints
				GameObject placement = spawnPoints [pickrandom];

				// Check if it is null, do not instantiate
				if (enemy != null) {
					Instantiate (enemy, placement.transform.position, Quaternion.identity);
					// Delay the next spawn, and decrease counter of spawning
					lastSpawnTime = Time.time;
					localZombCounter -= 1;
					Debug.Log (localZombCounter);
				}
			}
		}
	}

	public void spawnPlayer(){
		// Spawn the player, so that way it is possible to restart levels
		Instantiate (player, transform.position, Quaternion.identity);
	}

	public void restartRound(){
		DontDestroyOnLoad (this);
		SceneManager.LoadScene ("Main");
		// Find the number of enemies to spawn
		//spawnnumber = waveDesign [currRound].amtZombies;
		//spawnInterval = waveDesign [currRound].spawnInterval;
		//localZombCounter = spawnnumber;
		ZombiesDestroyed = 0;
	}

	void checkdead(){
		if (lives <= 0) {
			Debug.Log ("Game Over");
			Application.Quit();
			// Game Ends
		}
	}

	void checknewlevel(){
		if (ZombiesDestroyed == spawnnumber) {
			Debug.Log ("Load next level");
			currRound += 1;
			// Valid level is loaded
			if (currRound < finalLevel) {
				Debug.Log ("Loaded");
				newRound (currRound);
				ZombiesDestroyed = 0;
				spawnnumber = waveDesign [currRound].amtZombies;
				spawnInterval = waveDesign [currRound].spawnInterval;
				localZombCounter = spawnnumber;
				// else revert currRound changes
			} else {
				Debug.Log ("Level load failed");
				currRound -= 1;
			}

		}
	}



}
