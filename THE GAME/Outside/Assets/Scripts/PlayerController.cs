using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float groundControl;
	public float airControl;
	public float speed;
	public float jumpSpeed;
	public bool onPlatform;
	public bool facingRight;

	private bool canJump;
	private float jumpTimer;

	private Animator animator;
	private Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		animator = gameObject.GetComponent<Animator> ();
		canJump = true;
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
		animator.SetBool("OnPlatform", onPlatform);
		if (Input.GetKey ("a")) {
			transform.localScale = new Vector3 (-1, 1, 1);
			facingRight = false;
		}
		if (Input.GetKey ("d")) {
			transform.localScale = new Vector3 (1, 1, 1);
			facingRight = true;
		}
		if (Input.GetKeyDown ("space") && onPlatform && canJump) {
			rb2d.velocity = new Vector2 (rb2d.velocity.x, jumpSpeed);
			jumpTimer = 0.5f;
			canJump = false;
		}
		if (Input.GetKeyUp ("space")) {
			rb2d.velocity = new Vector2 (rb2d.velocity.x, rb2d.velocity.y * jumpTimer);
			canJump = true;
		}
	}
	void FixedUpdate() {
		if (Input.GetKey ("space")) {
			if (jumpTimer < 1) {
				jumpTimer += 0.05f;
			}
		}
		if (onPlatform) {
			if (!Input.GetKey ("a") && !Input.GetKey ("d")) {
				if (-0.1 < rb2d.velocity.x && rb2d.velocity.x < 0.1) {
					rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
				}
				else if (rb2d.velocity.x > 0) {
					rb2d.AddForce (new Vector2 (-groundControl, 0));
				}
				else if (rb2d.velocity.x < 0) {
					rb2d.AddForce (new Vector2 (groundControl, 0));
				}
			}
			if (Input.GetKey ("a")) {
				if (rb2d.velocity.x > -speed) {
					rb2d.AddForce (new Vector2 (-groundControl, 0));
				}
			}
			if (Input.GetKey ("d")) {
				if (rb2d.velocity.x < speed) {
					rb2d.AddForce (new Vector2 (groundControl, 0));
				}
			}
		} else {
			if (Input.GetKey ("a")) {
				if (rb2d.velocity.x > -speed) {
					rb2d.AddForce (new Vector2 (-airControl, 0));
				}
			}
			if (Input.GetKey ("d")) {
				if (rb2d.velocity.x < speed) {
					rb2d.AddForce (new Vector2 (airControl, 0));
				}
			}
		}
	}
}
