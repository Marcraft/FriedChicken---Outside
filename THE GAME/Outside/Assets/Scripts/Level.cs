using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	private LevelDesigns levelDesigns;
	public Color32[,] levelDesign;
	private int levelWidth;
	private int levelHeight;

	public int levelChoice;
	public float gridScale;
	public float resetTimer;

	public GameObject lynn;
	public GameObject ground;
	public GameObject grass;
	public GameObject platform;
	public GameObject ladder;
	public GameObject teleport;
	public GameObject spawnpoint;
	public GameObject slab;
	public GameObject boulder;
	public GameObject spikes;
	public GameObject weakground;
	public GameObject hiddenplatform;
	public GameObject tallstone;
	public GameObject savetree;
	public GameObject chest;
	public GameObject door;

	public GameObject raccoon;
	public GameObject wolf;
	public GameObject willowisp;

	public GameObject boss1;
	public GameObject boss2;

	public bool levelReady;
	// Use this for initialization
	void Start ()
	{
		levelReady = false;
		levelDesign = gameObject.GetComponentInChildren<LevelDesigns> ().getLevelDesign (levelChoice);

		levelWidth = levelDesign.GetLength (0);
		levelHeight = levelDesign.GetLength (1);
		for (int i = 0; i < levelWidth; i++) {
			for (int j = 0; j < levelHeight; j++) {
				//OUTSIDE BORDER
				if (i == 0 || i == levelWidth - 1 || j == 0 || j == levelHeight - 1) {
					//ground
					if (compare (levelDesign [i, j], 0, 0, 0)) {
						GameObject currentTile = (GameObject)Instantiate (ground, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
						if (levelChoice < 200) {
							currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().ground1;
						} else {
							currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().groundTwo1;
						}
					}
					//teleport
					else if (compare (levelDesign [i, j], 0, 255, 255)) {
						Instantiate (teleport, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
					}
				} 
				//INSIDE
				else if (compare (levelDesign [i, j], 0, 0, 0) || compare (levelDesign [i, j], 0, 128, 128) || compare (levelDesign [i, j], 0, 0, 255)) {
					//ground
					GameObject currentTile;
					if (compare (levelDesign [i, j], 0, 0, 0)) {
						currentTile = (GameObject)Instantiate (ground, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
						currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().ground2;
						if (!compare (levelDesign [i, j + 1], 0, 0, 0) && !compare (levelDesign [i, j + 1], 0, 128, 128) && !compare (levelDesign [i, j + 1], 0, 0, 255)) {
							Instantiate (grass, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
						}
						if (!compare (levelDesign [i, j - 1], 0, 0, 0) && !compare (levelDesign [i, j - 1], 0, 128, 128) && !compare (levelDesign [i, j - 1], 0, 0, 255)) {
							Instantiate (grass, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 180)));
						}
						if (!compare (levelDesign [i + 1, j], 0, 0, 0) && !compare (levelDesign [i + 1, j], 0, 128, 128) && !compare (levelDesign [i + 1, j], 0, 0, 255)) {
							Instantiate (grass, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 270)));
						}
						if (!compare (levelDesign [i - 1, j], 0, 0, 0) && !compare (levelDesign [i - 1, j], 0, 128, 128) && !compare (levelDesign [i - 1, j], 0, 0, 255)) {
							Instantiate (grass, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 90)));
						}
					}
					//weakground
					else if (!compare (levelDesign [i, j], 0, 0, 255)) {
						currentTile = (GameObject)Instantiate (weakground, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));

					}
					//hiddenground
					else {
						Instantiate (hiddenplatform, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
						currentTile = (GameObject)Instantiate (ground, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
					}
					if (levelChoice < 200) {
						int random = Random.Range (0, 20);
						if (random == 0) {
							currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().ground2;
						} else if (random == 1) {
							currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().ground3;
						} else if (random == 2) {
							currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().ground4;
						} else if (random == 3) {
							currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().ground5;
						} else {
							currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().ground1;
						}
					} else if (levelChoice < 300) {
						int random = Random.Range (0, 20);
						if (random == 0) {
							currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().groundTwo2;
						} else if (random == 1) {
							currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().groundTwo3;
						} else if (random == 2) {
							currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().groundTwo4;
						} else if (random == 3) {
							currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().groundTwo5;
						} else {
							currentTile.GetComponent<SpriteRenderer> ().sprite = currentTile.GetComponent<SpriteControl> ().groundTwo1;
						}
					}
				}

				//platforms and ladders
				else if (compare (levelDesign [i, j], 128, 128, 128)) {
					if (compare (levelDesign [i, j + 1], 128, 128, 128) || compare (levelDesign [i, j - 1], 128, 128, 128)) {
						if (!compare (levelDesign [i, j + 1], 128, 128, 128)) {
							Instantiate (platform, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
							Instantiate (ladder, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
						} else if (!compare (levelDesign [i, j + 1], 128, 128, 128)) {
							Instantiate (platform, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
						} else {
							Instantiate (ladder, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
						}
					} else {
						Instantiate (platform, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
					}
				} else if (compare (levelDesign [i, j], 255, 0, 0)) {
					Instantiate (spikes, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
				//teleports
				else if (compare (levelDesign [i, j], 0, 255, 255)) {
					Instantiate (teleport, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
				//spawnpoint
				else if (compare (levelDesign [i, j], 128, 128, 255)) {
					Instantiate (spawnpoint, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
				//slab
				else if (compare (levelDesign [i, j], 0, 128, 0)) {
					Instantiate (slab, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
				//boulder
				else if (compare (levelDesign [i, j], 128, 128, 0)) {
					Instantiate (boulder, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
				//tallstone
				else if (compare (levelDesign [i, j], 64, 64, 64)) {
					Instantiate (tallstone, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
				//raccoon
				else if (compare (levelDesign [i, j], 255, 128, 0)) {
					Instantiate (raccoon, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
				}

				//wolf
				else if (compare (levelDesign [i, j], 255, 128, 128)) {
					Instantiate (wolf, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
				//willowisp
				else if (compare (levelDesign [i, j], 255, 0, 128)) {
					Instantiate (willowisp, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
				//lynn
				else if (compare (levelDesign [i, j], 255, 128, 255)) {
					Instantiate (lynn, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
				//savetree
				else if (compare (levelDesign [i, j], 128, 255, 0)) {
					GameObject saveTree = (GameObject)Instantiate (savetree, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
					saveTree.GetComponentInChildren<spiritTree> ().currentLevel = levelChoice;
				}
				//chest
				else if (compare (levelDesign [i, j], 255, 255, 0)) {
					Instantiate (chest, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2) - 0.25f, -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
				//door
				else if (compare (levelDesign [i, j], 128, 0, 0)) {
					Instantiate (door, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
				//boss1
				else if (compare (levelDesign [i, j], 128, 0, 128)) {
					Instantiate (boss1, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
				//boss2
				else if (compare (levelDesign [i, j], 128, 0, 255)) {
					Instantiate (boss2, new Vector3 (gridScale * (i - levelWidth / 2), gridScale * (j - levelHeight / 2), -1), Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		levelReady = true;
	}

	public int getLevelWidth ()
	{
		return levelWidth;
	}

	public int getLevelHeight ()
	{
		return levelHeight;
	}

	public bool compare (Color32 color, int r, int g, int b)
	{
		if (color.r == r && color.g == g && color.b == b) {
			return true;
		}
		return false;
	}

	public void clearLevel ()
	{
		GameObject[] GameObjects = (FindObjectsOfType<GameObject> () as GameObject[]);
		for (int i = 0; i < GameObjects.Length; i++) {
			if (GameObjects [i].CompareTag ("Arrow") || GameObjects [i].CompareTag ("Boulder") || GameObjects [i].CompareTag ("Enemy")
			    || GameObjects [i].CompareTag ("Slab") || GameObjects [i].CompareTag ("Ground") || GameObjects [i].CompareTag ("tile")
				|| GameObjects [i].CompareTag ("Platform") || GameObjects [i].CompareTag ("Spikes") || GameObjects [i].CompareTag ("Spawn") 
				|| GameObjects [i].CompareTag ("Boulder") || GameObjects [i].CompareTag ("RaccoonProjectile") || GameObjects [i].CompareTag ("BossFight")
				|| GameObjects [i].CompareTag ("SaveTree")|| GameObjects [i].CompareTag ("Lynn") || GameObjects [i].CompareTag ("Orb")) {
				Destroy (GameObjects [i]);
			}
		}
	}
	public void loadLevel() {
		if (resetTimer <= 0) {
			Start ();
		}
	}
}
