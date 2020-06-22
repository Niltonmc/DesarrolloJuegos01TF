using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsManagerControl : MonoBehaviour{

    [Header("Button Variables")]
    public Button btnBack;
    public Button btnMusic;
    public Button btnSound;
    public List<Sprite> btnStates;

    [Header("SFX Variables")]
    public SFXControl sfxButton;
    public SFXControl sfxMusic;

    [Header("Loading Variables")]
    public LoadingControl loadingMenu;

    // Start is called before the first frame update
    void Start(){
        btnMusic.onClick.AddListener(() => ChangeMusicValue());
        btnSound.onClick.AddListener(() => ChangeSoundValue());
        SetInitialStates();
        btnBack.onClick.AddListener(() => GoMenu());
  }

    // Update is called once per frame
    void Update(){
        
    }

    void ChangeMusicValue(){
        float music = PlayerPrefs.GetFloat("Music",1);
        if(music == 1){
           btnMusic.GetComponent<Image>().sprite = btnStates[0];
           PlayerPrefs.SetFloat("Music",0);
        }else{
           btnMusic.GetComponent<Image>().sprite = btnStates[1];
           PlayerPrefs.SetFloat("Music",1);  
        }
        sfxMusic.SetNewSFXValue();
        sfxButton.PlaySound();

    }

    void ChangeSoundValue(){
        float sound = PlayerPrefs.GetFloat("Sound",1);
        if(sound == 1){
           btnSound.GetComponent<Image>().sprite = btnStates[0];
           PlayerPrefs.SetFloat("Sound",0);
        }else{
           btnSound.GetComponent<Image>().sprite = btnStates[1];
           PlayerPrefs.SetFloat("Sound",1);  
        }
        sfxButton.SetNewSFXValue();
        sfxButton.PlaySound();
    }

    void SetInitialStates(){
        float music = PlayerPrefs.GetFloat("Music",1);
         btnMusic.GetComponent<Image>().sprite = btnStates[Mathf.FloorToInt(music)];
        float sound = PlayerPrefs.GetFloat("Sound",1);
         btnSound.GetComponent<Image>().sprite = btnStates[Mathf.FloorToInt(sound)];

    }

    void GoMenu(){
        sfxButton.PlaySound(); 
        loadingMenu.ActivateLoadingCanvas();
    }
}
