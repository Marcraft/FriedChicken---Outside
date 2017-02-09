using UnityEngine;

public class Enemy : MonoBehaviour
{
	public bool wolf;
	public bool raccoon;
	public GameObject projectile;
	public float timerInterval;
	public float jumpTimer;
	public float groundControl;
	public float jumpStrength;
	public float speed;
	public int health;
	public bool facingRight;
	private float timer;
	private GameObject player;
	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindWithTag ("Player");
		rigidBody = gameObject.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update ()
	{
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
			if (!facingRight && rigidBody.velocity.x > -speed)
				rigidBody.AddForce (new Vector2 (-groundControl, 0));
			if (facingRight && rigidBody.velocity.x < speed)
				rigidBody.AddForce (new Vector2 (groundControl, 0));
			if (rigidBody.velocity.x > -0.01 && rigidBody.velocity.x < 0.01) {
				jumpTimer--;
			} else {
				jumpTimer = 5;
			}
			if (jumpTimer <= 0) {
				rigidBody.AddForce (new Vector2 (0, jumpStrength));
				jumpTimer = 30;
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
			timer -= Time.deltaTime;
		}
	}
}