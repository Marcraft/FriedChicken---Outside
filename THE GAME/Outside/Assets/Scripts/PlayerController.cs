using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public GameObject spirit;

	public float groundControl;
	public float airControl;
	public float spiritControl;
	public float speed;
	public float jumpStrength;
	public float climbingSpeed;
	public float platformDrop;
	public int onLadder;
	public bool onPlatform;
	public bool onGround;
	public bool isClimbing;
	public bool facingRight;

	public string leftKey;
	public string rightKey;
	public string upKey;
	public string downKey;
	public string jumpKey;
	public string oobKey;
	public string rangedKey;


	private bool inOOB;
	private bool crouching;
	private bool canJump;
	private bool shooting;
	private float jumpTimer;
	private Animator animator;
	private Rigidbody2D rigidBody;
	private Rigidbody2D spiritBody;
	public bool changeScene;

	// Use this for initialization
	void Start ()
	{
		rigidBody = gameObject.GetComponent<Rigidbody2D> ();
		spiritBody = spirit.GetComponent<Rigidbody2D> ();
		animator = gameObject.GetComponent<Animator> ();
		canJump = true;
		inOOB = false;
		isClimbing = false;
		onPlatform = false;
		changeScene = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		animator.SetFloat ("Speed", Mathf.Abs (rigidBody.velocity.x));
		animator.SetBool ("OnGround", onGround);
		animator.SetBool ("inOOB", inOOB);
		animator.SetBool ("Climbing", isClimbing);
		animator.SetBool ("Crouching", crouching);
		animator.SetBool ("Shooting", shooting);
		//IN OUT OF BODY
		if (inOOB) {
			if (Input.GetKeyDown (oobKey)) {
				spiritBody.velocity = new Vector2 (0, 0);
				spirit.transform.localPosition = new Vector2 (0, 0);
				spirit.transform.localScale = new Vector3 (1, 1, 1);
				spirit.GetComponent<SpriteRenderer> ().enabled = false;
				inOOB = false;
			}
			if (Input.GetKey (leftKey)) {
				if (facingRight)
					spirit.transform.localScale = new Vector3 (-1, 1, 1);
				else
					spirit.transform.localScale = new Vector3 (1, 1, 1);
			}
			if (Input.GetKey (rightKey)) {
				if (facingRight)
					spirit.transform.localScale = new Vector3 (1, 1, 1);
				else
					spirit.transform.localScale = new Vector3 (-1, 1, 1);
			}
		} else if (onLadder > 0) {
			if (!isClimbing && (Input.GetKey (upKey))) {
				rigidBody.velocity = new Vector2 (0, 0);
				rigidBody.isKinematic = true;
				isClimbing = true;
			}
			if (onGround && isClimbing) {
				isClimbing = false;
				rigidBody.isKinematic = false;

			}
			if (isClimbing) {
				if (Input.GetKeyDown (jumpKey) && canJump) {
					rigidBody.velocity = new Vector2 (rigidBody.velocity.x, jumpStrength / 2);
					jumpTimer = 0.5f;
					canJump = false;
					rigidBody.isKinematic = false;
					isClimbing = false;
				}
			}
				
		} else {
			if (isClimbing) {
				isClimbing = false;
				rigidBody.isKinematic = false;
			}
			if (Input.GetKeyDown (oobKey) && !shooting) {
				spiritBody.velocity = new Vector2 (0, 0);
				spirit.transform.localPosition = new Vector2 (0, 0);
				spirit.transform.localScale = new Vector3 (1, 1, 1);
				spirit.GetComponent<SpriteRenderer> ().enabled = true;
				inOOB = true;
			}
			if (Input.GetKey (leftKey)) {
				transform.localScale = new Vector3 (-1, 1, 1);
				facingRight = false;
			}
			if (Input.GetKey (rightKey)) {
				transform.localScale = new Vector3 (1, 1, 1);
				facingRight = true;
			}
			if (Input.GetKeyDown (rangedKey)) {
				shooting = true;
			}
			if (Input.GetKeyUp (rangedKey)) {
				shooting = false;
			}
		}
		if (onGround) {
			if (Input.GetKey (downKey)) {
				crouching = true;
			} else {
				crouching = false;
			}
			if (Input.GetKeyDown (jumpKey) && canJump) {
				if (Input.GetKey (downKey) && onPlatform) {
					rigidBody.position = new Vector2 (rigidBody.position.x, rigidBody.position.y - platformDrop);
				} else {
					rigidBody.velocity = new Vector2 (rigidBody.velocity.x, jumpStrength);
					jumpTimer = 0.5f;
				}
				canJump = false;
			}
		}
		if (Input.GetKeyUp (jumpKey)) {
			rigidBody.velocity = new Vector2 (rigidBody.velocity.x, rigidBody.velocity.y * jumpTimer);
			canJump = true;
		}
			

	}

	void FixedUpdate ()
	{
		if (Input.GetKey (jumpKey) && jumpTimer < 1) {
			jumpTimer += 0.05f;
		}
		if (onLadder > 0) {
			if (isClimbing) {
				if (Input.GetKey (upKey)) {
					rigidBody.transform.Translate (new Vector2 (0, climbingSpeed));
				}
				if (Input.GetKey (downKey)) {
					rigidBody.transform.Translate (new Vector2 (0, -climbingSpeed));
				}
			}
		}
		if (inOOB) {
			//spirit
			if (Input.GetKey (upKey) && spiritBody.velocity.y < speed)
				spiritBody.AddForce (new Vector2 (0, spiritControl));
			if (Input.GetKey (downKey) && spiritBody.velocity.y > -speed)
				spiritBody.AddForce (new Vector2 (0, -spiritControl));
			if (Input.GetKey (leftKey) && spiritBody.velocity.x > -speed)
				spiritBody.AddForce (new Vector2 (-spiritControl, 0));
			if (Input.GetKey (rightKey) && spiritBody.velocity.x < speed)
				spiritBody.AddForce (new Vector2 (spiritControl, 0));
			//human
			if (onGround) {
				if (-0.2 < rigidBody.velocity.x && rigidBody.velocity.x < 0.2)
					rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);
				else if (rigidBody.velocity.x > 0)
					rigidBody.AddForce (new Vector2 (-groundControl, 0));
				else if (rigidBody.velocity.x < 0)
					rigidBody.AddForce (new Vector2 (groundControl, 0));
			}
		} else {
			if (onGround) {
				if (((!Input.GetKey (leftKey) && !Input.GetKey (rightKey)) || crouching || shooting)) {
					if (-0.2 < rigidBody.velocity.x && rigidBody.velocity.x < 0.2) {
						rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);
					} else if (rigidBody.velocity.x > 0) {
						rigidBody.AddForce (new Vector2 (-groundControl, 0));
					} else if (rigidBody.velocity.x < 0) {
						rigidBody.AddForce (new Vector2 (groundControl, 0));
					}
				}
				if (!crouching && !shooting) {
					if (Input.GetKey (leftKey) && rigidBody.velocity.x > -speed)
						rigidBody.AddForce (new Vector2 (-groundControl, 0));
					if (Input.GetKey (rightKey) && rigidBody.velocity.x < speed)
						rigidBody.AddForce (new Vector2 (groundControl, 0));
				}
			} else {
				if (Input.GetKey (leftKey) && rigidBody.velocity.x > -speed)
					rigidBody.AddForce (new Vector2 (-airControl, 0));
				if (Input.GetKey (rightKey) && rigidBody.velocity.x < speed)
					rigidBody.AddForce (new Vector2 (airControl, 0));
			}
		}
	}

	public void setKinematic ()
	{
		rigidBody.isKinematic = true;
	}

	public void setDynamic ()
	{
		rigidBody.isKinematic = false;
	}
}
