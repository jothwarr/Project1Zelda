using UnityEngine;
using System.Collections;

public class LinkMovement2 : MonoBehaviour
	
{
	public bool canmove = true; //indicate if a keyboard key can move a piece
	public Vector3 targetPosition;//temporary value for moving (used in coroutines)
	public Vector3 position;
	public float health;
	public bool ignoreInput;
	public float stopTimer;
	public float stuckTimer;
	Vector3 swordPosition;
	Vector3 swordSize;
	BoxCollider swordBox;
	public int dir = 0;
	public int speed;
	public float gridSize;
	private Animator animator;
	public bool attacking = false;
	public bool hit = false;
	public float attackTimer = 0f;
	public float hitTimer = 0f;
	public Rigidbody sworddown;
	public Rigidbody swordleft;
	public Rigidbody swordup;
	public Rigidbody swordright;
	public float projSpeed = 7f;
	public Rect deadRect;

	void OnGUI() {
		string text = "Health: " + health;
		GUI.Box (new Rect(0, 0, 100, 30), text);
	}

	void Start()
	{
		animator = this.gameObject.GetComponent<Animator>();
		swordBox = this.gameObject.GetComponent<BoxCollider>();
		projSpeed = 7f;
		gridSize = .5f;
		speed = 7;
		health = 3f;
	}

	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag != "Enemy" && col.gameObject.tag != "Projectile" && col.gameObject.tag != "Item")
			ignoreInput = false;
		StartCoroutine (MoveInGrid (Mathf.Round (position.x), Mathf.Round (position.y), position.z));
	}
			
	void OnCollisionStay(Collision col)
	{
		ignoreInput = true;
		if (col.gameObject.tag != "Enemy" && col.gameObject.tag != "Projectile" && col.gameObject.tag != "Item") {
			if (dir == 0) {
				StopAllCoroutines ();
				canmove = false;
				stopTimer = 2f;
				while (stopTimer >= 0f) {
					StopAllCoroutines ();
					stopTimer -= Time.deltaTime;
				}
				if (stopTimer <= 0f)
					canmove = true;
				StartCoroutine (MoveInGrid (transform.position.x, transform.position.y + gridSize, transform.position.z));
			}
			if (dir == 1) {
				StopAllCoroutines ();
				canmove = false;
				stopTimer = 2f;
				while (stopTimer >= 0f) {
					StopAllCoroutines ();
					stopTimer -= Time.deltaTime;
				}
				if (stopTimer <= 0f)
					canmove = true;
				StartCoroutine (MoveInGrid (transform.position.x + gridSize, transform.position.y, transform.position.z));
			}
			if (dir == 2) {
				StopAllCoroutines ();
				canmove = false;
				stopTimer = 2f;
				while (stopTimer >= 0f) {
					StopAllCoroutines ();
					stopTimer -= Time.deltaTime;
				}
				if (stopTimer <= 0f)
					canmove = true;
				StartCoroutine (MoveInGrid (transform.position.x, transform.position.y - gridSize, transform.position.z));
			}
			if (dir == 3) {
				StopAllCoroutines ();
				canmove = false;
				stopTimer = 2f;
				while (stopTimer >= 0f) {
					StopAllCoroutines ();
					stopTimer -= Time.deltaTime;
				}
				if (stopTimer <= 0f)
					canmove = true;
				StartCoroutine (MoveInGrid (transform.position.x - gridSize, transform.position.y, transform.position.z));
			}

		}
	}

	void OnCollisionEnter(Collision col)
	{
		//this.rigidbody.velocity = Vector3.zero;
		//this.rigidbody.angularVelocity = Vector3.zero;
		this.rigidbody.freezeRotation = true;
		ignoreInput = true;
		/*Vector3 fixedpos;
		fixedpos.x = Mathf.Round(transform.position.x);
		fixedpos.y = Mathf.Round(transform.position.y);
		fixedpos.z = Mathf.Round(transform.position.z);
		this.transform.position = fixedpos;
		*/
		float angle = Vector3.Angle(col.contacts [0].normal, Vector3.right);
		hitTimer = .3f;

		if (col.gameObject.tag != "Enemy" && col.gameObject.tag != "Projectile" && col.gameObject.tag != "Item") {
			if (dir == 0) {
				StopAllCoroutines ();
				canmove = false;
				stopTimer = 2f;
				while (stopTimer >= 0f) {
					StopAllCoroutines ();
					stopTimer -= Time.deltaTime;
				}
				if (stopTimer <= 0f)
					canmove = true;
				StartCoroutine (MoveInGrid (transform.position.x, transform.position.y + gridSize, transform.position.z));
			}
			if (dir == 1) {
				StopAllCoroutines ();
				canmove = false;
				stopTimer = 2f;
				while (stopTimer >= 0f) {
					StopAllCoroutines ();
					stopTimer -= Time.deltaTime;
				}
				if (stopTimer <= 0f)
					canmove = true;
				StartCoroutine (MoveInGrid (transform.position.x + gridSize, transform.position.y, transform.position.z));
			}
			if (dir == 2) {
				StopAllCoroutines ();
				canmove = false;
				stopTimer = 2f;
				while (stopTimer >= 0f) {
					StopAllCoroutines ();
					stopTimer -= Time.deltaTime;
				}
				if (stopTimer <= 0f)
					canmove = true;
				StartCoroutine (MoveInGrid (transform.position.x, transform.position.y - gridSize, transform.position.z));
			}
			if (dir == 3) {
				StopAllCoroutines ();
				canmove = false;
				stopTimer = 2f;
				while (stopTimer >= 0f) {
					StopAllCoroutines ();
					stopTimer -= Time.deltaTime;
				}
				if (stopTimer <= 0f)
					canmove = true;
				StartCoroutine (MoveInGrid (transform.position.x - gridSize, transform.position.y, transform.position.z));
			}
		}


		if (col.gameObject.tag == "Enemy") {
			health -= .5f;
			hit = true;
		}
		//if dir = angle blah dont get hit by projectiles
		
		if (angle >= 135f || angle <= -135f) {//hit to the left
			if (col.gameObject.tag == "Enemy") {
				canmove = false;
				StartCoroutine (MoveInGrid (Mathf.Round (transform.position.x - gridSize), Mathf.Round(transform.position.y), transform.position.z));
			}
			if(col.gameObject.tag == "Projectile" && (dir != 3 || attacking == true)) {
				health -= .5f;
				hit = true;
				StartCoroutine (MoveInGrid (Mathf.Round (transform.position.x - gridSize), Mathf.Round(transform.position.y), transform.position.z));
			}

		}
		else if (angle <= 45f && angle >= -45f) {//hit to the right
			if (col.gameObject.tag == "Enemy") {
				canmove = false;
				StartCoroutine (MoveInGrid (Mathf.Round (transform.position.x + gridSize), Mathf.Round(transform.position.y), transform.position.z));
			}
			if(col.gameObject.tag == "Projectile" && (dir != 1 || attacking == true)){
				health -= .5f;
				canmove = false;
				StartCoroutine (MoveInGrid (Mathf.Round (transform.position.x + gridSize), Mathf.Round(transform.position.y), transform.position.z));
			}
		}
		else {
			angle = Vector3.Angle (col.contacts [0].normal, Vector3.up);
			if (angle <= 45f && angle >= -45f) {//hit up
				if (col.gameObject.tag == "Enemy") {
					canmove = false;
					StartCoroutine (MoveInGrid (Mathf.Round(transform.position.x), Mathf.Round(transform.position.y + gridSize), transform.position.z));
				}
				if(col.gameObject.tag == "Projectile" && (dir != 0 || attacking == true)){
					health -= .5f;
					canmove = false;
					StartCoroutine (MoveInGrid (Mathf.Round(transform.position.x), Mathf.Round(transform.position.y + gridSize), transform.position.z));
				}
			}
			else /*if (angle <= 45f && angle >= -45f)*/ {//hit down
				if (col.gameObject.tag == "Enemy") {
					canmove = false;
					StartCoroutine (MoveInGrid (Mathf.Round(transform.position.x), Mathf.Round(transform.position.y - gridSize), transform.position.z));
				}
				if(col.gameObject.tag == "Projectile" && (dir != 2 || attacking == true)){
					health -= .5f;
					canmove = false;
					StartCoroutine (MoveInGrid (Mathf.Round(transform.position.x), Mathf.Round(transform.position.y - gridSize), transform.position.z));
				}
			}
		}
		//fixedpos.x = Mathf.Round(transform.position.x);
		//fixedpos.y = Mathf.Round(transform.position.y);
		//fixedpos.z = Mathf.Round(transform.position.z);
		//this.transform.position = fixedpos;
		if (col.gameObject.tag == "Projectile")
			Destroy (col.gameObject);
	}
	void Update()
	{
		position = this.transform.position;
		dir = animator.GetInteger("Direction");
		//this.rigidbody.velocity = Vector3.zero;
		//this.rigidbody.angularVelocity = Vector3.zero;
		this.rigidbody.freezeRotation = true;
		swordBox.enabled = false;

		if (hitTimer > 0)
				hitTimer -= Time.deltaTime;
		if (hitTimer <= 0) {
			hit = false;
			hitTimer = 0f;
		}

		if (health <= 0f) {
			deadRect.x = 0;//transform.position.x;
			deadRect.y = 0;//transform.position.y;
			deadRect.height = 20;
			deadRect.width = 150;
			GUI.Label(deadRect, "You died. Press R to restart");
			Debug.Log ("You died. Press R to restart");
			Time.timeScale = 0;
			if(Input.GetKeyDown(KeyCode.R)){
				Application.LoadLevel (Application.loadedLevel);
				Time.timeScale = 1;
				health = 3f;
				Debug.Log("");
			}
		}

		//----------------ATTACKING
		attacking = false;
		animator.SetBool ("attacking", false);
		if (Input.GetKeyDown (KeyCode.J) || Input.GetKeyDown (KeyCode.X)) { 
				animator.SetBool ("attacking", true);
			if(attackTimer == 0)
				attackTimer = .35f;
			if(health == 3)
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
		if (hit == false) {
			if ((Input.GetKey (KeyCode.UpArrow) == true || Input.GetKey (KeyCode.W) == true) 
				&& canmove == true && attacking == false && ignoreInput == false) {
				animator.SetInteger ("Direction", 2);
				swordPosition.x = .4f;
				swordPosition.y = .5f;
				swordSize.x = .2f;
				swordSize.y = .8f;
				swordBox.center = swordPosition;
				swordBox.size = swordSize;
				canmove = false;
				StartCoroutine (MoveInGrid ((float)transform.position.x, (float)transform.position.y + gridSize, (float)transform.position.z));
			}
			if ((Input.GetKey (KeyCode.RightArrow) == true || Input.GetKey (KeyCode.D) == true) 
			    && canmove == true && attacking == false && ignoreInput == false) {
				animator.SetInteger ("Direction", 3);
				swordPosition.x = 1.3f;
				swordPosition.y = -.53f;
				swordSize.x = .8f;
				swordSize.y = .2f;
				swordBox.center = swordPosition;
				swordBox.size = swordSize;
				canmove = false;
				StartCoroutine (MoveInGrid ((float)transform.position.x + gridSize, (float)transform.position.y, (float)transform.position.z));
			}
			if ((Input.GetKey (KeyCode.LeftArrow) == true || Input.GetKey (KeyCode.A) == true) 
			    && canmove == true && attacking == false && ignoreInput == false) {
				animator.SetInteger ("Direction", 1);
				swordPosition.x = -.45f;
				swordPosition.y = -.53f;
				swordSize.x = .8f;
				swordSize.y = .2f;
				swordBox.center = swordPosition;
				swordBox.size = swordSize;
				canmove = false;
				StartCoroutine (MoveInGrid ((float)transform.position.x - gridSize, (float)transform.position.y, (float)transform.position.z));
			}
			if ((Input.GetKey (KeyCode.DownArrow) == true || Input.GetKey (KeyCode.S) == true) 
			    && canmove == true && attacking == false && ignoreInput == false) {
				animator.SetInteger ("Direction", 0);
				swordPosition.x = .53f;
				swordPosition.y = -1.3f;
				swordSize.x = .2f;
				swordSize.y = .8f;
				swordBox.center = swordPosition;
				swordBox.size = swordSize;
				canmove = false;
				StartCoroutine (MoveInGrid ((float)transform.position.x, (float)transform.position.y - gridSize, (float)transform.position.z));
			}
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
		stuckTimer = 0f;
		//x = Mathf.Round(x);
		//y = Mathf.Round(y);
		//z = Mathf.Round(z);
		while ((transform.position.x != x || transform.position.y != y) && stuckTimer <= .5f /*|| transform.position.z != z*/)
		{
			stuckTimer += Time.deltaTime;
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
			/*Vector3 hitPos;
			hitPos.x = x;
			hitPos.y = y;
			hitPos.z = z;
			if(hit){
				transform.position = Vector3.Lerp(transform.position, hitPos, 1);
				//targetPosition = Vector3.zero;
			}
			else*/
				//transform.position = Vector3.Lerp(transform.position, transform.position + targetPosition, 1);
				transform.Translate(targetPosition);
			//if(stuckTimer >= .2f)
			//	transform.position = hitPos;

			yield return 0;
		}
		//the work is ended now congratulation
		canmove = true;
		//canmove = !PauseMovement.isTimeStopped ();
	}
}

