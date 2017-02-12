using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour {

	// Used for Finding GameController
	private GameController GC;
	private GameObject GameManager;

	public Text WaveText;
	public Text ZombiesRemainText;
	public Text PointsText;

	// Use this for initialization
	void Start () {
		// Find Game Controller
		GameManager = GameObject.FindGameObjectWithTag("GM");
		GC = GameManager.GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateText ();
	}

	void UpdateText(){
		// Update wave text
		WaveText.text = "Wave: " + GC.CurrWave;
		// Update points text
		PointsText.text = "Points: " + GC.Points;
		// Update the number of zombies remaining
		int remaining = GC.waveDesign [GC.CurrWave].amtZombies - GC.ZombiesDestroyed;
		// Check if one zombie remains
		if (remaining == 1) {
			ZombiesRemainText.text = remaining + " Zombie Remains";
		} else {
			ZombiesRemainText.text = remaining  + " Zombies Remain";
		}

	}
}
