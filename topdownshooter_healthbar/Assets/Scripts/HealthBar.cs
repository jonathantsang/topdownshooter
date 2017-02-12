using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	private float maxHealth;
	private float currentHealth;
	private float originalScale;

	// Used for Finding GameController
	private GameController GC;
	private GameObject GameManager;

	// Use this for initialization
	void Start () {

		// Use GameController Data
		// Find Game Controller
		GameManager = GameObject.FindGameObjectWithTag("GM");
		GC = GameManager.GetComponent<GameController>();

		maxHealth = GC.waveDesign [GC.CurrWave].zombieHealth;
		currentHealth = GC.waveDesign [GC.CurrWave].zombieHealth;

		originalScale = gameObject.transform.localScale.x;	
	}

	// Update is called once per frame
	void Update () {
		currentHealth = 
		Vector3 tmpScale = gameObject.transform.localScale;
		tmpScale.x = currentHealth / maxHealth * originalScale;
		gameObject.transform.localScale = tmpScale;	
	}
}
