using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiritTree : MonoBehaviour {
	public PlayerController player;
	private float target;
	private float opacity;
	private float difference;
	private bool pulse;
	private float pulsing;
	public float scale;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
		opacity = 0;
		pulse = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.state == PlayerController.State.outofbody) {
			target = 1;
		} else {
			target = 0;
			pulse = false;
		}
	}
	void FixedUpdate() {
		if (pulse) {
			pulsing += Time.deltaTime;
			if (pulsing > Mathf.PI*2) {
				pulsing = 0;
			}
			GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, opacity + (Mathf.Cos(pulsing)-1)/4);
		} else {
			difference = target - opacity;
			opacity = opacity + difference / scale;
			if (opacity < 0.01f) {
				opacity = 0;
			}
			if (opacity > 0.99f) {
				opacity = 1;
				pulse = true;
				pulsing = 0;
			}
			GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, opacity);
		}



	}
}
