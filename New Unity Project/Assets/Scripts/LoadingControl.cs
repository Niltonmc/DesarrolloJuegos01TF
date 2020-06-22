using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingControl : MonoBehaviour {

	AsyncOperation operationAsync;
	public string levelToLoad;
	public GameObject canvasLoading;
	//public Image barFillImage; //NO SE SABE SI HABRÁ UNA BARRA
	public Animator loadingAnimator;
	// Use this for initialization
	void Start () {
		//barFillImage.fillAmount = 1f; //NO SE SABE SI HABRÁ UNA BARRA
		Invoke ("StayIdle", 1f);
	}

	// Update is called once per frame
	void Update () {
		
	}
	public void StayIdle (){
		loadingAnimator.SetBool ("isAppear", false);
		loadingAnimator.SetBool ("isDisappear", false);
		//barFillImage.fillAmount = 0f; //NO SE SABE SI HABRÁ UNA BARRA
	}
	public void ActivateLoadingCanvas(){

		loadingAnimator.SetBool ("isDisappear", false);
		loadingAnimator.SetBool ("isAppear", true);
		canvasLoading.SetActive (true);
		StartCoroutine (LoadLevelWithRealProgress ());
	}

	IEnumerator LoadLevelWithRealProgress(){
		yield return new WaitForSecondsRealtime (1);
		operationAsync = SceneManager.LoadSceneAsync (levelToLoad);
        Time.timeScale = 1;
        while (!operationAsync.isDone) {
			yield return null;
			//barFillImage.fillAmount = operationAsync.progress; //NO SE SABE SI HABRÁ UNA BARRA
		}
	}
}
