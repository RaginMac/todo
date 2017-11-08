using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnswerArea : MonoBehaviour {

	public Keypad keypad;
	public Animator keypadAnimator;
	public TextMesh answerArea;

	public GameObject[] toHighlight, toHide;

	void Start()
	{
		keypad = GameObject.Find("Keypad").GetComponent<Keypad>();
		keypadAnimator = GameObject.Find("Keypad").GetComponent<Animator>();
		keypadAnimator.SetBool("KeypadShow", false);
	}

	public void OnMouseDown()
	{
		//keypadAnimator.SetTrigger("ShowKeypad");
		keypadAnimator.SetBool("KeypadShow", true);
		ShowSelection ();
		keypad.answerArea = answerArea;
	}

	void ShowSelection(){
		for (int i = 0; i < toHighlight.Length; i++) {
			toHighlight [i].SetActive (true);
		}
		for (int j = 0; j < toHide.Length; j++) {
			toHide[j].SetActive (false);
		}
	}
}
