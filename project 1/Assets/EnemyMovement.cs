﻿using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public Vector3 targetPosition;
	public int speed = 4;
	public int dir = 0;
	public float gridSize=1.6f;
	public bool canmove = true;
//Script other : LinkMovement2 = GameObject.getComponent(LinkMovement2);
	// Use this for initialization
	void Start () {
	//other : LinkMovement2 = GameObject.getComponent(LinkMovement2);
	}

	void OnTriggerEnter (Collider col)
	{
		col.gameObject.transform.Translate (Vector3.zero);
		this.renderer.enabled = false;
		this.gameObject.collider.enabled = false;
		Destroy (this);

	}

/*	void OnTriggerStay (Collider col)
	{
		col.gameObject.transform.Translate (Vector3.zero);
		this.renderer.enabled = false;
		this.gameObject.collider.enabled = false;
		Destroy (this);
	}*/



	// Update is called once per frame
	void Update () {
		dir = Random.Range (0, 4);
		if (dir == 2) {
			canmove = false;
			StartCoroutine (MoveInGrid ((float)transform.position.x, (float)transform.position.y + gridSize, (float)transform.position.z));
		}
		if (dir == 3) {
			canmove = false;
			StartCoroutine (MoveInGrid ((float)transform.position.x + gridSize, (float)transform.position.y, (float)transform.position.z));
		}
		if (dir == 1) {
			canmove = false;
			StartCoroutine (MoveInGrid ((float)transform.position.x - gridSize, (float)transform.position.y, (float)transform.position.z));
		}
		if (dir == 0) {
			canmove = false;
			StartCoroutine (MoveInGrid ((float)transform.position.x + gridSize, (float)transform.position.y - gridSize, (float)transform.position.z));
		}
	}

IEnumerator MoveInGrid(float x,float y,float z)
{
	x = Mathf.Round (x * 100f) / 100f;
	y = Mathf.Round (y * 100f) / 100f;
	z = Mathf.Round (z * 100f) / 100f;
	while (transform.position.x != x || transform.position.y != y || transform.position.z != z)
	{
		//moving x forward
		if (transform.position.x < x)
		{
			//moving the point by speed
			targetPosition.x = speed * Time.deltaTime;
			//check if the point goes more than it should go and if yes clamp it back
			if (targetPosition.x + transform.position.x > x)
			{
				targetPosition.x = x - transform.position.x;
			}
		}
		//moving x backward
		else if (transform.position.x > x)
		{
			//moving the point by speed
			targetPosition.x = -speed * Time.deltaTime;
			//check if the point goes more than it should go and if yes clamp it back
			if (targetPosition.x + transform.position.x < x)
			{
				targetPosition.x = -(transform.position.x - x);
			}
		}
		else //x is unchanged so should be 0 in translate function
		{
			targetPosition.x = 0;
		}
		//moving y forward
		if (transform.position.y < y)
		{
			//moving the point by speed
			targetPosition.y = speed * Time.deltaTime;
			//check if the point goes more than it should go and if yes clamp it back
			if (targetPosition.y + transform.position.y > y)
			{
				targetPosition.y = y - transform.position.y;
			}
		}
		//moving y backward
		else if (transform.position.y > y)
		{
			//moving the point by speed
			targetPosition.y = -speed * Time.deltaTime;
			//check if the point goes more than it should go and if yes clamp it back
			if (targetPosition.y + transform.position.y < y)
			{
				targetPosition.y = -(transform.position.y - y);
			}
		}
		else //y is unchanged so it should be zero
		{
			targetPosition.y = 0;
		}
		//moving z forward
		if (transform.position.z < z)
		{
			//moving the point by speed
			targetPosition.z = speed * Time.deltaTime;
			//check if the point goes more than it should go and if yes clamp it back
			if (targetPosition.z + transform.position.z > z)
			{
				targetPosition.z = z - transform.position.z;
			}
		}
		//moving z backward
		else if (transform.position.z > z)
		{
			//moving the point by speed
			targetPosition.z = -speed * Time.deltaTime;
			//check if the point goes more than it should go and if yes clamp it back
			if (targetPosition.z + transform.position.z < z)
			{
				targetPosition.z = -(transform.position.z - z);
			}
		}
		else //z is unchanged so should be zero in translate function
		{
			targetPosition.z = 0;
		}
		
		transform.Translate(targetPosition);
		yield return 0;
	}
	//the work is ended now congratulation
	canmove = true;
}
}