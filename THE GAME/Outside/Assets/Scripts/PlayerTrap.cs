using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrap : MonoBehaviour
{
	public GameObject trap;
	public bool playerTrapped;
	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			playerTrapped = true;
			Instantiate (trap, new Vector3 (other.gameObject.GetComponent<Rigidbody2D> ().position.x, GetComponent<Rigidbody2D>().position.y, 0), Quaternion.Euler (new Vector3 (0, 0, 0)));
		}
	}
}
