using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float groundControl;
	public float airControl;
	public float speed;
	public float jumpSpeed;
	public bool onGround;
	public bool onPlatform;
	public bool facingRight;
	public float platformDrop;

	public string leftKey;
	public string rightKey;
	public string upKey;
	public string downKey;
	public string jumpKey;


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
		animator.SetBool("OnGround", onGround);
		if (Input.GetKey (leftKey)) {
			transform.localScale = new Vector3 (-1, 1, 1);
			facingRight = false;
		}
		if (Input.GetKey (rightKey)) {
			transform.localScale = new Vector3 (1, 1, 1);
			facingRight = true;
		}
		if (Input.GetKeyDown (jumpKey) && onGround && canJump) {
			if (Input.GetKey (downKey) && onPlatform) {
				rb2d.position = new Vector2 (rb2d.position.x, rb2d.position.y - platformDrop);
			} else {
				rb2d.velocity = new Vector2 (rb2d.velocity.x, jumpSpeed);
				jumpTimer = 0.5f;
			}
			canJump = false;
		}
		if (Input.GetKeyUp (jumpKey)) {
			rb2d.velocity = new Vector2 (rb2d.velocity.x, rb2d.velocity.y * jumpTimer);
			canJump = true;
		}
	}
	void FixedUpdate() {
		if (Input.GetKey (jumpKey)) {
			if (jumpTimer < 1) {
				jumpTimer += 0.05f;
			}
		}
		if (onGround) {
			if (!Input.GetKey (leftKey) && !Input.GetKey (rightKey)) {
				if (-0.2 < rb2d.velocity.x && rb2d.velocity.x < 0.2) {
					rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
				}
				else if (rb2d.velocity.x > 0) {
					rb2d.AddForce (new Vector2 (-groundControl, 0));
				}
				else if (rb2d.velocity.x < 0) {
					rb2d.AddForce (new Vector2 (groundControl, 0));
				}
			}
			if (Input.GetKey (leftKey)) {
				if (rb2d.velocity.x > -speed) {
					rb2d.AddForce (new Vector2 (-groundControl, 0));
				}
			}
			if (Input.GetKey (rightKey)) {
				if (rb2d.velocity.x < speed) {
					rb2d.AddForce (new Vector2 (groundControl, 0));
				}
			}
		} else {
			if (Input.GetKey (leftKey)) {
				if (rb2d.velocity.x > -speed) {
					rb2d.AddForce (new Vector2 (-airControl, 0));
				}
			}
			if (Input.GetKey (rightKey)) {
				if (rb2d.velocity.x < speed) {
					rb2d.AddForce (new Vector2 (airControl, 0));
				}
			}
		}
	}
}
