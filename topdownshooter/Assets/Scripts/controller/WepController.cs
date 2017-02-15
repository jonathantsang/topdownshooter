using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WepController : MonoBehaviour {

	// index starts at 0, but 1-6 are the available weapons
	public int weaponchoice;

	// Weapons
	private int numBlaster = 1;
	private int numAxe = 2;
	private int numBomb = 3;
	private int numRpg = 4;
	private int numShotgun = 5;
	private int numUzi = 6;

	private GameObject squareSelector;
	private Vector2 defaultplacement;

	private float width = 1.3f;

	// Use this for initialization
	void Start () {
		// Default is the blaster
		weaponchoice = 1;
		squareSelector = GameObject.FindGameObjectWithTag ("selector");
		defaultplacement = squareSelector.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		changeWeapon ();
	}

	void changeWeapon(){
		if (Input.GetKey(numBlaster.ToString())){
			Debug.Log("blaster");
			squareSelector.transform.position = defaultplacement;
			weaponchoice = numBlaster;
		} else if (Input.GetKey(numAxe.ToString())) {
			Debug.Log("axe");
			// Minus one because the numbers are not index 0, where the default blaster is actually at 0 changes to default
			squareSelector.transform.position = new Vector2 (defaultplacement.x + width * (numAxe-1), defaultplacement.y);
			weaponchoice = numAxe;
		} else if (Input.GetKey(numBomb.ToString())) {
			Debug.Log("bomb");
			squareSelector.transform.position = new Vector2 (defaultplacement.x + width * (numBomb-1), defaultplacement.y);
			weaponchoice = numBomb;
		} else if (Input.GetKey(numRpg.ToString())) {
			Debug.Log("RPG");
			squareSelector.transform.position = new Vector2 (defaultplacement.x + width * (numRpg-1), defaultplacement.y);
			weaponchoice = numRpg;
		} else if (Input.GetKey(numShotgun.ToString())) {
			Debug.Log("shotgun");
			squareSelector.transform.position = new Vector2 (defaultplacement.x + width * (numShotgun-1), defaultplacement.y);
			weaponchoice = numShotgun;
		} else if (Input.GetKey(numUzi.ToString())) {
			Debug.Log("uzi");
			squareSelector.transform.position = new Vector2 (defaultplacement.x + width * (numUzi-1), defaultplacement.y);
			weaponchoice = numUzi;
		} 
	}
}
