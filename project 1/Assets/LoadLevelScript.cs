using UnityEngine;
using System.Collections;

public class LoadLevelScript : MonoBehaviour {

	void OnGUI () {
		if (GUI.Button (new Rect (10, 10, 400, 30), "Overworld")) {
						Application.LoadLevel ("project1");
				}
		if (GUI.Button (new Rect (10, 50, 400, 30), "Load Custom Level")) {
						Application.LoadLevel ("scene2");
				}
	}
}
