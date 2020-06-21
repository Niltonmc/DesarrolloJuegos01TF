using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour {

	[Header("Dimensions Variables")]
	public float height;
	public float width;

	[Header("Image Type Variables")]
	public bool isBGImage;

	// Use this for initialization
	void Start () {
		height = Camera.main.orthographicSize * 2f; //cuenta desde el centro hacia abajo y hacia arriba
		width = height * Screen.width / Screen.height;
		if (isBGImage == true) {
			gameObject.transform.localScale = 
			new Vector3 (width, height,gameObject.transform.localScale.z);
		} else {
			gameObject.transform.localScale =
			 new Vector3 (width + 5, 4,gameObject.transform.localScale.z);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
