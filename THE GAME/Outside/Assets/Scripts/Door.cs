using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public Sprite closedDoor;
	public Sprite openDoor;
	public bool unlock;
	GameObject menuControls;
	// Use this for initialization
	void Start () {
		unlock = false;
		menuControls = GameObject.FindWithTag ("MenuControls");
	}
	
	// Update is called once per frame
	void Update () {
		if (unlock) {
			GetComponent<BoxCollider2D> ().enabled = false;
			GetComponent<SpriteRenderer> ().sprite = openDoor;
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
			if (other.gameObject.GetComponent<PlayerController> ().hasKey) {
				unlock = true;
				other.gameObject.GetComponent<PlayerController> ().hasKey = false;
				menuControls.GetComponent<MenuControls> ().haveKey = false;
			}
		}
	}
}
