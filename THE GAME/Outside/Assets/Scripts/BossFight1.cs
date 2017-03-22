using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight1 : MonoBehaviour {
	public PlayerTrap playerTrap;
	public GameObject Boss;
	private GameObject cutsceneBars;
	private GameObject mainCamera;
	public bool startCutscene;
	public float startCutsceneTimer;
	public bool endCutscene;
	public float endCutsceneTimer;

	private bool inBattle;
	public float battleInterval;
	private float battleTimer;
	private int battleAction;
	public float move;

	private float cutsceneBarTarget;
	private float cutsceneBarScale;
	private float screenWidth;
	private float screenHeight;
	private GameObject player;


	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag ("MainCamera");
		cutsceneBars = GameObject.FindWithTag ("CutsceneBars");
		screenWidth = cutsceneBars.GetComponent<RectTransform> ().rect.width;
		screenHeight = cutsceneBars.GetComponent<RectTransform> ().rect.height;
		cutsceneBarTarget = 0;
		cutsceneBarScale = 0;
		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (playerTrap.playerTrapped) {
			startCutscene = true;
			cutsceneBarTarget = 0.5f;
			mainCamera.GetComponent<CameraScript>().cutscene = true;
			mainCamera.GetComponent<CameraScript>().target = new Vector2 (Boss.GetComponent<Rigidbody2D> ().position.x, Boss.GetComponent<Rigidbody2D> ().position.y);
			GameObject[] GameObjects = (FindObjectsOfType<GameObject> () as GameObject[]);
			for (int i = 0; i < GameObjects.Length; i++) {
				if (GameObjects [i].CompareTag ("SpiritTouch")) {
					Destroy (GameObjects [i].transform.parent.gameObject);
				}
			}
		}
		if (startCutscene) {
			startCutsceneTimer -= Time.deltaTime;
			if (startCutsceneTimer <= 0) {
				cutsceneBarTarget = 0;
				mainCamera.GetComponent<CameraScript>().cutscene = false;
				playerTrap.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
				playerTrap.playerTrapped = false;
				GameObject[] GameObjects = (FindObjectsOfType<GameObject> () as GameObject[]);
				for (int i = 0; i < GameObjects.Length; i++) {
					if (GameObjects [i].CompareTag ("PlayerTrap")) {
						Destroy (GameObjects [i]);
						inBattle = true;
					}
				}
			}
		}
		//////////////////////////////
		if (inBattle) {
			Battle ();
		}
		//////////////////////////////
		cutsceneBarScale = cutsceneBarScale + (cutsceneBarTarget - cutsceneBarScale) / 10;
		cutsceneBars.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, -screenHeight*cutsceneBarScale);
	}

	void Battle() {
		if (battleTimer < battleInterval) {
			battleTimer += Time.deltaTime;
		} else {
			battleTimer = 0;
			battleAction = (int)Random.Range (0, 1.99f);
		}
		if (battleAction == 0) {
			if (player.GetComponent<Rigidbody2D> ().position.x < Boss.GetComponent<Rigidbody2D> ().position.x) {
				Boss.GetComponent<Rigidbody2D> ().transform.localScale = new Vector3 (-1, 1, 1);
				Boss.GetComponent<Rigidbody2D> ().position = new Vector2 (Boss.GetComponent<Rigidbody2D> ().position.x - move * Time.deltaTime, Boss.GetComponent<Rigidbody2D> ().position.y);
			} else {
				Boss.GetComponent<Rigidbody2D> ().transform.localScale = new Vector3 (1, 1, 1);
				Boss.GetComponent<Rigidbody2D> ().position = new Vector2 (Boss.GetComponent<Rigidbody2D> ().position.x + move * Time.deltaTime, Boss.GetComponent<Rigidbody2D> ().position.y);
			}
		}
		if (battleAction == 1) {

		}
	}
}
