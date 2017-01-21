using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
	public GameObject view;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(view.transform.position.x/2,view.transform.position.y/2, transform.position.z);
	}
}
