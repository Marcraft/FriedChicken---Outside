using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
	public GameObject player;
	public GameObject mainCamera;
	public GameObject canvas;
	public GameObject level;

	public TriggerScript leftTrigger;
	public TriggerScript rightTrigger;
	public TriggerScript upTrigger;
	public TriggerScript downTrigger;

	public int currentLevel;

	private float opacity;
	private bool FoundSpawn;
	private bool changeMap;


	public enum Spawn
	{
		left,
		right,
		up,
		down
	}

	public Spawn spawn = Spawn.left;
	private Spawn lastExit;

	void Awake() {
		level.GetComponent<Level> ().levelChoice = currentLevel;
	}

	// Use this for initialization
	void Start ()
	{
		changeMap = false;
		FoundSpawn = false;
		canvas.GetComponent<CanvasRenderer> ().SetAlpha (opacity);

	}

	// Update is called once per frame
	void Update ()
	{
		if (!FoundSpawn && level.GetComponent<Level>().levelReady) {
			if (spawn == Spawn.left) {
				mainCamera.transform.position = new Vector3 (leftTrigger.spawnPlayer ().x, leftTrigger.spawnPlayer ().y, -10);
				player.transform.position = leftTrigger.spawnPlayer ();

			}
			if (spawn == Spawn.right) {
				mainCamera.transform.position = new Vector3 (rightTrigger.spawnPlayer ().x, rightTrigger.spawnPlayer ().y, -10);
				player.transform.position = rightTrigger.spawnPlayer ();
			}
			if (spawn == Spawn.up) {
				mainCamera.transform.position = new Vector3 (upTrigger.spawnPlayer ().x, upTrigger.spawnPlayer ().y, -10);
				player.transform.position = upTrigger.spawnPlayer ();
			}
			if (spawn == Spawn.down) {
				mainCamera.transform.position = new Vector3 (downTrigger.spawnPlayer ().x, downTrigger.spawnPlayer ().y, -10);
				player.transform.position = downTrigger.spawnPlayer ();
			}
			FoundSpawn = true;
		}
		////////////////
		if (leftTrigger.changeScene) {
			spawn = Spawn.right;
			leftTrigger.changeScene = false;
			changeMap = true;
		} else if (rightTrigger.changeScene) {
			spawn = Spawn.left;
			rightTrigger.changeScene = false;
			changeMap = true;
		} else if (upTrigger.changeScene) {
			spawn = Spawn.down;
			upTrigger.changeScene = false;
			changeMap = true;
		} else if (downTrigger.changeScene) {
			spawn = Spawn.up;
			downTrigger.changeScene = false;
			changeMap = true;
		}
		////////////////
		if (changeMap) {
			canvas.GetComponent<CanvasRenderer> ().SetAlpha (opacity);
			opacity += Time.deltaTime * 5;
			if (opacity >= 1.5) {
				//----CHANGE LEVEL-----//
				changeMap = false;
				FoundSpawn = false;
				player.transform.position = new Vector2();

				player.GetComponent<PlayerController> ().changeScene = false;
				level.GetComponent<Level> ().levelChoice = chooseNextLevel ();
				level.GetComponent<Level> ().resetLevel ();
				Start ();
				player.GetComponent<PlayerController> ().canClimb = 0;
				//--------------------//

			}
		} else {
			if (opacity >= 0) {
				opacity -= Time.deltaTime * 5;
				canvas.GetComponent<CanvasRenderer> ().SetAlpha (opacity);
			}
		}
	}

	public int chooseNextLevel ()
	{
		int nextLevel = currentLevel;
		if (spawn == Spawn.left) {
			if (currentLevel == 101)
				nextLevel = 102;
			if (currentLevel == 102)
				nextLevel = 103;
			if (currentLevel == 103)
				nextLevel = 104;
			if (currentLevel == 104)
				nextLevel = 105;
			if (currentLevel == 105)
				nextLevel = 106;
		} else if (spawn == Spawn.right) {
			if (currentLevel == 101)
				nextLevel = 0;
			if (currentLevel == 102)
				nextLevel = 101;
			if (currentLevel == 103)
				nextLevel = 102;
			if (currentLevel == 104)
				nextLevel = 103;
			if (currentLevel == 105)
				nextLevel = 104;
			if (currentLevel == 106)
				nextLevel = 105;

		} else if (spawn == Spawn.up) {
			if (currentLevel == 106)
				nextLevel = 108;
			if (currentLevel == 107)
				nextLevel = 106;
			
		} else if (spawn == Spawn.down) {
			if (currentLevel == 106)
				nextLevel = 107;
			if (currentLevel == 108)
				nextLevel = 106;
		}
		currentLevel = nextLevel;
		return nextLevel;
	}
}
