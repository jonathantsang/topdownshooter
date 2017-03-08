using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WepController : MonoBehaviour {

	// index starts at 0, but 1-6 are the available weapons
	public int weaponchoice;

	public int[] unlocked;

	// Weapons
	private int numBlaster = 1;
	private int numAxe = 2;
	private int numBomb = 3;
	private int numRPG = 4;
	private int numShotgun = 5;
	private int numUzi = 6;

	//Weapon Menu
	private GameObject wep1;
	private GameObject wep2;
	private GameObject wep3;
	private GameObject wep4;
	private GameObject wep5;
	private GameObject wep6;

	// Black square placement
	private GameObject squareSelector;
	private Vector2 defaultplacement;

	//Mystery Box for the initialization of weapon select (which can change on scene reload)
	private GameObject mbcObj;
	private mysteryBoxController mbc;

	// GC
	private GameObject GCobj;
	private GameController GC;

	private float width = 1.3f;

	// Use this for initialization
	void Start () {
		// Default is the blaster
		weaponchoice = 1;
		squareSelector = GameObject.FindGameObjectWithTag ("selector");
		defaultplacement = squareSelector.transform.position;

		loadWep ();

		resetWepMenu ();

		GCobj = GameObject.FindGameObjectWithTag ("GM");
		GC = GCobj.GetComponent<GameController> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (validWep()) {
			changeWeapon ();
			setWepMenu ();
		}
	}

	// Reset after scene load
	void resetWepMenu(){
		Debug.Log ("reset weps array");
		// Initialize the weapon unlocked array to 0, length 7
		unlocked = new int[7];
		unlocked[1] = 1;
		for (int i = 2; i < 7; ++i) {
			unlocked [i] = 0;
		}
	}


	// Find each weapon to appear in the weapon menu
	void loadWep(){
		wep1 = GameObject.FindGameObjectWithTag ("1");
		wep2 = GameObject.FindGameObjectWithTag ("2");
		wep3 = GameObject.FindGameObjectWithTag ("3");
		wep4 = GameObject.FindGameObjectWithTag ("4");
		wep5 = GameObject.FindGameObjectWithTag ("5");
		wep6 = GameObject.FindGameObjectWithTag ("6");
	}

	// Checks if wep4 is loaded, everything else should be as well
	bool validWep(){
		if (wep4 == null) {
			return false;
		} else {
			return true;
		}	
	}

	// The problem is that elements that are not active cannot be found to make them active again
	// Fix this later
	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode){

		// Load GC
		GCobj = GameObject.FindGameObjectWithTag ("GM");
		GC = GCobj.GetComponent<GameController> ();


		squareSelector = GameObject.FindGameObjectWithTag ("selector");
		Debug.Log (scene.name + " on reloading");
		// Meaning fresh new game with 3 lives, hardcoded
		if (scene.name == "Main" && GameController.lives == 3) {
			resetWepMenu ();
		}
		// Reset back to the main weapon
		weaponchoice = 1;
		loadWep ();
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

	// Check if weapon is unlocked
	bool checkUnlocked(int number){
		if (unlocked [number] == 1) {
			return true;
		}
		return false;
	}

	void setWepMenu(){
		for (int i = 2; i < 7; ++i) {
			// If it is unlocked, set active, otherwise set not active
			if (checkUnlocked (i)) {
				returnWep (i).SetActive (true);
			} else {
				returnWep (i).SetActive(false);
			}
		}
	}

	GameObject returnWep(int number){
		if (number == 1) {
			return wep1;
		} else if (number == 2) {
			return wep2;
		} else if (number == 3) {
			return wep3;
		} else if (number == 4) {
			return wep4;
		} else if (number == 5) {
			return wep5;
		} else if (number == 6) {
			return wep6;
		} else {
			Debug.Log ("error in returning wep");
			return wep1;
		}
	}

	public void unlockWep(string name){
		if (name == "wep1") {
			wep1.SetActive(true);
		} else if (name == "wep2") {
			wep2.SetActive(true);
		} else if (name == "wep3") {
			wep3.SetActive(true);
		} else if (name == "wep4") {
			wep4.SetActive(true);
		} else if (name == "wep5") {
			wep5.SetActive(true);
		} else if (name == "wep6") {
			wep6.SetActive(true);
		} else {
			Debug.Log("problem in unlocking weapon");
		}
	} 

	// Changing the equipped weapon
	void changeWeapon(){
		// 1 blaster
		if (Input.GetKey(numBlaster.ToString()) && checkUnlocked(numBlaster)){
			Debug.Log("blaster");
			squareSelector.transform.position = defaultplacement;
			weaponchoice = numBlaster;
		// Axe
		} else if (Input.GetKey(numAxe.ToString()) && checkUnlocked(numAxe)) {
			Debug.Log("axe");
			// Minus one because the numbers are not index 0, where the default blaster is actually at 0 changes to default
			squareSelector.transform.position = new Vector2 (defaultplacement.x + width * (numAxe-1), defaultplacement.y);
			weaponchoice = numAxe;
		// Bomb
		} else if (Input.GetKey(numBomb.ToString()) && checkUnlocked(numBomb)) {
			Debug.Log("bomb");
			squareSelector.transform.position = new Vector2 (defaultplacement.x + width * (numBomb-1), defaultplacement.y);
			weaponchoice = numBomb;
		// RPG
		} else if (Input.GetKey(numRPG.ToString()) && checkUnlocked(numRPG)) {
			Debug.Log("RPG");
			squareSelector.transform.position = new Vector2 (defaultplacement.x + width * (numRPG-1), defaultplacement.y);
			weaponchoice = numRPG;
		// Shotgun
		} else if (Input.GetKey(numShotgun.ToString()) && checkUnlocked(numShotgun)) {
			Debug.Log("shotgun");
			squareSelector.transform.position = new Vector2 (defaultplacement.x + width * (numShotgun-1), defaultplacement.y);
			weaponchoice = numShotgun;
		// Uzi
		} else if (Input.GetKey(numUzi.ToString()) && checkUnlocked(numUzi)) {
			Debug.Log("uzi");
			squareSelector.transform.position = new Vector2 (defaultplacement.x + width * (numUzi-1), defaultplacement.y);
			weaponchoice = numUzi;
		} 
	}


}
