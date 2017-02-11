using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCheck : MonoBehaviour {
	public LayerMask ground;
	public LayerMask spirit;
	private PlayerController playerController;

	void Start() {
		playerController = gameObject.GetComponentInParent<PlayerController> ();
	}
	void Update() {
		playerController.onGround = (Physics2D.IsTouchingLayers (GetComponent<Collider2D>(), ground)||Physics2D.IsTouchingLayers (GetComponent<Collider2D>(), spirit)) && playerController.gameObject.GetComponent<Rigidbody2D> ().velocity.y < 0.1f;
	}
}
