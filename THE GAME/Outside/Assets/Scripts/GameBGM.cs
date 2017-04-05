using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGM : MonoBehaviour {
	public AudioClip level1;
	public AudioClip level1boss;
	public AudioClip level2;
	public AudioClip level2boss;
	public GameObject level;
	void Awake() {
		Destroy(GameObject.FindWithTag ("titlebgm"));
	}
	// Use this for initialization
	public void Start () {
		if (level.GetComponent<Level> ().levelChoice < 200) {
			GetComponent<AudioSource> ().clip = level1;
			GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Stop ("cave");
			GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Stop ("stream");
			GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("wind");
			GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("sea");
		} else {
			GetComponent<AudioSource> ().clip = level2;
			GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Stop ("wind");
			GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Stop ("sea");
			GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("cave");
			GameObject.FindWithTag ("SoundBoard").GetComponent<SoundBoard> ().Play ("stream");
		}
		GetComponent<AudioSource> ().Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void changeSong(int i) {
		if (i == 1) {
			GetComponent<AudioSource> ().clip = level1;
		}
		if (i == 2) {
			GetComponent<AudioSource> ().clip = level1boss;
		}
		if (i == 3) {
			GetComponent<AudioSource> ().clip = level2;
		}
		if (i == 4) {
			GetComponent<AudioSource> ().clip = level2boss;
		}
		GetComponent<AudioSource> ().Play();
	}
}
