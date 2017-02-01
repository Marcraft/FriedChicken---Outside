using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	public bool goingRight;
	public float speed;
	private float opacity;
	private bool hitEnemy;
	private bool hitOther;
	private Rigidbody2D rigidBody;
	// Use this for initialization
	void Start () {
		rigidBody = gameObject.GetComponent<Rigidbody2D> ();
		if (goingRight) {
			transform.localScale = new Vector3 (1, 1, 1);
			rigidBody.velocity = new Vector2 (speed, 0);
		} else {
			transform.localScale = new Vector3 (-1, 1, 1);
			rigidBody.velocity = new Vector2 (-speed, 0);
		}
		opacity = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (opacity <= 0) {
			Destroy (gameObject);
		}
		if (hitEnemy) {
			Destroy (gameObject);
		}
		if (hitOther) {
			opacity -= Time.deltaTime;
			GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, opacity);
			if (rigidBody.isKinematic) {
				if (goingRight) {
					rigidBody.velocity = new Vector2 (-speed / 20, speed / 10);
				} else {
					rigidBody.velocity = new Vector2 (speed / 20, speed / 10);
				}
			}
			float rotateSpeed =  -rigidBody.velocity.x * 200;
			transform.Rotate(0,0,rotateSpeed * Time.deltaTime);
			rigidBody.isKinematic = false;
		}
	}
	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Ground") || other.CompareTag("Platform") || other.CompareTag("Boulder")){
			hitOther = true;
			rigidBody.velocity = new Vector2 (0, 0);
		}

	}
}
