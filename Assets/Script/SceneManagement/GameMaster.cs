using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Ikira

public class GameMaster : MonoBehaviour
{

	public CanvasGroup BlackCanvas;

	[Header("Animation time")] //Time animation takes to complete its process.
	public float _time;
	[Header("Animation fade")] //Time fade animation takes to opaque/transparent.
	public float _fadeTime;

	[Header("Scene Index")]   //Int for scene index
	public int sceneIndex = 0;

	public static GameMaster fadeIn_out;
	[Header("Manger Bool Object")]
	bool created;


	void Awake()
	{
		if (!created)
		{
			DontDestroyOnLoad(this.gameObject);
			created = true;
		}

	}

	public void Start() {}

	//Update is called once per frame
	void Update() {}

	//Coroutine for fading in new scene
	IEnumerator FadeInScene()
	{
		//Fading in to the new scene (making the canvas transparent)
		while (BlackCanvas.alpha > 0)
		{
			//Increase time
			_time += Time.deltaTime;
			//Update alpha
			BlackCanvas.alpha = Mathf.Clamp01(1.0f - (_time / _fadeTime));
			//Pause coroutine (yield to rest of application process)
			yield return null;
		}
		//Reset time for next fade
		_time = 0;

		//Done fading in
		Debug.Log("!!!~~~~~ALERT~~~~!!! CANVAS IS AT 0.0F ALPHA ----- DONE FADING IN - LEVEL LOADED");

	}

	//Coroutine for fading out old scene
	IEnumerator FadeOutScene(int sceneIndex)
	{
		//Fading out from the old scene (making the canvas opaque)
		while (BlackCanvas.alpha < 1)
		{
			//Increase time
			_time += Time.deltaTime;
			//Update alpha
			BlackCanvas.alpha = Mathf.Clamp01((_time / _fadeTime) - 0.0f);
			//Pause coroutine (yield to rest of application process)
			yield return null;
		}
		//Reset time for next fade
		_time = 0;

		//Done fading out
		Debug.Log("!!!~~~~~ALERT~~~~!!! CANVAS IS AT 1.0F ALPHA ----- DONE FADING OUT - LOADING LEVEL");

		//Passing scene index to fade level param.
		Debug.Log("Passing scene index..");
		Debug.Log("invoking level change...");
		yield return new WaitForSeconds(.5f);
		//Load the next level
		LevelLoad(sceneIndex);
	}


	//Level Load Wrapper
	void LevelLoad(int sceneIndex)
	{
		SceneManager.LoadScene(sceneIndex);
		StartCoroutine(FadeInScene());
	}

	//Fade Out Coroutine Wrapper
	void FadeOut(int sceneIndex)
	{
		//Initiate fading out
		StartCoroutine(FadeOutScene(sceneIndex));
		SceneName();
	}

	public void NewFade(int sceneIndex)
	{
		FadeOut(sceneIndex);
	}

	public void SceneName()
	{
		Debug.Log("Current active scene is " + SceneManager.GetActiveScene().name + "."); //Used to get current scene name
	}
}


//Pass scene names from UI canvas buttons to transition between scenes - DONE

//TODO: Solve lighting issue when transition is achieve (canvas has black tinge to it) - DONE

//Use the canvas buttons to call the "wrapper"function of the fade out mechanism - DONE
//Then pass the chosen scene index number in the LoadLevel() once the coroutine finishes - DONE
//POS.Solution:Website covers var passing through coroutine, reference it to code. - DONE

//Problems so far: Unable to reference scene index number 
//from MainMenu scene -> Chem Laboratory Trigger (red button)
	//Game object is continued as inactive once scene has been loaded despite object being active in scene?
	//Possible solutions involve iterating current singleton class out of monobehavior extension but unsure how to proceed.

	//TODO: Continue working into chem lab space, multiply over to bio and physics.
	//Design sub-classes for singleton to use during other runtime routines (i.e. maybe different metric for different labs?)