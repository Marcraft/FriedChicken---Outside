using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
	public bool hitEnemy = true;
	public float knockback;
	public PlayerController playerController;
	// Use this for initialization
	void Start () {
		playerController = gameObject.GetComponentInParent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Enemy")){
			hitEnemy = true;
			other.GetComponent<Enemy> ().health--;
			if (playerController.facingRight)
				other.GetComponent<Rigidbody2D> ().velocity = new Vector2 (knockback, 0);
			else
				other.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-knockback, 0);
		}
	}
}
