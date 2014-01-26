using UnityEngine;
using System.Collections;

//based off of http://answers.unity3d.com/questions/9885/basic-movement-in-a-grid.html
public class LinkMovement2 : MonoBehaviour
	
{
	bool canmove = true; //indicate if a keyboard key can move a piece
	Vector3 targetPosition; //temporary value for moving (used in coroutines)
	public int speed = 4;
	public float gridSize=1.6f;
	private Animator animator;
	private bool attacking = false;
	private float timer = 2f;
	
	void Start()
	{
		animator = this.GetComponent<Animator>();
	}
	
	void Update()
	{
		//----------------ATTACKING
		attacking = false;
		animator.SetBool ("attacking", false);
		if (Input.GetKeyDown (KeyCode.J) || Input.GetKeyDown (KeyCode.X)) { 
			animator.SetBool ("attacking", true);
			attacking = true;
			while(timer > 0){
				timer -= Time.deltaTime;
			}
			timer = 2f;

		}
		//----------------MOVEMENT
		if ((Input.GetKey(KeyCode.UpArrow) == true || Input.GetKey(KeyCode.W) == true) 
		    && canmove == true && attacking == false)
		{
			animator.SetInteger("Direction", 2);
			canmove = false;
			StartCoroutine (MoveInGrid(attacking, (float)transform.position.x, (float)transform.position.y+gridSize, (float)transform.position.z));
		}
		if ((Input.GetKey(KeyCode.RightArrow) == true || Input.GetKey(KeyCode.D) == true) 
		    && canmove == true && attacking == false)
		{
			animator.SetInteger("Direction", 3);
			canmove = false;
			StartCoroutine(MoveInGrid(attacking, (float)transform.position.x+gridSize, (float)transform.position.y, (float)transform.position.z));
		}
		if ((Input.GetKey(KeyCode.LeftArrow) == true || Input.GetKey(KeyCode.A) == true) 
		    && canmove == true && attacking == false)
		{
			animator.SetInteger("Direction", 1);
			canmove = false;
			StartCoroutine(MoveInGrid(attacking, (float)transform.position.x-gridSize, (float)transform.position.y, (float)transform.position.z));
		}
		if ((Input.GetKey(KeyCode.DownArrow) == true || Input.GetKey(KeyCode.S) == true) 
		    && canmove == true && attacking == false)
		{
			animator.SetInteger("Direction", 0);
			canmove = false;
			StartCoroutine(MoveInGrid(attacking, (float)transform.position.x, (float)transform.position.y-gridSize, (float)transform.position.z));
		}
		
	}
	
	
	IEnumerator MoveInGrid(bool attacking, float x,float y,float z)
	{
		if (attacking == true)
			Debug.LogError("attacking = true");
		else{
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
}

