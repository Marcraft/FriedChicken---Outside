using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChange : MonoBehaviour {
	public GameObject level;

	public SpriteRenderer layer1;
	public SpriteRenderer layer2;
	public SpriteRenderer layer3;
	public SpriteRenderer layer4;
	public SpriteRenderer layer5;
	public SpriteRenderer layer6;
	public SpriteRenderer layer7;
	public SpriteRenderer layer8;

	public Sprite levelOne1;
	public Sprite levelOne2;
	public Sprite levelOne3;
	public Sprite levelOne4;
	public Sprite levelOne5;
	public Sprite levelOne6;
	public Sprite levelOne7;
	public Sprite levelOne8;

	public Sprite levelTwo1;
	public Sprite levelTwo2;
	public Sprite levelTwo3;
	public Sprite levelTwo4;
	public Sprite levelTwo5;
	public Sprite levelTwo6;
	public Sprite levelTwo7;

	public int currentLevel;
	public int previousLevel;

	// Use this for initialization
	void Start () {
		currentLevel = level.GetComponent<Level> ().levelChoice;
		previousLevel = currentLevel;

	}
	
	// Update is called once per frame
	void Update () {
		currentLevel = level.GetComponent<Level> ().levelChoice;
		if (currentLevel > 200 && previousLevel < 200) {
			layer1.sprite = levelTwo1;
			layer2.sprite = levelTwo2;
			layer3.sprite = levelTwo3;
			layer4.sprite = levelTwo4;
			layer5.sprite = levelTwo5;
			layer6.sprite = levelTwo6;
			layer7.sprite = levelTwo7;
		}
		if (currentLevel < 200 && previousLevel > 200) {
			layer1.sprite = levelOne1;
			layer2.sprite = levelOne2;
			layer3.sprite = levelOne3;
			layer4.sprite = levelOne4;
			layer5.sprite = levelOne5;
			layer6.sprite = levelOne6;
			layer7.sprite = levelOne7;
			layer8.sprite = levelOne8;
		}
		previousLevel = currentLevel;
	}
}
