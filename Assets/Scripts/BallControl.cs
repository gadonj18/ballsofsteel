using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {
	void FixedUpdate() {
		this.rigidbody2D.velocity = new Vector2(Mathf.Clamp(this.rigidbody2D.velocity.x, -5f, 5f), this.rigidbody2D.velocity.y);
	}
}