using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManagerControl : MonoBehaviour
{

    [Header("Button Variables")]
    public Button btnHome;
    public Button btnRetry;
    public Button btnSaveScore;
    public Button btnConfirmSaveScore;

    [Header("SFX Variables")]
    public SFXControl sfxButton;

    [Header("Score Variables")]
    public Text txtScore;
    public int currentScore;
    public string currentUserName;
    public Text txtMaxScore;
    public int maxScore;

    [Header("Save Score Container Variables")]
    public InputField inputUserName;
    public GameObject userNameContainer;

    [Header("Loading Variables")]
    public LoadingControl loadingPlay;
    public LoadingControl loadingMenu;

    // Start is called before the first frame update
    void Start(){
        btnRetry.onClick.AddListener(() => RetryLevel());
        btnHome.onClick.AddListener(() => GoHome());
        btnSaveScore.onClick.AddListener(() => AppearSaveScoreContainer());

        btnSaveScore.gameObject.SetActive(false);
        userNameContainer.SetActive(false);

        btnConfirmSaveScore.onClick.AddListener(() => SaveUserName());

        this.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update(){

    }

    void AppearSaveScoreContainer(){
        userNameContainer.SetActive(true);
        sfxButton.PlaySound();
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

        GetMaxScore();

        string pref;
        int userScore;
        int posToChange = -1;
        for (int i = 5; i >= 1; --i){
            pref = "Score" + (i).ToString();
            userScore = PlayerPrefs.GetInt(pref, 0);
            if (currentScore > userScore){
                posToChange = i;
            }else {
                break;
            }
        }
        if (posToChange != -1){
            btnSaveScore.gameObject.SetActive(true);
        }

        /*
        maxScore = PlayerPrefs.GetInt("MaxScore",0);
        if(maxScore < currentScore){
            PlayerPrefs.SetInt("MaxScore",currentScore);
        }
        GetMaxScore();
        */
    }

    void SaveUserName(){
        if (string.IsNullOrEmpty(inputUserName.text) == false){
            currentUserName = inputUserName.text;
            inputUserName.text = "";
            sfxButton.PlaySound();
            UpdateRanking();
            btnSaveScore.gameObject.SetActive(false);
            userNameContainer.SetActive(false);
        } else{
            print("INTRODUCE UN NOMBRE");
        }
    }

    void GetMaxScore(){
        maxScore = PlayerPrefs.GetInt("Score1", 0);
        txtMaxScore.text = "MAX SCORE: " + maxScore.ToString();
    }

    public void SaveScore(string nam){
        currentUserName = nam;
    }

    void UpdateRanking(){
        string pref;
        int userScore;
        int posToChange = -1;
        for (int i = 5; i >= 1; --i){
            pref = "Score" + (i).ToString();
            userScore = PlayerPrefs.GetInt(pref, 0);
            if (currentScore > userScore){
                posToChange = i;
            } else{
                break;
            }
        }

        int actual = 0;
        string actualName = "";

        if (posToChange < 5){

            //int previous = 0;
            //string previousName = "";

            for (int i = 4; i >= posToChange; --i){
                pref = "Score" + (i).ToString();
                actual = PlayerPrefs.GetInt(pref, 0);
                pref = "User" + (i).ToString();
                actualName = PlayerPrefs.GetString(pref, "USER");

                pref = "Score" + (i + 1).ToString();
                PlayerPrefs.SetInt(pref, actual);
                //previous = PlayerPrefs.GetInt(pref,0);
                pref = "User" + (i + 1).ToString();
                PlayerPrefs.SetString(pref, actualName);

                //previousName = PlayerPrefs.GetString(pref,"");
            }
        }

        if (posToChange != -1){
            print("LA POSICION A CAMBIAR FUE: " + posToChange);
            pref = "Score" + (posToChange).ToString();
            PlayerPrefs.SetInt(pref, currentScore);
            pref = "User" + (posToChange).ToString();
            PlayerPrefs.SetString(pref, currentUserName);
        }
    }
}
