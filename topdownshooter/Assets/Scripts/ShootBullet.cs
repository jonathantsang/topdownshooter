using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour {

	public GameObject bullet;

	private float firerate;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		shootproj ();
		firerate += Time.deltaTime;
	}

	void shootproj(){
		if(Input.GetKey("space") && firerate > 0.5){
			GameObject newbullet = Instantiate (bullet, gameObject.transform.position, Quaternion.identity) as GameObject;
			// Ignore the collision of bullet and player
			Physics2D.IgnoreCollision(newbullet.GetComponent<CircleCollider2D>(), gameObject.GetComponent<BoxCollider2D>());
			firerate = 0;
		}
	}
}
