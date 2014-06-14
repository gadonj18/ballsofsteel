using UnityEngine;
using System.Collections;

//Handles the logic for the paddle
//TODO: How to handle mouse for desktop build and swipe for android build?
public class PaddleControl : MonoBehaviour {
	public MainGameLogic logic; //Keep a reference to the script managing game logic
	public Camera mainCam; //Camera needed for calculating screen dimensions

	void Start() {
		//Set the paddle's Y coordinate
		this.transform.position = new Vector3(this.transform.position.x, mainCam.ScreenToWorldPoint(new Vector3(0f, 50f, 0f)).y, this.transform.position.z);
	}

	//Lock the paddle to a constan y position
	//Map the paddle's x position to the mouse
	void Update() {
		this.transform.position = new Vector3(
			this.mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0f, 0f)).x, 
			mainCam.ScreenToWorldPoint(new Vector3(0f, 50f, 0f)).y, 
			this.transform.position.z
		);
	}

	//Basically just used to notify the main game logic script that a ball was bounced
	void OnCollisionExit2D(Collision2D coll) {
		this.logic.OnBounce(coll.rigidbody, coll.transform.position.x - this.transform.position.x);
	}
}