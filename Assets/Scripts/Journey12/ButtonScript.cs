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
		if(Multiply.answerText[0].text.Length < 2)
			Multiply.answerText[0].text += this.GetComponentInChildren<Text>().text;
	}

	public void Erase()
	{
		//Manager.click.Play ();
	//	if (Multiply.i >= 0)
		{
			Multiply.answerText [0].text = "";
			//Multiply.i--;
		}
	}
}
