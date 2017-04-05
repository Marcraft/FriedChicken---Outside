using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SceneChange : MonoBehaviour
{
	public GameObject player;
	public GameObject mainCamera;
	public GameObject canvas;
	public GameObject deathCanvas;
	public GameObject level;
	public GameObject dialogue;
	GameObject menuControls;

	public TriggerScript leftTrigger;
	public TriggerScript rightTrigger;
	public TriggerScript upTrigger;
	public TriggerScript downTrigger;

	public int currentLevel;

	private float opacity;
	private float deathOpacity;
	private bool changeMap;
	private bool spawnSet;
	private bool deadStart;
	private bool deathSound;

	private bool spawnFromSave;

	public enum Spawn
	{
		left,
		right,
		up,
		down
	}

	public Spawn spawn = Spawn.left;
	private Spawn lastExit;

	void Awake() {
		opacity = 3;
		canvas.GetComponent<CanvasRenderer> ().SetAlpha (opacity);
		deathOpacity = 0;
		currentLevel = GameObject.FindWithTag ("MenuControls").GetComponent<MenuControls> ().map;
		level.GetComponent<Level> ().levelChoice = currentLevel;

		if (currentLevel != 101) {
			spawnFromSave = true;
		}
	}

	// Use this for initialization
	void Start ()
	{
		menuControls = GameObject.FindWithTag ("MenuControls");
		changeMap = false;
		canvas.GetComponent<CanvasRenderer> ().SetAlpha (opacity);
		deathCanvas.GetComponent<CanvasRenderer> ().SetAlpha (deathOpacity);
		dialogue.GetComponent<LynnDialogue> ().newMap = true;
		dialogue.GetComponent<LynnDialogue> ().currentMap = currentLevel;
		deathSound = false;
	}

	// Update is called once per frame
	void Update ()
	{	
		if (spawnSet == false && spawnFromSave && level.GetComponent<Level> ().levelReady && GameObject.FindWithTag ("SaveTree") != null) {
			player.transform.position = GameObject.FindWithTag ("SaveTree").transform.position;
			mainCamera.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -10);
			spawnFromSave = false;
			spawnSet = true;
		}
		else if (spawnSet == false) {
			if (spawn == Spawn.left && level.GetComponent<Level> ().levelReady && leftTrigger.foundSpawnPoint == true) {
				mainCamera.transform.position = new Vector3 (leftTrigger.spawnPlayer ().x, leftTrigger.spawnPlayer ().y, -10);
				player.transform.position = leftTrigger.spawnPlayer ();
				leftTrigger.foundSpawnPoint = false;
				spawnSet = true;
			}
			if (spawn == Spawn.right && level.GetComponent<Level> ().levelReady && rightTrigger.foundSpawnPoint == true) {
				mainCamera.transform.position = new Vector3 (rightTrigger.spawnPlayer ().x, rightTrigger.spawnPlayer ().y, -10);
				player.transform.position = rightTrigger.spawnPlayer ();
				rightTrigger.foundSpawnPoint = false;
				spawnSet = true;
			}
			if (spawn == Spawn.up && level.GetComponent<Level> ().levelReady && upTrigger.foundSpawnPoint == true) {
				mainCamera.transform.position = new Vector3 (upTrigger.spawnPlayer ().x, upTrigger.spawnPlayer ().y, -10);
				player.transform.position = upTrigger.spawnPlayer ();
				upTrigger.foundSpawnPoint = false;
				spawnSet = true;
			}
			if (spawn == Spawn.down && level.GetComponent<Level> ().levelReady && downTrigger.foundSpawnPoint == true) {
				mainCamera.transform.position = new Vector3 (downTrigger.spawnPlayer ().x, downTrigger.spawnPlayer ().y, -10);
				player.transform.position = downTrigger.spawnPlayer ();
				downTrigger.foundSpawnPoint = false;
				spawnSet = true;
			}
		}
		////////////////
		if (leftTrigger.changeScene) {
			spawn = Spawn.right;
			leftTrigger.changeScene = false;
			changeMap = true;
		} else if (rightTrigger.changeScene) {
			spawn = Spawn.left;
			rightTrigger.changeScene = false;
			changeMap = true;
		} else if (upTrigger.changeScene) {
			spawn = Spawn.down;
			upTrigger.changeScene = false;
			changeMap = true;
		} else if (downTrigger.changeScene) {
			spawn = Spawn.up;
			downTrigger.changeScene = false;
			changeMap = true;
		}
		////////////////
		if (changeMap) {
			canvas.GetComponent<CanvasRenderer> ().SetAlpha (opacity);
			opacity += Time.deltaTime * 5;
			if (opacity >= 1.5) {
				//----CHANGE LEVEL-----//
				changeMap = false;
				spawnSet = false;
				leftTrigger.foundSpawnPoint = false;
				rightTrigger.foundSpawnPoint = false;
				upTrigger.foundSpawnPoint = false;
				downTrigger.foundSpawnPoint = false;
				player.transform.position = new Vector2();
				player.GetComponent<PlayerController> ().changeScene = false;
				level.GetComponent<Level> ().levelChoice = chooseNextLevel ();
				level.GetComponent<Level> ().clearLevel ();
				level.GetComponent<Level> ().loadLevel ();
				Start ();
				player.GetComponent<PlayerController> ().canClimb = 0;
				dialogue.GetComponent<LynnDialogue> ().newMap = true;
				dialogue.GetComponent<LynnDialogue> ().currentMap = currentLevel;
				//--------------------//

			}
		} else {
			if (opacity >= 0) {
				opacity -= Time.deltaTime * 5;
				canvas.GetComponent<CanvasRenderer> ().SetAlpha (opacity);
			}
		}
		if (player.GetComponent<PlayerController> ().dead && level.GetComponent<Level> ().resetTimer <= 0) {
			canvas.GetComponent<CanvasRenderer> ().SetAlpha (opacity);
			changeMap = false;
			spawnSet = false;
			leftTrigger.foundSpawnPoint = false;
			rightTrigger.foundSpawnPoint = false;
			upTrigger.foundSpawnPoint = false;
			downTrigger.foundSpawnPoint = false;
			level.GetComponent<Level> ().resetTimer = 5;
			player.GetComponent<PlayerController> ().changeScene = false;
			deadStart = true;
		}
		if (player.GetComponent<PlayerController> ().dead) {
			if (!deathSound) {
				GameObject.FindWithTag ("gamebgm").GetComponent<AudioSource>().Stop();
				GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().StopAll();
				GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("death");
				deathSound = true;
			}
			level.GetComponent<Level> ().resetTimer -= Time.deltaTime;
			deathOpacity += Time.deltaTime;
			deathCanvas.GetComponent<CanvasRenderer> ().SetAlpha (deathOpacity);
		}
		if (level.GetComponent<Level> ().resetTimer <= 0 && deadStart) {
			deathOpacity = 0;
			deathCanvas.GetComponent<CanvasRenderer> ().SetAlpha (deathOpacity);
			opacity = 1.5f;
			player.transform.position = new Vector2 ();
			level.GetComponent<Level> ().clearLevel ();
			if (File.Exists (Application.persistentDataPath + "/OutsideSave.dat")) {
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (Application.persistentDataPath + "/OutsideSave.dat", FileMode.Open);
				SaveFile data = (SaveFile)bf.Deserialize (file);
				file.Close ();
				/////////////////////
				level.GetComponent<Level> ().levelChoice = data.map;
				currentLevel = data.map;
				menuControls.GetComponent<MenuControls> ().firstBossKilled = data.firstBossKilled;
				menuControls.GetComponent<MenuControls> ().secondBossKilled = data.secondBossKilled;
				menuControls.GetComponent<MenuControls> ().thirdBossKilled = data.thirdBossKilled;
				menuControls.GetComponent<MenuControls> ().haveKey = data.haveKey;
				player.gameObject.GetComponent<PlayerController> ().hasKey =data.haveKey;
				/////////////////////
				spawnFromSave = true;
			} else {
				level.GetComponent<Level> ().levelChoice = 101;
				currentLevel = 101;
			}
			level.GetComponent<Level> ().loadLevel ();
			Start ();
			player.GetComponent<PlayerController> ().dead = false;
			player.GetComponent<PlayerController> ().health = player.GetComponent<PlayerController> ().maxHealth;
			player.GetComponent<PlayerController> ().canClimb = 0;
			dialogue.GetComponent<LynnDialogue> ().newMap = true;
			dialogue.GetComponent<LynnDialogue> ().currentMap = currentLevel;
			GameObject.FindWithTag ("gamebgm").GetComponent<GameBGM> ().Start ();
			deadStart = false;
		}
	}

	public int chooseNextLevel ()
	{
		int nextLevel = currentLevel;
		if (spawn == Spawn.left) {
			if (currentLevel == 101)
				nextLevel = 102;
			if (currentLevel == 102)
				nextLevel = 103;
			if (currentLevel == 103)
				nextLevel = 104;
			if (currentLevel == 104)
				nextLevel = 105;
			if (currentLevel == 105)
				nextLevel = 106;
			if (currentLevel == 107)
				nextLevel = 109;
			if (currentLevel == 109)
				nextLevel = 110;
			if (currentLevel == 110) {
				GameObject.FindWithTag ("gamebgm").GetComponent<GameBGM> ().changeSong (3);
				GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Stop ("wind");
				GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Stop ("sea");
				GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("cave");
				GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("stream");
				nextLevel = 201;
			}
			if (currentLevel == 201)
				nextLevel = 202;
			if (currentLevel == 203)
				nextLevel = 204;
			if (currentLevel == 204)
				nextLevel = 208;
			if (currentLevel == 209)
				nextLevel = 210;
		} else if (spawn == Spawn.right) {
			if (currentLevel == 101)
				nextLevel = 102;
			if (currentLevel == 102)
				nextLevel = 101;
			if (currentLevel == 103)
				nextLevel = 102;
			if (currentLevel == 104)
				nextLevel = 103;
			if (currentLevel == 105)
				nextLevel = 104;
			if (currentLevel == 106)
				nextLevel = 105;
			if (currentLevel == 109)
				nextLevel = 107;
			if (currentLevel == 110)
				nextLevel = 109;
			if (currentLevel == 201) {
				GameObject.FindWithTag ("gamebgm").GetComponent<GameBGM> ().changeSong (1);
				GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Stop ("cave");
				GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Stop ("stream");
				GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("wind");
				GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("sea");
				nextLevel = 110;
			}
			if (currentLevel == 202)
				nextLevel = 201;
			if (currentLevel == 204)
				nextLevel = 203;
			if (currentLevel == 208)
				nextLevel = 204;
			if (currentLevel == 210)
				nextLevel = 209;

		} else if (spawn == Spawn.up) {
			if (currentLevel == 101)
				nextLevel = 102;
			if (currentLevel == 106)
				nextLevel = 108;
			if (currentLevel == 107)
				nextLevel = 106;
			if (currentLevel == 203)
				nextLevel = 202;
			if (currentLevel == 204)
				nextLevel = 205;
			if (currentLevel == 205)
				nextLevel = 206;
			if (currentLevel == 206)
				nextLevel = 207;
			if (currentLevel == 209)
				nextLevel = 208;
			
		} else if (spawn == Spawn.down) {
			if (currentLevel == 101)
				nextLevel = 102;
			if (currentLevel == 106)
				nextLevel = 107;
			if (currentLevel == 108)
				nextLevel = 106;
			if (currentLevel == 202)
				nextLevel = 203;
			if (currentLevel == 205)
				nextLevel = 204;
			if (currentLevel == 206)
				nextLevel = 205;
			if (currentLevel == 207)
				nextLevel = 206;
			if (currentLevel == 208)
				nextLevel = 209;
		}
		currentLevel = nextLevel;
		return nextLevel;
	}
}
