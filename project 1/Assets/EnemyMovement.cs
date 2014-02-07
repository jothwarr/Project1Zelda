using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public Vector3 targetPosition = Vector3.zero;
	public int speed = 4;
	public int dir = 3;
	public float gridSize=1.6f;
	public bool canmove = true;
	public float attackTimer = 1.5f;
	LinkMovement2 targetScript;
	public Rigidbody projectile;
	public float projSpeed = 15f;
	GameObject linkObject;
	// Use this for initialization
	void Start () {
		linkObject = GameObject.Find("Link");
		targetScript = linkObject.GetComponent<LinkMovement2>();
	}

	void OnTriggerEnter (Collider col)
	{
		col.gameObject.transform.Translate (Vector3.zero);
		col.gameObject
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
		if (linkObject.transform.position.x < this.transform.position.x)
			dir = 1;//left
		if (linkObject.transform.position.x > this.transform.position.x)
			dir = 3;//right
		if (linkObject.transform.position.y < this.transform.position.y)
			dir = 0;//up
		if (linkObject.transform.position.y > this.transform.position.y)
			dir = 2;//down
		//dir = Random.Range (0, 20);

		projSpeed = 15f;
		//Attacking
		if (dir == 0 && targetScript.position.x == transform.position.x && attackTimer == 0){
			Rigidbody newProjectile = (Rigidbody)Instantiate (projectile, transform.position, transform.rotation);
			newProjectile.velocity = Vector3.down * projSpeed;
			attackTimer = 1.5f;
			Destroy (newProjectile.gameObject, 3f);
		}
		if (dir == 1 && targetScript.position.y == transform.position.y && attackTimer == 0){
			Rigidbody newProjectile = (Rigidbody)Instantiate (projectile, transform.position, transform.rotation);
			newProjectile.velocity = Vector3.left * projSpeed;
			attackTimer = 1.5f;
			Destroy (newProjectile.gameObject, 3f);
		}
		if (dir == 2 && targetScript.position.x == transform.position.x && attackTimer == 0){
			Rigidbody newProjectile = (Rigidbody)Instantiate (projectile, transform.position, transform.rotation);
			newProjectile.velocity = Vector3.up * projSpeed;
			attackTimer = 1.5f;
			Destroy (newProjectile.gameObject, 3f);
		}
		if (dir == 3 && targetScript.position.y == transform.position.y && attackTimer == 0){
			Rigidbody newProjectile = (Rigidbody)Instantiate (projectile, transform.position, transform.rotation);
			newProjectile.velocity = Vector3.right * projSpeed;
			attackTimer = 1.5f;
			Destroy (newProjectile.gameObject, 3f);
		}

		attackTimer -= Time.deltaTime;
		if (attackTimer < 0)
			attackTimer = 0f;


		//Movement
		if (dir == 2 && canmove == true) {//up
			canmove = false;
			StartCoroutine (MoveInGrid ((float)transform.position.x, (float)transform.position.y + gridSize, (float)transform.position.z));
		}
		if (dir == 3 && canmove == true) {//right
			canmove = false;
			StartCoroutine (MoveInGrid ((float)transform.position.x + gridSize, (float)transform.position.y, (float)transform.position.z));
		}
		if (dir == 1 && canmove == true) {//left
			canmove = false;
			StartCoroutine (MoveInGrid ((float)transform.position.x - gridSize, (float)transform.position.y, (float)transform.position.z));
		}
		if (dir == 0 && canmove == true) {//down
			canmove = false;
			StartCoroutine (MoveInGrid ((float)transform.position.x, (float)transform.position.y - gridSize, (float)transform.position.z));
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
