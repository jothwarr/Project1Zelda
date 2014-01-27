using UnityEngine;
using System.Collections;

public class WallCollision : MonoBehaviour {

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.name == "Link")
		{
			col.rigidbody.position = Vector3.zero;
		}
	}
}
