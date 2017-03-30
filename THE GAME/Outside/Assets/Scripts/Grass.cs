using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour {
	private Level level;
	public Sprite grass;
	public Sprite rocks;
	public Sprite thickGrass;
	// Use this for initialization
	void Start () {
		level = GameObject.FindWithTag ("Level").GetComponent<Level> ();
		if (level.levelChoice < 200) {
			GetComponent<SpriteRenderer>().sprite = grass;
		} else if (level.levelChoice < 300) {
			GetComponent<SpriteRenderer>().sprite = rocks;
		} else {
			GetComponent<SpriteRenderer>().sprite = thickGrass;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
