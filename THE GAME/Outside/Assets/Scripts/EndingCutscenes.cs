using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingCutscenes : MonoBehaviour {

	public Text text;
	public Image image;
	private int textSelection;

	public Sprite cutscene10;
	public Sprite cutscene11;
	public Sprite cutscene12;
	public Sprite cutscene13;
	public Sprite cutscene14;

	GameObject menuControls;

	// Use this for initialization
	void Start () {
		menuControls = GameObject.FindWithTag ("MenuControls");
		textSelection = 0;
		text.text =  ". . . . . . . .";
	}

	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			textSelection += 1;
			text.text = GetText (textSelection);
			if (textSelection == 2) {
				image.sprite = cutscene11;
			}
			if (textSelection == 3) {
				image.sprite = cutscene12;
			}
			if (textSelection == 4) {
				image.sprite = cutscene13;
			}
			if (textSelection == 7) {
				image.sprite = cutscene14;
			}
			if (textSelection == 10) {
				Destroy (menuControls.gameObject);
				SceneManager.LoadScene ("menu");
			}
		}
	}

	string GetText(int selection) {
		if (selection == 1) {
			return ". . . . . . . .";
		}
		if (selection == 2) {
			return ". . . . . . . .";
		}
		if (selection == 3) {
			return "Lynn beamed at me saying she knew the curse was gone when the sunlight broke through the trees.";
		}
		if (selection == 4) {
			return "I told her every piece of my experience was amazing and priceless despite the pain, which i hoped was temporary.";
		}
		if (selection == 5) {
			return "I asked her if we could work together long-term, and - after a lot of stuttering -  I asked if we could talk about it over dinner.";
		}
		if (selection == 6) {
			return ". . . . . . . .";
		}
		if (selection == 7) {
			return "I'll be meeting her at 8 tonight.";
		}
		if (selection == 8) {
			return "THE END!";
		}
		if (selection == 9) {
			return "Thanks for playing!";
		}
		if (selection == 10) {
			return "Thanks for playing!";
		}
		return ". . . . . . . .";
	}
}
