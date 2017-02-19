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

	// Audio
	public AudioSource[] sounds;
	public AudioSource noise1;
	public AudioSource noise2;

	// WC
	private GameObject WCobj;
	private WepController WC;


	void Start(){
		spawnPlayer ();

		lastSpawnTime = Time.time;

		// Find objects needed for first level
		spawnPoints = GameObject.FindGameObjectsWithTag ("spawn");

		// Find enemy prefab
		enemy = GameObject.FindGameObjectWithTag("enemy");

		// Initialize to start wave 0
		cleanslate();

		// Find the number of enemies to spawn
		spawnnumber = waveDesign [currRound].amtZombies;
		spawnInterval = waveDesign [currRound].spawnInterval;

		// Counter initialized
		localZombCounter = 0;

		// Final Level prevents overflow (hard-coded since using a list not an array)
		finalLevel = 5;

		// Audio
		sounds = GetComponents<AudioSource>();
		noise1 = sounds[0];
		noise2 = sounds[1];

		// WC
		WCobj = GameObject.FindGameObjectWithTag ("WC");
		WC = WCobj.GetComponent<WepController> ();

		// Play at beginning
		noise2.Play();

	}

	void Update(){
		// Update spawner for level
		timeInterval = Time.time - lastSpawnTime;
		newRound (currRound);
		// Check for new level
		checknewlevel();
		checkdead ();
		cheat ();
	}

	void cheat(){
		if(Input.GetKey("p")){
			Points += 100;
		}
	}

	// For the new level spawn the new enemies, is in Update function, so is continuously called
	public void newRound(int level){
		// For each one, find a random spawn location and instantiate the enemy
		if (localZombCounter <= spawnnumber) {
			timeInterval = Time.time - lastSpawnTime;
			if (timeInterval > spawnInterval) {
				if (spawnPoints.Length > 0) {
					Debug.Log (localZombCounter);
					pickrandom = (int)(Time.deltaTime + Random.Range (1, 200)) % spawnPoints.Length;

					// Find which enemy to instantiate
					// If the number of norm have been passed
					//if(waveDesign[currRound].amtNorm
					// Find enemy prefab
					if (localZombCounter < waveDesign [currRound].amtNorm) {
						enemy = GameObject.FindGameObjectWithTag ("enemy");
					} else {
						enemy = GameObject.FindGameObjectWithTag ("enemy2");
						Debug.Log ("fast zombie");
						Debug.Log (enemy == null);
					}
						
					// Find objects needed for first level
					spawnPoints = GameObject.FindGameObjectsWithTag ("spawn");

					// Select the placement AFTER the redefinition of the list of spawnPoints
					GameObject placement = spawnPoints [pickrandom];

					// Check if it is null, do not instantiate
					if (enemy != null) {
						Instantiate (enemy, placement.transform.position, Quaternion.identity);
						// Delay the next spawn, and decrease counter of spawning
						lastSpawnTime = Time.time;
						localZombCounter += 1;
						//Debug.Log (localZombCounter);
					}
				} else {
					Debug.Log ("spawnPoints is empty");
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
		DontDestroyOnLoad (WC);
		SceneManager.LoadScene ("Main");
		Debug.Log ("restarted");
	}

	void OnEnable()
	{
		//Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		//Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode){
		Debug.Log("Level Loaded");
		// Reload stuff after scene changes
		// Find the number of enemies to spawn
		spawnnumber = waveDesign [currRound].amtZombies;
		spawnInterval = waveDesign [currRound].spawnInterval;
		// localZombCounter is counting from 0 to spawnnumber
		localZombCounter = 0;
		ZombiesDestroyed = 0;
		// Re-find spawn points
		spawnPoints = GameObject.FindGameObjectsWithTag ("spawn");

		// Reload saved on scene load WC
		WCobj = GameObject.FindGameObjectWithTag ("WC");
		WC = WCobj.GetComponent<WepController> ();

		// Instantiate sqr
		GameObject square = GameObject.FindGameObjectWithTag("selector");
		if (square != null) {
			Instantiate (square, new Vector2 (0, 12), Quaternion.identity);
		}

		// Gotta do this since the scene start is not run since the gamecontroller is not destroyed on load
		spawnPlayer ();

	}

	void checkdead(){
		if (lives <= 0) {
			Debug.Log ("Game Over");
			SceneManager.LoadScene ("GameOver");
			// Game Ends
			cleanslate();
		}
	}

	public void cleanslate(){
		currRound = 0;
		ZombiesDestroyed = 0;
		Points = 0;
		lives = 3;
	}

	void checknewlevel(){
		if (ZombiesDestroyed == spawnnumber) {
			Debug.Log ("Load next level");
			currRound += 1;
			// Valid level is loaded
			if (currRound < finalLevel) {
				Debug.Log ("Loaded");

				// Play Zombies are coming audio clip
				noise2.Play();

				newRound (currRound);
				ZombiesDestroyed = 0;
				spawnnumber = waveDesign [currRound].amtZombies;
				spawnInterval = waveDesign [currRound].spawnInterval;
				// localZombCounter counts from 0 to spawnnumber
				localZombCounter = 0;
				// else revert currRound changes
			} else {
				Debug.Log ("Level load failed");
				currRound -= 1;
			}

		}
	}



}
