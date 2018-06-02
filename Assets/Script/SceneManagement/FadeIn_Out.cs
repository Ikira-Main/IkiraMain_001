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
		if (Input.GetMouseButtonDown(0) /*&& BlackCanvas.alpha == 1.0f*/)     //Update script to give error alert if trying to test script fade
		{
			//if (BlackCanvas.alpha == 1.0f)
			//{
				Debug.Log("!!!~~~~~ALERT~~~~!!! CANVAS IS AT 1.0F ALPHA");
			//}
			//else
			//{
				StartCoroutine(FadeInScene());
			//}
		}

		if (Input.GetMouseButtonDown(1) /*&& BlackCanvas.alpha == 0.0f*/)
		{
			//if (BlackCanvas.alpha == 0.0f)
			//{
				Debug.Log("!!!~~~~~ALERT~~~~!!! CANVAS IS AT 0.0F ALPHA");
			//}
			//else
			//{
				StartCoroutine(FadeOutScene());
			//}
		}
	}

	IEnumerator FadeInScene()
	{
		//LevelManager.SetBool("FadeOut", true);

		while (BlackCanvas.alpha > 0)
		{
			_time += Time.deltaTime;
			BlackCanvas.alpha = Mathf.Clamp01(1.0f - (_time / _fadeTime));
			yield return null;
		}
		//if (BlackCanvas.alpha == 0.0f)
		//{
		//	Debug.Log("!!!~~~~~ALERT~~~~!!! CANVAS IS AT 0.0F ALPHA");
		//}
		_time = 0;
	}

	IEnumerator FadeOutScene()
	{
		while (BlackCanvas.alpha < 1)
		{
			_time += Time.deltaTime;
			BlackCanvas.alpha = Mathf.Clamp01((_time / _fadeTime) - 0.0f);
			yield return null;
		}
		//if (BlackCanvas.alpha == 1.0f)
		//{
		//	Debug.Log("!!!~~~~~ALERT~~~~!!! CANVAS IS AT 1.0F ALPHA");
		//}
		_time = 0;
		FadeLevel(levelToLoad, Scene_name);
	}


	public void FadeLevel(int levelIndex, string levelName)
	{
		levelToLoad = levelIndex;
		levelName = Scene_name;
		SceneManager.LoadScene(Scene_name);
		StartCoroutine(FadeInScene());
	}

}