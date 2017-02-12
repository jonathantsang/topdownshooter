using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private float speed = 3.2f;
	private Rigidbody2D rb;

	void Start(){
		rb = GetComponent<Rigidbody2D>();
	}


	void Update ()
	{
		movement ();
		facingturn ();
	}

	void OnCollisionEnter2D(Collision2D col) {
		print ("Touched");
	}

	void OnCollisionExit2D(Collision2D col) {
		print ("Exit");
	}

	void movement(){
		if (Input.GetKey("a"))
		{
			rb.MovePosition(rb.position += Vector2.left * speed * Time.deltaTime);
		}
		if (Input.GetKey("d"))
		{
			rb.MovePosition(rb.position += Vector2.right * speed * Time.deltaTime);
		}
		if (Input.GetKey("w"))
		{
			rb.MovePosition(rb.position += Vector2.up * speed * Time.deltaTime);
		}
		if (Input.GetKey("s"))
		{
			rb.MovePosition(rb.position += Vector2.down * speed * Time.deltaTime);
		}
	}

	void facingturn(){
		//Aim player at mouse
		//which direction is up
		Vector3 upAxis = new Vector3(0,0,1);
		Vector3 mouseScreenPosition = Input.mousePosition;
		//set mouses z to your targets
		mouseScreenPosition.z = transform.position.z;
		Vector3 mouseWorldSpace = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
		transform.LookAt(mouseWorldSpace, upAxis);
		//zero out all rotations except the axis I want
		transform.eulerAngles = new Vector3(0,0,-transform.eulerAngles.z);
	}
}