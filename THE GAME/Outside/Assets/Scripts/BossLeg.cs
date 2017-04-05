using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLeg : MonoBehaviour {
	public bool touchingGround;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Ground")) {
			touchingGround = true;
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		if(other.CompareTag("Ground")) {
			touchingGround = false;
		}
	}
}
