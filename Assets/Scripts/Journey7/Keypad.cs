using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour {
	public TextMesh answerArea;
	public Text inputArea;
	public Manager manager;

	public Text[] allInputAreas;

	public void SetText(string input){
		if(answerArea!=null){
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
		inputArea.text = "";
	}

	public void EraseAllText()
	{
		for (int i = 0; i < allInputAreas.Length; i++) {
			allInputAreas[i].text = "";
		}
	}



}
