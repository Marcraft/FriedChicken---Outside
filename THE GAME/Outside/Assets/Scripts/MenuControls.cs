using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class MenuControls : MonoBehaviour {
	public static MenuControls control;

	public GameObject menuSelect;
	public int menuSelection;
	bool gameStarted = false;

	public int map;
	public bool firstBossKilled;
	public bool secondBossKilled;
	public bool thirdBossKilled;
	public bool haveKey;

	void Awake() {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (control != this) {
			Destroy (gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		menuSelection = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameStarted) {
			if (menuSelection == 0) {
				menuSelect.transform.localPosition = new Vector2 (0, 133);
			}
			if (menuSelection == 1) {
				menuSelect.transform.localPosition = new Vector2 (0, 66);
			}
			if (menuSelection == 2) {
				menuSelect.transform.localPosition = new Vector2 (0, 0);
			}
			if (menuSelection == 3) {
				menuSelect.transform.localPosition = new Vector2 (0, -66);
			}
			if (Input.GetKeyDown ("enter") || Input.GetKeyDown ("return") || Input.GetKeyDown ("f") || Input.GetKeyDown ("space")) {
				if (menuSelection == 0) {
					if (File.Exists (Application.persistentDataPath + "/OutsideSave.dat")) {
						File.Delete (Application.persistentDataPath + "/OutsideSave.dat");
					}
					gameStarted = true;
					SceneManager.LoadScene ("game");
				}
				if (menuSelection == 1) {
					if (File.Exists (Application.persistentDataPath + "/OutsideSave.dat")) {
						BinaryFormatter bf = new BinaryFormatter ();
						FileStream file = File.Open (Application.persistentDataPath + "/OutsideSave.dat", FileMode.Open);
						SaveFile data = (SaveFile)bf.Deserialize (file);
						file.Close ();
						/////////////////////
						map = data.map;
						firstBossKilled = data.firstBossKilled;
						secondBossKilled = data.secondBossKilled;
						thirdBossKilled = data.thirdBossKilled;
						haveKey = data.haveKey;
						/////////////////////
						gameStarted = true;
						SceneManager.LoadScene ("game");
					}

				}
				if (menuSelection == 2) {
					
				}
				if (menuSelection == 3) {
					Application.Quit();
				}
			}
			if (Input.GetKeyDown ("up")) {
				if (menuSelection == 0) {
					menuSelection = 3;
				}
				else if (menuSelection == 1) {
					menuSelection = 0;
				}
				else if (menuSelection == 2) {
					menuSelection = 1;
				}
				else if (menuSelection == 3) {
					menuSelection = 2;
				}
			}
			if (Input.GetKeyDown ("down")) {
				if (menuSelection == 0) {
					menuSelection = 1;
				}
				else if (menuSelection == 1) {
					menuSelection = 2;
				}
				else if (menuSelection == 2) {
					menuSelection = 3;
				}
				else if (menuSelection == 3) {
					menuSelection = 0;
				}
			}
		}
	}
}

[Serializable]
class SaveFile {
	public int map;
	public bool firstBossKilled;
	public bool secondBossKilled;
	public bool thirdBossKilled;
	public bool haveKey;
}
