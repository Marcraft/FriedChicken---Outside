using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
	private LevelDesigns levelDesigns;
	public Color32[,] levelDesign;
	private int levelWidth;
	private int levelHeight;

	public int levelChoice;
	public float gridScale;

	public GameObject ground;
	public GameObject platform;
	public GameObject ladder;
	public GameObject teleport;
	// Use this for initialization
	void Start () {
		levelDesign = gameObject.GetComponentInChildren<LevelDesigns> ().getLevelDesign(levelChoice);

		levelWidth = levelDesign.GetLength (0);
		levelHeight = levelDesign.GetLength (1);
		for (int i = 0; i < levelWidth; i++) {
			for (int j = 0; j < levelHeight; j++) {
				//OUTSIDE BORDER
				if (i == 0 || i == levelWidth - 1 || j == 0 || j == levelHeight - 1) {
					//ground
					if (compare (levelDesign [i, j], 0, 0, 0)) {
						GameObject currentTile = (GameObject)Instantiate (ground, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
						currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().ground2;
					}
					//teleport
					else if (compare (levelDesign [i, j], 0, 255, 255)) {
						GameObject currentTile = (GameObject)Instantiate (teleport, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
					}
				} 
				//INSIDE
				//ground
				else if (compare(levelDesign [i, j], 0,0,0)) {
					GameObject currentTile = (GameObject)Instantiate (ground, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
					if (!compare(levelDesign [i, j + 1], 0,0,0)) {
						currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().ground1;
					} else if (!compare(levelDesign [i, j - 1], 0,0,0)) {
						currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().ground3;
					} else {
						currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().ground2;
					}
				} 
				//platforms and ladders
				else if (compare(levelDesign [i, j], 128,128,128)) {
					if (compare(levelDesign [i, j+1], 128,128,128) || compare(levelDesign [i, j-1], 128,128,128)) {
						if (!compare(levelDesign [i, j+1], 128,128,128)) {
							Instantiate (platform, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
							Instantiate (ladder, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
						}
						else if(!compare(levelDesign [i, j+1], 128,128,128)) {
							Instantiate (platform, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
						}else {
							Instantiate (ladder, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
						}
					} else {
						Instantiate (platform, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
					}
				} 
				//teleports
				else if (compare (levelDesign [i, j], 0, 255, 255)) {
					GameObject currentTile = (GameObject)Instantiate (teleport, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
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

	public bool compare(Color32 color, int r, int g, int b) {
		if(color.r == r && color.g == g && color.b == b) {
			return true;
		}
		return false;
	}
}
