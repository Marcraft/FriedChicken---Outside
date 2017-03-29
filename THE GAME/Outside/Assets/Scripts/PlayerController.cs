using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public GameObject spirit;
	public GameObject darkness;
	public GameObject arrow;
	public GameObject globalVariables;

	public float health;
	public float maxHealth;
	public bool dead;

	public int elixir;
	public int maxElixir;
	public int defaultElixir;

	public float groundControl;
	public float airControl;
	public float spiritControl;
	public float speed;
	public float jumpStrength;
	public float rollLeapStrength;
	public float climbingSpeed;
	public float platformDrop;
	public int canClimb;
	public bool onPlatform;
	public bool onGround;
	public bool facingRight;

	public string leftKey;
	public string rightKey;
	public string upKey;
	public string downKey;
	public string jumpKey;
	public string oobKey;
	public string rangedKey;
	public string meleeKey;
	public string rollKey;

	public float combo1;
	public float combo2;
	public float combo3;
	public float comboAir;
	public float knockback;
	public float bowCooldown;


	private float comboTimer;
	private bool nextCombo;
	private float bowTimer;
	private float jumpTimer;
	private bool ChangedState;
	private Animator animator;
	private Rigidbody2D rigidBody;
	private Rigidbody2D spiritBody;
	public bool changeScene;
	public bool hurt;
	public bool knockedBack;
	private float hurtTimer;
	private bool invincible;
	private float invincibleTimer;
	private int invincibleBlink;
	public bool roll;
	public bool leaped;
	private float rollTimer;

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
		ranged

	}

	public CombatState combatState = CombatState.idle;

	public enum MeleeCombo
	{
		zero,
		one,
		two,
		three,
		air
	}

	public MeleeCombo meleeCombo = MeleeCombo.zero;
	// Use this for initialization
	void Start ()
	{
		hurtTimer = 0.2f;
		rollTimer = 0.6f;
		rigidBody = gameObject.GetComponent<Rigidbody2D> ();
		spiritBody = spirit.GetComponent<Rigidbody2D> ();
		animator = gameObject.GetComponent<Animator> ();
		health = maxHealth;
		elixir = defaultElixir;
	}

	// Update is called once per frame
	void Update ()
	{
		animator.SetFloat ("Speed", Mathf.Abs (rigidBody.velocity.x));
		animator.SetFloat ("VerticalSpeed", rigidBody.velocity.y);
		animator.SetFloat ("Health", health);
		animator.SetInteger ("State", (int)state);
		animator.SetInteger ("CombatState", (int)combatState);
		animator.SetInteger ("Combo", (int)meleeCombo);
		animator.SetBool ("Hurt", hurt);
		animator.SetBool ("Roll", roll);
		animator.SetBool ("ChangeState", ChangedState);
		ChangedState = false;
		if (bowTimer > 0 && combatState != CombatState.ranged) {
			bowTimer -= Time.deltaTime;
		}
		if (invincible) {
			invincibleTimer += Time.deltaTime;
			invincibleBlink++;
			if (invincibleBlink % 2 == 0) {
				GetComponent<Renderer>().enabled = false;
			} else {
				GetComponent<Renderer>().enabled = true;
			}
			if (invincibleTimer > 2) {
				GetComponent<Renderer>().enabled = true;
				invincible = false;
				invincibleBlink = 0;
				invincibleTimer = 0;
				Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Enemy"), false);
			}		
		}
		if (dead) {
			hurt = false;
			rigidBody.velocity = new Vector2 (0, 0);
			knockedBack = false;
			hurtTimer = 0.2f;
			state = State.standing;
			combatState = CombatState.idle;
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Enemy"), false);
		}
		else if (hurt) {
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Enemy"), true);
			hurtTimer -= Time.deltaTime;
			if (health < 0) {
				dead = true;
			}
			if (state == State.climbing) {
				rigidBody.isKinematic = false;
			}
			if (state == State.outofbody) {
				rigidBody.isKinematic = false;
				spiritBody.velocity = new Vector2 (0, 0);
				spirit.transform.localPosition = new Vector2 (0, 0);
				spirit.transform.localScale = new Vector3 (1, 1, 1);
				spirit.GetComponent<SpriteRenderer> ().enabled = false;
				spirit.GetComponent <BoxCollider2D> ().enabled = false;
				darkness.GetComponent<Darkness> ().OOB = false;
				state = State.standing;
				ChangedState = true;
			}
			if (facingRight && !knockedBack) {
				rigidBody.velocity = new Vector2 (-knockback, knockback);
				knockedBack = true;
			} else if (!facingRight && !knockedBack) {
				rigidBody.velocity = new Vector2 (knockback, knockback);
				knockedBack = true;
			}
			if (hurtTimer <= 0 && onGround) {
				hurtTimer = 0.2f;
				hurt = false;
				knockedBack = false;
				state = State.standing;
				combatState = CombatState.idle;
				invincible = true;
			}
		} else if (roll) {
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Enemy"), true);
			rollTimer -= Time.deltaTime;
			if (facingRight && !leaped) {
				rigidBody.velocity = new Vector2 (rollLeapStrength, rigidBody.velocity.y);
				leaped = true;
			} else if (!facingRight && !leaped) {
				rigidBody.velocity = new Vector2 (-rollLeapStrength, rigidBody.velocity.y);
				leaped = true;
			}
			if (rollTimer <= 0) {
				roll = false;
				leaped = false;
				rollTimer = 0.6f;
				if (rigidBody.velocity.x < -speed)
					rigidBody.velocity = new Vector2 (-speed, rigidBody.velocity.y);
				if (rigidBody.velocity.x > speed)
					rigidBody.velocity = new Vector2 (speed, rigidBody.velocity.y);
				Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Enemy"), false);
			}
		} else {
			//STATE STANDING
			if (state == State.standing) {
				if (!onGround) {
					state = State.jumping;
					ChangedState = true;
				}
				if (combatState == CombatState.idle) {
					if (Input.GetKeyDown (rollKey)) {
						roll = true;
					}
					if (Input.GetKey (leftKey)) {
						transform.localScale = new Vector3 (-1, 1, 1);
						facingRight = false;
					}
					if (Input.GetKey (rightKey)) {
						transform.localScale = new Vector3 (1, 1, 1);
						facingRight = true;
					}
					if (Input.GetKeyDown (jumpKey)) {
						rigidBody.velocity = new Vector2 (rigidBody.velocity.x, jumpStrength);
						jumpTimer = 0.5f;
						state = State.jumping;
						ChangedState = true;
					}
					if (Input.GetKey (upKey)) {
						if (canClimb > 0) {
							rigidBody.velocity = new Vector2 (0, 0);
							rigidBody.isKinematic = true;
							state = State.climbing;
							ChangedState = true;
						}
					}
					if (Input.GetKey (downKey)) {
						state = State.crouching;
						ChangedState = true;
					}
					if (Input.GetKeyDown (rangedKey) && bowTimer <= 0) {
						bowTimer = bowCooldown;
						combatState = CombatState.ranged;
					}
					if (Input.GetKeyDown (meleeKey)) {
						meleeCombo = MeleeCombo.zero;
						combatState = CombatState.melee;
					}
					if (Input.GetKeyDown (oobKey) && elixir > 0) {
						elixir--;
						spiritBody.velocity = new Vector2 (0, 0);
						spirit.transform.localPosition = new Vector2 (0, 0);
						spirit.transform.localScale = new Vector3 (1, 1, 1);
						spirit.GetComponent<SpriteRenderer> ().enabled = true;
						spirit.GetComponent <BoxCollider2D> ().enabled = true;
						darkness.GetComponent<Darkness> ().OOB = true;
						state = State.outofbody;
						rigidBody.velocity = new Vector2 (0, 0);
						//rigidBody.isKinematic = true;
						ChangedState = true;

					}
				}
				if (combatState == CombatState.melee) {
					if (meleeCombo == MeleeCombo.zero) {
						comboTimer = combo1;
						meleeCombo = MeleeCombo.one;
						if (facingRight)
							rigidBody.velocity = new Vector2 (speed * combo1, 0);
						else
							rigidBody.velocity = new Vector2 (-speed * combo1, 0);
					} else if (meleeCombo == MeleeCombo.one) {
						if (comboTimer <= 0) {
							if (Input.GetKey (rollKey)) {
								roll = true;
								meleeCombo = MeleeCombo.zero;
								combatState = CombatState.idle;
							} else if (nextCombo) {
								if (Input.GetKey (leftKey)) {
									transform.localScale = new Vector3 (-1, 1, 1);
									facingRight = false;
								}
								if (Input.GetKey (rightKey)) {
									transform.localScale = new Vector3 (1, 1, 1);
									facingRight = true;
								}
								comboTimer = combo2;
								meleeCombo = MeleeCombo.two;
								nextCombo = false;
								if (facingRight)
									rigidBody.velocity = new Vector2 (speed * combo2 * 1.5f, 0);
								else
									rigidBody.velocity = new Vector2 (-speed * combo2 * 1.5f, 0);
							} else {
								meleeCombo = MeleeCombo.zero;
								combatState = CombatState.idle;
							}

						}
						if (Input.GetKeyDown (meleeKey)) {
							nextCombo = true;
						}

					} else if (meleeCombo == MeleeCombo.two) {
						if (comboTimer <= 0) {
							if (Input.GetKey (rollKey)) {
								roll = true;
								meleeCombo = MeleeCombo.zero;
								combatState = CombatState.idle;
							} else if (nextCombo) {
								if (Input.GetKey (leftKey)) {
									transform.localScale = new Vector3 (-1, 1, 1);
									facingRight = false;
								}
								if (Input.GetKey (rightKey)) {
									transform.localScale = new Vector3 (1, 1, 1);
									facingRight = true;
								}
								comboTimer = combo3;
								meleeCombo = MeleeCombo.three;
								nextCombo = false;
								if (facingRight)
									rigidBody.velocity = new Vector2 (speed * combo3 * 2, 0);
								else
									rigidBody.velocity = new Vector2 (-speed * combo3 * 2, 0);
							} else {
								meleeCombo = MeleeCombo.zero;
								combatState = CombatState.idle;
							}
						}
						if (Input.GetKeyDown (meleeKey)) {
							nextCombo = true;
						}

					} else if (meleeCombo == MeleeCombo.three) {
						if (comboTimer <= 0) {
							meleeCombo = MeleeCombo.zero;
							combatState = CombatState.idle;
						}
					} else if (meleeCombo == MeleeCombo.air) {
						meleeCombo = MeleeCombo.zero;
						combatState = CombatState.idle;
					}
				}
				if (combatState == CombatState.ranged) {
					rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);
					if (Input.GetKey (leftKey)) {
						transform.localScale = new Vector3 (-1, 1, 1);
						facingRight = false;
					}
					if (Input.GetKey (rightKey)) {
						transform.localScale = new Vector3 (1, 1, 1);
						facingRight = true;
					}
					if (Input.GetKeyDown (jumpKey)) {
						rigidBody.velocity = new Vector2 (rigidBody.velocity.x, jumpStrength);
						jumpTimer = 0.5f;
						state = State.jumping;
						ChangedState = true;
					}
					if (Input.GetKeyUp (rangedKey)) {
						if (facingRight) {
							GameObject currentArrow = (GameObject)Instantiate (arrow, new Vector3 (transform.position.x + 0.5f, transform.position.y - 0.02f, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
							currentArrow.GetComponent<Arrow> ().goingRight = true;
						} else {
							GameObject currentArrow = (GameObject)Instantiate (arrow, new Vector3 (transform.position.x - 0.5f, transform.position.y - 0.02f, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
							currentArrow.GetComponent<Arrow> ().goingRight = false;
						}
						combatState = CombatState.idle;
					}
					if (Input.GetKey (downKey)) {
						state = State.crouching;
						ChangedState = true;
					}

				}

			}
		//STATE CROUCHING
		else if (state == State.crouching) {
				if (Input.GetKeyUp (downKey)) {
					state = State.standing;
					ChangedState = true;
				}
				if (Input.GetKeyDown (jumpKey)) {
					if (onPlatform) {
						rigidBody.position = new Vector2 (rigidBody.position.x, rigidBody.position.y - platformDrop);
						state = State.jumping;
						ChangedState = true;
					}
				}
				if (combatState == CombatState.idle) {
					if (Input.GetKeyDown (rangedKey) && bowTimer <= 0) {
						bowTimer = bowCooldown;
						combatState = CombatState.ranged;
					}
					if (Input.GetKey (upKey)) {
						if (canClimb > 0) {
							rigidBody.velocity = new Vector2 (0, 0);
							rigidBody.isKinematic = true;
							state = State.climbing;
							ChangedState = true;
						}
					}
					if (Input.GetKeyDown (meleeKey)) {
						state = State.standing;
						ChangedState = true;
						combatState = CombatState.melee;
					}
					if (Input.GetKeyDown (oobKey) && elixir > 0) {
						elixir--;
						spiritBody.velocity = new Vector2 (0, 0);
						spirit.transform.localPosition = new Vector2 (0, 0);
						spirit.transform.localScale = new Vector3 (1, 1, 1);
						spirit.GetComponent<SpriteRenderer> ().enabled = true;
						spirit.GetComponent <BoxCollider2D> ().enabled = true;
						darkness.GetComponent<Darkness> ().OOB = true;
						state = State.outofbody;
						//rigidBody.isKinematic = true;
						rigidBody.velocity = new Vector2 (0, 0);
						ChangedState = true;
					}
				}
				if (combatState == CombatState.ranged) {
					if (Input.GetKey (leftKey)) {
						transform.localScale = new Vector3 (-1, 1, 1);
						facingRight = false;
					}
					if (Input.GetKey (rightKey)) {
						transform.localScale = new Vector3 (1, 1, 1);
						facingRight = true;
					}
					if (Input.GetKeyUp (rangedKey)) {
						if (facingRight) {
							GameObject currentArrow = (GameObject)Instantiate (arrow, new Vector3 (transform.position.x + 0.5f, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
							currentArrow.GetComponent<Arrow> ().goingRight = true;
						} else {
							GameObject currentArrow = (GameObject)Instantiate (arrow, new Vector3 (transform.position.x - 0.5f, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
							currentArrow.GetComponent<Arrow> ().goingRight = false;
						}
						rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);
						combatState = CombatState.idle;
					}

				}
			} 
		//STATE JUMPING
		else if (state == State.jumping) {
				if (onGround) {
					rigidBody.velocity = new Vector2 (0, 0);
					if (comboTimer <= 0) {
						state = State.standing;
						ChangedState = true;
					}
				}
				if (Input.GetKeyUp (jumpKey)) {
					rigidBody.velocity = new Vector2 (rigidBody.velocity.x, rigidBody.velocity.y * jumpTimer);
				}
				if (combatState == CombatState.idle) {
					if (Input.GetKey (leftKey)) {
						transform.localScale = new Vector3 (-1, 1, 1);
						facingRight = false;
					}
					if (Input.GetKey (rightKey)) {
						transform.localScale = new Vector3 (1, 1, 1);
						facingRight = true;
					}
					if (canClimb > 0) {
						if ((Input.GetKey (upKey))) {
							rigidBody.velocity = new Vector2 (0, 0);
							rigidBody.isKinematic = true;
							state = State.climbing;
							ChangedState = true;
						}
					}
					if (Input.GetKeyDown (rangedKey) && bowTimer <= 0) {
						bowTimer = bowCooldown;
						combatState = CombatState.ranged;
					}
					if (Input.GetKeyDown (meleeKey)) {
						meleeCombo = MeleeCombo.zero;
						combatState = CombatState.melee;
					}
				}
				if (combatState == CombatState.melee) {
					if (meleeCombo == MeleeCombo.zero) {
						comboTimer = comboAir;
						meleeCombo = MeleeCombo.air;
					} else if (meleeCombo == MeleeCombo.air) {
						if (comboTimer <= 0) {
							meleeCombo = MeleeCombo.zero;
							combatState = CombatState.idle;
						}
					}
				}
				if (combatState == CombatState.ranged) {
					if (Input.GetKey (leftKey)) {
						transform.localScale = new Vector3 (-1, 1, 1);
						facingRight = false;
					}
					if (Input.GetKey (rightKey)) {
						transform.localScale = new Vector3 (1, 1, 1);
						facingRight = true;
					}
					if (Input.GetKeyUp (rangedKey)) {
						if (facingRight) {
							GameObject currentArrow = (GameObject)Instantiate (arrow, new Vector3 (transform.position.x + 0.5f, transform.position.y + 0.09f, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
							currentArrow.GetComponent<Arrow> ().goingRight = true;
						} else {
							GameObject currentArrow = (GameObject)Instantiate (arrow, new Vector3 (transform.position.x - 0.5f, transform.position.y + 0.09f, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
							currentArrow.GetComponent<Arrow> ().goingRight = false;
						}
						combatState = CombatState.idle;
					}
				}

			} 
		//STATE CLIMBING
		else if (state == State.climbing) {
				if (Input.GetKeyDown (jumpKey)) {
					rigidBody.velocity = new Vector2 (rigidBody.velocity.x, jumpStrength / 2);
					jumpTimer = 0.5f;
					rigidBody.isKinematic = false;
					state = State.jumping;
					ChangedState = true;
				}
				if (canClimb <= 0 || onGround) {
					rigidBody.isKinematic = false;
					state = State.standing;
					ChangedState = true;
				}
			} 
		//STATE OUT OF BODY
		else if (state == State.outofbody) {
				rigidBody.velocity = new Vector2 (0, 0);
				if (Input.GetKeyDown (oobKey)) {
					spiritBody.velocity = new Vector2 (0, 0);
					spirit.transform.localPosition = new Vector2 (0, 0);
					spirit.transform.localScale = new Vector3 (1, 1, 1);
					spirit.GetComponent<SpriteRenderer> ().enabled = false;
					spirit.GetComponent <BoxCollider2D> ().enabled = false;
					darkness.GetComponent<Darkness> ().OOB = false;
					state = State.standing;
					//rigidBody.isKinematic = false;
					ChangedState = true;
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
			

	}

	void FixedUpdate ()
	{
		if (dead) {
			rigidBody.velocity = new Vector2 (0, 0);
		}
		else if (!hurt) {
			if (Input.GetKey (jumpKey) && jumpTimer < 1) {
				jumpTimer += Time.deltaTime;
			}
			if (state == State.climbing) {
				if (Input.GetKey (upKey)) {
					rigidBody.transform.Translate (new Vector2 (0, climbingSpeed * Time.deltaTime));
				}
				if (Input.GetKey (downKey)) {
					rigidBody.transform.Translate (new Vector2 (0, -climbingSpeed * Time.deltaTime));
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
				if ((Input.GetKey (leftKey) == Input.GetKey (rightKey)) || combatState != CombatState.idle || roll) {
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
			if (comboTimer > 0) {
				comboTimer -= Time.deltaTime;
			}
		}
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (!hurt && !dead) {
			if (other.gameObject.tag == "Enemy") {
				if (!other.gameObject.GetComponent<Enemy> ().hurting) {
					if (other.rigidbody.position.x > rigidBody.position.x) {
						facingRight = true;
					} else {
						facingRight = false;
					}
					hurt = true;
					health--;
				}
			}
			if (other.gameObject.tag == "Boss") {
				if (!other.gameObject.GetComponent<Boss> ().dead) {
					hurt = true;
					health--;
				}
			}
		}
	}
	void OnTriggerEnter2D (Collider2D other)
	{
		if (!dead) {
			if (other.tag == "Spikes") {
				hurt = true;
				roll = false;
				health = -1;
			}
		}
	}
}
