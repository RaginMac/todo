﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FishManager : MonoBehaviour {

	public bool countWrongQuestion = false;
	public GameObject[] traceQuestions;
	//public TraceHelper[] helper;
	public int fishQuestionNumber = 0;
	public Image[] stars;
	public Image[] wrongStars, popupStars;

	public int noOfQuestionsAnswered, totalNoOfQuestions;

	public GameObject gameCompletePopup;

	public AudioSource UIAudioSource, UIAudioSource2;
	public AudioClip click, correct, wrong, popup, star;
	public int isCorrect;
	public bool isGameComplete = false;

	// Use this for initialization
	void Start () {
		gameCompletePopup.SetActive (false);
		ShuffleQuestionArray ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShuffleQuestionArray()
	{
		for (int i = 0; i < traceQuestions.Length; i++) 
		{
			GameObject temp = traceQuestions [i];
			int r = Random.Range (i, traceQuestions.Length);
			traceQuestions [i] = traceQuestions [r];
			traceQuestions [r] = temp;
		}

		traceQuestions [fishQuestionNumber].SetActive (true);
	}

	public void NextQuestion()
	{
		if ((noOfQuestionsAnswered < totalNoOfQuestions)) {
			traceQuestions [fishQuestionNumber].SetActive (false);
			fishQuestionNumber++;
			traceQuestions [fishQuestionNumber].SetActive (true);
		} 
		else if((noOfQuestionsAnswered >= totalNoOfQuestions)) {
			//gameCompletePopup.SetActive (true);
			Invoke ("CompletedSection", 0.25f);
		}
	}

	public void RewardStars(int starIndex)
	{
		//	print ("Star rewarded");
		isCorrect++;
		stars[starIndex].gameObject.SetActive(true);
	}

	public void RewardWrongStars(int starIndex)
	{
		//	print ("Star rewarded");
		wrongStars[starIndex].gameObject.SetActive(true);
	}

	public void CheckAnswer()
	{
		int tempNum = 0;
		for (int i = 0; i < traceQuestions [fishQuestionNumber].GetComponent<Trace>().answer.Length; i++)
		{
			if ((traceQuestions [fishQuestionNumber].GetComponent<Trace>().traceCollider [i] == traceQuestions [fishQuestionNumber].GetComponent<Trace>().answer [i])
				&& (traceQuestions [fishQuestionNumber].GetComponent<Trace>().traceCollider.Count == traceQuestions [fishQuestionNumber].GetComponent<Trace>().answer.Length)) 
			{
				tempNum++;
			}
			else {
				break;
			}
		}

		if(tempNum >= traceQuestions [fishQuestionNumber].GetComponent<Trace>().answer.Length)
		{
			if(traceQuestions [fishQuestionNumber].GetComponent<Trace>().isAnswered==0){
				//print ("Correct");
				PlayAudio(correct);
				PlayStarAudio();
				PlayFishAnimations(true);
				RewardStars (fishQuestionNumber);
				noOfQuestionsAnswered++;
				Invoke("NextQuestion", 2f);
				traceQuestions [fishQuestionNumber].GetComponent<Trace>().isAnswered = 1;
			}

		} else {
			//print ("Wrong");
			PlayFishAnimations(false);
			PlayAudio(wrong);
			if(countWrongQuestion){
				RewardWrongStars(fishQuestionNumber);
				noOfQuestionsAnswered++;
				Invoke("NextQuestion", 2f);
			}
			else{
				traceQuestions [fishQuestionNumber].GetComponent<Trace> ().ResetPosition ();
				for (int j = 0; j <= traceQuestions [fishQuestionNumber].GetComponent<Trace> ().helper.Length - 1;j++) {
					traceQuestions [fishQuestionNumber].GetComponent<Trace> ().helper[j].hasEntered = false;
				} 
			}
		}
//		noOfQuestionsAnswered++;
//		NextQuestion ();
	}

	public void PlayFishAnimations(bool isCorrect){
		if(isCorrect){
			GameObject[] temp = GameObject.FindGameObjectsWithTag("Fish");
			for (int i = 0; i < temp.Length; i++) {
				temp[i].GetComponent<FishAnim>().PlayAnim("Correct");
			}
		}
		else{
			GameObject[] temp = GameObject.FindGameObjectsWithTag("Fish");

			for (int i = 0; i < temp.Length; i++) {
				temp[i].GetComponent<FishAnim>().PlayAnim("Wrong");
			}
		}
	}

	public void GoToStartScreen(string scene)
	{
		SceneManager.LoadScene (scene);
	}

	public void RepeatGame()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void PlayNextGame()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void CompletedSection()
	{
		PlayAudio(popup);
		for (int i = 0; i < isCorrect; i++) {
			popupStars [i].gameObject.SetActive (true);			
		}
		isGameComplete = true;
		gameCompletePopup.SetActive (true);
	}

	public void PlayAudio(AudioClip clip){
		UIAudioSource.clip = clip;
		UIAudioSource.Play();
	}

	public void PlayStarAudio(){
		UIAudioSource2.clip = star;
		UIAudioSource2.Play();
	}
//
//	public void GoToHomeScreen(string scene)
//	{
//		SceneManager.LoadScene(scene);
//	}
}
