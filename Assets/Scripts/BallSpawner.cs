using UnityEngine;
using System.Collections;

//Factory for creating ball objects from its own position in-game
public class BallSpawner : MonoBehaviour {
	public Transform ballPrefab; //See the prefabs folder for the asset to be created
	public MainGameLogic logic; //Keep a reference to the script managinglastBall game logic
	private float ballDelay; //Time in seconds between balls
	private float lastBall; //Timestamp when the last ball was spawned

	void Start() {
		this.ballDelay = 0.0f;
		this.lastBall = Time.time;
	}

	//Starts spawning balls with a delay
	public void TurnOn(float nextBall) {
		this.ballDelay = nextBall;
		this.TurnOn();
	}

	//Starts spawning balls
	public void TurnOn() {
		if(this.ballDelay > 0.0f) {
			Invoke("DelaySpawn", this.ballDelay);
		} else {
			StartCoroutine("MakeBall");
		}
	}

	private void DelaySpawn() {
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
			this.lastBall = Time.time;
			this.ballDelay = 8.0f + Random.Range(-2.0f, 2.0f); //Needs a little variation or else each game will always have the same-timed balls
			yield return new WaitForSeconds(this.ballDelay); //This delays the coroutine
		}
	}

	void OnGUI() {
		GUI.skin.label.fontSize = 40;
		GUI.color = this.logic.textColor;
		float timeLeft = Mathf.Ceil(this.ballDelay - (Time.time - this.lastBall));
		if(this.logic.GameState == "Playing" && timeLeft > 0 && timeLeft <= 3.0f) {
			GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 100), timeLeft.ToString());
		}
	}
}