using UnityEngine;
using System.Collections;

//Draws and handles the click events of the buttons on the start screen
public class StartButtons : MonoBehaviour {
	public ApplicationLogic logic; //Keep reference to script managing interactions between scenes

	//Not ENTIRELY sure what this function actually is or when it's called
	//Its named like it should be called once but behavious like a Draw() loop or some shit
	//Like, it initializes the button AND listens for the click
	void OnGUI() {
		GUI.skin.button.fontSize = 30;
		GUI.skin.button.fontStyle = FontStyle.Bold;
		if(GUI.Button(new Rect(Screen.width / 2 - 220, Screen.height - 90, 200, 60), (this.logic.UnlockedLevel > 1 ? "Continue" : "Start Game"))) {
			this.logic.StartGame(this.logic.UnlockedLevel);
		}
		if(GUI.Button(new Rect(Screen.width / 2 + 20, Screen.height - 90, 200, 60), "Level Select")) {
			this.logic.ShowLevelSelect();
		}
	}
}
