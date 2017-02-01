using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour {
	public float rotateFactor;
	private Rigidbody2D rigidBody;
	// Use this for initialization
	void Start () {
		rigidBody = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		 float rotateSpeed =  -rigidBody.velocity.x * rotateFactor;
		transform.Rotate(0,0,rotateSpeed * Time.deltaTime);
	}
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Slab") {
			Destroy (this.gameObject);
		}
	}
}
