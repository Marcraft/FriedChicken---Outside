using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class BossFight1 : MonoBehaviour {
	public PlayerTrap playerTrap;
	public GameObject boss;
	private GameObject cutsceneBars;
	private GameObject mainCamera;
	public GameObject spikeDown;

	public bool startCutscene;
	public float startCutsceneTimer;
	public bool endCutscene;
	public float endCutsceneTimer;

	private bool inBattle;
	public float battleInterval;
	private float battleTimer;
	private int battleAction;
	public float move;

	public float attackTimer;

	private float cutsceneBarTarget;
	private float cutsceneBarScale;
	private float screenWidth;
	private float screenHeight;
	private GameObject player;
	private GameObject level;
	private Animator animator;

	private bool clearWay;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag ("MainCamera");
		cutsceneBars = GameObject.FindWithTag ("CutsceneBars");
		screenWidth = cutsceneBars.GetComponent<RectTransform> ().rect.width;
		screenHeight = cutsceneBars.GetComponent<RectTransform> ().rect.height;
		cutsceneBarTarget = 0;
		cutsceneBarScale = 0;
		player = GameObject.FindWithTag ("Player");
		level = GameObject.FindWithTag ("Level");
		animator = gameObject.GetComponentInChildren<Animator> ();

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;
		SaveFile data;
		if (File.Exists (Application.persistentDataPath + "/OutsideSave.dat")) {
			file = File.Open (Application.persistentDataPath + "/OutsideSave.dat", FileMode.Open);
			data = (SaveFile)bf.Deserialize (file);
			file.Close ();
			/////////////////////
			int map = data.map;
			bool firstBossKilled = data.firstBossKilled;
			bool secondBossKilled = data.secondBossKilled;
			bool thirdBossKilled = data.thirdBossKilled;
			if (firstBossKilled) {
				Destroy (this.gameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetInteger ("BattleAction", battleAction);
		if (playerTrap.playerTrapped) {
			startCutscene = true;
			cutsceneBarTarget = 0.5f;
			mainCamera.GetComponent<CameraScript>().cutscene = true;
			mainCamera.GetComponent<CameraScript>().target = new Vector2 (boss.GetComponent<Rigidbody2D> ().position.x, boss.GetComponent<Rigidbody2D> ().position.y);
			GameObject[] GameObjects = (FindObjectsOfType<GameObject> () as GameObject[]);
			for (int i = 0; i < GameObjects.Length; i++) {
				if (GameObjects [i].CompareTag ("SpiritTouch")) {
					GameObject.FindWithTag ("gamebgm").GetComponent<GameBGM> ().changeSong (2);
					Destroy (GameObjects [i].transform.parent.gameObject);
				}
			}
		}
		if (startCutscene) {
			startCutsceneTimer -= Time.deltaTime;
			battleAction = -1;
			if (startCutsceneTimer <= 0) {
				battleAction = 0;
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
				startCutscene = false;
			}
		}
		//////////////////////////////
		if (inBattle) {
			Battle ();
		}
		//////////////////////////////
		if (endCutscene) {
			endCutsceneTimer -= Time.deltaTime;
			if (endCutsceneTimer <= 0) {
				clearWay = true;
			}
		}
		cutsceneBarScale = cutsceneBarScale + (cutsceneBarTarget - cutsceneBarScale) / 10;
		cutsceneBars.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, -screenHeight*cutsceneBarScale);
	}

	void Battle() {
		if (!boss.GetComponent<Boss> ().dead) {
			if (battleTimer < battleInterval) {
				battleTimer += Time.deltaTime;
			} else {
				battleTimer = 0;
				if (battleAction == 0) {
					battleAction = 1;
				} else if (battleAction == 1) {
					battleAction = 2;
					GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("bambiAttack");
				} else if (battleAction == 2) {
					battleAction = 0;
				}
			}
			if (battleAction == 0) {
				if (player.GetComponent<Rigidbody2D> ().position.x < boss.GetComponent<Rigidbody2D> ().position.x) {
					boss.GetComponent<Rigidbody2D> ().transform.localScale = new Vector3 (-1, 1, 1);
				} else {
					boss.GetComponent<Rigidbody2D> ().transform.localScale = new Vector3 (1, 1, 1);
				}
			}
			if (battleAction == 1) {
				if (player.GetComponent<Rigidbody2D> ().position.x < boss.GetComponent<Rigidbody2D> ().position.x) {
					boss.GetComponent<Rigidbody2D> ().transform.localScale = new Vector3 (-1, 1, 1);
					boss.GetComponent<Rigidbody2D> ().position = new Vector2 (boss.GetComponent<Rigidbody2D> ().position.x - move * Time.deltaTime, boss.GetComponent<Rigidbody2D> ().position.y);
				} else {
					boss.GetComponent<Rigidbody2D> ().transform.localScale = new Vector3 (1, 1, 1);
					boss.GetComponent<Rigidbody2D> ().position = new Vector2 (boss.GetComponent<Rigidbody2D> ().position.x + move * Time.deltaTime, boss.GetComponent<Rigidbody2D> ().position.y);
				}
			}
			if (battleAction == 2) {
				attackTimer += Time.deltaTime;
				if (attackTimer > 1.1f) {
					Instantiate (spikeDown, new Vector3 (boss.transform.position.x, boss.transform.position.y - 1.5f, boss.transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
					Instantiate (spikeDown, new Vector3 (boss.transform.position.x, boss.transform.position.y - 1.5f, boss.transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
					Instantiate (spikeDown, new Vector3 (boss.transform.position.x, boss.transform.position.y - 1.5f, boss.transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
					Instantiate (spikeDown, new Vector3 (boss.transform.position.x, boss.transform.position.y - 1.5f, boss.transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
					Instantiate (spikeDown, new Vector3 (boss.transform.position.x, boss.transform.position.y - 1.5f, boss.transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
					Instantiate (spikeDown, new Vector3 (boss.transform.position.x, boss.transform.position.y - 1.5f, boss.transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
					Instantiate (spikeDown, new Vector3 (boss.transform.position.x, boss.transform.position.y - 1.5f, boss.transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
					attackTimer = -99;
				}
			} else {
				attackTimer = 0;
			}
		} else {
			GameObject.FindWithTag ("gamebgm").GetComponent<GameBGM> ().changeSong (1);
			battleAction = 3;
			boss.GetComponent<Rigidbody2D> ().isKinematic = false;
			endCutscene = true;
			inBattle = false;
			////////////////////////////////////
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file;
			SaveFile data;
			if (File.Exists (Application.persistentDataPath + "/OutsideSave.dat")) {
				file = File.Open (Application.persistentDataPath + "/OutsideSave.dat", FileMode.Open);
				data = (SaveFile)bf.Deserialize (file);
				file.Close ();
				/////////////////////
				int map = data.map;
				bool firstBossKilled = data.firstBossKilled;
				bool secondBossKilled = data.secondBossKilled;
				bool thirdBossKilled = data.thirdBossKilled;
				/////////////////////
				file = File.Create (Application.persistentDataPath + "/OutsideSave.dat");
				data = new SaveFile ();
				data.map = level.GetComponent<Level>().levelChoice;
				data.firstBossKilled = true;
				data.secondBossKilled = secondBossKilled;
				data.thirdBossKilled = thirdBossKilled;
				data.haveKey = GameObject.FindWithTag ("MenuControls").GetComponent<MenuControls> ().haveKey;
				bf.Serialize (file, data);
				file.Close ();
			} else {
				file = File.Create (Application.persistentDataPath + "/OutsideSave.dat");
				data = new SaveFile ();
				data.map = level.GetComponent<Level>().levelChoice;
				data.haveKey = GameObject.FindWithTag ("MenuControls").GetComponent<MenuControls> ().haveKey;
				data.firstBossKilled = true;
				bf.Serialize (file, data);
				file.Close ();
			}
			////////////////////////////////////
		}
	}
	void OnTriggerStay2D(Collider2D other) {
		if (other.CompareTag ("Ground")) {
			if (clearWay) {
				Destroy (other.gameObject);
			}
		}
	}
}
