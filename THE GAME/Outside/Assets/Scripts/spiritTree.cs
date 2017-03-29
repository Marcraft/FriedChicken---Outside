using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class spiritTree : MonoBehaviour {
	public PlayerController player;
	private float target;
	private float opacity;
	private float difference;
	private bool pulse;
	private float pulsing;
	public float scale;
	public float saveDelay;

	public int currentLevel;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
		opacity = 0;
		pulse = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.state == PlayerController.State.outofbody) {
			target = 1;
		} else {
			target = 0;
			pulse = false;
		}
		if (saveDelay > 0) {
			saveDelay -= Time.deltaTime;
		}
	}
	void FixedUpdate() {
		if (pulse) {
			pulsing += Time.deltaTime;
			if (pulsing > Mathf.PI*2) {
				pulsing = 0;
			}
			GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, opacity + (Mathf.Cos(pulsing)-1)/4);
		} else {
			difference = target - opacity;
			opacity = opacity + difference / scale;
			if (opacity < 0.01f) {
				opacity = 0;
			}
			if (opacity > 0.99f) {
				opacity = 1;
				pulse = true;
				pulsing = 0;
			}
			GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, opacity);
		}
	}
	void OnTriggerStay2D(Collider2D other) {
		if (player.state == PlayerController.State.outofbody) {
			if (other.gameObject.CompareTag ("PlayerSpirit")) {
				if (saveDelay <= 0) {
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
						data.map = currentLevel;
						data.firstBossKilled = firstBossKilled;
						data.secondBossKilled = secondBossKilled;
						data.thirdBossKilled = thirdBossKilled;
						bf.Serialize (file, data);
						file.Close ();
					} else {
						file = File.Create (Application.persistentDataPath + "/OutsideSave.dat");
						data = new SaveFile ();
						data.map = currentLevel;
						bf.Serialize (file, data);
						file.Close ();
					}
					player.health = player.maxHealth;
					if (player.elixir <= player.defaultElixir) {
						player.elixir = player.defaultElixir;
					}
					saveDelay = 5;
				}
			}
		}
	}
}
