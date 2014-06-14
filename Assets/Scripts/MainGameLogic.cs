using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This class handles interactions between objects in the main game scene
public class MainGameLogic : MonoBehaviour {
	public ApplicationLogic logic; //Keep reference to script managing interactions between scenes
	public Camera mainCam; //Camera needed for calculating screen dimensions

	//On screen text elements
	public GUIText scoreText;
	public GUIText hitsText;

	//Walls for ball to bounce off of
	public BoxCollider2D topWall;
	public BoxCollider2D bottomWall;
	public BoxCollider2D leftWall;
	public BoxCollider2D rightWall;

	//Sounds to play
	public AudioClip[] bounceSounds = new AudioClip[4];
	public AudioClip buzzerSound;

	//Not sure why I need the list of balls
	private ArrayList balls = new ArrayList();

	//Counters/scores
	private uint targetHits;
	private uint streakHits;
	private ushort streakMiss;
	private uint totalHits;
	private long score;

	void Start() {
		this.logic = GameObject.Find("_ApplicationLogic").GetComponent<ApplicationLogic>();

		BaseLevel level = (BaseLevel)Instantiate(Resources.Load("Level" + this.logic.CurrentLevel, typeof(BaseLevel)));
		this.targetHits = (uint)level.targetHits;
		Screen.showCursor = false;

		this.streakHits = 0;
		this.streakMiss = 0;
		this.totalHits = 0;

		//Setup walls using the camera and screen dimensions (should size to any resolution)
		this.topWall.size = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(Screen.width * 2f, 0f, 0f)).x, 1f);
		this.topWall.center = new Vector2(0f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y + 0.5f);
		
		this.bottomWall.size = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(Screen.width * 2f, 0f, 0f)).x, 1f);
		this.bottomWall.center = new Vector2(0f, mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y - 0.5f);
		
		this.leftWall.size = new Vector2(1f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
		this.leftWall.center = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x - 0.5f, 0f);

		this.rightWall.size = new Vector2(1f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
		this.rightWall.center = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x + 0.5f, 0f);
	}

	//Keep a list of each ball (called from BallSpawner)
	public void AddBall(Transform newBall) {
		this.balls.Add(newBall);
	}

	//TODO: Should this object be modifying the balls velocity? Or should it be the paddle?
	public void OnBounce(Rigidbody2D ball, float paddleOffset) {
		//Update counters/scores
		this.totalHits++;
		this.streakHits++;
		this.streakMiss = 0;
		this.score += (long)this.streakHits;

		//Play one of the four bounce sounds
		AudioSource.PlayClipAtPoint(this.bounceSounds[Random.Range(0, 3)], new Vector3(0, 0, 0));

		//Update the score on the GUI
		this.UpdateGUI();

		//Ensure vertical velocity remains constant each bounce.
		//Add to horizontal velocity based on where the ball hit the paddle.
		//Hitting the ball on the right side of the paddle with add rightward velocity,
		//while hitting the ball on the left isde of the paddle will add leftward velocity.
		ball.velocity = new Vector2(ball.velocity.x + paddleOffset * 2f, 8.4f);
	}

	public void OnMissBall(GameObject ball) {
		//Remove ball from the list (TODO: Pretty sure this doesn't do shit)
		for(int i = 0; i < this.balls.Count; i++) {
			if(this.balls[i] == ball) {
				this.balls.RemoveAt(i);
			}
		}

		//Update counters/scores
		this.streakHits = 0;
		this.streakMiss++;
		this.score -= (long)(100 * this.streakMiss);

		//Play buzzer sound
		AudioSource.PlayClipAtPoint(this.buzzerSound, new Vector3(0, 0, 0));

		//Get rid of the ball object
		Destroy(ball);

		//Update the score on the GUI
		this.UpdateGUI();
	}

	//Pretty self explanatory
	private void UpdateGUI() {
		this.scoreText.text = "Score: " + this.score;
		this.hitsText.text = "Hits: " + this.totalHits;	
	}
}