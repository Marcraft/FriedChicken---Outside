using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningCutscenes : MonoBehaviour {

	public Text text;
	public Image image;
	private int textSelection;

	public Sprite cutscene01;
	public Sprite cutscene02;
	public Sprite cutscene03;
	public Sprite cutscene04;
	public Sprite cutscene05;
	public Sprite cutscene06;
	public Sprite cutscene07;
	public Sprite cutscene08;
	public Sprite cutscene09;

	// Use this for initialization
	void Start () {
		textSelection = 0;
		text.text = "(press any key to continue...)";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			textSelection += 1;
			text.text = GetText (textSelection);
			if (textSelection == 4) {
				image.sprite = cutscene02;
			}
			if (textSelection == 6) {
				image.sprite = cutscene03;
			}
			if (textSelection == 9) {
				image.sprite = cutscene04;
			}
			if (textSelection == 11) {
				image.sprite = cutscene05;
			}
			if (textSelection == 14) {
				image.sprite = cutscene06;
			}
			if (textSelection == 16) {
				image.sprite = cutscene07;
			}
			if (textSelection == 17) {
				image.sprite = cutscene08;
			}
			if (textSelection == 18) {
				image.sprite = cutscene09;
			}
			if (textSelection == 21) {
				SceneManager.LoadScene ("game");
			}
		}
	}

	string GetText(int selection) {
		if (selection == 1) {
			return "My name is Jake Dahle.";
		}
		if (selection == 2) {
			return "I’m a researcher hoping to learn more about my mother’s side of the family, who happens to be Native American.";
		}
		if (selection == 3) {
			return "My mom loved my enthusiasm when I was a kid, so she talked about our family history very often. She even convinced my grandfather to teach me archery and war club combat.";
		}
		if (selection == 4) {
			return "This is Sasq’ets, a Coast Salish village still immersed in their tradition due to their isolation and distance from the cities.";
		}
		if (selection == 5) {
			return "I was looking for a place that could provide me with experience through work done in the field, and I was contacted by one of the villagers via email.";
		}
		if (selection == 6) {
			return "I have been in touch with Lynn for a little over a year for this trip. She filled me in on the details of how to get here and also arranged a place for me to stay.";
		}
		if (selection == 7) {
			return "She shares my enthusiasm when it comes Native American culture, but unlike me, she’s immersed in it due to her grandmother being the village shaman.";
		}
		if (selection == 8) {
			return "We made a deal, I’d be given an opportunity to experience her people’s rituals first-hand, if I help their village with a certain problem they’re facing.";
		}
		if (selection == 9) {
			return "I’m not sure of the specifics of her dilemma, but she says it began after a portion of their forest was cut down.";
		}
		if (selection == 10) {
			return "Majority of the team of men who instigated the forest clearing were killed, and the bodies were never found.";
		}
		if (selection == 11) {
			return "Her grandmother insists that their disrespect towards nature caused the spirits to inflict a curse on anyone born in the village or the forest that surrounds it.";
		}
		if (selection == 12) {
			return "All the animals, including pets, were now hostile towards people.";
		}
		if (selection == 13) {
			return "When any villager entered the forest, their mental state degraded and they behaved much like the animals. However, individuals from other places, like the surviving loggers, are unaffected.";
		}
		if (selection == 14) {
			return "I suggested that maybe a doctor would be more help than a masters student, but she says attempts at medicine were unsuccessful and only aggravated the symptoms.";
		}
		if (selection == 15) {
			return "She and her grandmother created a brew that would allow me to see beyond the physical world to prove that the source of the problem was indeed spiritual.";
		}
		if (selection == 16) {
			return "I was probably a little careless by not thinking that decision through, who knows what’s in that thing? But I was one of those people that would do anything for an experience like that, so I gave it a shot.";
		}
		if (selection == 17) {
			return ". . . . . . . .";
		}
		if (selection == 18) {
			return "I stared at the creatures I have only heard of through stories and books with my very eyes.";
		}
		if (selection == 19) {
			return "Even though I was on the ground physically, Lynn looked straight at my standing, yet bewildered corporeal form. She pointed at my body while saying if I leave myself vulnerable, I can still be hurt.";
		}
		if (selection == 20) {
			return "She then walked towards the entrance of the forest.";
		}
		if (selection == 21) {
			return ". . . . . . . .";
		}
		return ". . . . . . . .";
	}
}
