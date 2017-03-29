using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
	public bool hitEnemy = true;
	public PlayerController playerController;
	// Use this for initialization
	void Start () {
		playerController = gameObject.GetComponentInParent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerController.combatState == PlayerController.CombatState.melee) {
			GetComponent<BoxCollider2D> ().enabled = true;
		} else {
			GetComponent<BoxCollider2D> ().enabled = false;
		}
		
	}
	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Enemy")){
			hitEnemy = true;
			other.GetComponent<Enemy> ().hurt = true;
		}
		if(other.CompareTag("Boss")){
			hitEnemy = true;
			other.GetComponent<Boss> ().hit = true;
		}
	}
}
