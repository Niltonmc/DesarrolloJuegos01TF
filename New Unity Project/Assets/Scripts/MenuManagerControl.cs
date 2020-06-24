using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManagerControl : MonoBehaviour{

    [Header("Button Variables")]
    public Button btnPlay;
    public Button btnMenu;
     public Button btnRanking;
    public Button btnTutorial;

    [Header("SFX Variables")]
    public SFXControl sfxButton;

    [Header("Loading Variables")]
    public LoadingControl loadingPlay;
    public LoadingControl loadingOptions;
     public LoadingControl loadingRanking;
    public LoadingControl loadingTutorial;

    // Start is called before the first frame update
    void Start(){
        btnPlay.onClick.AddListener(() => GoGame());
        btnMenu.onClick.AddListener(() => GoOptions());
         btnRanking.onClick.AddListener(() => GoRanking());
        btnTutorial.onClick.AddListener(() => GoTutorial());
    }

    // Update is called once per frame
    void Update(){
        
    }

    void GoOptions(){
        sfxButton.PlaySound();
        loadingOptions.ActivateLoadingCanvas();
    }

    void GoGame(){
        sfxButton.PlaySound(); 
        loadingPlay.ActivateLoadingCanvas();
    }

     void GoRanking(){
        sfxButton.PlaySound(); 
        loadingRanking.ActivateLoadingCanvas();
    }

     void GoTutorial(){
        sfxButton.PlaySound(); 
        loadingTutorial.ActivateLoadingCanvas();
    }
}
