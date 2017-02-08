using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
	public GameObject player;
	public GameObject camera;
	public GameObject canvas;
	public string levelName;
	public bool LeftExit;
	public bool RightExit;
	public bool UpExit;
	public bool DownExit;
	private float opacity;
	private bool FoundSpawn;
	private bool changeMap;
	private bool SpawnHere;
	private bool fadeIn;
	private GameObject[] GlobalVariables;
	private GameObject[] SpawnPoints;
	private Bounds bounds;
	private Spawn lastExit;

	// Use this for initialization
	void Start () {
		GlobalVariables = GameObject.FindGameObjectsWithTag("Global");
		lastExit = GlobalVariables[0].GetComponent<GlobalVariables>().getSpawn();
		bounds = GetComponent<BoxCollider2D>().bounds;
		opacity = 1;
		if (lastExit == Spawn.left && LeftExit) {
			SpawnHere = true;
			fadeIn = true;
		}
		if (lastExit == Spawn.right && RightExit) {
			SpawnHere = true;
			fadeIn = true;
		}
		if (lastExit == Spawn.up && UpExit) {
			SpawnHere = true;
			fadeIn = true;
		}
		if (lastExit == Spawn.down && DownExit) {
			SpawnHere = true;
			fadeIn = true;
		}
		canvas.GetComponent<CanvasRenderer> ().SetAlpha (opacity);

	}

	// Update is called once per frame
	void Update () {
		//canvas.GetComponent<CanvasRenderer> ().SetAlpha (opacity);
		SpawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
		if (!FoundSpawn) {
			if (SpawnHere) {
				for (int i = 0; i < SpawnPoints.Length; i++) {
					Vector2 spawnposition = SpawnPoints [i].transform.position;
					if (bounds.Contains (spawnposition)) {
						camera.transform.position = new Vector3 (spawnposition.x, spawnposition.y, -10);
						player.transform.position = spawnposition;
						FoundSpawn = true;
					}
				}
			}
		}
		if (changeMap) {
			canvas.GetComponent<CanvasRenderer> ().SetAlpha (opacity);
			opacity += Time.deltaTime * 3;
			if (opacity >= 1) {
				SceneManager.LoadScene (levelName);
			}
		} else {
			if (opacity >= 0 && fadeIn) {
				opacity -= Time.deltaTime * 3;
				canvas.GetComponent<CanvasRenderer> ().SetAlpha (opacity);
			}
		}
	}
		
	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			PlayerController player = other.gameObject.GetComponent<PlayerController> ();
			if (player.changeScene) {
				if (LeftExit) {
					GlobalVariables[0].GetComponent<GlobalVariables>().setSpawn(Spawn.right);
				}
				if (RightExit) {
					GlobalVariables[0].GetComponent<GlobalVariables>().setSpawn(Spawn.left);
				}
				if (UpExit) {
					GlobalVariables[0].GetComponent<GlobalVariables>().setSpawn(Spawn.down);
				}
				if (DownExit) {
					GlobalVariables[0].GetComponent<GlobalVariables>().setSpawn(Spawn.up);
				}
				changeMap = true;
				//SceneManager.LoadScene (levelName);
			}
		}
	}
}
