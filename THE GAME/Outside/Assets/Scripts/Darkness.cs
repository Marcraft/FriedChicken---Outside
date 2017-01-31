using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour {
	public float zoomIn;
	public float zoomOut;
	public float scale;
	private float zoomTarget;
	public bool OOB;
	private float difference;
	// Use this for initialization
	void Start () {
		zoomTarget = zoomIn;
	}
	
	// Update is called once per frame
	void Update() {
		if (OOB) {
			zoomTarget = zoomIn;
		} else {
			zoomTarget = zoomOut;
		}
	}
	void FixedUpdate () {
		difference = zoomTarget - transform.localScale.x ;
		transform.localScale = new Vector2 (transform.localScale.x + difference / scale, transform.localScale.y + difference / scale);
	}
}
