using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Ikira

public class GameMaster : Singleton<GameMaster>
{
	//Ensures only one copy of this class (prevents creation using "new GameMaster();")
	protected GameMaster() { }

	//Canvas to fade (using alpha)
	public CanvasGroup BlackCanvas;
	
	[Header("Animation Fade Time")] //Time fade animation takes to opaque/transparent.
	public float fadeTime;

	[Header("Current Scene Transition Time - (Do Not Edit)")]   //current transition time
	[SerializeField]
	float time;

	[Header("Current Scene Name")]   //name of current scene
	[SerializeField]
	string currentScene = "MainTitle";

	//has this been created yet (singleton)
	bool created;

	void Awake()
	{
		if (!created)
		{
			DontDestroyOnLoad(this.gameObject);
			created = true;
		}
	}

	public void Start()
	{
		//show starting scene name in debug
		DebugSceneName();
	}

	//Update is called once per frame
	void Update() {}

	//Coroutine for fading in new scene
	IEnumerator FadeInScene()
	{
		//Fading in to the new scene (making the canvas transparent)
		while (BlackCanvas.alpha > 0)
		{
			//Increase time
			time += Time.deltaTime;
			//Update alpha
			BlackCanvas.alpha = Mathf.Clamp01(1.0f - (time / fadeTime));
			//Pause coroutine (yield to rest of application process)
			yield return null;
		}
		//Reset time for next fade
		time = 0;

		//Done fading in
		//Debug.Log("!!!~~~~~ALERT~~~~!!! CANVAS IS AT 0.0F ALPHA ----- DONE FADING IN - LEVEL LOADED");

		//show freshly loaded scene name in debug
		DebugSceneName();

	}

	//Coroutine for fading out old scene
	IEnumerator FadeOutScene(string _sceneName)
	{
		//Fading out from the old scene (making the canvas opaque)
		while (BlackCanvas.alpha < 1)
		{
			//Increase time
			time += Time.deltaTime;
			//Update alpha
			BlackCanvas.alpha = Mathf.Clamp01((time / fadeTime) - 0.0f);
			//Pause coroutine (yield to rest of application process)
			yield return null;
		}
		//Reset time for next fade
		time = 0;

		//Done fading out
		//Debug.Log("!!!~~~~~ALERT~~~~!!! CANVAS IS AT 1.0F ALPHA ----- DONE FADING OUT - LOADING LEVEL");

		//Passing scene index to fade level param.
		//Debug.Log("Passing scene index..");
		//Debug.Log("invoking level change...");
		//yield return new WaitForSeconds(.5f);
		
		//Load the next level
		LevelLoad(_sceneName);
	}


	//Level Load Wrapper
	void LevelLoad(string _sceneName)
	{
		//load the actual level (Unity removes old scene and adds new scene behind our fade canvas)
		SceneManager.LoadScene(_sceneName);

		//Initiate fading in
		StartCoroutine(FadeInScene());
	}

	//Fade Out Coroutine Wrapper
	void FadeOut(string _sceneName)
	{
		//Initiate fading out
		StartCoroutine(FadeOutScene(_sceneName));
	}

	//Public function to start scene transitions from any script - access with GameMaster.Instance.NewFade("-sceneName-");
	public void NewFade(string _sceneName)
	{
		//Can't start if already in transition
		if (time != 0)
		{
			return;
		}
		//Set Current Scene String
		currentScene = _sceneName;

		//Start Transition
		FadeOut(currentScene);
	}

	public void DebugSceneName()
	{
		Debug.Log("Current active scene is " + SceneManager.GetActiveScene().name + "."); //Used to get current scene name
	}
}


//Pass scene names from UI canvas buttons to transition between scenes - DONE

//TODO: Solve lighting issue when transition is achieve (canvas has black tinge to it) - DONE

//Use the canvas buttons to call the "wrapper"function of the fade out mechanism - DONE
//Then pass the chosen scene index number in the LoadLevel() once the coroutine finishes - DONE - NOTE: Changed to scene names.
//POS.Solution:Website covers var passing through coroutine, reference it to code. - DONE

//Problems so far: Unable to reference scene index number - NOTE: Fixed by referencing all scenes by name.
//from MainMenu scene -> Chem Laboratory Trigger (red button) - NOTE: Working (see below)
	//Game object is continued as inactive once scene has been loaded despite object being active in scene? - NOTE: Fixed by accessing true singleton GameMaster with GameMaster.Instance...
	//Possible solutions involve iterating current singleton class out of monobehavior extension but unsure how to proceed. - NOTE: No need, it was working all along!

	//TODO: Continue working into chem lab space, multiply over to bio and physics.
	//Design sub-classes for singleton to use during other runtime routines (i.e. maybe different metric for different labs?) - NOTE: This is a fantastic idea... there is an inheritable Singleton Unity MonoBehaviour script, I'll find it and add it. It will be in the project when you read this, just implement it into any script you need the singleton behaviour on (very easy, just inherit, I've done this script so look above if you're confused about anything... but basically it's just ClassName : Singleton<ClassName> and you can access with ClassName.Instance).