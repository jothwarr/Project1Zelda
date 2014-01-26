using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	Transform link;
	Rect room;

	// Use this for initialization
	void Start () {
		link = GameObject.Find ("Link").transform;
		room = new Rect (0, 0, 12f, 12.8f);
	}
	
	// Update is called once per frame
	void Update () {
		Rect newRect = room;
		Vector3 translation = new Vector3 (0f, 0f, 0f);
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
		Camera.main.transform.Translate (translation);
	}
}
