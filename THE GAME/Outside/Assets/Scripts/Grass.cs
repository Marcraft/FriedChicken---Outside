using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour {
	public LayerMask ground;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GrowGrass ();
	}

	public void GrowGrass() {
		if (!Physics2D.IsTouchingLayers (GetComponent<Collider2D> (), ground)) {
			GetComponent<SpriteRenderer> ().enabled = true;
		} else {
			GetComponent<SpriteRenderer> ().enabled = false;
		}
	}
}
