using UnityEngine;
using System.Collections;

public class HeartScript : MonoBehaviour {

	LinkMovement2 linkScript;
	GameObject linkObject;

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Link") {
			linkScript.health += 1.5f;
			Destroy (this.gameObject);
			if (linkScript.health >= 3f)
				linkScript.health = 3f;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag != "Sword") {
			linkScript.health += 1f;
			Destroy (this.gameObject);
			if (linkScript.health >= 3f)
				linkScript.health = 3f;
		}
	}

	// Use this for initialization
	void Start () {
		linkObject = GameObject.Find("Link");
		linkScript = linkObject.GetComponent<LinkMovement2>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
