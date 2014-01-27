using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	Transform link;
	Rect room;
	Vector3 translation = new Vector3(0f, 0f, 0f);
	static float roomWidth = 24f;
	static float roomHeight = 12.8f;
	static float speed = roomWidth;

	// Use this for initialization
	void Start () {
		link = GameObject.Find ("Link").transform;
		room = new Rect (link.position.x - (roomWidth / 2), link.position.y + (roomHeight / 2), roomWidth, roomHeight);
	}
	
	// Update is called once per frame
	void Update () {
		Rect newRect = room;
		if (link.position.x > room.x + room.width) {
			newRect.x += newRect.width;
			translation.x += newRect.width;
		}
		if (link.position.x < room.x) {
			newRect.x -= newRect.width;
			translation.x -= newRect.width;
		}
		if (link.position.y > room.y) {
			newRect.y += newRect.height;
			translation.y += newRect.height;
		}
		if (link.position.y < room.y - room.height) {
			newRect.y -= newRect.height;
			translation.y -= newRect.height;
		}
		room = newRect;
		Vector3 move = translation.normalized * Time.deltaTime * speed;
		if (translation.magnitude < move.magnitude)
						move = translation;
		Camera.main.transform.Translate (move);
		translation -= move;
	}
}
