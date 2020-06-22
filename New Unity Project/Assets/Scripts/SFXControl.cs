using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXControl : MonoBehaviour{

    [Header("Applicate SFX Variables")]
    public float maxVolumenValue;
	private float sfxValue;
	public bool canPlaySound;
	private AudioSource audioSour;
	public string soundPref;

	void Awake(){
		GetInitialComponents ();
		SetNewSFXValue ();
		Invoke ("ActivateCanPlaySound", 0.5f);
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void SetNewSFXValue(){
		sfxValue = PlayerPrefs.GetFloat (soundPref,1);
		sfxValue = sfxValue * maxVolumenValue;
		audioSour.volume = sfxValue;
	}

	void GetInitialComponents(){
		audioSour = GetComponent<AudioSource> ();
	}

	public void PlaySound(){
		if (canPlaySound == true) {
			audioSour.Play ();
		}
	}

	void ActivateCanPlaySound(){
		canPlaySound = true;
	}
}
