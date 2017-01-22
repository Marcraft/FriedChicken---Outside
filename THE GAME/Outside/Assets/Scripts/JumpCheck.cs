using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCheck : MonoBehaviour {
	public LayerMask ground;
	private PlayerController playerController;

	void Start() {
		playerController = gameObject.GetComponentInParent<PlayerController> ();
	}
	void FixedUpdate() {
		playerController.onGround = Physics2D.IsTouchingLayers (GetComponent<Collider2D>(), ground);
	}
}
