using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mysteryBoxController : MonoBehaviour {

	private int costOfBox;

	// GC
	private GameObject GCobj;
	private GameController GC;

	// WC
	private GameObject WCobj;
	private WepController WC;

	//Weapon Menu
	private GameObject wep1;
	private GameObject wep2;
	private GameObject wep3;
	private GameObject wep4;
	private GameObject wep5;
	private GameObject wep6;

	// Unlockable

	// Box timer
	private float boxtime;
	private float timeElapsed;

	// box sound
	private AudioSource aud;

	// Use this for initialization
	void Start () {
		GCobj = GameObject.FindGameObjectWithTag ("GM");
		GC = GCobj.GetComponent<GameController> ();

		WCobj = GameObject.FindGameObjectWithTag ("WC");
		WC = WCobj.GetComponent<WepController> ();

		// Find each weapon to appear in mystery box
		wep1 = GameObject.FindGameObjectWithTag ("keep1");
		wep2 = GameObject.FindGameObjectWithTag ("keep2");
		wep3 = GameObject.FindGameObjectWithTag ("keep3");
		wep4 = GameObject.FindGameObjectWithTag ("keep4");
		wep5 = GameObject.FindGameObjectWithTag ("keep5");
		wep6 = GameObject.FindGameObjectWithTag ("keep6");

		// Mystery Box sound
		aud = GetComponent<AudioSource>();

		boxtime = 14f;
		timeElapsed = 14f;

		costOfBox = 950;
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;
	}

	// Need to fix the object being instantiated since it is originally not active like the weapon menu
	void OnCollisionEnter2D(Collision2D col){
		if (GC.Points > costOfBox && col.gameObject.tag == "player" && timeElapsed > boxtime) {
			aud.Play();
			Debug.Log ("box rolled");
			GC.Points -= costOfBox;
			int randomNum = ((int) Time.time + Random.Range (1, 5)) % 4 + 2;
			GameObject rolled = findWeapon (randomNum);
			// Find position to instantiate
			Vector2 posn = gameObject.transform.position;
			posn.y -= 1;
			GameObject newrolled = Instantiate (rolled, posn, Quaternion.identity) as GameObject;
			Destroy (newrolled, 12);

			// Make the item available
			WC.unlocked[randomNum] = 1;
			string name = "wep" + randomNum;
			WC.unlockWep (name);

			timeElapsed = 0;
		}
	}

	// Hardcoded finding weapons for now
	GameObject findWeapon(int randomNum){
		return GameObject.FindGameObjectWithTag ("keep" + randomNum);
	}

}
