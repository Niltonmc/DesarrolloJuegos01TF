using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RankingManagerControl : MonoBehaviour{

   [Header("Button Variables")]
    public Button btnBack;

    [Header("Loading Variables")]
    public LoadingControl loadingMenu;

    [Header("SFX Variables")]
    public SFXControl sfxButton;

    [Header("Ranking Texts Variables")]
    public List<Text> allRankingNames;
    public List<Text> allRankingScores;

    // Start is called before the first frame update
    void Start(){
        btnBack.onClick.AddListener(() => GoMenu());
        SetScores();
  }

    // Update is called once per frame
    void Update(){
        
    }
    void GoMenu(){
        sfxButton.PlaySound(); 
        loadingMenu.ActivateLoadingCanvas();
    }

    void SetScores(){
        string pref;
        for(int i = 0; i < allRankingNames.Count;++i){
            pref = "User" + (i+1).ToString();
            allRankingNames[i].text = PlayerPrefs.GetString(pref,"USER");
            pref = "Score" + (i+1).ToString();
            allRankingScores[i].text = PlayerPrefs.GetInt(pref,0).ToString();
        }
    }
}
