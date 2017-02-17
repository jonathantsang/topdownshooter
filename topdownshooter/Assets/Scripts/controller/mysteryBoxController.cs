using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mysteryBoxController : MonoBehaviour {

	private int costOfBox;

	// GC
	private GameObject GCobj;
	private GameController GC;

	// Box timer
	private float boxtime;
	private float timeElapsed;

	// box sound
	public AudioSource aud;

	// Use this for initialization
	void Start () {
		GCobj = GameObject.FindGameObjectWithTag ("GM");
		GC = GCobj.GetComponent<GameController> ();

		aud = GetComponent<AudioSource>();

		boxtime = 15f;
		timeElapsed = 15f;

		costOfBox = 950;
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (GC.Points > costOfBox && col.gameObject.tag == "player" && timeElapsed > boxtime) {
			aud.Play();
			Debug.Log ("box rolled");
			GC.Points -= costOfBox;
			int randomNum = Random.Range (1, 20) % 4 + 2;
			GameObject rolled = findWeapon (randomNum);
			// Find position to instantiate
			Vector2 posn = transform.position;
			posn.y -= 1;
			GameObject newrolled = Instantiate (rolled, posn, Quaternion.identity) as GameObject;
			Destroy (newrolled, 12);

			timeElapsed = 0;
		}
	}

	// Hardcoded finding weapons for now
	GameObject findWeapon(int randomNum){
		return GameObject.FindGameObjectWithTag (randomNum.ToString());
	}
}
