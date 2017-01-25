using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLadder : MonoBehaviour {
	public bool topLadder;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			PlayerController player = other.gameObject.GetComponent<PlayerController> ();
			player.onLadder += 1;
			if (player.isClimbing) {
				player.transform.position = new Vector2 (transform.position.x, player.transform.position.y);
			}
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			PlayerController player = other.gameObject.GetComponent<PlayerController> ();
			player.onLadder -= 1;
		}
	}
}
