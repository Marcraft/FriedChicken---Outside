using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLadder : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			PlayerController player = other.gameObject.GetComponent<PlayerController> ();
			player.canClimb += 1;



			if (player.state == PlayerController.State.climbing) {
				player.transform.position = new Vector2 (transform.position.x, player.transform.position.y);
			}
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			PlayerController player = other.gameObject.GetComponent<PlayerController> ();
			player.canClimb -= 1;
		}
	}
}
