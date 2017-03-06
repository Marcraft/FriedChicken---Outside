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
		showDelay = 5;
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
		if (showDialogue) {
			if (opacity < 1) {
				opacity += Time.deltaTime;
			}
			if (sizeX < Screen.width / 2) {
				sizeX += Time.deltaTime * 600;
			}
			if (opacity >= 1 && sizeX >= Screen.width / 2) {
				text.text = retrieveDialogue(currentMap);
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
	string retrieveDialogue(int i) {
		if (i == 101) {
			return "Thank you for doing the favor for us, we owe you a huge debt.";
		}
		if (i == 102) {
			return "The outbreak caused those wild animals to become aggressive, watch yourselves out there.";
		}
		if (i == 103) {
			return "Remember the elixir I gave you? Your soul can see the things that your eyes can't, use it wisely.";
		}
		if (i == 104) {
			return "Please do be careful, the forest can be a dangerous place.";
		}
		if (i == 105) {
			return "Careful, the trap with the spikes were used to keep invaders from entering the forest, you don't want to fall in there.";
		}
		if (i == 106) {
			return "The elixir's energy is not endless, try to explore around for more energy sources.";
		}
		if (i == 107) {
			return "If you accidentally block your way, come back again later, the environment will fix itself.";
		}
		if (i == 108) {
			return "Remember, you can see things that you normally can't when you are in soul form.";
		}
		if (i == 109) {
			return "There is a corrupted deer hanging around in this place, it is very dangerous, ready yourself.";
		}
		return "error, did not find dialogue";
	}
}
