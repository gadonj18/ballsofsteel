using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This class handles interactions between the scenes and stores data to persist across scenes
public class ApplicationLogic : MonoBehaviour {
	private string playerName; //Not sure why I even need this, I guess for high scores?
	private long currentScore; //Will carry through to the scoreboard
	private ushort currentLevel; //Level being played
	private ushort unlockedLevel; //Highest level user has unlocked (Default = 1)

	void Start() {
		//Used to persist object across scenes
		DontDestroyOnLoad(this.gameObject);

		//Initialize the settings we want to save/persist across scenes
		#if UNITY_WEBPLAYER
			this.PlayerName = PlayerPrefs.GetString("PlayerName", "Player1");
		#else
			this.PlayerName = PlayerPrefs.GetString("PlayerName", System.Environment.UserName);
		#endif
		this.CurrentScore = 0;
		this.CurrentLevel = 0;
		this.UnlockedLevel = (ushort)PlayerPrefs.GetInt("UnlockedLevel", 1);
	}

	//On button click from start screen
	public void ShowLevelSelect() {
		Application.LoadLevel("LevelSelectScene");
	}

	//On button click from start screen
	public void StartGame(ushort levelNum = 0) {
		if(levelNum > 0) this.CurrentLevel = levelNum;
		this.CurrentScore = 0;
		Application.LoadLevel("MainGameScene");
	}

	private void NextLevel() {
		this.CurrentLevel++;
		this.StartGame();
	}

	private void ReplayLevel() {
		this.StartGame(this.CurrentLevel);
	}

	public void WinGame() {
		//TODO: Keep scores. Should the high score screen be shown after each level?
		Invoke("NextLevel", 3.0f);
	}

	public void LoseGame() {
		Invoke("ReplayLevel", 3.0f);
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