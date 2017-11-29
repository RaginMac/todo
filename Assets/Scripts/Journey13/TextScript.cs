using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour {

	public MultiplicationGrid mulScript;
	public Animator textAnime;

	public bool zoomText;
	public GameObject text;
	public GameObject textHighlight;
	public GameObject[] otherTextFields;

	// Use this for initialization
	void Start () {
		textAnime = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		ZoomText ();
	}

	void OnMouseUp()
	{
		if (!zoomText) {
			zoomText = true;
		} else if (zoomText) {
			zoomText = false;
		}
		ZoomInOtherTexts ();

	}

	public void ZoomText()
	{ 
		if(zoomText) {
			text.SetActive (false);
			textHighlight.SetActive (true);
			textAnime.SetBool("zoom", true);
		} else if(!zoomText){
			text.SetActive (true);
			textHighlight.SetActive (false);
			textAnime.SetBool("zoom", false);
		}
	}


	public void ZoomInOtherTexts()
	{
		for (int i = 0; i < otherTextFields.Length; i++) {
			if (!otherTextFields [i].GetComponent<TextScript> ().zoomText) {
				continue;
			} else {
				otherTextFields [i].GetComponent<TextScript> ().zoomText = false;
			}
		}
	}
}
