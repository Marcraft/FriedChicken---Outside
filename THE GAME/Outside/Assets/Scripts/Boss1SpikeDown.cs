using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1SpikeDown : MonoBehaviour {
	public PlayerController player;
	public GameObject spikeUp;
	private float target;
	private float opacity;
	private float difference;
	private bool pulse;
	private float pulsing;
	public float scale;
	private bool spawnSpikeUp;
	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (1, 0, 1);
		transform.Rotate (Vector3.forward*180);
		transform.Rotate (Vector3.forward*(Random.Range(-40,40)));
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
		opacity = 0;
		pulse = false;
		spawnSpikeUp = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.state == PlayerController.State.outofbody) {
			target = 1;
		} else {
			target = 0;
			pulse = false;
		}
		transform.localScale += new Vector3 (0, Time.deltaTime, 0);
		if (transform.localScale.y > 1 && !spawnSpikeUp) {
			transform.localScale += new Vector3 (0, Time.deltaTime * 0.1f, 0);
			Instantiate (spikeUp, new Vector3 (player.transform.position.x + (Random.Range (-4f, 4f)), transform.position.y - 3, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
			spawnSpikeUp = true;
		}
		if (transform.localScale.y > 1.5) {
			Destroy (this.gameObject);
		}
	}
	void FixedUpdate() {
		if (pulse) {
			pulsing += Time.deltaTime;
			if (pulsing > Mathf.PI*2) {
				pulsing = 0;
			}
			GetComponentInChildren<SpriteRenderer> ().color = new Color (1f, 1f, 1f, opacity + (Mathf.Cos(pulsing)-1)/4);
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
			GetComponentInChildren<SpriteRenderer> ().color = new Color (1f, 1f, 1f, opacity);
		}



	}
}
  