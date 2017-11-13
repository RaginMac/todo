using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCalcValue : MonoBehaviour {

	public Text calcText;
	public Manager manager;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find("Manager").GetComponent<Manager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetAnswer(string ans){
		if(calcText.text.Length<3){
			calcText.text+=ans;
		}
		manager.questionArray[manager.questionNumber].GetComponent<DrawGrid>().clickedAnswer = int.Parse(calcText.text);
	}

	public void EraseText()
	{
		calcText.text = "";
	}

	public void Clear(){
		manager.questionArray[manager.questionNumber].GetComponent<DrawGrid>().ClearGrid();
	}
}
