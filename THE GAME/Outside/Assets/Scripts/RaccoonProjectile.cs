using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonProjectile : MonoBehaviour {
	public bool goingRight;
	public float speed;
	private float opacity;
	private bool hitPlayer;
	private bool hitOther;
	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = gameObject.GetComponent<Rigidbody2D> ();
		if (goingRight) {
			transform.localScale = new Vector3 (1, 1, 1);
			rigidBody.velocity = new Vector2 (speed, speed);
		} else {
			transform.localScale = new Vector3 (-1, 1, 1);
			rigidBody.velocity = new Vector2 (-speed, speed);
		}
		opacity = 1;
	}
	
	// Update is called once per frame
	void Update () {
		float rotateSpeed =  -rigidBody.velocity.x * 50;
		transform.Rotate(0,0,rotateSpeed * Time.deltaTime);
		if (opacity <= 0) {
			Destroy (gameObject);
		}
		if (hitPlayer && !hitOther) {
			Destroy (gameObject);
		}
		if (hitOther) {
			opacity -= Time.deltaTime;
			GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, opacity);
			GetComponent<Collider2D> ().isTrigger = false;
		}
	}
	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Ground") || other.CompareTag("Platform") || other.CompareTag("Boulder")){
			hitOther = true;
			rigidBody.velocity = new Vector2 (0, 0);
		}
		if(other.CompareTag("Player")){
			hitPlayer = true;
			other.GetComponent <PlayerController>().health--;
			other.GetComponent <PlayerController> ().hurt = true;
		}
		if(other.CompareTag("Arrow") || other.CompareTag("Attack")){
			hitPlayer = true;
		}

	}
}
