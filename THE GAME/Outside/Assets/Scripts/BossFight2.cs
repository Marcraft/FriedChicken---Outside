using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
//using System;

public class BossFight2 : MonoBehaviour {
	public PlayerTrap playerTrap;
	public GameObject boss;
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

	public float sawTimer;
	public float drillTimer;
	public float jumpTimer;
	public bool drillLock;


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
		sawTimer = 4;
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
			if (secondBossKilled) {
				Destroy (this.gameObject);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		//boss.GetComponent<Boss> ().saw.transform.Rotate (0,0,Time.deltaTime*50);
		boss.GetComponent<Boss> ().saw.GetComponent<Rigidbody2D> ().AddTorque (0.5f);
		if (playerTrap.playerTrapped) {
			startCutscene = true;
			cutsceneBarTarget = 0.5f;
			mainCamera.GetComponent<CameraScript>().cutscene = true;
			mainCamera.GetComponent<CameraScript>().target = new Vector2 (boss.GetComponent<Rigidbody2D> ().position.x, transform.position.y);
			GameObject[] GameObjects = (FindObjectsOfType<GameObject> () as GameObject[]);
			for (int i = 0; i < GameObjects.Length; i++) {
				if (GameObjects [i].CompareTag ("SpiritTouch")) {
					GameObject.FindWithTag ("gamebgm").GetComponent<GameBGM> ().changeSong (4);
					Destroy (GameObjects [i].transform.parent.gameObject);
				}
			}
		}
		if (startCutscene) {
			startCutsceneTimer -= Time.deltaTime;
			if (boss.transform.localPosition.y < 2) {
				boss.transform.position = new Vector2 (boss.transform.position.x, boss.transform.position.y + Time.deltaTime);
			}
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
						boss.GetComponent <Rigidbody2D>().isKinematic = false;
						boss.GetComponent <Boss> ().saw.GetComponent<CircleCollider2D> ().isTrigger = false;
						boss.GetComponent <Boss> ().drill.GetComponent<PolygonCollider2D> ().isTrigger = false;
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
			boss.transform.position = new Vector2 (boss.transform.position.x, boss.transform.position.y - Time.deltaTime/2);
			if (endCutsceneTimer <= 0) {
				clearWay = true;
				cutsceneBarTarget = 0;
				mainCamera.GetComponent<CameraScript>().cutscene = false;
				SceneManager.LoadScene ("ending");
			}
		}
		cutsceneBarScale = cutsceneBarScale + (cutsceneBarTarget - cutsceneBarScale) / 10;
		cutsceneBars.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, -screenHeight*cutsceneBarScale);
	}

	void Battle() {
		if (boss.GetComponent<Boss> ().leftLeg.GetComponent<BossLeg> ().touchingGround || boss.GetComponent<Boss> ().rightLeg.GetComponent<BossLeg> ().touchingGround) {
			boss.GetComponent <Rigidbody2D> ().isKinematic = true;
			if (boss.GetComponent<Rigidbody2D> ().velocity.y < 0) {
				boss.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
			}
		} else {
			boss.GetComponent <Rigidbody2D> ().isKinematic = false;
		}
		if (!boss.GetComponent<Boss> ().dead) {
			Jump ();
			SawAttack ();
			DrillAttack ();
			Boss bossScript = boss.GetComponent<Boss>();
			if (bossScript.health < (bossScript.maxHealth / 4)) {
				boss.GetComponent<SpriteRenderer> ().sprite = bossScript.health25;
			}
			else if (bossScript.health < (2*bossScript.maxHealth / 4)) {
				boss.GetComponent<SpriteRenderer> ().sprite = bossScript.health50;

			}
			else if (bossScript.health < (3*bossScript.maxHealth / 4)) {
				boss.GetComponent<SpriteRenderer> ().sprite = bossScript.health75;
			}
		} else {
			GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("robotStomp");
			boss.GetComponent<SpriteRenderer> ().sprite = boss.GetComponent<Boss>().health0;
			battleAction = 3;
			endCutscene = true;
			boss.GetComponent <Boss> ().saw.GetComponent<CircleCollider2D> ().enabled = false;
			boss.GetComponent <Boss> ().drill.GetComponent<PolygonCollider2D> ().enabled = false;
			boss.GetComponent <Rigidbody2D> ().isKinematic = true;
			boss.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
			cutsceneBarTarget = 0.5f;
			mainCamera.GetComponent<CameraScript>().cutscene = true;
			mainCamera.GetComponent<CameraScript>().target = new Vector2 (boss.GetComponent<Rigidbody2D> ().position.x, transform.position.y);
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
				data.firstBossKilled = firstBossKilled;
				data.secondBossKilled = secondBossKilled;
				data.thirdBossKilled = thirdBossKilled;
				bf.Serialize (file, data);
				file.Close ();
			} else {
				file = File.Create (Application.persistentDataPath + "/OutsideSave.dat");
				data = new SaveFile ();
				data.map = level.GetComponent<Level>().levelChoice;
				data.secondBossKilled = true;
				bf.Serialize (file, data);
				file.Close ();
			}
			////////////////////////////////////
		}
	}

	void SawAttack() {
		sawTimer -= Time.deltaTime;
		if (sawTimer < 0) {
			sawTimer = Random.Range (7f, 15f);
		} else if (sawTimer < 2) {
			boss.GetComponent<Boss> ().saw.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (20, -20));
		} else if (sawTimer < 3) {
			boss.GetComponent<Boss> ().saw.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-20, -10));
		}else if (sawTimer < 4) {
			boss.GetComponent<Boss> ().saw.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-20, 0));
		}
	}
	void DrillAttack() {
		drillTimer -= Time.deltaTime;
		if (drillTimer < 0) {
			drillTimer = Random.Range (5f, 20f);
		} else if (drillTimer < 2) {
			boss.GetComponent<Boss> ().drill.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-20, -20));
		} else if (sawTimer < 3) {
			boss.GetComponent<Boss> ().drill.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (20, -10));
		}else if (sawTimer < 4) {
			boss.GetComponent<Boss> ().drill.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (20, 0));
		}
	}
	void Jump() {
		jumpTimer -= Time.deltaTime;
		if (jumpTimer < 0) {
			GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("robotStep3");
			jumpTimer = Random.Range (5f, 10f);
			if (boss.GetComponent<Rigidbody2D> ().transform.localPosition.x < -5) {
				boss.GetComponent<Rigidbody2D> ().velocity = new Vector3 (Random.Range (1f, 4f), Random.Range (5f, 7f), 0);
			} else if (boss.GetComponent<Rigidbody2D> ().transform.localPosition.x > 5) {
				boss.GetComponent<Rigidbody2D> ().velocity = new Vector3 (Random.Range (-4f, -1f), Random.Range (5f, 7f), 0);
			} else {
				if (player.GetComponent<Rigidbody2D> ().position.x < boss.GetComponent<Rigidbody2D> ().position.x) {
					boss.GetComponent<Rigidbody2D> ().velocity = new Vector3 (Random.Range (-4f, -1f), Random.Range (5f, 7f), 0);
				} else {
					boss.GetComponent<Rigidbody2D> ().velocity = new Vector3 (Random.Range (1f, 4f), Random.Range (5f, 7f), 0);
				}
			}
		}
	}
	void OnTriggerStay2D(Collider2D other) {
		if (other.CompareTag ("Ground")) {
			if (clearWay) {
				
			}
		}
	}
}
