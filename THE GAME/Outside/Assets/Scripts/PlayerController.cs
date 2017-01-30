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
	public int canClimb;
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
	private float jumpTimer;
	private Animator animator;
	private Rigidbody2D rigidBody;
	private Rigidbody2D spiritBody;
	public bool changeScene;

	public enum State
	{
		standing,
		crouching,
		jumping,
		climbing,
		outofbody
	}

	public State state = State.standing;

	public enum CombatState
	{
		idle,
		melee,
		ranged,
		roll
	}

	public CombatState combatState = CombatState.idle;
	// Use this for initialization
	void Start ()
	{
		rigidBody = gameObject.GetComponent<Rigidbody2D> ();
		spiritBody = spirit.GetComponent<Rigidbody2D> ();
		animator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		animator.SetFloat ("Speed", Mathf.Abs (rigidBody.velocity.x));
		animator.SetInteger ("State", (int)state);
		animator.SetInteger ("CombatState", (int)combatState);
		if (state == State.standing) {
			if (!onGround) {
				state = State.jumping;
			}
			if (Input.GetKeyDown (jumpKey)) {
				rigidBody.velocity = new Vector2 (rigidBody.velocity.x, jumpStrength);
				jumpTimer = 0.5f;
				state = State.jumping;
			}
			if (Input.GetKey (leftKey)) {
				transform.localScale = new Vector3 (-1, 1, 1);
				facingRight = false;
			}
			if (Input.GetKey (rightKey)) {
				transform.localScale = new Vector3 (1, 1, 1);
				facingRight = true;
			}

			if (combatState == CombatState.idle) {
				if (Input.GetKey (upKey)) {
					if (canClimb > 0) {
						rigidBody.velocity = new Vector2 (0, 0);
						rigidBody.isKinematic = true;
						state = State.climbing;
					}
				}
				if (Input.GetKey (downKey)) {
					state = State.crouching;
				}
				if (Input.GetKeyDown (rangedKey)) {
					combatState = CombatState.ranged;
				}
				if (Input.GetKeyDown (oobKey)) {
					spiritBody.velocity = new Vector2 (0, 0);
					spirit.transform.localPosition = new Vector2 (0, 0);
					spirit.transform.localScale = new Vector3 (1, 1, 1);
					spirit.GetComponent<SpriteRenderer> ().enabled = true;
					state = State.outofbody;
				}
			}
			if (combatState == CombatState.melee) {

			}
			if (combatState == CombatState.ranged) {
				if (Input.GetKeyUp (rangedKey)) {
					combatState = CombatState.idle;
				}
				if (Input.GetKey (downKey)) {
					state = State.crouching;
				}

			}
			if (combatState == CombatState.roll) {

			}

		} else if (state == State.crouching) {
			if (Input.GetKeyUp (downKey)) {
				state = State.standing;
			}
			if (Input.GetKeyDown (jumpKey)) {
				if (onPlatform) {
					rigidBody.position = new Vector2 (rigidBody.position.x, rigidBody.position.y - platformDrop);
					state = State.jumping;
				}
			}
			if (combatState == CombatState.idle) {
				if (Input.GetKeyDown (rangedKey)) {
					combatState = CombatState.ranged;
				}
				if (Input.GetKey (upKey)) {
					if (canClimb > 0) {
						rigidBody.velocity = new Vector2 (0, 0);
						rigidBody.isKinematic = true;
						state = State.climbing;
					}
				}
				if (Input.GetKeyDown (rangedKey)) {
					combatState = CombatState.ranged;
				}
				if (Input.GetKeyDown (oobKey)) {
					spiritBody.velocity = new Vector2 (0, 0);
					spirit.transform.localPosition = new Vector2 (0, 0);
					spirit.transform.localScale = new Vector3 (1, 1, 1);
					spirit.GetComponent<SpriteRenderer> ().enabled = true;
					state = State.outofbody;
				}
			}
			if (combatState == CombatState.ranged) {
				if (Input.GetKeyUp (rangedKey)) {
					combatState = CombatState.idle;
				}
				if (Input.GetKey (downKey)) {
					state = State.crouching;
				}

			}
		} else if (state == State.jumping) {
			if (onGround) {
				state = State.standing;
			}
			if (Input.GetKey (leftKey)) {
				transform.localScale = new Vector3 (-1, 1, 1);
				facingRight = false;
			}
			if (Input.GetKey (rightKey)) {
				transform.localScale = new Vector3 (1, 1, 1);
				facingRight = true;
			}
			if (Input.GetKeyUp (jumpKey)) {
				rigidBody.velocity = new Vector2 (rigidBody.velocity.x, rigidBody.velocity.y * jumpTimer);
			}
			if (combatState == CombatState.idle) {
				if (canClimb > 0) {
					if ((Input.GetKey (upKey))) {
						rigidBody.velocity = new Vector2 (0, 0);
						rigidBody.isKinematic = true;
						state = State.climbing;
					}
				}
				if (Input.GetKeyDown (rangedKey)) {
					combatState = CombatState.ranged;
				}
			}
			if (combatState == CombatState.melee) {

			}
			if (combatState == CombatState.ranged) {
				if (Input.GetKeyUp (rangedKey)) {
					combatState = CombatState.idle;
				}
			}

		} else if (state == State.climbing) {
			if (Input.GetKeyDown (jumpKey)) {
				rigidBody.velocity = new Vector2 (rigidBody.velocity.x, jumpStrength / 2);
				jumpTimer = 0.5f;
				rigidBody.isKinematic = false;
				state = State.jumping;
			}
			if (canClimb <= 0 || onGround) {
				rigidBody.isKinematic = false;
				state = State.standing;
			}
		} else if (state == State.outofbody) {
			if (Input.GetKeyDown (oobKey)) {
				spiritBody.velocity = new Vector2 (0, 0);
				spirit.transform.localPosition = new Vector2 (0, 0);
				spirit.transform.localScale = new Vector3 (1, 1, 1);
				spirit.GetComponent<SpriteRenderer> ().enabled = false;
				state = State.standing;
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
		}


			

	}

	void FixedUpdate ()
	{
		if (Input.GetKey (jumpKey) && jumpTimer < 1) {
			jumpTimer += 0.05f;
		}
		if (state == State.climbing) {
			if (Input.GetKey (upKey)) {
				rigidBody.transform.Translate (new Vector2 (0, climbingSpeed));
			}
			if (Input.GetKey (downKey)) {
				rigidBody.transform.Translate (new Vector2 (0, -climbingSpeed));
			}
		}
		if (state == State.crouching) {
			if (-0.2 < rigidBody.velocity.x && rigidBody.velocity.x < 0.2)
				rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);
			else if (rigidBody.velocity.x > 0)
				rigidBody.AddForce (new Vector2 (-groundControl, 0));
			else if (rigidBody.velocity.x < 0)
				rigidBody.AddForce (new Vector2 (groundControl, 0));
		}
		if (state == State.outofbody) {
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
			if (-0.2 < rigidBody.velocity.x && rigidBody.velocity.x < 0.2)
				rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);
			else if (rigidBody.velocity.x > 0)
				rigidBody.AddForce (new Vector2 (-groundControl, 0));
			else if (rigidBody.velocity.x < 0)
				rigidBody.AddForce (new Vector2 (groundControl, 0));
		} 
		if (state == State.standing) {
			if ((!Input.GetKey (leftKey) && !Input.GetKey (rightKey)) || combatState != CombatState.idle) {
				if (-0.2 < rigidBody.velocity.x && rigidBody.velocity.x < 0.2) {
					rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);
				} else if (rigidBody.velocity.x > 0) {
					rigidBody.AddForce (new Vector2 (-groundControl, 0));
				} else if (rigidBody.velocity.x < 0) {
					rigidBody.AddForce (new Vector2 (groundControl, 0));
				}
			} else {
				if (Input.GetKey (leftKey) && rigidBody.velocity.x > -speed)
					rigidBody.AddForce (new Vector2 (-groundControl, 0));
				if (Input.GetKey (rightKey) && rigidBody.velocity.x < speed)
					rigidBody.AddForce (new Vector2 (groundControl, 0));
			}
		}
		if (state == State.jumping) {
			if (Input.GetKey (leftKey) && rigidBody.velocity.x > -speed)
				rigidBody.AddForce (new Vector2 (-airControl, 0));
			if (Input.GetKey (rightKey) && rigidBody.velocity.x < speed)
				rigidBody.AddForce (new Vector2 (airControl, 0));
		}
	}
}
