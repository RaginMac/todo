using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subtraction : MonoBehaviour {

	public enum Difficulty {Grade1, Grade2, Grade3};
	public Difficulty diff;
	public Animator CamAnime;

	public bool subWithBorrow;
	public bool subWithoutBorrow;

	public string n1;
	public string n2;

	public string answer;
	public string playerAnswer;


	// display first number
	public TextMesh n1FirstDigit;
	public TextMesh n1SecondDigit;
	public TextMesh n1ThirdDigit;

	// display second number
	public TextMesh n2FirstDigit;
	public TextMesh n2SecondDigit;
	public TextMesh n2ThirdDigit;


	//answer texts
	public TextMesh ans1;
	public TextMesh ans2;
	public TextMesh ans3;

	void Start () {
		if (subWithoutBorrow) {
			CreateQuestionsSWOB ();
		} else if (subWithBorrow) {
			CreateQuestionsSWB ();
		}
	}

	void Update () {
		
	}


	//SWOB - subtraction without borrow
	void CreateQuestionsSWOB()
	{
		if (diff == Difficulty.Grade1) {
			int tempN1 = 20;
			int tempN2 = Random.Range (1, 10);

			n1 = tempN1.ToString ();
			n2 = tempN2.ToString ();

			FindAnswer (n1, n2);
		
		} else if (diff == Difficulty.Grade2) {
			int tempN1 = Random.Range (1, 10);
			int tempN2 = Random.Range (1, 10);
			int tempN3 = Random.Range (1, tempN1);
			int tempN4 = Random.Range (1, tempN2);
		
			n1 = tempN1.ToString () + tempN2.ToString ();
			n2 = tempN3.ToString () + tempN4.ToString ();
		

			StartCoroutine(DisplayQuestion("0", tempN1.ToString (), tempN2.ToString (), "0", tempN3.ToString (), tempN4.ToString ()));
			FindAnswer (n1, n2);

		} else if (diff == Difficulty.Grade3) {
			int tempN1 = Random.Range (1, 10);
			int tempN2 = Random.Range (1, 10);
			int tempN3 = Random.Range (1, 10);

			int tempN4 = Random.Range (1, tempN1);
			int tempN5 = Random.Range (1, tempN2);
			int tempN6 = Random.Range (1, tempN3);

			n1 = tempN1.ToString () + tempN2.ToString () + tempN3.ToString();
			n2 = tempN4.ToString () + tempN5.ToString () + tempN6.ToString();

			StartCoroutine(DisplayQuestion(tempN1.ToString (), tempN2.ToString (), tempN3.ToString (), tempN4.ToString (), tempN5.ToString (), tempN6.ToString ()));
			FindAnswer (n1, n2);
		}
	}

	//SWB - subtraction with borrow
	void CreateQuestionsSWB()
	{
		if (diff == Difficulty.Grade1) {
			int tempN1 = 20;
			int tempN2 = Random.Range (1, tempN1 - 1);

			n1 = tempN1.ToString ();
			n2 = tempN2.ToString ();
			FindAnswer (n1, n2);

		} else if (diff == Difficulty.Grade2) {
			int tempN1 = Random.Range (1, 10);
			int tempN2 = Random.Range (1, 10);

			int tempN3 = Random.Range (1, tempN1 - 1);
			int tempN4 = Random.Range (1, 10);

			n1 = tempN1.ToString () + tempN2.ToString ();
			n2 = tempN3.ToString () + tempN4.ToString ();

			StartCoroutine(DisplayQuestion("0", tempN1.ToString (), tempN2.ToString (), "0", tempN3.ToString (), tempN4.ToString ())); 
			FindAnswer (n1, n2);

		} else if (diff == Difficulty.Grade3) {
			int tempN1 = Random.Range (1, 10);
			int tempN2 = Random.Range (1, 10);
			int tempN3 = Random.Range (1, 10);

			int tempN4 = Random.Range (1, tempN1 - 1);
			int tempN5 = Random.Range (1, 10);
			int tempN6 = Random.Range (1, 10);

			n1 = tempN1.ToString () + tempN2.ToString () + tempN3.ToString();
			n2 = tempN4.ToString () + tempN5.ToString () + tempN6.ToString();

			StartCoroutine(DisplayQuestion(tempN1.ToString (), tempN2.ToString (), tempN3.ToString (), tempN4.ToString (), tempN5.ToString (), tempN6.ToString ()));
			FindAnswer (n1, n2);
		}
	}

	void FindAnswer(string num1, string num2){
		int tempN1 = int.Parse (num1);
		int tempN2 = int.Parse (num2);

		int ans = tempN1 - tempN2;
		answer = ans.ToString ();
	}

	IEnumerator DisplayQuestion(string n1First, string n1Second, string n1Third, string n2First, string n2Second, string n2Third) {

		yield return new WaitForSeconds (1);
		n1FirstDigit.text = n1First;
		n1SecondDigit.text = n1Second;
		n1ThirdDigit.text = n1Third;

		yield return new WaitForSeconds (1);
		n2FirstDigit.text = n2First;
		n2SecondDigit.text = n2Second;    
		n2ThirdDigit.text = n2Third;

		yield return new WaitForSeconds (1);
		ans1.text = "?";
		ans2.text = "?";
		ans3.text = "?";

		CamAnime.SetBool ("moveCamera", true);

		yield return new WaitForSeconds (1);
		ans1.text = "";
		ans2.text = "";
		ans3.text = "";
	}
}
