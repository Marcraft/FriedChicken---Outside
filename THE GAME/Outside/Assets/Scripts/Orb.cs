using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour {
	public PlayerController player;
	private float floating;
	private Vector3 startPos;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		floating += Time.deltaTime*2;
		if (floating > Mathf.PI*2) {
			floating = 0;
		}
		transform.position= startPos + new Vector3 (0, Mathf.Cos(floating)/4);
	}
	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player")){
			GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("elixir");
			if (other.GetComponent<PlayerController> ().elixir < other.GetComponent<PlayerController> ().maxElixir) {
				other.GetComponent<PlayerController> ().elixir += 1;
			}
			if (other.GetComponent<PlayerController> ().health < other.GetComponent<PlayerController> ().maxHealth) {
				other.GetComponent<PlayerController> ().health += 0.2f;
				if (other.GetComponent<PlayerController> ().health > other.GetComponent<PlayerController> ().maxHealth) {
					other.GetComponent<PlayerController> ().health = other.GetComponent<PlayerController> ().maxHealth;
				}
			}
			Destroy (this.gameObject);
		}
	}
}
