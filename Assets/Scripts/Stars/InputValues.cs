//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class InputValues : MonoBehaviour {
//	//This script is just to provide values to the star manager to set the stars. This value will come from the actual itself not from here. this is just to simulate the game.
//
//	public StarManager sm;
//	public int maxQuestions;
//
//
//	[SerializeField]
//	int noOfCorrectAnswers;
//	[SerializeField] 
//	int noOfQuestionsAnswered;
//
//	void Start()
//	{
//		
//	}
//
//	void Update()
//	{ 
//		if(noOfQuestionsAnswered>=maxQuestions-1)
//		{
//			sm.ShowCompleteScreen();
//		}
//	}
//
//	public void CorrectAnswer()
//	{
//		noOfCorrectAnswers++;
//		noOfQuestionsAnswered++;
//		if(noOfQuestionsAnswered<=maxQuestions)
//		{
//			sm.RewardStars(noOfQuestionsAnswered);
//		}
//	}
//
//	public void WrongAnswer()
//	{}
//
//	public void NextGame()
//	{
//		Debug.Log("Load next game");
//	}
//}
