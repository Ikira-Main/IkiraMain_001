using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRot : MonoBehaviour {

	[Header("Cube Rotation")]
	public GameObject cubeRot;

	public int cubeRot_x;
	public int cubeRot_z;
	public int _time;
	
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		while (_time < 0)
		{
			cubeRot_x+= 1;
			cubeRot_z+= 1;
		}
		cubeRot.transform.rotation = Quaternion.Euler(cubeRot_x, 0, cubeRot_z);
	}
}
