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

	private float cutsceneBarTarget;
	private float cutsceneBarScale;
	private float screenWidth;
	private float screenHeight;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag ("MainCamera");
		cutsceneBars = GameObject.FindWithTag ("CutsceneBars");
		screenWidth = cutsceneBars.GetComponent<RectTransform> ().rect.width;
		screenHeight = cutsceneBars.GetComponent<RectTransform> ().rect.height;
		cutsceneBarTarget = 0;
		cutsceneBarScale = 0;
		Debug.Log (screenHeight);
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
					}
				}
			}
		}
		cutsceneBarScale = cutsceneBarScale + (cutsceneBarTarget - cutsceneBarScale) / 10;
		Debug.Log (screenHeight);
		//Debug.Log (cutsceneBarScale);
		cutsceneBars.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, -screenHeight*cutsceneBarScale);
		Debug.Log (cutsceneBars.GetComponent<RectTransform> ().sizeDelta);

	}
}
