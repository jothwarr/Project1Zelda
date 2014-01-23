using UnityEngine;
using System.Collections;

public class LinkMovement : MonoBehaviour {
	public float speed = 8;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		Vector2 vel = rigidbody2D.velocity;
		vel.x = h * speed;
		vel.y = v * speed;

		rigidbody2D.velocity = vel;
	}
}
