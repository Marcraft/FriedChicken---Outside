using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOB : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Ground"), LayerMask.NameToLayer ("Spirit"));
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

}
