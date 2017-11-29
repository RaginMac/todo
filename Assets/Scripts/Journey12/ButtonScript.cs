using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {
	
	public multiply Multiply;
	public Manager Manager;
	// Use this for initialization
	void Start () {
		Manager = GameObject.Find ("Manager").GetComponent<Manager> ();
		//Multiply = Manager.questionArray [Manager.questionNumber].GetComponent<multiply> ();
	}
	
	// Update is called once per frame
	void Update () {
		Multiply = Manager.questionArray [Manager.questionNumber].GetComponent<multiply> ();
	}

	public void FillAnswer()
	{
		//Manager.click.Play ();
//		if (Multiply.diff == multiply.Difficulty.Grade0) {
//            if (Multiply.stringLength == 2)
//            {
//                if (Multiply.i < 1)
//                {
//                    Multiply.i++;
//                    Multiply.answerText[Multiply.i].GetComponentInChildren<Text>().text = this.GetComponentInChildren<Text>().text;
//                }
//            }
//            else if (Multiply.stringLength == 1) {
//                if (Multiply.i < 0)
//                {
//                    Multiply.i++;
//                    Multiply.answerText[Multiply.i].GetComponentInChildren<Text>().text = this.GetComponentInChildren<Text>().text;
//                }
//            }
//
//        } else if (Multiply.diff == multiply.Difficulty.Grade1) {
//			if (Multiply.i < 1) {
//				Multiply.i++;
//				Multiply.answerText [Multiply.i].GetComponentInChildren<Text>().text = this.GetComponentInChildren<Text> ().text;
//			}
//		} else if (Multiply.diff == multiply.Difficulty.Grade2) {
//			if (Multiply.i < 1) {
//				Multiply.i++;
//				Multiply.answerText [Multiply.i].GetComponentInChildren<Text>().text = this.GetComponentInChildren<Text> ().text;
//			}
//		} else if (Multiply.diff == multiply.Difficulty.Grade3) {
//			if (Multiply.i < 1) {
//				Multiply.i++;
//				Multiply.answerText [Multiply.i].GetComponentInChildren<Text>().text = this.GetComponentInChildren<Text> ().text;
//			}
//		}

		if (Multiply.stringLength == 2)
        {
            if (Multiply.i < 1)
            {
                Multiply.i++;
                Multiply.answerText[Multiply.i].GetComponentInChildren<Text>().text = this.GetComponentInChildren<Text>().text;
            }
        }
        else if (Multiply.stringLength == 1)
		{
            if (Multiply.i < 0)
            {
                Multiply.i++;
                Multiply.answerText[Multiply.i].GetComponentInChildren<Text>().text = this.GetComponentInChildren<Text>().text;
            }
        }
	}

	public void Erase()
	{
		//Manager.click.Play ();
		if (Multiply.i >= 0) {
			Multiply.answerText [Multiply.i].GetComponentInChildren<Text>().text = "";
			Multiply.i--;
		}
	}
}
