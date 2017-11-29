using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadShow : MonoBehaviour {

	public string whichToFill;
	public Animator keypadAnim;
	public GameObject remainderHighlight, ansHighlight;
	public int isPlaying = 0;
	// Use this for initialization
	void Start () {
		keypadAnim = GameObject.Find("Keypad").GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {

	}

	public void ShowKeypad(string ans){
		if(ans=="answer"){
			ansHighlight.SetActive(true);
			if(remainderHighlight!=null){
				remainderHighlight.SetActive(false);
			}
		}else if(ans=="remainder"){
			remainderHighlight.SetActive(true);
			ansHighlight.SetActive(false);
		}

		whichToFill = ans;
		if(isPlaying==0){
			keypadAnim.SetTrigger("ShowKeypad");
			isPlaying = 1;
		}
	}

}
