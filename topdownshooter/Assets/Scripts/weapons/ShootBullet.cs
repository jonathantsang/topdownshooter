using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour {

	public GameObject bullet;
	public GameObject axe;
	public GameObject bomb;
	public GameObject rocket;
	public GameObject shotgunb;
	public GameObject uzib;

	private float firerate;
	private double timetowait;

	// Finding which weapon is selected
	private GameObject WepController;
	private WepController WC;
	private int weaponSelect;

	// Use this for initialization
	void Start () {
		timetowait = 0.7;
		// Find Wep Controller
		WepController = GameObject.FindGameObjectWithTag("WC");
		WC = WepController.GetComponent<WepController>();
		weaponSelect = WC.weaponchoice;
	}
	
	// Update is called once per frame
	void Update () {
		shootproj ();
		checkUpgrade ();
		weaponSelect = WC.weaponchoice;

		firerate += Time.deltaTime;

	}

	// Check if player is touching the upgrade station
	void checkUpgrade(){
		if (Input.GetKeyDown("u")) {
			timetowait -= 0.1;
			print ("upgraded");
		}
	}

	void shootproj(){
		if(Input.GetKey("space") && firerate > timetowait){
			checkWeapon ();
		}
	}

	void checkWeapon(){
			// Reg bullet
		if (weaponSelect == 1) {
			GameObject newthing = Instantiate (bullet, gameObject.transform.position, Quaternion.identity) as GameObject;
			// Ignore the collision of bullet and player
			Physics2D.IgnoreCollision (newthing.GetComponent<CircleCollider2D> (), gameObject.GetComponent<BoxCollider2D> ());
			firerate = 0.1f;

			// Axe
		} else if (weaponSelect == 2) {
			GameObject newthing = Instantiate (axe, gameObject.transform.position, Quaternion.identity) as GameObject;
			// Ignore the collision of bullet and player
			Physics2D.IgnoreCollision (newthing.GetComponent<CircleCollider2D> (), gameObject.GetComponent<BoxCollider2D> ());
			firerate = 0;

			// Bomb
		} else if (weaponSelect == 3) {
			GameObject newthing = Instantiate (bomb, gameObject.transform.position, Quaternion.identity) as GameObject;
			// Ignore the collision of bullet and player
			Physics2D.IgnoreCollision (newthing.GetComponent<CircleCollider2D> (), gameObject.GetComponent<BoxCollider2D> ());
			firerate = -0.5f;

			// RPG
		} else if (weaponSelect == 4) {
			// Set the rocket to face the mouse pos with "gameObject.transform.rotation"
			GameObject newthing = Instantiate (rocket, gameObject.transform.position, Quaternion.identity) as GameObject;
			// GameObject spritepart = newthing.GetComponentInChildren<GameObject> () as GameObject;
			// spritepart.transform.rotation = gameObject.transform.rotation;
			// Ignore the collision of bullet and player
			// Physics2D.IgnoreCollision (newthing.GetComponent<CircleCollider2D> (), gameObject.GetComponent<BoxCollider2D> ());
			Debug.Log(newthing);
			firerate = -1;

			// Shotgun
		} else if (weaponSelect == 5) {

			// Spread
			int spread = 5;
			for (int i = 0; i < spread; i++) {
				float spreadRandx = (Random.Range (1, 20)) % 6 + 1;
				float spreadRandy = (Random.Range (1, 20)) % 6 + 1;
				// Side bullets need rotation
				GameObject newthing1 = Instantiate (shotgunb, gameObject.transform.position, Quaternion.identity) as GameObject;
				Rigidbody2D ntrb1 = newthing1.GetComponent<Rigidbody2D> ();
				ntrb1.AddForce (new Vector2 (spreadRandx, spreadRandy));
				Physics2D.IgnoreCollision (newthing1.GetComponent<CircleCollider2D> (), gameObject.GetComponent<BoxCollider2D> ());
			}
			firerate = 0;

		} else if (weaponSelect == 6) {
			// Set the rocket to face the mouse pos with "gameObject.transform.rotation"
			GameObject newthing = Instantiate (uzib, gameObject.transform.position, Quaternion.identity) as GameObject;

			// Ignore the collision of bullet and player
			Physics2D.IgnoreCollision (newthing.GetComponent<CircleCollider2D> (), gameObject.GetComponent<BoxCollider2D> ());
			// Uzi fires faster
			firerate = 0.5f;

			// Shotgun
		} else {
			Debug.Log ("We got a problem with weapon select with" );
		}
	}






}
