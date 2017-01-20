using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCheck : MonoBehaviour {
	private PlayerController playercontroller;

	void Start() {
		playercontroller = gameObject.GetComponentInParent<PlayerController> ();
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Platform")) {
			playercontroller.onPlatform = true;
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Platform")) {
			playercontroller.onPlatform = false;
		}
	}
}
