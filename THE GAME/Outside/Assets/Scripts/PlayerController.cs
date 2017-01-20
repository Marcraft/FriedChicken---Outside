using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 50f;
	public float jumpPower = 150f;
	public bool onPlatform = false;

	private Animator animator;
	private Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		animator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
		animator.SetBool("OnPlatform", onPlatform);
		if (Input.GetAxis ("Horizontal") > 0.1f) {
			transform.localScale = new Vector3 (1, 1, 1);
		}
		if (Input.GetAxis ("Horizontal") < -0.1f) {
			transform.localScale = new Vector3 (-1, 1, 1);
		}
	}
	void FixedUpdate() {
		if (onPlatform) {
			if (Input.GetKey ("space")) {
				rb2d.AddForce (new Vector2 (0, jumpPower));
				onPlatform = false;
			}
			if (!Input.GetKey ("a") && !Input.GetKey ("d")) {
				rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
			}
			if (Input.GetKey ("d")) {
				rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
			}
			if (Input.GetKey ("a")) {
				rb2d.velocity = new Vector2 (-speed, rb2d.velocity.y);
			}
		} else {
			if (!Input.GetKey ("a") && !Input.GetKey ("d")) {
				rb2d.AddForce (new Vector2 (-rb2d.velocity.x, 0));
			}
			if (Input.GetKey ("d") && rb2d.velocity.x < speed) {
				rb2d.AddForce (new Vector2 (speed*2, 0));
			}
			if (Input.GetKey ("a") && rb2d.velocity.x > -speed) {
				rb2d.AddForce (new Vector2 (-speed*2, 0));
			}
		}
	}
}
