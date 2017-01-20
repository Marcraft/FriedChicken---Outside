using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
	private LevelDesigns levelDesigns;
	public int[,] levelDesign;

	public int levelChoice;
	public float gridScale;

	public GameObject ground;
	// Use this for initialization
	void Start () {
		levelDesign = gameObject.GetComponentInChildren<LevelDesigns> ().getLevelDesign(0);

		int levelWidth = levelDesign.GetLength (0);
		int levelHeight = levelDesign.GetLength (1);
		for (int i = 0; i < levelWidth; i++) {
			for (int j = 0; j < levelHeight; j++) {
				if (levelDesign [i, j] == 1) {
					GameObject currentTile = (GameObject)Instantiate (ground, new Vector3 (gridScale * (levelWidth / 2 - j), gridScale * (levelHeight / 2 - i), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
