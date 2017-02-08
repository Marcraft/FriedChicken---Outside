using UnityEngine;

public class Wolf : MonoBehaviour
{
	public float jumpTimer;
	public float groundControl;
	public float jumpStrength;
	public float speed;
	public bool facingRight;
	private GameObject player;
	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindWithTag ("Player");
		rigidBody = gameObject.GetComponent<Rigidbody2D> ();
		jumpTimer = 5;
	}

	// Update is called once per frame
	void Update ()
	{
		if (player.transform.position.x < transform.position.x) {
			transform.localScale = new Vector3 (1, 1, 1);
			facingRight = false;
		} else {
			transform.localScale = new Vector3 (-1, 1, 1);
			facingRight = true;
		}
	}
	void FixedUpdate() {
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
}