using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour {
	GameObject menuControls;
	// Use this for initialization
	void Start () {
		menuControls = GameObject.FindWithTag ("MenuControls");
	}
	
	// Update is called once per frame
	void Update () {
		if (menuControls.GetComponent<MenuControls>().haveKey) {
			GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);
		} else {
			GetComponent<Image> ().color = new Color (1f, 1f, 1f, 0f);
		}
		
	}
}
