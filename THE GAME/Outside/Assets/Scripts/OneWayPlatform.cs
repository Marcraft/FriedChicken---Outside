using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour {
	public Collider2D platform;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			platform.enabled = false;
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			platform.enabled = true;
		}
	}

}
