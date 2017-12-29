using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour {
	public TextMesh answerArea;
	public Text inputArea;
	public Manager manager;
	public GameObject keypadBG;
	public Text[] allInputAreas;
	public TextMesh[] allAnswerAreas;

	public bool show = false;

	public void SetText(string input) 
	{
		manager.PlayClickAudio ();
		if(answerArea!=null) {
			answerArea.text = input;
		}else if(inputArea!=null){
			if(inputArea.text.Length<2){
				inputArea.text += input;
			}

			if(inputArea.gameObject.tag=="InputBox"){
				manager.questionArray[manager.questionNumber].GetComponent<DivideCarrots>().clickedAnswer = inputArea.text ;
			}else if(inputArea.gameObject.tag=="RemainderBox")
			{
				manager.questionArray[manager.questionNumber].GetComponent<DivideCarrots>().clickedRemainder = inputArea.text ;
			}
		}

		//this.GetComponent<Animator>().SetBool("KeypadShow", false);
		//tempString = input;
//		manager.questionArray[manager.questionNumber].GetComponent<PlaceValueAddition>().SetPlayerAnswer();
	}

	public void  Erase()
	{
		manager.PlayClickAudio ();
		//print ("Erase");
		if(inputArea != null)
			inputArea.text = "";
		else if(answerArea != null)
			answerArea.text = "";
	}

	public void EraseAllText()
	{
		if (allInputAreas.Length > 0) {
			for (int i = 0; i < allInputAreas.Length; i++) {
				allInputAreas [i].text = "";
			}
		} else if (allAnswerAreas.Length > 0) {
			for (int i = 0; i < allAnswerAreas.Length; i++) {
				allAnswerAreas [i].text = "";
			}
		}
	}

	public void HideBG(bool hide)
	{
		keypadBG.SetActive (hide);
		show = false;
		this.GetComponent<Animator>().SetBool("KeypadShow", false);
	}
}
