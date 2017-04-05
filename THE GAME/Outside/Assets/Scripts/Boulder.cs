using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Slab") {
			GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("boulderDrop");
			Destroy (this.gameObject);
		}
	}
	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player")){
			other.GetComponent <PlayerController>().health = -1;
			other.GetComponent <PlayerController> ().hurt = true;
		}
		if(other.CompareTag("Enemy")){
			GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("boulderDrop");
			other.GetComponent <Enemy>().health = -1;
			other.GetComponent <Enemy> ().hurt = true;
		}
	}
}
