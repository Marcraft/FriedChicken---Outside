using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritBoulder : MonoBehaviour {
	public PlayerController player;
	private float target;
	private float opacity;
	private float difference;
	public float scale;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
		opacity = 0;
	}

	// Update is called once per frame
	void Update () {
		if (player.state == PlayerController.State.outofbody) {
			target = 1;
		} else {
			target = 0;
		}
	}
	void FixedUpdate() {
		difference = target - opacity;
		opacity = opacity + difference / scale;
		if (opacity < 0.01) {
			opacity = 0;
		}
		GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, opacity);
	}
}
