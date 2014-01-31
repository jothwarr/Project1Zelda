using UnityEngine;
using System.Collections;

public class LinkMovement2 : MonoBehaviour
	
{
	public bool canmove = true; //indicate if a keyboard key can move a piece
	public Vector3 targetPosition; //temporary value for moving (used in coroutines)
	Vector3 swordPosition;
	Vector3 swordSize;
	BoxCollider swordBox;
	public int speed = 4;
	public float gridSize=1.6f;
	private Animator animator;
	public bool attacking = false;
	public float attackTimer = 0f;

	void Start()
	{
		animator = this.gameObject.GetComponent<Animator>();
		swordBox = this.gameObject.GetComponent<BoxCollider>();
	}
	/*void OnTriggerEnter (Collider col)
	{
		canmove = true;
	}
	void OnTriggerExit (Collider col)
	{
		canmove = true;
	}*/

	void Update()
	{
		//----------------ATTACKING
		attacking = false;
		animator.SetBool ("attacking", false);
		if (Input.GetKeyDown (KeyCode.J) || Input.GetKeyDown (KeyCode.X)) { 
				animator.SetBool ("attacking", true);
				attackTimer = .35f;
		}
		attackTimer -= Time.deltaTime;
		if (attackTimer > 0f) {
			attacking = true;
			swordBox.isTrigger = true;
		}
		else {
			attackTimer = 0f;
			attacking = false;
			swordBox.isTrigger = false;
			//canmove = true;
		}
		//----------------MOVEMENT

		if ((Input.GetKey(KeyCode.UpArrow) == true || Input.GetKey(KeyCode.W) == true) 
		    && canmove == true && attacking == false)
		{
			animator.SetInteger("Direction", 2);
			swordPosition.x = .06f;
			swordPosition.y = .04f;
			swordSize.x = .06f;
			swordSize.y = .2f;
			swordBox.center = swordPosition;
			swordBox.size = swordSize;
			canmove = false;
			StartCoroutine (MoveInGrid((float)transform.position.x, (float)transform.position.y+gridSize, (float)transform.position.z));
		}
		if ((Input.GetKey(KeyCode.RightArrow) == true || Input.GetKey(KeyCode.D) == true) 
		    && canmove == true && attacking == false)
		{
			animator.SetInteger("Direction", 3);
			swordPosition.x = .16f;
			swordPosition.y = -.08f;
			swordSize.x = .2f;
			swordSize.y = .06f;
			swordBox.center = swordPosition;
			swordBox.size = swordSize;
			canmove = false;
			StartCoroutine(MoveInGrid((float)transform.position.x+gridSize, (float)transform.position.y, (float)transform.position.z));
		}
		if ((Input.GetKey(KeyCode.LeftArrow) == true || Input.GetKey(KeyCode.A) == true) 
		    && canmove == true && attacking == false)
		{
			animator.SetInteger("Direction", 1);
			swordPosition.x = -.04f;
			swordPosition.y = -.08f;
			swordSize.x = .2f;
			swordSize.y = .06f;
			swordBox.center = swordPosition;
			swordBox.size = swordSize;
			canmove = false;
			StartCoroutine(MoveInGrid((float)transform.position.x-gridSize, (float)transform.position.y, (float)transform.position.z));
		}
		if ((Input.GetKey(KeyCode.DownArrow) == true || Input.GetKey(KeyCode.S) == true) 
		    && canmove == true && attacking == false)
		{
			animator.SetInteger("Direction", 0);
			swordPosition.x = .075f;
			swordPosition.y = -.16f;
			swordSize.x = .06f;
			swordSize.y = .2f;
			swordBox.center = swordPosition;
			swordBox.size = swordSize;
			canmove = false;
			StartCoroutine(MoveInGrid((float)transform.position.x, (float)transform.position.y-gridSize, (float)transform.position.z));
		}
		
	}
	
	//based off of http://answers.unity3d.com/questions/9885/basic-movement-in-a-grid.html
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

