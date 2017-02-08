using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raccoon : MonoBehaviour {
	public GameObject projectile;
	public float timerInterval;
	public bool facingRight;
	private float timer;
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > timerInterval / 5 && timer < timerInterval*4/5) {
			if (player.transform.position.x < transform.position.x) {
				transform.localScale = new Vector3 (1, 1, 1);
				facingRight = false;
			} else {
				transform.localScale = new Vector3 (-1, 1, 1);
				facingRight = true;
			}
		}
		if (timer <= 0) {
			GameObject currentProjectile = (GameObject)Instantiate (projectile, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
			if (facingRight)
				currentProjectile.GetComponent<RaccoonProjectile> ().goingRight = true;
			else {
				currentProjectile.GetComponent<RaccoonProjectile> ().goingRight = false;
			}
			timer = timerInterval;
		}
		timer -= Time.deltaTime;
	}
}
