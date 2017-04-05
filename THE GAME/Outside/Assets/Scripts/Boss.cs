using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
	public float health;
	public float maxHealth;
	public bool hit;
	private float hurtTimer;
	private float hurtTime;
	public bool dead;

	public GameObject leftLeg;
	public GameObject rightLeg;
	public GameObject saw;
	public GameObject drill;

	public Sprite health100;
	public Sprite health75;
	public Sprite health50;
	public Sprite health25;
	public Sprite health0;
	public Sprite legStand;
	public Sprite legJump;


	// Use this for initialization
	void Start () {
		hit = false;
		dead = false;
		hurtTimer = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead) {
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
		} else { //if dead

		}
	}
}
