﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	public GameObject player;
	public GameObject spirit;
	public GameObject level;
	private Vector2 target;
	private Vector2 difference;
	private Vector2 newPos;
	private float cameraVerticalSize;
	private float cameraHorizontalSize;

	public float cameraSpeedX;
	public float cameraSpeedY;
	// Use this for initialization
	void Start () {
		cameraVerticalSize = Camera.main.orthographicSize;
		cameraHorizontalSize = cameraVerticalSize * Screen.width / Screen.height;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		cameraVerticalSize = Camera.main.orthographicSize;
		cameraHorizontalSize = cameraVerticalSize * Screen.width / Screen.height;
		if (player.GetComponent<PlayerController>().state == PlayerController.State.outofbody) {
			target = new Vector2 ((player.transform.position.x + spirit.transform.position.x)/2, (player.transform.position.y + spirit.transform.position.y)/2);
		}
		else if (player.GetComponent<PlayerController> ().facingRight) {
			target = new Vector2 (player.transform.position.x + 1, player.transform.position.y);
		} else {
			target = new Vector2 (player.transform.position.x - 1, player.transform.position.y);
		}
		difference = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
		newPos = new Vector2(transform.position.x + difference.x/cameraSpeedX, transform.position.y + difference.y/cameraSpeedY);
		if (newPos.x - cameraHorizontalSize < (-level.GetComponent <Level> ().getLevelWidth () / 2 + 1) * level.GetComponent <Level> ().gridScale) {
			newPos.x = (-level.GetComponent <Level> ().getLevelWidth () / 2 + 1) * level.GetComponent <Level> ().gridScale + cameraHorizontalSize;
		}
		if (newPos.x + cameraHorizontalSize > (level.GetComponent <Level> ().getLevelWidth () / 2 - 2) * level.GetComponent <Level> ().gridScale) {
			newPos.x = (level.GetComponent <Level> ().getLevelWidth () / 2 - 2) * level.GetComponent <Level> ().gridScale - cameraHorizontalSize;
		}
		if (newPos.y - cameraVerticalSize < (-level.GetComponent <Level> ().getLevelHeight () / 2 + 1) * level.GetComponent <Level> ().gridScale) {
			newPos.y = (-level.GetComponent <Level> ().getLevelHeight () / 2 + 1) * level.GetComponent <Level> ().gridScale + cameraVerticalSize;
		}
		if (newPos.y + cameraVerticalSize > (level.GetComponent <Level> ().getLevelHeight () / 2 - 1) * level.GetComponent <Level> ().gridScale) {
			newPos.y = (level.GetComponent <Level> ().getLevelHeight () / 2 - 1) * level.GetComponent <Level> ().gridScale - cameraVerticalSize;
		}
		
		transform.position = new Vector3 (newPos.x, newPos.y, transform.position.z);
	}
}
