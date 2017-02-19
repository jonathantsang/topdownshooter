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
		GameObject wep1 = GameObject.FindGameObjectWithTag ("keep1");
		GameObject wep2 = GameObject.FindGameObjectWithTag ("keep2");
		GameObject wep3 = GameObject.FindGameObjectWithTag ("keep3");
		GameObject wep4 = GameObject.FindGameObjectWithTag ("keep4");
		GameObject wep5 = GameObject.FindGameObjectWithTag ("keep5");
		GameObject wep6 = GameObject.FindGameObjectWithTag ("keep6");
		Debug.Log (wep1.name + wep2.name + wep3.name + wep4.name + wep5.name + wep6.name + "s");

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
		// For wep1-6
		return GameObject.FindGameObjectWithTag ("keep" + randomNum);
	}

}
