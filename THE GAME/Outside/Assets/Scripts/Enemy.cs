using UnityEngine;

public class Enemy : MonoBehaviour
{
	public GameObject orb;
	public bool wolf;
	public bool raccoon;
	public bool willowisp;
	public bool hurt;
	public bool hurting;
	public bool walk;
	public bool attack;
	public float knockback;
	private float hurtTimer;
	public GameObject projectile;
	public float timerInterval;
	public float groundControl;
	public float pounceStrength;
	public int health;
	public bool facingRight;
	private float timer;

	private Vector2 playerProximity;
	private Vector3 spawnPosition;

	private GameObject player;
	private Rigidbody2D rigidBody;
	private Animator animator;
	bool attackSound;
	// Use this for initialization
	void Start ()
	{
		hurtTimer = 0.5f;
		player = GameObject.FindWithTag ("Player");
		rigidBody = gameObject.GetComponent<Rigidbody2D> ();
		animator = gameObject.GetComponent<Animator> ();
		spawnPosition = transform.position;
		playerProximity = new Vector2 (Mathf.Abs(player.transform.position.x - transform.position.x), Mathf.Abs(player.transform.position.y - transform.position.y));
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			if (other.GetComponent<Rigidbody2D> ().position.x > rigidBody.position.x) {
				other.GetComponent<PlayerController> ().facingRight = false;
			} else {
				other.GetComponent<PlayerController> ().facingRight = true;
			}
			other.GetComponent <PlayerController> ().health--;
			other.GetComponent <PlayerController> ().hurt = true;
		}

	}
	// Update is called once per frame
	void Update ()
	{
		playerProximity = new Vector2 (Mathf.Abs(player.transform.position.x - transform.position.x), Mathf.Abs(player.transform.position.y - transform.position.y));
		if (wolf || raccoon || willowisp) {
			animator.SetBool ("hurt", hurting);
			animator.SetBool ("walk", walk);
			animator.SetBool ("attack", attack);
			animator.SetBool ("dead", health <= 0);
		}
		if (health > 0) {
			if (!wolf) {
				if (player.transform.position.x < transform.position.x) {
					transform.localScale = new Vector3 (1, 1, 1);
					facingRight = false;
				} else {
					transform.localScale = new Vector3 (-1, 1, 1);
					facingRight = true;
				}
			}
			if (hurt) {
				hurtTimer = 0.7f;
				hurting = true;
				if (facingRight) {
					rigidBody.velocity = new Vector2 (-knockback, knockback);
					health--;
				} else if (!facingRight) {
					rigidBody.velocity = new Vector2 (knockback, knockback);
					health--;
				}
				if (wolf) {
					GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("wolfHurt");
				} else if (raccoon) {
					GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("raccoonHurt");
				} else if (willowisp) {
					GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("wispHurt");
				}else {
					Instantiate (orb, new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
				hurt = false;
			}
			if (hurting) {
				Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Enemy"), true);
				hurtTimer -= Time.deltaTime;
				if (hurtTimer <= 0) {
					hurting = false;
					Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Enemy"), false);
				}
			}
			if (willowisp) {
				if (!hurting) {
					if (playerProximity.x < 3 && playerProximity.y < 3) {
						attack = true;
						if (!attackSound) {
							GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("wispAttack");
							attackSound = true;
						}
						if (player.transform.position.x < transform.position.x)
							rigidBody.AddForce (new Vector2 (-3, 0));
						if (player.transform.position.x > transform.position.x)
							rigidBody.AddForce (new Vector2 (3, 0));
						if (player.transform.position.y < transform.position.y)
							rigidBody.AddForce (new Vector2 (0, -3));
						if (player.transform.position.y > transform.position.y)
							rigidBody.AddForce (new Vector2 (0, 3));
					} else {
						attack = false;
						attackSound = false;
						if (player.transform.position.x < transform.position.x)
							rigidBody.AddForce (new Vector2 (-1, 0));
						if (player.transform.position.x > transform.position.x)
							rigidBody.AddForce (new Vector2 (1, 0));
						if (player.transform.position.y < transform.position.y)
							rigidBody.AddForce (new Vector2 (0, -1));
						if (player.transform.position.y > transform.position.y)
							rigidBody.AddForce (new Vector2 (0, 1));
					}
				}
			}
			if (raccoon) {
				if (timer <= 0) {
					GameObject currentProjectile = (GameObject)Instantiate (projectile, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
					if (facingRight)
						currentProjectile.GetComponent<RaccoonProjectile> ().goingRight = true;
					else {
						currentProjectile.GetComponent<RaccoonProjectile> ().goingRight = false;
					}
					timer = timerInterval;
				}
			}
			if (wolf) {
				if (playerProximity.x < 6 && playerProximity.y < 3) {
					if (player.transform.position.x < transform.position.x) {
						transform.localScale = new Vector3 (1, 1, 1);
						facingRight = false;
					} else {
						transform.localScale = new Vector3 (-1, 1, 1);
						facingRight = true;
					}
				} else {
					if (timer <= 0) {
						if (spawnPosition.x < transform.position.x) {
							transform.localScale = new Vector3 (1, 1, 1);
							facingRight = false;
						} else {
							transform.localScale = new Vector3 (-1, 1, 1);
							facingRight = true;
						}
					}
				}
			}
		}
	}
	void FixedUpdate() {
		if (health > 0) {
			if (wolf) {
				if (attack) {
					if (timer <= 0) {
						GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("wolfAttack");
						if (!facingRight)
							rigidBody.velocity = new Vector2 (-pounceStrength, pounceStrength / 2);
						if (facingRight)
							rigidBody.velocity = new Vector2 (pounceStrength, pounceStrength / 2);
						timer = timerInterval;
					}
				}
				if (playerProximity.x < 3 && playerProximity.y < 1) {
					attack = true;
					walk = false;
				} else if (playerProximity.x < 6 && playerProximity.y < 3) {
					walk = true;
					attack = false;
					if (!facingRight)
						rigidBody.velocity = new Vector3 (-1, rigidBody.velocity.y, 0);
					if (facingRight)
						rigidBody.velocity = new Vector3 (1, rigidBody.velocity.y, 0);
				} else {
					if (timer <= 0) {
						timer = timerInterval;
					}
					if (timer < timerInterval / 2) {
						walk = true;
						attack = false;
						if (!facingRight)
							rigidBody.velocity = new Vector3 (-1, 0, 0);
						if (facingRight)
							rigidBody.velocity = new Vector3 (1, 0, 0);
					} else {
						walk = false;
						attack = false;
					}

				}
			}
			if (!willowisp) {
				if (-0.2 < rigidBody.velocity.x && rigidBody.velocity.x < 0.2)
					rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);
				if (rigidBody.velocity.x > 0)
					rigidBody.AddForce (new Vector2 (-groundControl, 0));
				else if (rigidBody.velocity.x < 0)
					rigidBody.AddForce (new Vector2 (groundControl, 0));
			}
			if (!hurt)
				timer -= Time.deltaTime;
		} else if(wolf || raccoon || willowisp) {
			if (timer < 0 || GetComponent<SpriteRenderer>().color.a <= 0) {
				Instantiate (orb, new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
				Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Enemy"), false);
				Destroy (gameObject);
			} else {
				rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);
				Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Enemy"), true);
				GetComponent<SpriteRenderer> ().color = new Color(1,1,1,GetComponent<SpriteRenderer>().color.a - Time.deltaTime);
				timer -= Time.deltaTime;

			}
		}
	}
}