using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManagerControl : MonoBehaviour{

    [Header("Button Variables")]
    public Button btnPlay;
    public Button btnMenu;

    [Header("SFX Variables")]
    public SFXControl sfxButton;

    [Header("Loading Variables")]
    public LoadingControl loadingPlay;
     public LoadingControl loadingOptions;

    // Start is called before the first frame update
    void Start(){
        btnPlay.onClick.AddListener(() => GoGame());
        btnMenu.onClick.AddListener(() => GoOptions());
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
}
