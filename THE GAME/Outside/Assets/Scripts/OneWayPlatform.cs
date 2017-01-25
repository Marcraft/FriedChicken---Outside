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
			for (int i = 0; i < other.gameObject.GetComponents<Collider2D> ().Length; i++) {
				Physics2D.IgnoreCollision (other.gameObject.GetComponents<Collider2D> ()[i], platform);
			}
			for (int i = 0; i < other.gameObject.GetComponentsInChildren<Collider2D> ().Length; i++) {
				Physics2D.IgnoreCollision (other.gameObject.GetComponentsInChildren<Collider2D> ()[i], platform);
			}
			//platform.enabled = false;
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			for (int i = 0; i < other.gameObject.GetComponents<Collider2D> ().Length; i++) {
				Physics2D.IgnoreCollision (other.gameObject.GetComponents<Collider2D> ()[i], platform, false);
			}
			for (int i = 0; i < other.gameObject.GetComponentsInChildren<Collider2D> ().Length; i++) {
				Physics2D.IgnoreCollision (other.gameObject.GetComponentsInChildren<Collider2D> ()[i], platform, false);
			}
			//platform.enabled = true;
		}
	}

}
