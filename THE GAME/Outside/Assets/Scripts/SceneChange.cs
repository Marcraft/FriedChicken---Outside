﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
	public GameObject player;
	public GameObject mainCamera;
	public GameObject canvas;
	public GameObject deathCanvas;
	public GameObject level;

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
		deathOpacity = 0;
		level.GetComponent<Level> ().levelChoice = currentLevel;
	}

	// Use this for initialization
	void Start ()
	{
		changeMap = false;
		canvas.GetComponent<CanvasRenderer> ().SetAlpha (opacity);
		deathCanvas.GetComponent<CanvasRenderer> ().SetAlpha (deathOpacity);

	}

	// Update is called once per frame
	void Update ()
	{	
		if (spawnSet == false) {
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
				level.GetComponent<Level> ().resetLevel ();
				Start ();
				player.GetComponent<PlayerController> ().canClimb = 0;
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
			//level.GetComponent<Level> ().levelChoice = chooseNextLevel ();
			level.GetComponent<Level> ().levelChoice = 101;
			currentLevel = 101;
			deadStart = true;
		}
		if (player.GetComponent<PlayerController> ().dead) {
			level.GetComponent<Level> ().resetTimer -= Time.deltaTime;
			deathOpacity += Time.deltaTime;
			deathCanvas.GetComponent<CanvasRenderer> ().SetAlpha (deathOpacity);
		}
		if (level.GetComponent<Level> ().resetTimer <= 0 && deadStart) {
			deathOpacity = 0;
			deathCanvas.GetComponent<CanvasRenderer> ().SetAlpha (deathOpacity);
			opacity = 1.5f;
			player.transform.position = new Vector2 ();
			level.GetComponent<Level> ().resetLevel ();
			Start ();
			player.GetComponent<PlayerController> ().dead = false;
			player.GetComponent<PlayerController> ().health = player.GetComponent<PlayerController> ().maxHealth;
			player.GetComponent<PlayerController> ().canClimb = 0;
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
		} else if (spawn == Spawn.right) {
			if (currentLevel == 101)
				nextLevel = 0;
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

		} else if (spawn == Spawn.up) {
			if (currentLevel == 106)
				nextLevel = 108;
			if (currentLevel == 107)
				nextLevel = 106;
			
		} else if (spawn == Spawn.down) {
			if (currentLevel == 106)
				nextLevel = 107;
			if (currentLevel == 108)
				nextLevel = 106;
		}
		currentLevel = nextLevel;
		return nextLevel;
	}
}
