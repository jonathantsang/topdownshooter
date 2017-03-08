using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour {

	// Used for Finding GameController
	private GameController GC;
	private GameObject GameControllerObject;

	[Header("UI Elements")]
	public Text roundText;
	public Text zombiesRemainText;
	public Text pointsText;
	public Text livesText;

	// Use this for initialization
	void Start () {
		// Find Game Manager Script
		GameControllerObject = GameObject.FindGameObjectWithTag ("GM");
		GC = GameControllerObject.GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateText ();
	}

	void UpdateText(){
		// Update wave text
		roundText.text = "Round: " + GameController.currRound;
		// Update points text
		pointsText.text = "Points: " + GameController.Points;
		// Update the number of zombies remaining
		int remaining = GC.waveDesign [GameController.currRound].amtZombies - GameController.ZombiesDestroyed;
		// Check if one zombie remains
		if (remaining == 1) {
			zombiesRemainText.text = remaining + " Zombie Remains";
		} else {
			zombiesRemainText.text = remaining  + " Zombies Remain";
		}
		livesText.text = "Lives: " + GameController.lives;

	}
}
