using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour {

	public GameObject bullet;
	public GameObject axe;

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
		if(Input.GetKeyDown("space") && firerate > timetowait){
			checkWeapon ();
		}
	}

	void checkWeapon(){
		// Reg bullet
		if (weaponSelect == 1) {
			GameObject newthing = Instantiate (bullet, gameObject.transform.position, Quaternion.identity) as GameObject;
			// Ignore the collision of bullet and player
			Physics2D.IgnoreCollision (newthing.GetComponent<CircleCollider2D> (), gameObject.GetComponent<BoxCollider2D> ());
			firerate = 0;

			// Axe
		} else if (weaponSelect == 2) {
			GameObject newthing = Instantiate (axe, gameObject.transform.position, Quaternion.identity) as GameObject;
			// Ignore the collision of bullet and player
			Physics2D.IgnoreCollision (newthing.GetComponent<CircleCollider2D> (), gameObject.GetComponent<BoxCollider2D> ());
			firerate = 0;
		} else {
			Debug.Log ("We got a problem with weapon select");
		}
	}






}
