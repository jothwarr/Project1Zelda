using UnityEngine;
using System.Collections;

public class HeartScript : MonoBehaviour {

	LinkMovement2 linkScript;
	GameObject linkObject;

	void OnCollisionExit(Collision col)
	{
		linkScript.ignoreInput = false;
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Link") {
			linkScript.ignoreInput = false;
			Destroy (this.gameObject);
			linkScript.health += 1f;
			if (linkScript.health >= 3f)
				linkScript.health = 3f;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag != "Sword") {
			linkScript.ignoreInput = false;
			Destroy (this.gameObject);
			linkScript.health += 1f;
			if (linkScript.health >= 3f)
				linkScript.health = 3f;
		}
	}

	// Use this for initialization
	void Start () {
		linkObject = GameObject.Find("Link");
		linkScript = linkObject.GetComponent<LinkMovement2>();
		Destroy (this.gameObject, 8f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
