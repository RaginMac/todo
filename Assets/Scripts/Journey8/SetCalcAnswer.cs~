using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCalcAnswer : MonoBehaviour {
	public bool isAdd;
	public Text calcAnswerText;
	public SetCarrotCount carrotCountSub;
	public SetCarrotForAdd carrotCountAdd;

	public Animator calcAnimator;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetAnswerText(){
		calcAnswerText.text = this.GetComponentInChildren<Text> ().text;
//		carrotCountSub = Manager.Instance.questionArray [Manager.Instance.questionNumber].GetComponent<SetCarrotCount> ();
//		carrotCountAdd = Manager.Instance.questionArray [Manager.Instance.questionNumber].GetComponent<SetCarrotForAdd> ();

		if(!isAdd){
			Manager.Instance.questionArray [Manager.Instance.questionNumber].GetComponent<SetCarrotCount> ().clickedAnswer = calcAnswerText.text;
		}
		else if(isAdd){
			Manager.Instance.questionArray [Manager.Instance.questionNumber].GetComponent<SetCarrotForAdd> ().clickedAnswer = calcAnswerText.text;
		}

	}

	public void EraseText(){
		CalcAnim();
		//Manager.Instance.CheckFinalCarrotCountAdd();
		calcAnswerText.text = "";
	}

	public void CalcAnim(){
		//print(Manager.Instance.questionArray [Manager.Instance.questionNumber].GetComponent<SetCarrotCount> ().answer);
		if(!isAdd){
			if(Manager.Instance.questionArray [Manager.Instance.questionNumber].GetComponent<SetCarrotCount> ().answer != calcAnswerText.text){
				calcAnimator.SetTrigger("CalculatorShake");
			}
		}
		else if(isAdd){
			//print("add");
			if(Manager.Instance.questionArray [Manager.Instance.questionNumber].GetComponent<SetCarrotForAdd> ().answer != calcAnswerText.text){
			//	print("CORRECT");
				calcAnimator.SetTrigger("CalculatorShake");
			}

		}
	}
}
