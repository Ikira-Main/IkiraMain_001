using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class ButtonTrigger : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	}

	private void Awake()
	{
	
	}

	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Player"/* && gameObject.tag == "Chem_button"*/)
		{
			Debug.Log("Player is touching Chem Button");
			//Load appropriate level
			Debug.Log("Loading Chemical Laboratory...");
			//TODO Reference coroutine from GameMaster to start fade out coroutine while passing index reference number from trigger collider to LoadLevel()
			GameMaster.Instance.NewFade("Lab_Chem");
		}
	}
}
