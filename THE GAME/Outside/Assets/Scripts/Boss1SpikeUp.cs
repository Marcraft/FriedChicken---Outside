using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1SpikeUp : MonoBehaviour {

	public PlayerController player;
	public GameObject spiritSpike;
	public GameObject orb;
	private float target;
	private float opacity;
	private float difference;
	private bool pulse;
	private float pulsing;
	public float scale;
	private float startingDepth;
	private bool attacked;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
		opacity = 0;
		pulse = false;
		startingDepth = transform.position.y;
		attacked = false;
	}

	// Update is called once per frame
	void Update () {
		if (player.state == PlayerController.State.outofbody) {
			target = 1;
		} else {
			target = 0;
			pulse = false;
		}
		if (!attacked) {
			if (transform.position.y < startingDepth + 2) {
				transform.position += new Vector3 (0, Time.deltaTime, 0);
			} else if (transform.position.y < startingDepth + 4) {
				transform.position += new Vector3 (0, Time.deltaTime * 4, 0);
			} else {
				attacked = true;
				if (Random.Range (0f, 2f) < 1) {
					Instantiate (orb, new Vector3 (transform.position.x , transform.position.y, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
			}
		}
		if (attacked) {
			transform.position += new Vector3 (0, -Time.deltaTime * 3, 0);
		}
		if (transform.position.y < startingDepth - 0.1f) {
			Destroy (this.gameObject);
		}
	}
	void FixedUpdate() {
		if (pulse) {
			pulsing += Time.deltaTime;
			if (pulsing > Mathf.PI*2) {
				pulsing = 0;
			}
			spiritSpike.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, opacity + (Mathf.Cos(pulsing)-1)/4);
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
			spiritSpike.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, opacity);
		}
	}
	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("Player")){
			other.gameObject.GetComponent <PlayerController>().health--;
			other.gameObject.GetComponent <PlayerController> ().hurt = true;
			Destroy (this.gameObject);
		}
	}
}
