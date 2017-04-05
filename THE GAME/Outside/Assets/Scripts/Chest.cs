using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {
	public Sprite closed;
	public Sprite opened;
	GameObject menuControls;
	bool open;
	// Use this for initialization
	void Start () {
		menuControls = GameObject.FindWithTag ("MenuControls");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player") && !open) {
			GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("chestOpen");
			GetComponent<SpriteRenderer> ().sprite = opened;
			menuControls.GetComponent<MenuControls> ().haveKey = true;
			other.GetComponent<PlayerController> ().hasKey = true;
			open = true;
		}
	}
}
