using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOB : MonoBehaviour
{
	public Sprite still;
	public Sprite floating;
	// Use this for initialization
	void Start ()
	{
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Ground"), LayerMask.NameToLayer ("Spirit"));
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Enemy"), LayerMask.NameToLayer ("Spirit"));
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) > 0.5) {
			GetComponent<SpriteRenderer> ().sprite = floating;
		} else {
			GetComponent<SpriteRenderer> ().sprite = still;
		}
	}

}
