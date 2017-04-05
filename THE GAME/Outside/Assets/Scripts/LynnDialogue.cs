using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LynnDialogue : MonoBehaviour {
	public Image image;
	public Text text;

	public int currentMap;
	public bool newMap;

	private float opacity;
	private float sizeX;
	private float delay;
	private float timer;
	private float showDelay;
	private float showTimer;
	private bool showDialogue;

	// Use this for initialization
	void Start () {
		opacity = 0;
		delay = 1;
		timer = delay;
		showDelay = 7;
		showTimer = showDelay;
		showDialogue = false;
		sizeX = 0;
		newMap = true;
		image.color = new Color(1f,1f,1f,opacity);
		gameObject.GetComponent<Image> ().color = new Color(1f,1f,1f,opacity/3);
		gameObject.GetComponent<Image> ().rectTransform.sizeDelta = new Vector2 (sizeX, gameObject.GetComponent<Image> ().rectTransform.sizeDelta.y);
	}
	
	// Update is called once per frame
	void Update () {
		image.color = new Color(1f,1f,1f,opacity);
		gameObject.GetComponent<Image> ().color = new Color(1f,1f,1f,opacity/3);
		gameObject.GetComponent<Image> ().rectTransform.sizeDelta = new Vector2 (sizeX, gameObject.GetComponent<Image> ().rectTransform.sizeDelta.y);
		if (newMap) {
			opacity = 0;
			sizeX = 0;
			showTimer = showDelay;
			text.text = "";
			showDialogue = false;
			if (timer >= 0) {
				timer -= Time.deltaTime;
			}
			if (timer <= 0) {
				timer = delay;
				showDialogue = true;
				newMap = false;
			}
		}
		if (currentMap < 202) {
			if (showDialogue) {
				if (opacity < 1) {
					opacity += Time.deltaTime;
				}
				if (sizeX < Screen.width / 2) {
					sizeX += Time.deltaTime * 600;
				}
				if (opacity >= 1 && sizeX >= Screen.width / 2) {
					text.text = retrieveDialogue (currentMap);
				}
				showTimer -= Time.deltaTime;
				if (showTimer < 0) {
					text.text = "";
					showDialogue = false;
					showTimer = showDelay;
				}
			} else {
				if (opacity >= 0) {
					opacity -= Time.deltaTime;
				}
				if (sizeX >= 0) {
					sizeX -= Time.deltaTime * 600;
				}
			}
		}

	}
	string retrieveDialogue(int i) {
		if (i == 101) {
			return "I'm guessing the harvesters destroyed one of our sacred trees, and caused all this. There's an open area roughly a mile from here. Take this seed and plant it there, then the curse should be cleared. Good Luck!";
		}
		if (i == 102) {
			return "Remember the elixir I gave you? Drink a sip and your soul can see the things that your eyes can't. You don't have that much so use it wisely.";
		}
		if (i == 103) {
			return "The vine tree will heal you and give you some elixir. Touching the tree with your spirit will also save your progress.";
		}
		if (i == 104) {
			return "Defeating the wild animals will allow you to free their soul which will give you some elixir and health in return.";
		}
		if (i == 105) {
			return "If you don't want to fight the wild animals, you can dodge roll past them. Or use your bow and arrow to shoot them from safety.";
		}
		if (i == 106) {
			return "I just remembered, there are giant gate doors that will prevent you from proceeding further later on. I believe the key can be found in a chest down below.";
		}
		if (i == 107) {
			return "If you think you are stuck, leave and come back again. The curse seems to reset the environment.";
		}
		if (i == 108) {
			return "Remember, you can see things that you normally can't when you are in soul form.";
		}
		if (i == 109) {
			return "There is a corrupted deer hanging around nearby, it is very dangerous, be careful.";
		}
		if (i == 110) {
			return "There it is! looks like you have to defeat it to go further.";
		}
		if (i == 201) {
			return "...This is as far...s the radio....signal goes....loo...like yo....re on your own fr..m here on out...";
		}
		return "error, did not find dialogue";
	}
}
