using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
	//THIS IS TEST FILE FOR GIT
	public static Manager Instance;
	public Intantiation_G_1_3 fishSpawner;
	public Image[] stars, popupStars, starsWrong;

	public GameObject[] questionArray;
	//public GameObject[] answerArray;

	public int questionNumber = 0;

	public Transform[] objectSpawnPoints;
	public bool useDisplayQuestion = false;
	//public bool directCompareAnswer = false;

	public bool autoPlayAudio = false, countWrongAnswer = false;
	public AudioSource audioSource, UIAudioSource, UIAudioSource2;

	public int noOfQuestionsAnswered, totalNoOfQuestions;

	public GameObject gameCompletePopup;
	public int isCorrect;

	public AudioClip UIClick, correctAudio, wrongAudio, popupAudio, starAudio, balloonPop;
	public AudioSource questionSource;

	// Use this for initialization
	void Start () {
		gameCompletePopup.SetActive (false);
	//	audioSource = this.gameObject.GetComponent<AudioSource> ();
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

	public void PlayClickAudio (){
		UIAudioSource.clip = UIClick;
		UIAudioSource.Play ();
	}

	public void PlayQuestionAudio(){
		if (!questionArray [questionNumber].GetComponent<AudioSource> ().isPlaying) {
			questionArray [questionNumber].GetComponent<AudioSource> ().Play ();
		}
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
		//audioSource.clip = questionArray [questionNumber].GetComponent<Question> ().question.questionAudio;
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
			RewardStars (true, noOfQuestionsAnswered);			
		} else if ((noOfQuestionsAnswered <= totalNoOfQuestions) && !isCorrect) {
			RewardStars(false, noOfQuestionsAnswered);
		}

		if (noOfQuestionsAnswered >= totalNoOfQuestions) {
			Invoke("CompletedSection", 1.6f);
		}
//		else if ((noOfQuestionsAnswered < totalNoOfQuestions) && isCorrect){
//			RewardStars (noOfQuestionsAnswered);			
//		}
	}

	public void CompletedSection()
	{
		print ("One section finished");
		AudioSource source = questionArray [questionNumber].GetComponent<AudioSource> ();
		if (source != null) {
			source.Stop ();
		}
		UIAudioSource.Stop ();
		PlayPopupAudio ();
		for (int i = 0; i < isCorrect; i++) {
			popupStars [i].gameObject.SetActive (true);			
		}
		gameCompletePopup.SetActive (true);
	}

	public void PlayPopupAudio(){
		UIAudioSource.clip = popupAudio;
		UIAudioSource.Play ();
	}

	public void PlayCorrectSound(){
		UIAudioSource2.clip = correctAudio;
		UIAudioSource2.Play ();
	}
	public void PlayWrongSound(){
		UIAudioSource2.clip = wrongAudio;
		UIAudioSource2.Play ();
	}

	public void RewardStars(bool correct, int starIndex)
	{
	//	print ("Star rewarded");

		if (correct) {
			PlayStarAudio();
			PlayCorrectSound ();
			stars [starIndex - 1].gameObject.SetActive (true);
			isCorrect++;
		} else if (!correct) {
			PlayWrongSound ();
			starsWrong [starIndex - 1].gameObject.SetActive (true);
		}
	}

	public void PlayStarAudio(){
		UIAudioSource.clip = starAudio;
		UIAudioSource.Play ();
	}

	public void PlayNextGame()
	{
		print ("Load next game");
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void RepeatGame()
	{
		print ("Play Again");
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void GoToStartScreen()
	{
		SceneManager.LoadScene ("StartScene");
	}

	public void GoToHomeScreen(string home){
		SceneManager.LoadScene (home);
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

	public void CheckTextSub(){
		if (questionArray [questionNumber].GetComponent<SetNumberTextSub> ().answerNumber.ToString () == questionArray [questionNumber].GetComponent<SetNumberTextSub> ().answerPressed) {
			CountQuestionsAnswered (true);
		} else {
			CountQuestionsAnswered (false);
		}

		NextQuestion ();
	}

	public void CheckCarrotCount(){
		if (questionArray [questionNumber].GetComponent<SetCarrotCount> ().answer == questionArray [questionNumber].GetComponent<SetCarrotCount> ().clickedAnswer) {
			CountQuestionsAnswered (true);
		} else {
			CountQuestionsAnswered (false);
		}
		NextQuestion ();
	}

	public void CheckSubtractionFromAddition(string grade){
		print (grade);

		if (grade == "Grade1") {
			if (questionArray [questionNumber].GetComponent<GetSubFromAdd> ().noOneNumber.ToString () == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed) {
				print ("correct");
				CountQuestionsAnswered (true);
			} else {
				CountQuestionsAnswered (false);
			}
		}
		if (grade == "Grade2") {
			if (((questionArray [questionNumber].GetComponent<GetSubFromAdd> ().noOneNumber.ToString() == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed)||
				(questionArray [questionNumber].GetComponent<GetSubFromAdd> ().noTwoNumber.ToString() == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed)) &&
				((questionArray [questionNumber].GetComponent<GetSubFromAdd> ().noOneNumber.ToString() == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed2)||
					(questionArray [questionNumber].GetComponent<GetSubFromAdd> ().noTwoNumber.ToString() == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed2))) {
				//print("correct");
				CountQuestionsAnswered (true);
			} else {
				CountQuestionsAnswered (false);
			}
		}

		if (grade == "Grade3") {
			if ((questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerNumber.ToString () == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed) &&
			    ((questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed2 == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().noOneNumber.ToString ()) ||
			    (questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed2 == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().noTwoNumber.ToString ())) &&
			    ((questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed3 == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().noOneNumber.ToString ()) ||
			    (questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed3 == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().noTwoNumber.ToString ()))) {
				CountQuestionsAnswered (true);
			} else {
				CountQuestionsAnswered (false);
			}
		}

		NextQuestion ();
	}
}
