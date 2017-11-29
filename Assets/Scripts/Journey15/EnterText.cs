using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterText : MonoBehaviour {

	public Text answerText, remainderText;
	public Manager manager;
	public KeyPadShow keypadAnsArea;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find("Manager").GetComponent<Manager>();
		keypadAnsArea = GameObject.Find("Keypad").GetComponent<KeyPadShow>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//
	public void SetAnswerText(string ans)
	{
		if(keypadAnsArea.whichToFill == "remainder"){
			remainderText.text = ans;
			manager.questionArray[manager.questionNumber].GetComponent<FishDrop>().clickedRemainder = int.Parse(ans);
		}else if(keypadAnsArea.whichToFill=="answer"){
			answerText.text = ans;
			manager.questionArray[manager.questionNumber].GetComponent<FishDrop>().clickedAns = int.Parse(ans);
		}
//		manager.questionArray[manager.questionNumber].GetComponent<FishDrop>().clickedAns = int.Parse(ans);
	}

	public void EraseText(){
		if(keypadAnsArea.whichToFill=="remainder"){
			remainderText.text = "";
		}else if(keypadAnsArea.whichToFill=="answer"){
			answerText.text = "";
		}
	}

	public void ClearInputBoxes(){
		remainderText.text = "";
		answerText.text = "";
	}

}
