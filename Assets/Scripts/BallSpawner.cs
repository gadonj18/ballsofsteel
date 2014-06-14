using UnityEngine;
using System.Collections;

//Factory for creating ball objects from its own position in-game
public class BallSpawner : MonoBehaviour {
	public Transform ballPrefab; //See the prefabs folder for the asset to be created
	public MainGameLogic logic; //Keep a reference to the script managing game logic
	private float nextBallIn; //Time in seconds between balls

	void Start() {
		nextBallIn = 0;
		this.TurnOn();
	}

	//Starts spawning balls with a delay
	public void TurnOn(float nextBall) {
		this.nextBallIn = nextBall;
		StartCoroutine("MakeBall");
	}

	//Starts spawning balls
	public void TurnOn() {
		StartCoroutine("MakeBall");
	}

	//Stops spawning balls
	public void TurnOff() {
		StopCoroutine("MakeBall");
	}

	//Coroutine that makes a new ball and calculates the time until the next one
	IEnumerator MakeBall() {
		while(true) {
			Transform newBall = (Transform)Instantiate(this.ballPrefab, this.transform.position, this.transform.rotation);
			newBall.gameObject.layer = LayerMask.NameToLayer("BallLayer"); //Layer stops balls from colliding with each other
			this.logic.AddBall(newBall); //Game logic script keeps a list of active balls
			this.nextBallIn = 8.0f + Random.Range(-2.0f, 2.0f); //Needs a little variation or else each game will always have the same-timed balls
			yield return new WaitForSeconds(this.nextBallIn); //This delays the coroutine
		}
	}
}