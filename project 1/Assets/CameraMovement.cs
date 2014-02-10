using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	Transform link;
	//LinkMovement2 linkInstance;
	Rect room;
	Vector3 translation = new Vector3(0f, 0f, 0f);
	Vector3 move = new Vector3 (0f, 0f, 0f);
	//float linkX;
	//float linkY;
	const float roomWidth = 16f;
	const float roomHeight = 11f;
	const float speed = roomWidth;
	const float tileWidth = 1f;
	const float give = .1f;
	bool isMoving = false;

	float truncate (float input) {
		return Mathf.Round (input * 100f) / 100f;
	}

	// Use this for initialization
	void Start () {
		link = GameObject.Find ("Link").transform;
		//linkInstance = GameObject.Find ("Link").GetComponent<LinkMovement2> ();
		room = new Rect (112f, -77f, roomWidth, roomHeight);
		//linkX = link.position.x;
		//linkY = link.position.y;
	}
	
	// Update is called once per frame
	void Update () {

		//Calculate distance to next room
		if (!isMoving) {
			Rect newRect = room;
			if (truncate(link.position.x) > room.x + room.width - tileWidth + give) {
				newRect.x += newRect.width;
				translation.x += newRect.width;
				//linkX = link.position.x + tileWidth;
			}
			if (truncate(link.position.x) < room.x - give) {
				newRect.x -= newRect.width;
				translation.x -= newRect.width;
				//linkX = link.position.x - tileWidth;
			}
			if (truncate(link.position.y) > room.y + give) {
				newRect.y += newRect.height;
				translation.y += newRect.height;
				//linkY = link.position.y + tileWidth;
			}
			if (truncate(link.position.y) < room.y - room.height + tileWidth - give) {
				newRect.y -= newRect.height;
				translation.y -= newRect.height;
				//linkY = link.position.y - tileWidth;
			}
			room = newRect;
		}

		//Calculate distance to move this frame
		if (translation.magnitude != 0) {
			move = translation.normalized * Time.deltaTime * speed;
			if (translation.magnitude < move.magnitude)
					move = translation;
		} else {
			move = translation;
		}
		
		//Begin movement to other room
		if (!PauseMovement.isTimeStopped() && move.magnitude >= give) {
			PauseMovement.stopEverything ();
			isMoving = true;
			//StartCoroutine(linkInstance.MoveInGrid(linkX, linkY, link.position.z));
		}

		//Move camera
		Camera.main.transform.Translate (move);
		translation -= move;

		//End movement to other room
		if (PauseMovement.isTimeStopped() && translation.magnitude <= give) {
			PauseMovement.startEverything ();
			PauseMovement.unfreezeTime();
			isMoving = false;
		}
	}
}