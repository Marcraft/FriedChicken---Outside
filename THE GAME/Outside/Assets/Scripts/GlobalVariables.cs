using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Spawn {
	left, 
	right,
	up,
	down
}

public class GlobalVariables : MonoBehaviour {
	
	Spawn spawn = Spawn.left;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Spawn getSpawn() {
		return spawn;
	}
	public void setSpawn(Spawn spawn) {
		this.spawn = spawn;
	}
}
