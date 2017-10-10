using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
	//THIS IS TEST FILE FOR GIT
	public static Manager Instance;
	public Intantiation_G_1_3 fishSpawner;
	public Image[] stars, popupStars, wrongAnsStars;

	public GameObject[] questionArray;
	//public GameObject[] answerArray;

	public int questionNumber = 0;

	public Transform[] objectSpawnPoints;
	public bool useDisplayQuestion = false;
	//public bool directCompareAnswer = false;

	public bool autoPlayAudio = false, countWrongAnswer = false;
	public AudioSource audioSource;

	public int noOfQuestionsAnswered, totalNoOfQuestions;

	public GameObject gameCompletePopup;
	public int isCorrect;

	public AudioSource correctAns;
	public AudioSource wrongAns;
	public AudioSource popUp;
	public AudioSource starAward;
	public AudioSource click;

	// Use this for initialization
	void Start () {
		gameCompletePopup.SetActive (false);
		//audioSource = this.gameObject.GetComponent<AudioSource> ();
		Instance = this;
		if (questionArray.Length > 0) {
			ShuffleQuestionArray ();

			if(autoPlayAudio)
				PlayAudio (questionArray [questionNumber].GetComponent<Question> ().question.questionAudio);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShuffleQuestionArray() {
		if (questionArray.Length > 0) {
			for (int i = 0; i < questionArray.Length; i++) {
				GameObject temp = questionArray [i];
				int r = Random.Range (i, questionArray.Length);
				questionArray [i] = questionArray [r];
				questionArray [r] = temp;
			}
			questionArray [questionNumber].SetActive (true);
			if (audioSource != null) {
				audioSource.clip = questionArray [questionNumber].GetComponent<Question> ().question.questionAudio;
			}
		}
	}

	public void NextQuestion() {

		if ((noOfQuestionsAnswered < totalNoOfQuestions)) {
		//	print ("next question");
			if ((questionArray.Length > 1)) {
				questionArray [questionNumber].SetActive (false);
				questionNumber++;
				questionArray [questionNumber].SetActive (true);
				if (audioSource != null) {
					audioSource.clip = questionArray [questionNumber].GetComponent<Question> ().question.questionAudio;
				}

				if ((questionArray [questionNumber].GetComponent<Question> ()!=null)&&(questionArray [questionNumber].GetComponent<Question> ().question.questionAudio != null) && autoPlayAudio)
					PlayAudio (questionArray [questionNumber].GetComponent<Question> ().question.questionAudio);
			}
		}
//		fishSpawner.spawnObjects ();
	}

	public string ValidateAnswer(string answer)
	{

		if (answer == questionArray [questionNumber].GetComponent<Question> ().question.answerValue) {
			print ("correct answer");
			CountQuestionsAnswered (true);
			return "correct";
		} else {
			if(countWrongAnswer){
				CountQuestionsAnswered (false);
			}
			print ("wrong answer");
			return "wrong";
		}
	}

	public void PlayAudio(AudioClip clip)
	{
		
		if (audioSource.isPlaying) {
			audioSource.Stop ();
		}
		if (clip != null) {
			audioSource.clip = clip;
		}
		audioSource.Play ();
	}

	public void RepeatAudio()
	{
		audioSource.clip = questionArray [questionNumber].GetComponent<Question> ().question.questionAudio;
		audioSource.Play ();
	}

	public string ValidateAnswerDirectly(string answer)			//for BubblePopper 
	{
		if(answer==Question.Instance.options[questionNumber].answerText){
			print ("correct answer");
			CountQuestionsAnswered (true);
			return "correct";
		} else {
			print ("wrong answer");
			//CountQuestionsAnswered (false);
			return "wrong";
		}
	}

	public void CountQuestionsAnswered(bool isCorrect)
	{
		noOfQuestionsAnswered++;
		if ((noOfQuestionsAnswered <= totalNoOfQuestions) && isCorrect) {
			RewardStars (noOfQuestionsAnswered);	
			//correctAns.Play ();

		} else if ((noOfQuestionsAnswered <= totalNoOfQuestions) && !isCorrect) {
			RewardWrongStars (noOfQuestionsAnswered);
			wrongAns.Play ();
		}

		if (noOfQuestionsAnswered >= totalNoOfQuestions) {
			Invoke("CompletedSection", 1.5f);
		}
//		else if ((noOfQuestionsAnswered < totalNoOfQuestions) && isCorrect){
//			RewardStars (noOfQuestionsAnswered);			
//		}
	}

	public void CompletedSection()
	{
		print ("One section finished");
		for (int i = 0; i < isCorrect; i++) {
			popupStars [i].gameObject.SetActive (true);			
		}
		gameCompletePopup.SetActive (true);
		//popUp.Play ();
	}

	public void RewardStars(int starIndex)
	{
	//	print ("Star rewarded");
		//starAward.Play();
		stars[starIndex - 1].gameObject.SetActive(true);
		isCorrect++;
	}

	public void RewardWrongStars(int starIndex)
	{
		//	print ("Star rewarded");
		wrongAnsStars[starIndex - 1].gameObject.SetActive(true);
		//isCorrect++;
	}

	public void PlayNextGame()
	{
		print ("Load next game");
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		//click.Play ();
	}

	public void RepeatGame()
	{
		print ("Play Again");
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		//click.Play ();
	}

	public void GoToStartScreen()
	{
		click.Play ();
		SceneManager.LoadScene ("StartScene");

	}

	public void GoToLevelSelectionScreen()
	{
		//click.Play ();
		SceneManager.LoadScene ("LevelSelectionScreen");
	}

	public void CheckAnswerArray()
	{
		questionArray [questionNumber].GetComponent<SwapNumbers> ().answerCounter = 0;
		for (int i = 0; i < questionArray [questionNumber].GetComponent<SwapNumbers> ().answerArray.Length; i++) {
			if (questionArray [questionNumber].GetComponent<SwapNumbers> ().answerArray [i] == questionArray [questionNumber].GetComponent<SwapNumbers> ().numberArray [i]) {
				questionArray [questionNumber].GetComponent<SwapNumbers> ().answerCounter++;
			} 
			else {
				continue;
			}
		}

		if (questionArray [questionNumber].GetComponent<SwapNumbers> ().answerCounter == questionArray [questionNumber].GetComponent<SwapNumbers> ().answerArray.Length) {
			//print ("Correct");
			questionArray [questionNumber].GetComponent<SwapNumbers> ().animator.SetTrigger ("H_cat");
			CountQuestionsAnswered (true);

		} else {
			//print ("Wrong");
			questionArray [questionNumber].GetComponent<SwapNumbers> ().animator.SetTrigger ("S_cat");
			if(countWrongAnswer){
				CountQuestionsAnswered (false);
			}
		}

		Invoke ("NextQuestion", 2.5f);
	}

	public void CheckAnswer()
	{
		questionArray [questionNumber].GetComponent<ArrangementScript> ().answerCounter = 0;
		for (int i = 0; i < questionArray [questionNumber].GetComponent<ArrangementScript> ().answerArray.Length; i++) {
			if (questionArray [questionNumber].GetComponent<ArrangementScript> ().answerArray [i] == questionArray [questionNumber].GetComponent<ArrangementScript> ().playerAnswer [i]) {
				questionArray [questionNumber].GetComponent<ArrangementScript> ().answerCounter++;
			} 
			else {
				continue;
			}
		}

		if (questionArray [questionNumber].GetComponent<ArrangementScript> ().answerCounter == questionArray [questionNumber].GetComponent<ArrangementScript> ().answerArray.Length - 1) {
			print ("Correct");
			CountQuestionsAnswered (true);

		} else {
			print ("Wrong");
			if(countWrongAnswer){
				CountQuestionsAnswered (false);
			}
		}

		NextQuestion ();
	}

	public void MakeAnswerArray()
	{
		questionArray [questionNumber].GetComponent<SwapNumbers> ().answerCounter = 0;
		for (int i = 0; i < questionArray [questionNumber].GetComponent<SwapNumbers> ().newAnswerArrayIndex; i++) {
			if (questionArray [questionNumber].transform.GetChild (i + 1).GetComponentInChildren<TextMesh> ().text == questionArray [questionNumber].GetComponent<SwapNumbers> ().newAnswerArray [i]) {
				questionArray [questionNumber].GetComponent<SwapNumbers> ().answerCounter++; 
			} else {
				continue;
			}

		}

		if (questionArray [questionNumber].GetComponent<SwapNumbers> ().answerCounter == questionArray [questionNumber].GetComponent<SwapNumbers> ().newAnswerArray.Length) {
			questionArray [questionNumber].GetComponent<SwapNumbers> ().animator.SetTrigger ("HappyCat");
			CountQuestionsAnswered (true);

		} else {
			questionArray [questionNumber].GetComponent<SwapNumbers> ().animator.SetTrigger ("SadCat");
			if(countWrongAnswer){
				CountQuestionsAnswered (false);
			}
		}

		Invoke("NextQuestion", 2.5f);
	}

	public void GreaterThan(){
		if (questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n1 > questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n2) {
		//	print ("Correct");
			CountQuestionsAnswered (true);
		} else {
		//	print ("Wrong");
			CountQuestionsAnswered (false);
		}
		questionArray [questionNumber].GetComponent<SetQuestionNumber> ().crocAnime.SetTrigger ("Greater");
		Invoke("NextQuestion", 2.5f);
	}

	public void LessThan(){
		if (questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n1 < questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n2) {
		//	print ("Correct");
			CountQuestionsAnswered (true);
		} else {
		//	print ("Wrong");
			CountQuestionsAnswered (false);
		}
		questionArray [questionNumber].GetComponent<SetQuestionNumber> ().crocAnime.SetTrigger ("Lesser");
		Invoke("NextQuestion", 2.7f);
	}

	public void EqualTo(){
		if (questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n1 == questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n2) {
		//	print ("Correct");
			CountQuestionsAnswered (true);
		}
		else {
		//	print ("Wrong");
			CountQuestionsAnswered (false);
		}

		questionArray [questionNumber].GetComponent<SetQuestionNumber> ().crocAnime.SetTrigger ("EqualTo");
		Invoke("NextQuestion", 2.7f);
	}

	public void NotEqualTo(){
		if (questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n1 == questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n2) {
		//	print ("Wrong");
			CountQuestionsAnswered (false);
		} 
		else {
		//	print ("Correct");
			CountQuestionsAnswered (true);
		}

		Invoke("NextQuestion", 2.7f);
	}

	public void RatNElephant()
	{
		questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalRatNumber = questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalAnswerNumbers[0] + questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalAnswerNumbers[1];
		questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalElephantNumber = questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalAnswerNumbers[2] + questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalAnswerNumbers[3];

		int n1 = int.Parse(questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalRatNumber);
		int n2 = int.Parse(questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalElephantNumber);
		if (n1 < n2) 
		{
			print("Correct");
			CountQuestionsAnswered(true);
		}
		else
		{
			print("Wrong");
			CountQuestionsAnswered(false);
		}
		NextQuestion();
	}

	public void SetAnswer(int ans){
		//int temp = ans;
		questionArray [questionNumber].GetComponent<SetStartPositions> ().answerPressed = ans;
		//CheckAddition (temp);
	}

	public void CheckAddition(){
		if (questionArray [questionNumber].GetComponent<SetStartPositions> ().answer == questionArray [questionNumber].GetComponent<SetStartPositions> ().answerPressed) {
			//print ("Correct");
			CountQuestionsAnswered (true);
		} else {
			//print ("Wrong");
			CountQuestionsAnswered (false);
		}

		NextQuestion ();
	}

	public void CheckTextAdd(){
		if (questionArray [questionNumber].GetComponent<SetNumberText> ().answerNumber.ToString () == questionArray [questionNumber].GetComponent<SetNumberText> ().answerPressed) {
			CountQuestionsAnswered (true);
		} else {
			CountQuestionsAnswered (false);
		}

		NextQuestion ();
	}

	public void CheckTextMultiply() {
		
		//click.Play ();

		if (questionArray [questionNumber].GetComponent<multiply> ().answer == questionArray [questionNumber].GetComponent<multiply> ().finalAnswer) {
			CountQuestionsAnswered (true);
		} else {
			CountQuestionsAnswered (false);
		}
		questionArray [questionNumber].GetComponent<multiply> ().eggTrayObj.SetActive (false);
		NextQuestion ();
	}

	public void CheckMultiplication(){

		//click.Play ();
		int tempnum = 0;

		for (int i = 0; i < questionArray [questionNumber].GetComponent<MultiplicationTable> ().answerArray.Length; i++) {
			if (questionArray [questionNumber].GetComponent<MultiplicationTable> ().answerArray[i] == questionArray [questionNumber].GetComponent<MultiplicationTable> ().playerAnswerArray[i]) {
				//CountQuestionsAnswered (true);
				tempnum++;
				//print (questionArray [questionNumber].GetComponent<MultiplicationTable> ().playerAnswerArray[i]);
				//continue;
			} else {
				//CountQuestionsAnswered (false);
				//print(questionArray [questionNumber].GetComponent<MultiplicationTable> ().answerArray[i]);
				break;
			}

		}

		if (tempnum >= questionArray [questionNumber].GetComponent<MultiplicationTable> ().answerArray.Length) {
			CountQuestionsAnswered (true);
			//NextQuestion ();
		} else {
			CountQuestionsAnswered (false);
			//NextQuestion ();
		}
		NextQuestion ();
	}

	public void HintButton()
	{
		//click.Play ();
		if (!questionArray [questionNumber].GetComponent<multiply> ().Arrange && !questionArray [questionNumber].GetComponent<multiply> ().hintPressed) 
		{
			questionArray [questionNumber].GetComponent<multiply> ().CreateEggTrayGrid ();
			questionArray [questionNumber].GetComponent<multiply> ().anime.SetTrigger ("Move");
			questionArray [questionNumber].GetComponent<multiply> ().eggTrayObj.SetActive (true);
			questionArray [questionNumber].GetComponent<multiply> ().hintPressed = true;
		}
	}
}
