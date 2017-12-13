﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlaceValue : MonoBehaviour {


	public enum Difficulty {Grade1, Grade2};
	public Animator cam;

	public List<GameObject> Coin1Array = new List<GameObject>();
	public List<GameObject> Coin10Array = new List<GameObject>();
	public List<GameObject> Coin100Array = new List<GameObject>();

	public bool G3_2;

	public int firstDigit, secondDigit, thirdDigit;

	[Header("Select a grade")]
	public Difficulty diff;

	[Header("Answer Texts:")]
	//answer texts
	public TextMesh ans1;
	public TextMesh ans10;
	public TextMesh ans100;

	[Header("Question Texts:")]
	public TextMesh q1;
	public TextMesh q10;
	public TextMesh q100;

	public  string answer;
	public  string playerAnswer;

	public GameObject DropSpawn1;
	public GameObject DropSpawn10;
	public GameObject DropSpawn100;

	public GameObject lever_1;
	public GameObject lever_10;
	public GameObject lever_100;

	public bool answered;

	// Use this for initialization
	void Start () {
		cam = Camera.main.GetComponent<Animator> ();
		CreateQuestion ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateQuestion()
	{
		if (diff == Difficulty.Grade1) {

			secondDigit = Random.Range (0, 10);
			thirdDigit = Random.Range (1, 10);

			answer = "0" + secondDigit.ToString() + thirdDigit.ToString ();

			if (!G3_2) {
				for (int i = 0; i < secondDigit; i++) {
					Coin10Array [i].SetActive (true);
				}

				for (int i = 0; i < thirdDigit; i++) {
					Coin1Array [i].SetActive (true);
				}

			} else {
				q100.text = "0";
				q10.text = secondDigit.ToString ();
				q1.text = thirdDigit.ToString ();
			}

		} else if (diff == Difficulty.Grade2) {
			firstDigit = Random.Range (0, 10);
			secondDigit = Random.Range (0, 10);
			thirdDigit = Random.Range (1, 10);

			answer = firstDigit.ToString() + secondDigit.ToString() + thirdDigit.ToString ();

			if (!G3_2) {
				for (int i = 0; i < firstDigit; i++) {
					Coin100Array [i].SetActive (true);
				}

				for (int i = 0; i < secondDigit; i++) {
					Coin10Array [i].SetActive (true);
				}

				for (int i = 0; i < thirdDigit; i++) {
					Coin1Array [i].SetActive (true);
				}
			} else {
				
				q100.text = firstDigit.ToString();
				q10.text = secondDigit.ToString ();
				q1.text = thirdDigit.ToString ();
			}
		}

		//StartCoroutine (MoveCam ());
	}

	public void InstantiateCoins ()
	{
//		if(Drop1)
//		GameObject tempObject = Instantiate( )
//		if (lever_1.GetComponent<Lever> ().leverValue == 1) {
//			this.GetComponent<Animator>().SetTrigger("OneLever");
//		}

	}

	IEnumerator MoveCam ()
	{
		yield return new WaitForSeconds (1);
		cam.SetBool ("moveCamera", true);
	}
}
