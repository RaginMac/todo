using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetAnswerArea : MonoBehaviour {

	public Keypad keypad;
	public Animator keypadAnimator;
	public TextMesh answerArea;
	public Text inputArea;

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
		if(answerArea!=null){
			keypad.answerArea = answerArea;
		}
		else if(inputArea!=null)
		{
			keypad.inputArea = inputArea;
		}
	}

	public void ShowSelection(){
		for (int i = 0; i < toHighlight.Length; i++) {
			toHighlight [i].SetActive (true);
		}
		for (int j = 0; j < toHide.Length; j++) {
			toHide[j].SetActive (false);
		}
	}
}
