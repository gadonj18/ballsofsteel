using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This class handles interactions between the scenes and stores data to persist across scenes
public class ApplicationLogic : MonoBehaviour {
	public static ApplicationLogic control; //Used for singleton-like behaviour
	public List<BaseLevel> levels = new List<BaseLevel>();

	private string playerName; //Not sure why I even need this, I guess for high scores?
	private long currentScore; //Will carry through to the scoreboard
	private ushort currentLevel; //Level being played
	private ushort unlockedLevel; //Highest level user has unlocked (Default = 1)

	void Awake() {
		//Ensure that there is only ever 1 instance of this object
		if(control == null) {
			DontDestroyOnLoad(this.gameObject);
			control = this;
		} else if(control != this) {
			Destroy(this.gameObject);
			return;
		}

		//Initialize the settings we want to save/persist across scenes
		this.PlayerName = PlayerPrefs.GetString("PlayerName", System.Environment.UserName);
		this.CurrentScore = 0;
		this.CurrentLevel = 0;
		this.UnlockedLevel = (ushort)PlayerPrefs.GetInt("UnlockedLevel", 1);
	}

	//Switch scene
	public void ShowLevelSelect() {
		Application.LoadLevel("LevelSelect");
	}

	//Switch scene
	public void StartGame(ushort levelNum) {
		this.CurrentLevel = levelNum;
		this.CurrentScore = 0;
		Application.LoadLevel("MainGameScene");
	}

	//--------------- GETTERS/SETTERS BELOW --------------------------

	public string PlayerName {
		get { return this.playerName; }
		private set { this.playerName = value; }
	}
	
	public long CurrentScore {
		get { return this.currentScore; }
		private set { this.currentScore = value; }
	}
	
	public ushort CurrentLevel {
		get { return this.currentLevel; }
		private set { this.currentLevel = value; }
	}
	
	public ushort UnlockedLevel {
		get { return this.unlockedLevel; }
		private set { this.unlockedLevel = value; }
	}
}