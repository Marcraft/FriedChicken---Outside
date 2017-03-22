using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
	public GameObject player;
	public GameObject bar;
	public float maxHealth;
	public float currentHealth;
	// Use this for initialization
	void Start () {
		maxHealth = player.GetComponent<PlayerController> ().maxHealth;
		currentHealth = player.GetComponent<PlayerController> ().health;
	}
	
	// Update is called once per frame
	void Update () {
		currentHealth = player.GetComponent<PlayerController> ().health; 
		bar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (GetComponent<RectTransform> ().sizeDelta.x*currentHealth/maxHealth, bar.transform.localScale.y);
		bar.GetComponent<RectTransform> ().localPosition = new Vector2 (-(GetComponent<RectTransform> ().sizeDelta.x/(maxHealth*2))*(maxHealth - currentHealth), bar.transform.localScale.y);
	}
}
