using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Ikira

public class FadeIn_Out : MonoBehaviour {

	public CanvasGroup BlackCanvas;

	[Header("Animation time")] //Time animation takes to complete its process.
	public float _time;
	[Header("Animation fade")] //Time fade animation takes to opaque/transparent.
	public float _fadeTime;
	[Header("Scene Name")]	  //String for scene name
	public string Scene_name;
	[Header("Level Index")]   //Level index
	public int levelToLoad;

	public static FadeIn_Out fadeIn_out;
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

	public void Start(){}

	//Update is called once per frame
	void Update()
	{
		//Left Mouse Button Clicked - start fade in unless alpha is not 1 (already started)
		if (Input.GetMouseButtonDown(0))
		{
			//If there is not full alpha we're fading already
			if (BlackCanvas.alpha != 1.0f)
			{
				//Notify of current transition
				Debug.Log("!!!~~~~~ALERT~~~~!!! CANVAS IS NOT AT 1.0F ALPHA ----- IN TRANSITION");
			}
			else
			{
				//Start fading in
				FadeIn();
			}
		}

		//Right Mouse Button Clicked - start fade out unless alpha is not 0 (already started)
		if (Input.GetMouseButtonDown(1))
		{
			//If there is not zero alpha we're fading already
			if (BlackCanvas.alpha != 0.0f)
			{
				//Notify of current transition
				Debug.Log("!!!~~~~~ALERT~~~~!!! CANVAS IS NOT AT 0.0F ALPHA ----- IN TRANSITION");
			}
			else
			{
				//Start fading out
				FadeOut();
			}
		}
	}

	//Coroutine for fading in new scene
	IEnumerator FadeInScene()
	{
		//LevelManager.SetBool("FadeOut", true);

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
	IEnumerator FadeOutScene()
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

		//Load the next level
		FadeLevel(levelToLoad, Scene_name);
	}

	//Fade Out Coroutine Wrapper
	void FadeOut()
	{
		//Initiate fading out
		StartCoroutine(FadeOutScene());
	}

	//Level Load Wrapper
	void LoadLevel(int levelIndex, string levelName)
	{
		//Set index
		levelToLoad = levelIndex;
		//Set name
		levelName = Scene_name;
		//Load level by name
		SceneManager.LoadScene(Scene_name);
	}

	//Fade In Coroutine Wrapper
	void FadeIn()
	{
		//Initiate fading in
		StartCoroutine(FadeInScene());
	}

	//Fade Level Wrapper
	public void FadeLevel(int levelIndex, string levelName)
	{
		//Load the level
		LoadLevel(levelIndex, levelName);

		//Fade in the new level
		FadeIn();
	}

}