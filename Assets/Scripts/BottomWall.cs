using UnityEngine;
using System.Collections;

//Unlike the other walls, the bottom wall is used to trigger a miss instead of bouncing
public class BottomWall : MonoBehaviour {
	public MainGameLogic logic; //Keep a reference to the script managing game logic

	//Basically just used to notify the main game logic script that a ball was missed and which ball it is
	void OnCollisionEnter2D(Collision2D coll) {
		this.logic.OnMissBall(coll.gameObject);
	}
}
