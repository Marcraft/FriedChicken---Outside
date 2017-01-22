using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
	public GameObject view;
	public float scale;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(view.transform.position.x/scale,view.transform.position.y/scale, transform.position.z);
	}
}
