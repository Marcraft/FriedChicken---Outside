using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {
	public bool playerIsHere;
	public bool changeScene;
	private Bounds bounds;
	private GameObject[] SpawnPoints;
	private Vector2 spawnPoint;

	// Use this for initialization
	void Start () {
		playerIsHere = false;
		changeScene = false;
		bounds = GetComponent<BoxCollider2D> ().bounds;
	}
	
	// Update is called once per frame
	void Update () {
		SpawnPoints = GameObject.FindGameObjectsWithTag ("Spawn");
		/*for (int i = 0; i < SpawnPoints.Length; i++) {
			Vector2 spawnposition = SpawnPoints [i].transform.position;
			if (bounds.Contains (spawnposition)) {
				spawnPoint = spawnposition;
			}
		}*/
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
		if (other.gameObject.CompareTag ("Spawn")) {
			spawnPoint = other.transform.position;
			Destroy (other.gameObject);
		}
	}
	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			playerIsHere = false;
		}
	}
	public Vector3 spawnPlayer() {
		
		return spawnPoint;
	}
}
