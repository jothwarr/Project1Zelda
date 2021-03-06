﻿using UnityEngine;
using System.Collections;

public class TriforceScript : MonoBehaviour {

	LinkMovement2 linkScript;
	GameObject linkObject;

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Link") {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag != "Sword") {
			Destroy (this.gameObject);
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
