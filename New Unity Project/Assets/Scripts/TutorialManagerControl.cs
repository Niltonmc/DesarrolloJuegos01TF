using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManagerControl : MonoBehaviour{

    [Header("Button Variables")]
    public Button btnBack;

     [Header("Loading Variables")]
    public LoadingControl loadingMenu;

    [Header("SFX Variables")]
    public SFXControl sfxButton;

    // Start is called before the first frame update
    void Start(){
        btnBack.onClick.AddListener(() => GoMenu());
    }

    // Update is called once per frame
    void Update(){
        
    }

    void GoMenu(){
        sfxButton.PlaySound(); 
        loadingMenu.ActivateLoadingCanvas();
    }
}
