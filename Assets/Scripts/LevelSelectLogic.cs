using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectLogic : MonoBehaviour {
	public SpriteRenderer PreviewImage;

	private ushort currentLevel;
	private Dictionary<ushort, BaseLevel> levels = new Dictionary<ushort, BaseLevel>();

	void Start () {
		this.CurrentLevel = 1;
		this.ShowLevel();
	}

	private void ShowLevel() {
		if(!levels.ContainsKey(this.CurrentLevel)) {
			Object resource = Resources.Load("Level" + this.CurrentLevel, typeof(BaseLevel));
			if(resource) {
				levels.Add(this.CurrentLevel, (BaseLevel)Instantiate(resource));
			} else {
				this.CurrentLevel--;
				this.ShowLevel();
				return;
			}
		}
		this.PreviewImage.sprite = levels[this.CurrentLevel].background;
		//float width = this.background.sprite.bounds.size.x;
		//float height = this.background.sprite.bounds.size.y;
		//float worldScreenHeight = Camera.main.orthographicSize * 2f;
		//float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
		//if(worldScreenWidth / width >= worldScreenHeight / height) {
		//	this.background.transform.localScale = new Vector3(worldScreenWidth / width, worldScreenWidth / width, 1);
		//} else {
		//	this.background.transform.localScale = new Vector3(worldScreenHeight / height, worldScreenHeight / height, 1);
		//}
	}

	void OnGUI() {
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