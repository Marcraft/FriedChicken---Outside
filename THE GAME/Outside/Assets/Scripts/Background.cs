using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
	public GameObject view;

	public GameObject level;

	public GameObject layer1;
	public GameObject layer2;
	public GameObject layer3;
	public GameObject layer4;
	public GameObject layer5;

	public float scale1X;
	public float scale1Y;
	public float scale2X;
	public float scale2Y;
	public float scale3X;
	public float scale3Y;
	public float scale4X;
	public float scale4Y;
	public float scale5X;
	public float scale5Y;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(view.transform.position.x,view.transform.position.y, transform.position.z);
		layer1.transform.position = new Vector3(view.transform.position.x/scale1X,view.transform.position.y/scale1Y, transform.position.z);
		layer2.transform.position = new Vector3(view.transform.position.x/scale2X,(-level.GetComponent <Level> ().getLevelHeight () / 2 + 1) * level.GetComponent <Level> ().gridScale + layer5.GetComponent<SpriteRenderer>().bounds.size.y/2 + view.transform.position.y/scale2Y, transform.position.z);
		layer3.transform.position = new Vector3(view.transform.position.x/scale3X,(-level.GetComponent <Level> ().getLevelHeight () / 2 + 1) * level.GetComponent <Level> ().gridScale + layer5.GetComponent<SpriteRenderer>().bounds.size.y/2 + view.transform.position.y/scale3Y, transform.position.z);
		layer4.transform.position = new Vector3(view.transform.position.x/scale4X,(-level.GetComponent <Level> ().getLevelHeight () / 2 + 1) * level.GetComponent <Level> ().gridScale + layer5.GetComponent<SpriteRenderer>().bounds.size.y/2 + view.transform.position.y/scale4Y, transform.position.z);
		layer5.transform.position = new Vector3(view.transform.position.x/scale5X,(-level.GetComponent <Level> ().getLevelHeight () / 2 + 1) * level.GetComponent <Level> ().gridScale + layer5.GetComponent<SpriteRenderer>().bounds.size.y/2 + view.transform.position.y/scale5Y, transform.position.z);
	}
}
