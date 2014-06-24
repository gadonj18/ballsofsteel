using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectLogic : MonoBehaviour {
	public ApplicationLogic logic;
	public SpriteRenderer PreviewImage;

	private int currentLevelNum;
	private BaseLevel currentLevel;

	void Start () {
		this.logic = GameObject.Find("_ApplicationLogic").GetComponent<ApplicationLogic>();
		this.currentLevelNum = 1;
		this.ShowLevel();
	}

	private void ShowLevel() {
		if(this.currentLevelNum < 0 || this.currentLevelNum > this.logic.numLevels) return;
		if(this.currentLevel) Destroy(this.currentLevel.gameObject);
		this.currentLevel = (BaseLevel)Instantiate(Resources.Load("Level" + this.currentLevelNum, typeof(BaseLevel)));
		this.PreviewImage.sprite = this.currentLevel.background;
		//TODO: Scale image properly... too lazy right now
	}

	void OnGUI() {
		if(this.currentLevel) {
			GUI.skin.label.fontSize = 30;
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.Label(new Rect(Screen.width / 2 - 70, 20, 140, 50), "Level " + this.currentLevelNum);
			GUI.skin.label.alignment = TextAnchor.MiddleLeft;
			GUI.Label(new Rect(Screen.width / 2 - this.PreviewImage.sprite.rect.x / 2, 60, 200, 50), "Target: " + this.currentLevel.targetHits + " hits");
			if(this.currentLevelNum == 1)
				GUI.enabled = false;
			if(GUI.Button(new Rect(10, Screen.height / 2, 50, 50), "<")) {
				this.currentLevelNum--;
				this.ShowLevel();
			}
			GUI.enabled = true;
			if(this.currentLevelNum == this.logic.numLevels)
				GUI.enabled = false;
			if(GUI.Button(new Rect(Screen.width - 60, Screen.height / 2, 50, 50), ">")) {
				this.currentLevelNum++;
				this.ShowLevel();
			}
			GUI.enabled = true;
			if(GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 100, 100, 50), "Play!")) {
				this.currentLevelNum = 1;
				Destroy(this.currentLevel);
				this.logic.StartGame(this.currentLevelNum);
			}
		}
	}
}