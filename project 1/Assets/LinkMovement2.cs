using UnityEngine;
using System.Collections;

public class LinkMovement2 : MonoBehaviour
	
{
	public bool canmove = true; //indicate if a keyboard key can move a piece
	public Vector3 targetPosition;//temporary value for moving (used in coroutines)
	public Vector3 position;
	Vector3 swordPosition;
	Vector3 swordSize;
	BoxCollider swordBox;
	public int dir = 0;
	public int speed;
	public float gridSize;
	private Animator animator;
	public bool attacking = false;
	public float attackTimer = 0f;
	public Rigidbody sworddown;
	public Rigidbody swordleft;
	public Rigidbody swordup;
	public Rigidbody swordright;
	public float projSpeed = 7f;

	void Start()
	{
		animator = this.gameObject.GetComponent<Animator>();
		swordBox = this.gameObject.GetComponent<BoxCollider>();
		projSpeed = 7f;
		gridSize = 1;
		speed = 5;
	}
	void OnCollisionEnter(Collision col)
	{
		this.rigidbody.velocity = Vector3.zero;
		this.rigidbody.angularVelocity = Vector3.zero;
		this.rigidbody.freezeRotation = true;
		this.rigidbody.isKinematic = true;
		Vector3 fixedpos;
		fixedpos.x = Mathf.Round(transform.position.x);
		fixedpos.y = Mathf.Round(transform.position.y);
		fixedpos.z = Mathf.Round(transform.position.z);
		this.transform.position = fixedpos;

		float angle = Vector3.Angle(col.contacts [0].normal, Vector3.right);
		if (angle >= 135f || angle <= -135f) {
			if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Projectile") {
				//canmove = false;
				StartCoroutine (MoveInGrid (Mathf.Round (transform.position.x - gridSize), transform.position.y, transform.position.z));
			}
		}
		if (angle <= 45f && angle >= -45f) {
			if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Projectile") {
				//canmove = false;
				StartCoroutine (MoveInGrid (Mathf.Round (transform.position.x + gridSize), transform.position.y, transform.position.z));
			}
		}
		if (angle >= 45f && angle <= 135f) {
			if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Projectile") {
				//canmove = false;
				StartCoroutine (MoveInGrid (transform.position.x, Mathf.Round(transform.position.y + gridSize), transform.position.z));
			}
		}
		if (angle <= -45 && angle >= -135) {
			if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Projectile") {
				//canmove = false;
				StartCoroutine (MoveInGrid (transform.position.x, Mathf.Round(transform.position.y - gridSize), transform.position.z));
			}
		}
		if (col.gameObject.tag == "Projectile")
			Destroy (col.gameObject);
		this.rigidbody.isKinematic = false;
	}
	void Update()
	{
		position = this.transform.position;
		dir = animator.GetInteger("Direction");
		this.rigidbody.velocity = Vector3.zero;
		this.rigidbody.angularVelocity = Vector3.zero;
		this.rigidbody.freezeRotation = true;
		swordBox.enabled = false;

		//----------------ATTACKING
		attacking = false;
		animator.SetBool ("attacking", false);
		if (Input.GetKeyDown (KeyCode.J) || Input.GetKeyDown (KeyCode.X)) { 
				animator.SetBool ("attacking", true);
			if(attackTimer == 0)
				attackTimer = .35f;
			//if(health is full)
			ShootSword(attackTimer, dir);
		}
		attackTimer -= Time.deltaTime;
		if (attackTimer > 0f) {
			attacking = true;
			swordBox.enabled = true;
			swordBox.isTrigger = true;
		}
		else {
			attackTimer = 0f;
			attacking = false;
			swordBox.isTrigger = false;
			//canmove = true;
		}
		//----------------MOVEMENT
		//canmove = canmove && !PauseMovement.isTimeStopped ();

		if ((Input.GetKey(KeyCode.UpArrow) == true || Input.GetKey(KeyCode.W) == true) 
		    && canmove == true && attacking == false)
		{
			animator.SetInteger("Direction", 2);
			swordPosition.x = .4f;
			swordPosition.y = .5f;
			swordSize.x = .2f;
			swordSize.y = .8f;
			swordBox.center = swordPosition;
			swordBox.size = swordSize;
			canmove = false;
			StartCoroutine (MoveInGrid((float)transform.position.x, (float)transform.position.y+gridSize, (float)transform.position.z));
		}
		if ((Input.GetKey(KeyCode.RightArrow) == true || Input.GetKey(KeyCode.D) == true) 
		    && canmove == true && attacking == false)
		{
			animator.SetInteger("Direction", 3);
			swordPosition.x = 1.3f;
			swordPosition.y = -.53f;
			swordSize.x = .8f;
			swordSize.y = .2f;
			swordBox.center = swordPosition;
			swordBox.size = swordSize;
			canmove = false;
			StartCoroutine(MoveInGrid((float)transform.position.x+gridSize, (float)transform.position.y, (float)transform.position.z));
		}
		if ((Input.GetKey(KeyCode.LeftArrow) == true || Input.GetKey(KeyCode.A) == true) 
		    && canmove == true && attacking == false)
		{
			animator.SetInteger("Direction", 1);
			swordPosition.x = -.45f;
			swordPosition.y = -.53f;
			swordSize.x = .8f;
			swordSize.y = .2f;
			swordBox.center = swordPosition;
			swordBox.size = swordSize;
			canmove = false;
			StartCoroutine(MoveInGrid((float)transform.position.x-gridSize, (float)transform.position.y, (float)transform.position.z));
		}
		if ((Input.GetKey(KeyCode.DownArrow) == true || Input.GetKey(KeyCode.S) == true) 
		    && canmove == true && attacking == false)
		{
			animator.SetInteger("Direction", 0);
			swordPosition.x = .53f;
			swordPosition.y = -1.3f;
			swordSize.x = .2f;
			swordSize.y = .8f;
			swordBox.center = swordPosition;
			swordBox.size = swordSize;
			canmove = false;
			StartCoroutine(MoveInGrid((float)transform.position.x, (float)transform.position.y-gridSize, (float)transform.position.z));
		}
		
	}

	void ShootSword(float attackTimer, int dir)
	{//dir: 0 = down, 1 = left, 2 = up, 3 = right
		GameObject[] swords = GameObject.FindGameObjectsWithTag ("Sword");
		if (attackTimer == .35f && swords.Length == 0) {
			if(dir == 0){
				Rigidbody swordClone = (Rigidbody)Instantiate (sworddown, transform.position + swordPosition, transform.rotation);
				swordClone.velocity = Vector3.down * projSpeed;
				Destroy (swordClone.gameObject, 2f);
			}
			if(dir == 1){
				Rigidbody swordClone = (Rigidbody)Instantiate (swordleft, transform.position + swordPosition, transform.rotation);
				swordClone.velocity = Vector3.left * projSpeed;
				Destroy (swordClone.gameObject, 2f);
			}
			if(dir == 2){
				Rigidbody swordClone = (Rigidbody)Instantiate (swordup, transform.position + swordPosition, transform.rotation);
				swordClone.velocity = Vector3.up * projSpeed;
				Destroy (swordClone.gameObject, 2f);
			}
			if(dir == 3){
				Rigidbody swordClone = (Rigidbody)Instantiate (swordright, transform.position + swordPosition, transform.rotation);
				swordClone.velocity = Vector3.right * projSpeed;
				Destroy (swordClone.gameObject, 2f);
			}
		}
	}
	
	//based off of http://answers.unity3d.com/questions/9885/basic-movement-in-a-grid.html
	public IEnumerator MoveInGrid(float x,float y,float z)
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
		//canmove = !PauseMovement.isTimeStopped ();
	}
}

