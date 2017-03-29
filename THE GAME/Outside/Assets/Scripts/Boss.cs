using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
	public float health;
	public bool hit;
	private float hurtTimer;
	private float hurtTime;
	public bool dead;
	// Use this for initialization
	void Start () {
		hit = false;
		dead = false;
		hurtTimer = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (hit) {
			health--;
			hurtTime = hurtTimer;
			hit = false;
		}
		if (hurtTime > 0) {
			hurtTime -= Time.deltaTime;
			GetComponent<SpriteRenderer> ().color = new Color (1, 0, 0);
		} else {
			GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1);
		}
		if (health <= 0) {
			dead = true;
		}
	}
}
