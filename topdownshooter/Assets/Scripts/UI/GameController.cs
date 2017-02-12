using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controller deals with enemy spawning in different waves of enemies
public class GameController : MonoBehaviour {

	// Main Storage Data
	public int CurrWave;
	public int ZombiesDestroyed;
	public int Points;
	public int FinalLevel;
	public WaveData[] waveDesign;

	// Spawning Enemies
	public GameObject enemy;
	private GameObject[] spawnPoints;
	private int pickrandom;
	private int spawnnumber;
	private int localZombCounter;

	// Timing
	private double timeInterval;
	private float lastSpawnTime;
	private double spawnInterval;


	void Start(){
		lastSpawnTime = Time.time;
		// Find objects needed for first level
		spawnPoints = GameObject.FindGameObjectsWithTag ("spawn");

		// Initialize to start wave 0
		CurrWave = 0;
		ZombiesDestroyed = 0;

		// Find the number of enemies to spawn
		spawnnumber = waveDesign [CurrWave].amtZombies;
		spawnInterval = waveDesign [CurrWave].spawnInterval;

		// Counter initialized
		localZombCounter = spawnnumber;

	
		// Final Level prevents overflow (hard-coded since using a list not an array)
		FinalLevel = 5;
	}

	void Update(){
		// Update spawner for level
		timeInterval = Time.time - lastSpawnTime;
		newlevel (CurrWave);
		// Check for new level
		if (ZombiesDestroyed == spawnnumber) {
			Debug.Log ("Load next level");
			CurrWave += 1;
			// Valid level is loaded
			if (CurrWave < FinalLevel) {
				Debug.Log ("Loaded");
				newlevel (CurrWave);
				ZombiesDestroyed = 0;
				spawnnumber = waveDesign [CurrWave].amtZombies;
				spawnInterval = waveDesign [CurrWave].spawnInterval;
				localZombCounter = spawnnumber;
			// Else revert CurrWave changes
			} else {
				Debug.Log ("Level load failed");
				CurrWave -= 1;
			}

		}
	}

	// For the new level spawn the new enemies
	void newlevel(int level){
		// For each one, find a random spawn location and instantiate the enemy
			if (localZombCounter > 0) {
				timeInterval = Time.time - lastSpawnTime;
				if (timeInterval > spawnInterval) {
					pickrandom = (int) (Time.deltaTime + Random.Range(1,200)) % spawnPoints.Length;
					GameObject placement = spawnPoints [pickrandom];
					Instantiate (enemy, placement.transform.position, Quaternion.identity);
					// Delay the next spawn, and decrease counter of spawning
					lastSpawnTime = Time.time;
					localZombCounter -= 1;
			}
		}
	}




}
