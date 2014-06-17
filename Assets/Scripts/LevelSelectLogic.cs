using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectLogic : MonoBehaviour {
	public SpriteRenderer PreviewImage;

	private ushort currentLevel;
	private Dictionary<ushort, BaseLevel> levels = new Dictionary<ushort, BaseLevel>();

	void Start () {
		this.LoadLevels();
		this.CurrentLevel = 1;
		this.ShowLevel();
	}

	private void LoadLevels() {
		for(ushort i = 1; i <= 100; i++) {
			Object resource = Resources.Load("Level" + i, typeof(BaseLevel));
			if(!resource) break;
			levels.Add(i, (BaseLevel)Instantiate(resource));
		}
	}

	private void ShowLevel() {
		if(!levels.ContainsKey(this.CurrentLevel)) return;
		this.PreviewImage.sprite = levels[this.CurrentLevel].background;
		//TODO: Scale image properly... too lazy right now
	}

	void OnGUI() {
		GUI.skin.label.fontSize = 30;
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUI.Label(new Rect(Screen.width / 2 - 70, 20, 140, 50), "Level " + this.CurrentLevel);
		GUI.skin.label.alignment = TextAnchor.MiddleLeft;
		GUI.Label(new Rect(Screen.width / 2 - this.PreviewImage.sprite.rect.x / 2, 60, 200, 50), "Target: " + this.levels[this.CurrentLevel].targetHits + " hits");
		if(this.CurrentLevel == 1) GUI.enabled = false;
		if(GUI.Button(new Rect(10, Screen.height / 2, 50, 50), "<")) {
			this.CurrentLevel--;
			this.ShowLevel();
		}
		GUI.enabled = true;
		if(this.CurrentLevel == this.levels.Count) GUI.enabled = false;
		if(GUI.Button(new Rect(Screen.width - 60, Screen.height / 2, 50, 50), ">")) {
			this.CurrentLevel++;
			this.ShowLevel();
		}
		GUI.enabled = true;
		if(GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 100, 100, 50), "Play!")) {
		}
	}
	
	public ushort CurrentLevel {
		get { return this.currentLevel; }
		private set { this.currentLevel = value; }
	}
}