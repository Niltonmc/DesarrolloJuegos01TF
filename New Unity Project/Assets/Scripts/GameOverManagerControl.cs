using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManagerControl : MonoBehaviour{
    
    [Header("Button Variables")]
    public Button btnHome;
    public Button btnRetry;

    [Header("SFX Variables")]
    public SFXControl sfxButton;

    [Header("Score Variables")]
    public Text txtScore;
    public int currentScore;
    public Text txtMaxScore;
    public int maxScore;

     [Header("Loading Variables")]
     public LoadingControl loadingPlay;
     public LoadingControl loadingMenu;

    // Start is called before the first frame update
    void Start(){
        btnRetry.onClick.AddListener(() => RetryLevel());
        btnHome.onClick.AddListener(() => GoHome());
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
        
    }

    void RetryLevel(){
        sfxButton.PlaySound();
       loadingPlay.ActivateLoadingCanvas();
         Time.timeScale = 1;
    }

    void GoHome(){
        sfxButton.PlaySound();
      loadingMenu.ActivateLoadingCanvas();
        Time.timeScale = 1;
    }

    public void SetScore(int score){
        currentScore = score;
        txtScore.text = "SCORE: " + currentScore.ToString();
        maxScore = PlayerPrefs.GetInt("MaxScore",0);
        if(maxScore < currentScore){
            PlayerPrefs.SetInt("MaxScore",currentScore);
        }
        GetMaxScore();
    }

    void GetMaxScore(){
        maxScore = PlayerPrefs.GetInt("MaxScore",0);
        txtMaxScore.text = "MAX SCORE: " + maxScore.ToString();
    }
}
