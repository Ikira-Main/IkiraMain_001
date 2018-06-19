using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectScrollLook : MonoBehaviour
{
	public GameObject[] subjectsArray;
	public Transform subjectCamera;

	[Header("Index number")]
	public int index = 0;

	bool isRunning = false;

	//float xAxisClamp = 0.0f;

	void Awake()
	{
		//Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		//Key pressed to run coroutine
		//if (Input.GetKeyDown(KeyCode.A))
		//{
		//	isRunning = true;
		//	Debug.Log("A key is being pressed");
		//	StartCoroutine(RotateCamera());
		//}
	}

	//Coroutine to iterate through subjects array and return an index within the array that the camera will 'snap' to as a lookAt() transform.
	IEnumerator RotateCamera()
	{

		//While true, loop through array once and return gameobject transform position for the camera to look at.
		while (isRunning)
		{
			//Iterate through gameobject array and return gameobject position, if index is at array length reset index;
			if (index >= subjectsArray.Length - 1)
			{
				index = 0;
				Debug.Log("Array has reached length, resetting indicie...");
			}
			else
			{
				index = index + 1;
				Debug.Log("Iterating through array");
				Debug.Log("Changing camera transform..");
			}

			subjectCamera.LookAt(subjectsArray[index].gameObject.transform);
			Debug.Log("Camera position is at " + subjectsArray[index].name + " position.");
			//Return gameobject transform to camera lookAt() function to "scroll" through array





			////subjectsArray[index];
			//Debug.Log("inside while");
			//if (index < subjectsArray.Length)
			//{
			//	index = (index < subjectsArray.Length) ? index + 1 : 0;
			//Debug.Log("Iterating through array");

			//}
			//else if(index == subjectsArray.Length)
			//{
			//Debug.Log("Subjects array is at array length");
			//	index = index - (subjectsArray.Length);
			//	yield return index;
			//}
			//subjectCamera.LookAt(subjectsArray[index].transform);
			Debug.Log("Exiting the while loop");
			isRunning = false;
			yield return null;
		}
		Debug.Log(isRunning);
	}

	//void RotateCamera()
	//{
	//	int i;
	//	for (i = 0; i < subjectsArray.Length; i++)
	//	{
	//		//Debug.Log("Inserting into array for loop");
	//		//if (i == subjectsArray.Length - 1)
	//		//{
	//		//	Debug.Log("If i equal array length, i returns to 0");
	//		//	i = 0;
	//		//}
	//		//else
	//		//{
	//		//	Debug.Log("Else i++");
	//			i++;
	//		//}
	//	}
	//	Debug.Log("Camera position is at " + subjectsArray[i] + " position.");
	//}



}


//Create a method to "cycle" which door the player looks at via a canvas button and a lookAt() feature. This will eliminiate the need for a first person character controller should the project be mobile-centric.