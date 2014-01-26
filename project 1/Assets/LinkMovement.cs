using UnityEngine;
using System.Collections;

public class LinkMovement : MonoBehaviour {
	public float speed = 8;
	private Animator animator;
	
	// Use this for initialization
	void Start()
	{
		animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		Vector2 vel = rigidbody.velocity;
		vel.x = h * speed;
		vel.y = v * speed;

		//taken from http://michaelcummings.net/mathoms/creating-2d-animated-sprites-using-unity-4.3#.UuB1K7TTnIV
		if (v > 0)
		{
			animator.SetInteger("Direction", 2);
		}
		else if (v < 0)
		{
			animator.SetInteger("Direction", 0);
		}
		else if (h < 0)
		{
			animator.SetInteger("Direction", 1);
		}
		else if (h > 0)
		{
			animator.SetInteger("Direction", 3);
		}

		rigidbody.velocity = vel;
	}
}
