using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elixir : MonoBehaviour {

	public GameObject player;

	public Sprite flask0;
	public Sprite flask1;
	public Sprite flask2;
	public Sprite flask3;
	public Sprite flask4;
	public Sprite flask5;
	public Sprite flask6;
	public Sprite flask7;
	public Sprite flask8;
	public Sprite flask9;
	public Sprite flask10;

	Image image;


	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<PlayerController> ().elixir == 0) {
			image.sprite = flask0;
		}
		if (player.GetComponent<PlayerController> ().elixir == 1) {
			image.sprite = flask1;
		}
		if (player.GetComponent<PlayerController> ().elixir == 2) {
			image.sprite = flask2;
		}
		if (player.GetComponent<PlayerController> ().elixir == 3) {
			image.sprite = flask3;
		}
		if (player.GetComponent<PlayerController> ().elixir == 4) {
			image.sprite = flask4;
		}
		if (player.GetComponent<PlayerController> ().elixir == 5) {
			image.sprite = flask5;
		}
		if (player.GetComponent<PlayerController> ().elixir == 6) {
			image.sprite = flask6;
		}
		if (player.GetComponent<PlayerController> ().elixir == 7) {
			image.sprite = flask7;
		}
		if (player.GetComponent<PlayerController> ().elixir == 8) {
			image.sprite = flask8;
		}
		if (player.GetComponent<PlayerController> ().elixir == 9) {
			image.sprite = flask9;
		}
		if (player.GetComponent<PlayerController> ().elixir == 10) {
			image.sprite = flask10;
		}
	}
}
