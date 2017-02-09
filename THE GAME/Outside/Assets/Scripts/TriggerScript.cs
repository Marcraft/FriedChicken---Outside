using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {
	public bool playerIsHere;
	public bool changeScene;
	private Bounds bounds;
	private GameObject[] SpawnPoints;

	// Use this for initialization
	void Start () {
		playerIsHere = false;
		changeScene = false;
		bounds = GetComponent<BoxCollider2D> ().bounds;
	}
	
	// Update is called once per frame
	void Update () {
		SpawnPoints = GameObject.FindGameObjectsWithTag ("Spawn");
	}
	void OnTriggerStay2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			PlayerController player = other.gameObject.GetComponent<PlayerController> ();
			playerIsHere = true;
			if (player.changeScene) {
				changeScene = true;
			}
		}
	}
	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			playerIsHere = false;
		}
	}
	public Vector3 spawnPlayer() {
		for (int i = 0; i < SpawnPoints.Length; i++) {
			Vector2 spawnposition = SpawnPoints [i].transform.position;
			if (bounds.Contains (spawnposition)) {
				return spawnposition;
			}
		}
		return new Vector3 ();
	}
}
