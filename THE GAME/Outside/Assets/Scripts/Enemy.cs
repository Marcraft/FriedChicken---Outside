using UnityEngine;

public class Enemy : MonoBehaviour
{
	public bool wolf;
	public bool raccoon;
	public bool hurt;
	public float knockback;
	private bool knockedBack;
	private float hurtTimer;
	public GameObject projectile;
	public float timerInterval;
	public float groundControl;
	public float pounceStrength;
	public int health;
	public bool facingRight;
	private float timer;


	private GameObject player;
	private Rigidbody2D rigidBody;
	private Animator animator;

	// Use this for initialization
	void Start ()
	{
		hurtTimer = 0.5f;
		player = GameObject.FindWithTag ("Player");
		rigidBody = gameObject.GetComponent<Rigidbody2D> ();
		animator = gameObject.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update ()
	{
		animator.SetBool ("hurt", hurt);
		if (hurt) {
			hurtTimer -= Time.deltaTime;
			if (facingRight && !knockedBack) {
				rigidBody.velocity = new Vector2 (-knockback, knockback);
				health--;
				knockedBack = true;
			} else if(!facingRight && !knockedBack) {
				rigidBody.velocity = new Vector2 (knockback, knockback);
				health--;
				knockedBack = true;
			}
			if (hurtTimer <= 0) {
				hurt = false;
				knockedBack = false;
				hurtTimer = 0.5f;
			}
		}
		if (raccoon) {
			if (timer > timerInterval / 5 && timer < timerInterval*4/5) {
				if (player.transform.position.x < transform.position.x) {
					transform.localScale = new Vector3 (1, 1, 1);
					facingRight = false;
				} else {
					transform.localScale = new Vector3 (-1, 1, 1);
					facingRight = true;
				}
			}
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
			if (player.transform.position.x < transform.position.x) {
				transform.localScale = new Vector3 (1, 1, 1);
				facingRight = false;
			} else {
				transform.localScale = new Vector3 (-1, 1, 1);
				facingRight = true;
			}
		}
		if (health <= 0) {
			Destroy (gameObject);
		}
	}
	void FixedUpdate() {
		if (wolf) {
			if (timer <= 0) {
				if (!facingRight)
					rigidBody.velocity = new Vector2 (-pounceStrength, pounceStrength/2);
				if (facingRight)
					rigidBody.velocity = new Vector2 (pounceStrength, pounceStrength/2);
				timer = timerInterval;
			}
		}
		if (-0.2 < rigidBody.velocity.x && rigidBody.velocity.x < 0.2)
			rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);
		if (rigidBody.velocity.x > 0)
			rigidBody.AddForce (new Vector2 (-groundControl, 0));
		else if (rigidBody.velocity.x < 0)
			rigidBody.AddForce (new Vector2 (groundControl, 0));
		if(!hurt)
			timer -= Time.deltaTime;
	}
}