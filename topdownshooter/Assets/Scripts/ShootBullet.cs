using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour {

	public GameObject bullet;

	private float firerate;

	private double timetowait;

	// Use this for initialization
	void Start () {
		timetowait = 0.7;
	}
	
	// Update is called once per frame
	void Update () {
		shootproj ();
		checkUpgrade ();
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
			GameObject newbullet = Instantiate (bullet, gameObject.transform.position, Quaternion.identity) as GameObject;
			// Ignore the collision of bullet and player
			Physics2D.IgnoreCollision(newbullet.GetComponent<CircleCollider2D>(), gameObject.GetComponent<BoxCollider2D>());
			firerate = 0;
		}
	}
}
