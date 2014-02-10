using UnityEngine;
using System.Collections;

public class HeartScript : MonoBehaviour {

	LinkMovement2 linkScript;
	GameObject linkObject;

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Link") {
			Destroy (this.gameObject);
			linkScript.health += 1f;
			if (linkScript.health >= 3f)
				linkScript.health = 3f;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag != "Sword") {
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
