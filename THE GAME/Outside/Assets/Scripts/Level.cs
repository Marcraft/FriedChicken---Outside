using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
	private LevelDesigns levelDesigns;
	public int[,] levelDesign;
	private int levelWidth;
	private int levelHeight;

	public int levelChoice;
	public float gridScale;

	public GameObject ground;
	// Use this for initialization
	void Start () {
		levelDesign = gameObject.GetComponentInChildren<LevelDesigns> ().getLevelDesign(levelChoice);

		levelWidth = levelDesign.GetLength (0);
		levelHeight = levelDesign.GetLength (1);
		for (int i = 0; i < levelWidth; i++) {
			for (int j = 0; j < levelHeight; j++) {
				if (levelDesign [i, j] == 0) {
					GameObject currentTile = (GameObject)Instantiate (ground, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
					if (i == 0 || i == levelWidth - 1 || j == 0 || j == levelHeight - 1) {
						currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().ground2;
					} else {
						if (levelDesign [i, j + 1] == 1) {
							currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().ground1;
						} else if (levelDesign [i, j - 1] == 1) {
							currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().ground3;
						} else {
							currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().ground2;
						}
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getLevelWidth() {
		return levelWidth;
	}

	public int getLevelHeight() {
		return levelHeight;
	}
}
