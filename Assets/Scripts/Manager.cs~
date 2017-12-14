using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
	
	public static Manager Instance;
	public AudioManager audio_manager;

	public Intantiation_G_1_3 fishSpawner;
	public Image[] stars, popupStars, starsWrong;

	public GameObject[] questionArray;
	//public GameObject[] answerArray;

	public int questionNumber = 0;

	public Transform[] objectSpawnPoints;
	public bool useDisplayQuestion = false;
	//public bool directCompareAnswer = false;

	public bool autoPlayAudio = false, countWrongAnswer = false;
	public AudioSource audioSource, UIAudioSource, UIAudioSource2, source;

	public int noOfQuestionsAnswered, totalNoOfQuestions;

	public GameObject gameCompletePopup;
	public int isCorrect;

	public AudioClip UIClick, correctAudio, wrongAudio, popupAudio, starAudio, balloonPop;
	public AudioSource questionSource;

	public bool isGameComplete = false;

//	int n1, n2;
	// Use this for initialization
	void Start () {
		gameCompletePopup.SetActive (false);
	//	audioSource = this.gameObject.GetComponent<AudioSource> ();
		Instance = this;
		if (questionArray.Length > 0) {
			ShuffleQuestionArray ();

			if(autoPlayAudio){
				Invoke("PlayQuestionNumber", audio_manager.source.clip.length + 0.5f);
				//PlayAudio (questionArray [questionNumber].GetComponent<Question> ().question.questionAudio);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadAllNumbers()
	{
		
	}

	public void PlayQuestionNumber()
	{
		//print("PLAYQUESTIONAUDIO");
		PlayAudio(PlayerPrefs.GetString("Language") + questionArray [questionNumber].GetComponent<Question> ().question.questionClip);
	}


	public void PlayClickAudio (){
		UIAudioSource.clip = UIClick;
		UIAudioSource.Play ();
	}

	public void PlayQuestionAudio(){
	//print("PLAYQUESTIONAUDIO");
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

			if ((audioSource != null)&&(questionArray [questionNumber].GetComponent<Question> ()!=null)) {
				audioSource.clip = questionArray [questionNumber].GetComponent<Question> ().question.questionAudio;
			}
		}
	}

	public void NextQuestion() {
//		CountQuestionsAnswered (true);
		if ((noOfQuestionsAnswered < totalNoOfQuestions)) {
		//	print ("next question");
			if ((questionArray.Length > 1)) {
				questionArray [questionNumber].SetActive (false);
				questionNumber++;
				questionArray [questionNumber].SetActive (true);
				if ((audioSource != null)&&(questionArray [questionNumber].GetComponent<Question> ()!=null)) {
					audioSource.clip = questionArray [questionNumber].GetComponent<Question> ().question.questionAudio;
				}

				if ((questionArray [questionNumber].GetComponent<Question> ()!=null)&&(questionArray [questionNumber].GetComponent<Question> ().question.questionAudio != null) && autoPlayAudio){
					//PlayAudio (questionArray [questionNumber].GetComponent<Question> ().question.questionAudio);
					PlayQuestionNumber();
				}
			}
		}
//		fishSpawner.spawnObjects ();
	}

	public string ValidateAnswer(string answer)
	{

		if (answer == questionArray [questionNumber].GetComponent<Question> ().question.answerValue) {
			if(questionArray [questionNumber].GetComponent<Question> ().isAnswered == 0){
				CountQuestionsAnswered (true);
				questionArray [questionNumber].GetComponent<Question> ().isAnswered = 1;
				return "correct";
			}
			return "correct2";
		} else {
			if(questionArray [questionNumber].GetComponent<Question> ().isAnswered == 0){
				PlayWrongSound();
				if(countWrongAnswer)
				{
					CountQuestionsAnswered (false);
				}
				return "wrong";
			}
			return "wrong2";
		}

	
	}

	public void PlayAudio(string path)
	{
		if (audioSource.isPlaying) {
			audioSource.Stop ();
		}
		//if (clip != null)
		{
			//audioSource.clip = clip;
			audioSource.clip = Resources.Load(path) as AudioClip;
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
			CountQuestionsAnswered (true);
			return "correct";
		} else {
			CountQuestionsAnswered (false);
			return "wrong";
		}
	}

	public void CountQuestionsAnswered(bool isCorrect)
	{
		if ((noOfQuestionsAnswered <= totalNoOfQuestions) && !isCorrect && countWrongAnswer)
		{
			noOfQuestionsAnswered++;
			PlayWrongSound();
			RewardStars(false, noOfQuestionsAnswered);
		}
		else if((noOfQuestionsAnswered <= totalNoOfQuestions) && !isCorrect){
			PlayWrongSound();
		}
		else if ((noOfQuestionsAnswered <= totalNoOfQuestions) && isCorrect){
			noOfQuestionsAnswered++;

			//if ((noOfQuestionsAnswered <= totalNoOfQuestions) && isCorrect)
			{
				RewardStars (true, noOfQuestionsAnswered);			
			} 
	//		else if ((noOfQuestionsAnswered <= totalNoOfQuestions) && !isCorrect)
	//		{
	//			RewardStars(false, noOfQuestionsAnswered);
	//		}
		}
		if (noOfQuestionsAnswered >= totalNoOfQuestions) {
			Invoke("CompletedSection", 1.6f);
		}
	}

	public void CompletedSection()
	{
		//print ("One section finished");

		if(questionArray.Length>0){
			source = questionArray [questionNumber].GetComponent<AudioSource> ();
		}
		if (source != null) {
			source.Stop ();
		}
		UIAudioSource.Stop ();
		PlayPopupAudio ();
		for (int i = 0; i < isCorrect; i++) {
			popupStars [i].gameObject.SetActive (true);			
		}
		isGameComplete = true;
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
//			PlayWrongSound ();
			starsWrong [starIndex - 1].gameObject.SetActive (true);
		}
	}

	public void PlayStarAudio(){
		UIAudioSource.clip = starAudio;
		UIAudioSource.Play ();
	}

	public void PlayNextGame()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void RepeatGame()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void GoToStartScreen()
	{
		SceneManager.LoadScene ("StartScene");
	}

	public void GoToHomeScreen(string home){
		SceneManager.LoadScene (home);
	}
	//G2.2
	public void CheckAnswerArray()
	{
		if (!questionArray[questionNumber].GetComponent<SwapNumbers>().ansClicked)
		{
			questionArray[questionNumber].GetComponent<SwapNumbers>().answerCounter = 0;

			for (int i = 0; i < questionArray[questionNumber].GetComponent<SwapNumbers>().answerArray.Length; i++) {
				//print (questionArray [questionNumber].GetComponent<SwapNumbers> ().answerArray [i].GetComponentInChildren<TextMesh>().text);
				if (questionArray[questionNumber].GetComponent<SwapNumbers>().answerArray[i].GetComponentInChildren<TextMesh>().text == questionArray[questionNumber].GetComponent<SwapNumbers>().newAnswerArray[i]) {
					questionArray[questionNumber].GetComponent<SwapNumbers>().answerCounter++;

				}
				else {
					continue;
				}
			}

			if (questionArray[questionNumber].GetComponent<SwapNumbers>().answerCounter == questionArray[questionNumber].GetComponent<SwapNumbers>().answerArray.Length) {
				//print ("Correct");
				questionArray[questionNumber].GetComponent<SwapNumbers>().animator.SetTrigger("HappyCat");
				CountQuestionsAnswered(true);
				questionArray[questionNumber].GetComponent<SwapNumbers>().ansClicked = true;
				Invoke("NextQuestion", 2.5f);

			} else {
				//print ("Wrong");
				questionArray[questionNumber].GetComponent<SwapNumbers>().animator.SetTrigger("SadCat");
				CountQuestionsAnswered(false);
				if (countWrongAnswer) {
					// CountQuestionsAnswered(false);
					questionArray[questionNumber].GetComponent<SwapNumbers>().ansClicked = true;
					Invoke("NextQuestion", 2.5f);
				}
			}

		}
	}

	public void MakeAnswerArray()
	{
		if (!questionArray [questionNumber].GetComponent<ArrangeInOrder> ().ansClicked) {
			int tempNum = 0;
			string tempString = "";

			for (int i = 0; i <= questionArray [questionNumber].GetComponent<ArrangeInOrder> ().answer.Length - 1; i++) { 
				if (questionArray [questionNumber].GetComponent<ArrangeInOrder> ().caterpillarBodies [i] != null)
					tempString = questionArray [questionNumber].GetComponent<ArrangeInOrder> ().caterpillarBodies [i].GetComponentInChildren<TextMesh> ().text;
				else
					tempString = "";

				string tempAnsString = questionArray [questionNumber].GetComponent<ArrangeInOrder> ().answer [i];

				if (tempString == tempAnsString && tempString != "") {
					tempNum++;
				} else {
					break;
				}
			}

			if (tempNum == questionArray [questionNumber].GetComponent<ArrangeInOrder> ().answer.Length) {
				questionArray [questionNumber].GetComponent<ArrangeInOrder> ().anime.SetTrigger ("HappyCat");
				CountQuestionsAnswered (true);
				questionArray [questionNumber].GetComponent<ArrangeInOrder> ().ansClicked = true;
				Invoke ("NextQuestion", 2.5f);

			} else {
				questionArray [questionNumber].GetComponent<ArrangeInOrder> ().anime.SetTrigger ("SadCat");
				CountQuestionsAnswered (false);
				if (countWrongAnswer) {
					//CountQuestionsAnswered(false);
					questionArray[questionNumber].GetComponent<ArrangeInOrder>().ansClicked = true;
					Invoke("NextQuestion", 2.5f);
				}
			}


		}
	}

	//game 2.4 and 2.5
	public void MakeAnswerArray2()
	{
		if (!questionArray [questionNumber].GetComponent<BeforeNAfter> ().ansClicked) {
			int tempNum = 0;
			string tempString = "";

			for (int i = 0; i <= questionArray [questionNumber].GetComponent<BeforeNAfter> ().answer.Length - 1; i++) { 
				if (questionArray [questionNumber].GetComponent<BeforeNAfter> ().caterpillarBodies [i] != null)
					tempString = questionArray [questionNumber].GetComponent<BeforeNAfter> ().caterpillarBodies [i].GetComponentInChildren<TextMesh> ().text;
				else
					tempString = "";

				string tempAnsString = questionArray [questionNumber].GetComponent<BeforeNAfter> ().answer [i];

				if (tempString == tempAnsString && tempString != "") {
					tempNum++;
				} else {
					break;
				}
			}

			if (tempNum == questionArray [questionNumber].GetComponent<BeforeNAfter> ().answer.Length) {
				questionArray [questionNumber].GetComponent<BeforeNAfter> ().anime.SetTrigger ("HappyCat");
				CountQuestionsAnswered (true);
				questionArray [questionNumber].GetComponent<BeforeNAfter> ().ansClicked = true;
				Invoke ("NextQuestion", 2.5f);

			} else {
				questionArray [questionNumber].GetComponent<BeforeNAfter> ().anime.SetTrigger ("SadCat");
				CountQuestionsAnswered(false);
				questionArray [questionNumber].GetComponent<BeforeNAfter> ().ResetAnswer ();
				if (countWrongAnswer) {
					CountQuestionsAnswered (false);
					questionArray [questionNumber].GetComponent<BeforeNAfter> ().ansClicked = true;
					Invoke ("NextQuestion", 2.5f);
				}
			}


		}
	}

	//Game 2.6
	public void CheckBlockSet()
	{
		int tempNum = 0;

		for (int i = 1; i <= questionArray [questionNumber].GetComponent<BlockGame> ().numberBoxes.Length - 1; i++) {
			if (questionArray [questionNumber].GetComponent<BlockGame> ().numberBoxes [i] != null) {
				tempNum++;
			} else {
				break;
			}
		}

		if (tempNum >= questionArray [questionNumber].GetComponent<BlockGame> ().numberBoxes.Length - 1) {
			CountQuestionsAnswered (true);
			Invoke ("NextQuestion", 1f);
		} else {
			CountQuestionsAnswered (false);
		}
	}

	//J4 check answers
	public void GreaterThan() {
		if(questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered==0){
			if (questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n1 > questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n2) {
				questionArray [questionNumber].GetComponent<SetQuestionNumber> ().crocAnime.SetTrigger ("Greater");
				Invoke("NextQuestion", 5.2f);
				CountQuestionsAnswered (true);
				questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 1;
			} else {
				questionArray [questionNumber].GetComponent<SetQuestionNumber> ().crocAnime.SetTrigger ("WrongAns");
				//questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 0;
				CountQuestionsAnswered (false);
				if (countWrongAnswer) {
					Invoke("NextQuestion", 3.9f);
					questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 1;
				}
			}
			//			questionArray [questionNumber].GetComponent<SetQuestionNumber> ().crocAnime.SetTrigger ("Greater");
			//			questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 1;
			//			Invoke("NextQuestion", 3.9f);
		}
		//Invoke("NextQuestion", 3.9f);
	}

	public void LessThan() {
		if(questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered==0){
			if (questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n1 < questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n2) {
				questionArray [questionNumber].GetComponent<SetQuestionNumber> ().crocAnime.SetTrigger ("Lesser");
				CountQuestionsAnswered (true);
				Invoke("NextQuestion", 5.2f);
				questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 1;
			} else {
				questionArray [questionNumber].GetComponent<SetQuestionNumber> ().crocAnime.SetTrigger ("WrongAns");
				CountQuestionsAnswered (false);
				if(countWrongAnswer){
					Invoke("NextQuestion", 3.9f);
					questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 1;
				}
			}
			//questionArray [questionNumber].GetComponent<SetQuestionNumber> ().crocAnime.SetTrigger ("Lesser");
			//			questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 1;

			//			Invoke("NextQuestion", 3.9f);
		}
	}

	public void EqualTo() {
		if(questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered==0){
			if (questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n1 == questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n2) {
				questionArray [questionNumber].GetComponent<SetQuestionNumber> ().crocAnime.SetTrigger ("EqualTo");
				CountQuestionsAnswered (true);
				questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 1;
				Invoke("NextQuestion", 2.7f);
			}
			else {
				questionArray [questionNumber].GetComponent<SetQuestionNumber> ().crocAnime.SetTrigger ("WrongAns");
				CountQuestionsAnswered (false);
				if (countWrongAnswer) {
					questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 1;
					Invoke("NextQuestion", 2.7f);
				}
			}

			//	questionArray [questionNumber].GetComponent<SetQuestionNumber> ().crocAnime.SetTrigger ("EqualTo");
			//			questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 1;
			//			Invoke("NextQuestion", 2.7f);
		}
	}

	public void NotEqualTo(){
		if(questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered==0){
			if (questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n1 == questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n2) {
				//	print ("Wrong");
				questionArray [questionNumber].GetComponent<SetQuestionNumber> ().crocAnime.SetTrigger ("WrongAns");
				CountQuestionsAnswered (false);
				if(countWrongAnswer){
					questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 1;
					Invoke("NextQuestion", 2.7f);
				}
			} 
			else {
				//	print ("Correct");
				//questionArray [questionNumber].GetComponent<SetQuestionNumber> ().crocAnime.SetTrigger ("EqualTo");
				CountQuestionsAnswered (true);
				Invoke("NextQuestion", 2.7f);
				questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 1;
			}
			//questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 1;

		}
	}

	public void CompareDraggedImage(){
		//if(questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered==0)
		{
			if(questionArray [questionNumber].GetComponent<SetQuestionNumber> ().parent.transform.childCount<=0){
				//CountQuestionsAnswered(false);
				questionArray [questionNumber].GetComponent<SetQuestionNumber> ().Reset();

			}
			else{
				string temp = questionArray [questionNumber].GetComponent<SetQuestionNumber> ().parent.GetChild(0).gameObject.name;

				if ((questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n1 > questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n2)&&(temp=="GreaterThan")) {
					CountQuestionsAnswered (true);
					//					NextQuestion();
					Invoke("NextQuestion", 2f);
				}
				else if((questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n1 < questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n2)&&(temp=="LessThan")){
					CountQuestionsAnswered (true);
					//					NextQuestion();
					Invoke("NextQuestion", 2f);
				}
				else if((questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n1 == questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n2)&&(temp=="EqualTo")){
					CountQuestionsAnswered (true);
					//					NextQuestion();
					Invoke("NextQuestion", 2f);
				}
				else {
					//CountQuestionsAnswered(false);
					questionArray [questionNumber].GetComponent<SetQuestionNumber> ().Reset();
				}
			}
			//questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 1;
			//NextQuestion();
		}

	}

	public void CheckAnswer()
	{
		questionArray [questionNumber].GetComponent<ArrangementScript> ().answerCounter = 0;

		for (int i = 0; i < questionArray [questionNumber].GetComponent<ArrangementScript> ().answerArray.Length; i++)
		{
			if (questionArray [questionNumber].GetComponent<ArrangementScript> ().answerArray [i] == questionArray [questionNumber].GetComponent<ArrangementScript> ().playerAnswer [i]) {
				questionArray [questionNumber].GetComponent<ArrangementScript> ().answerCounter++;
			} 
			else {
				break;
			}
		}

		if ((questionArray [questionNumber].GetComponent<ArrangementScript> ().answerCounter == questionArray [questionNumber].GetComponent<ArrangementScript> ().answerArray.Length) 
			&& !questionArray [questionNumber].GetComponent<ArrangementScript> ().ansClicked)
		{
			CountQuestionsAnswered (true);
			questionArray [questionNumber].GetComponent<ArrangementScript> ().ansClicked = true;
			Invoke ("NextQuestion", 2f); //NextQuestion ();
		} else {
			if (countWrongAnswer) {
				Invoke ("NextQuestion", 2f);
			}
			CountQuestionsAnswered (false);
			questionArray [questionNumber].GetComponent<ArrangementScript> ().boyAnime.SetTrigger ("Wrong");
		}
	}

	public void RatNElephant()
	{
		if((questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalRatNumber=="")||(questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalElephantNumber=="")){
			CountQuestionsAnswered(false);
			if (countWrongAnswer) {
				NextQuestion();
			}
		} else {
			int n1 = int.Parse(questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalRatNumber);
			int n2 = int.Parse(questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalElephantNumber);

			if (n1 < n2) 
			{
				CountQuestionsAnswered(true);
				questionArray [questionNumber].GetComponent<GreaterOrLesser> ().ElephantAnime.SetTrigger ("ElephantHappy");
				questionArray [questionNumber].GetComponent<GreaterOrLesser> ().RatAnime.SetTrigger ("Rat_Happy");
				Invoke ("NextQuestion", 2.5f);
			}
			else
			{
				CountQuestionsAnswered(false);
				questionArray [questionNumber].GetComponent<GreaterOrLesser> ().ResetAnswer ();
				if (countWrongAnswer) {
					NextQuestion();
				}
			}
		}
		//NextQuestion();
	}

	public void RatNElephant2()
	{
		string tempFRN = questionArray [questionNumber].GetComponent<GreaterOrLesser_2> ().finalRatNumber; // FRN = finalRatNumber
		string tempFEN = questionArray [questionNumber].GetComponent<GreaterOrLesser_2> ().finalElephantNumber; // FEN = finalElephantNumber

		string tempSmallestNum = questionArray [questionNumber].GetComponent<GreaterOrLesser_2> ().smallestNum;
		string tempBiggestNum = questionArray [questionNumber].GetComponent<GreaterOrLesser_2> ().biggestNum;

		if((questionArray[questionNumber].GetComponent<GreaterOrLesser_2>().finalRatNumber == "") || (questionArray[questionNumber].GetComponent<GreaterOrLesser_2>().finalElephantNumber == "")){
			CountQuestionsAnswered(false);
			if (countWrongAnswer) {
				NextQuestion();
			}
		} else{
			int n1 = int.Parse(questionArray[questionNumber].GetComponent<GreaterOrLesser_2>().finalRatNumber);
			int n2 = int.Parse(questionArray[questionNumber].GetComponent<GreaterOrLesser_2>().finalElephantNumber);

			if (n1 < n2 && tempFRN == tempSmallestNum && tempFEN == tempBiggestNum) 
			{
				CountQuestionsAnswered(true);
				questionArray [questionNumber].GetComponent<GreaterOrLesser_2> ().ElephantAnime.SetTrigger ("ElephantHappy");
				questionArray [questionNumber].GetComponent<GreaterOrLesser_2> ().RatAnime.SetTrigger ("Rat_Happy");
				Invoke ("NextQuestion", 2.5f);
			}
			else
			{
				CountQuestionsAnswered(false);
				questionArray [questionNumber].GetComponent<GreaterOrLesser_2> ().ResetAnswer ();
				if (countWrongAnswer) {
					NextQuestion();
				}
			}
		}
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
			NextQuestion ();
		} else {
			CountQuestionsAnswered (false);
			if(countWrongAnswer){
				NextQuestion();
			}
		}

//		NextQuestion ();
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
		if ((questionArray [questionNumber].GetComponent<SetCarrotCount> ().answer == questionArray [questionNumber].GetComponent<SetCarrotCount> ().clickedAnswer))
		{
			if(questionArray [questionNumber].GetComponent<SetCarrotCount> ().isAnswered ==0)
			{
				CountQuestionsAnswered (true);
				questionArray [questionNumber].GetComponent<SetCarrotCount> ().isAnswered = 1;
				NextQuestion ();

			}

		} else {
			CountQuestionsAnswered (false);
			if(countWrongAnswer){
				NextQuestion ();
			}
		}
		//NextQuestion ();
	}

	public void CheckFinalCarrotCountAdd(){
		if ((questionArray [questionNumber].GetComponent<SetCarrotForAdd> ().answer == questionArray [questionNumber].GetComponent<SetCarrotForAdd> ().clickedAnswer))
		{
		//	if(questionArray [questionNumber].GetComponent<SetCarrotForAdd> ().isAnswered==0)
			{
				CountQuestionsAnswered (true);
				NextQuestion ();
				questionArray [questionNumber].GetComponent<SetCarrotForAdd> ().isAnswered = 1;
			}
	
		} else {
			CountQuestionsAnswered (false);
			if(countWrongAnswer){
				NextQuestion ();
			}
		}
//		NextQuestion ();
	}

	public void CheckSubtractionFromAddition(string grade){
		//print (grade);
		if (!questionArray [questionNumber].GetComponent<GetSubFromAdd> ().isAnswered){
			if (grade == "Grade1") {
				if (questionArray [questionNumber].GetComponent<GetSubFromAdd> ().noOneNumber.ToString () == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed) {
					CountQuestionsAnswered (true);
					questionArray [questionNumber].GetComponent<GetSubFromAdd> ().isAnswered = true;
					NextQuestion ();
				} 
				else {
					CountQuestionsAnswered (false);
					questionArray [questionNumber].GetComponent<GetSubFromAdd> ().ResetBalloonPositions();
					if(countWrongAnswer){
						questionArray [questionNumber].GetComponent<GetSubFromAdd> ().isAnswered = true;
						NextQuestion ();
					}
				}
			}

		}
//		else if (grade == "Grade2") {
//			if (((questionArray [questionNumber].GetComponent<GetSubFromAdd> ().noOneNumber.ToString() == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed)||
//				(questionArray [questionNumber].GetComponent<GetSubFromAdd> ().noTwoNumber.ToString() == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed)) &&
//				((questionArray [questionNumber].GetComponent<GetSubFromAdd> ().noOneNumber.ToString() == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed2)||
//					(questionArray [questionNumber].GetComponent<GetSubFromAdd> ().noTwoNumber.ToString() == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed2))) {
//				//print("correct");
//				CountQuestionsAnswered (true);
//				NextQuestion ();
//			}
//			else {
//				CountQuestionsAnswered (false);
//			}
//		}
//		else if (grade == "Grade3") {
//			if ((questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerNumber.ToString () == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed) &&
//			    ((questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed2 == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().noOneNumber.ToString ()) ||
//			    (questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed2 == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().noTwoNumber.ToString ())) &&
//			    ((questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed3 == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().noOneNumber.ToString ()) ||
//			    (questionArray [questionNumber].GetComponent<GetSubFromAdd> ().answerPressed3 == questionArray [questionNumber].GetComponent<GetSubFromAdd> ().noTwoNumber.ToString ())))
//			{
//				CountQuestionsAnswered (true);
//				NextQuestion ();
//			} 
//			else {
//				CountQuestionsAnswered (false);
//			}
//		}
//		else {
//			CountQuestionsAnswered (false);
//			if(countWrongAnswer){
//				NextQuestion ();
//			}
//		}
//		NextQuestion ();
	}

	public void CountFishesInBowl(){
		FishDrop fish = questionArray[questionNumber].GetComponent<FishDrop>();

		if(fish.qType==FishDrop.QuestionType.withoutRemainder){
			if(fish.clickedAns==fish.answer)
			{
				CountQuestionsAnswered(true);
				fish.ShowFishAnim(true);

				Invoke("NextQuestion", 2f);
			}

			else
			{
				CountQuestionsAnswered(false);
				fish.ShowFishAnim(false);
			}
		}
		else if(fish.qType==FishDrop.QuestionType.withRemainder)
		{
			print("with");
			if(fish.clickedAns==fish.answerInPot && fish.clickedRemainder==fish.answer)
			{
				CountQuestionsAnswered(true);
				fish.ShowFishAnim(true);
				Invoke("NextQuestion", 2f);
			}

			else
			{
				CountQuestionsAnswered(false);
				fish.ShowFishAnim(false);
			}
		}
	}

	public void CheckGridAnswer(){
		if(questionArray[questionNumber].GetComponent<DrawGrid>().clickedAnswer==questionArray[questionNumber].GetComponent<DrawGrid>().ansNumber){
			CountQuestionsAnswered(true);
			Invoke("NextQuestion", 2f);
		}else{
			CountQuestionsAnswered(false);
		}
	}

	public void CheckPlaceValueAddition()
	{
		string a1 = "";
		string a10 = "";
		string a100 = "";

		if (questionArray [questionNumber].GetComponent<PlaceValueAddition> ().ans3.text == "") {
			a100 = "0";
		} else if(questionArray [questionNumber].GetComponent<PlaceValueAddition> ().ans3.text != ""){
			a100 = questionArray [questionNumber].GetComponent<PlaceValueAddition> ().ans3.text; 
		}

		if (questionArray [questionNumber].GetComponent<PlaceValueAddition> ().ans2.text == "") {
			a10 = "0";
		} else if(questionArray [questionNumber].GetComponent<PlaceValueAddition> ().ans2.text != ""){
			a10 = questionArray [questionNumber].GetComponent<PlaceValueAddition> ().ans2.text; 
		}

		if (questionArray [questionNumber].GetComponent<PlaceValueAddition> ().ans1.text == "") {
			a1 = "0";
		} else if(questionArray [questionNumber].GetComponent<PlaceValueAddition> ().ans1.text != "") {
			a1 = questionArray [questionNumber].GetComponent<PlaceValueAddition> ().ans1.text; 
		}

		questionArray [questionNumber].GetComponent<PlaceValueAddition> ().playerAnswer =  a100 + a10 + a1;

		int n1 = int.Parse (questionArray [questionNumber].GetComponent<PlaceValueAddition> ().answer);
		int n2 = int.Parse (questionArray [questionNumber].GetComponent<PlaceValueAddition> ().playerAnswer);

		if(n1 == n2 && !questionArray [questionNumber].GetComponent<PlaceValueAddition> ().answered){
			CountQuestionsAnswered(true);
			questionArray [questionNumber].GetComponent<PlaceValueAddition> ().answered = true;
			Invoke("NextQuestion", 2f);
			questionArray[questionNumber].GetComponent<PlaceValueAddition>().ResetAnim();
		}else{
			CountQuestionsAnswered(false);
		}

	}

	public void CheckPlaceValue()
	{

		string a1 = "";
		string a10 = "";
		string a100 = "";

		if (questionArray [questionNumber].GetComponent<PlaceValue> ().ans100.text == "") {
			a100 = "0";
		} else if(questionArray [questionNumber].GetComponent<PlaceValue> ().ans100.text != ""){
			a100 = questionArray [questionNumber].GetComponent<PlaceValue> ().ans100.text; 
		}

		if (questionArray [questionNumber].GetComponent<PlaceValue> ().ans10.text == "") {
			a10 = "0";
		} else if(questionArray [questionNumber].GetComponent<PlaceValue> ().ans10.text != ""){
			a10 = questionArray [questionNumber].GetComponent<PlaceValue> ().ans10.text; 
		}

		if (questionArray [questionNumber].GetComponent<PlaceValue> ().ans1.text == "") {
			a1 = "0";
		} else if(questionArray [questionNumber].GetComponent<PlaceValue> ().ans1.text != "") {
			a1 = questionArray [questionNumber].GetComponent<PlaceValue> ().ans1.text; 
		}

		questionArray [questionNumber].GetComponent<PlaceValue> ().playerAnswer =  a100 + a10 + a1;

		int n1 = int.Parse (questionArray [questionNumber].GetComponent<PlaceValue> ().answer);
		int n2 = int.Parse (questionArray [questionNumber].GetComponent<PlaceValue> ().playerAnswer);

		if (n1 == n2 && !questionArray [questionNumber].GetComponent<PlaceValue> ().answered){
			CountQuestionsAnswered(true);
			questionArray [questionNumber].GetComponent<PlaceValue> ().answered = true;
			Invoke("NextQuestion", 2f);
			questionArray [questionNumber].GetComponent<PlaceValue> ().cam.SetBool ("moveCamera", false);

		} else {
			CountQuestionsAnswered(false);
		}
	}

	public void CheckPlaceValue2()
	{
		int temp1 = 0;
		int temp10 = 0;
		int temp100 = 0;

		int tempA1 = 0;
		int tempA10 = 0;
		int tempA100 = 0;

		tempA100 = int.Parse(questionArray [questionNumber].GetComponent<TestJ3> ().answer100);
		tempA10 = int.Parse(questionArray [questionNumber].GetComponent<TestJ3> ().answer10);
		tempA1 = int.Parse(questionArray [questionNumber].GetComponent<TestJ3> ().answer1);

		temp100 = int.Parse(questionArray [questionNumber].GetComponent<TestJ3> ().playerAnswer100);
		temp10 = int.Parse(questionArray [questionNumber].GetComponent<TestJ3> ().playerAnswer10);
		temp1 = int.Parse(questionArray [questionNumber].GetComponent<TestJ3> ().playerAnswer1);

		if (tempA100 == temp100 && tempA10 == temp10 && tempA1 == temp1 && !questionArray [questionNumber].GetComponent<TestJ3> ().answered) { 
			CountQuestionsAnswered (true);
			questionArray [questionNumber].GetComponent<TestJ3> ().answered = true;
			Invoke ("NextQuestion", 2f);
			questionArray[questionNumber].GetComponent<TestJ3>().ResetAnim();
		} else {
			CountQuestionsAnswered(false);
		}
	}


	//J10 check answer
	public void CheckSubtractionAnswer()
	{

		string a1 = "";
		string a10 = "";
		string a100 = "";
		bool allBlocksFilled = false;

		if (questionArray [questionNumber].GetComponent<Subtraction> ().ans100.text == "") {
			a100 = "0";
		} else if(questionArray [questionNumber].GetComponent<Subtraction> ().ans100.text != ""){
			a100 = questionArray [questionNumber].GetComponent<Subtraction> ().ans100.text; 
		}

		if (questionArray [questionNumber].GetComponent<Subtraction> ().ans10.text == "") {
			a10 = "0";
		} else if(questionArray [questionNumber].GetComponent<Subtraction> ().ans10.text != ""){
			a10 = questionArray [questionNumber].GetComponent<Subtraction> ().ans10.text; 
		}

		if (questionArray [questionNumber].GetComponent<Subtraction> ().ans1.text == "") {
			allBlocksFilled = false;
		} else if(questionArray [questionNumber].GetComponent<Subtraction> ().ans1.text != "") {
			a1 = questionArray [questionNumber].GetComponent<Subtraction> ().ans1.text; 
			allBlocksFilled = true;
		}

		questionArray [questionNumber].GetComponent<Subtraction> ().playerAnswer =  a100 + a10 + a1;


		int n1 = int.Parse (questionArray [questionNumber].GetComponent<Subtraction> ().answer);
		int n2 = int.Parse (questionArray [questionNumber].GetComponent<Subtraction> ().playerAnswer);

		if (n1 == n2 && !questionArray [questionNumber].GetComponent<Subtraction> ().answered && allBlocksFilled) {
			CountQuestionsAnswered(true);
			questionArray [questionNumber].GetComponent<Subtraction> ().answered = true;
			Invoke("NextQuestion", 2f);
			questionArray [questionNumber].GetComponent<Subtraction> ().camAnimation.SetBool ("moveCamera", false); 
		} else {
			CountQuestionsAnswered(false);
		}
	}

	//J11 check answer
	public void CheckMulByRepeatedAdd()
	{
		int playerAns = int.Parse (questionArray [questionNumber].GetComponent<MulByRepeatedAdd> ().playerAns);
		if (playerAns == questionArray [questionNumber].GetComponent<MulByRepeatedAdd> ().answer) {
			CountQuestionsAnswered (true);
			questionArray [questionNumber].GetComponent<MulByRepeatedAdd> ().playerAns = "";
			Invoke("NextQuestion", 0.2f);
			questionArray [questionNumber].GetComponent<MulByRepeatedAdd>().ResetKeypad();
			//Invoke("ScrollAnimation",2f);
			//print ("correct");
		}else {
			
			CountQuestionsAnswered (false);
			questionArray [questionNumber].GetComponent<MulByRepeatedAdd> ().ScrollAnimation ();
			//print ("wrong");

		}
		questionArray [questionNumber].GetComponent<MulByRepeatedAdd> ().count = 0;
	}

	//J12 check answer
	public void CheckTextMultiply() {

		//click.Play ();

		if (questionArray [questionNumber].GetComponent<multiply> ().answer == questionArray [questionNumber].GetComponent<multiply> ().finalAnswer) {
			CountQuestionsAnswered (true);
			questionArray [questionNumber].GetComponent<multiply> ().eggTrayObj.SetActive (false);
			NextQuestion ();
		} else {
			CountQuestionsAnswered (false);
			//wrongAns.Play();
			questionArray [questionNumber].GetComponent<multiply> ().anime.SetTrigger ("CalculatorShake");
			questionArray [questionNumber].GetComponent<multiply> ().Reset ();
		}

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
			NextQuestion ();
		} else {
			//wrongAns.Play ();
			questionArray [questionNumber].GetComponent<MultiplicationTable> ().Reset ();
			CountQuestionsAnswered (false);
			//NextQuestion ();
		}
		//NextQuestion ();
	}

	public void HintButton()
	{
		//click.Play ();
		if (!questionArray [questionNumber].GetComponent<multiply> ().hintPressed && questionArray [questionNumber].GetComponent<multiply> ().moveCalci != null) 
		{
			questionArray [questionNumber].GetComponent<multiply> ().CreateEggTrayGrid ();
			questionArray [questionNumber].GetComponent<multiply> ().moveCalci.SetTrigger ("Move");
			questionArray [questionNumber].GetComponent<multiply> ().eggTrayObj.SetActive (true);
			questionArray [questionNumber].GetComponent<multiply> ().hintPressed = true;
		}
	}


	//j16.1


	public void CheckCarrotDivision()
	{
		DivideCarrots carrotDivQuestion =  questionArray[questionNumber].GetComponent<DivideCarrots>();

		if(carrotDivQuestion.qType==DivideCarrots.QuestionType.withoutRemainder){
			if(carrotDivQuestion.answer.ToString()==carrotDivQuestion.clickedAnswer)
			{
				CountQuestionsAnswered(true);
				questionArray[questionNumber].GetComponent<DivideCarrots>().ResetKeypad();
				NextQuestion();
			}
			else{
				CountQuestionsAnswered(false);
			}
		}
		else if(carrotDivQuestion.qType==DivideCarrots.QuestionType.withRemainder)
		{
			if(carrotDivQuestion.answer.ToString()==carrotDivQuestion.clickedAnswer && carrotDivQuestion.remainder.ToString()==carrotDivQuestion.clickedRemainder)
			{
				CountQuestionsAnswered(true);
				questionArray[questionNumber].GetComponent<DivideCarrots>().ResetKeypad();
				NextQuestion();
			}
			else{
				CountQuestionsAnswered(false);
			}
		}

	}

	//j16.3
	public void J16_3CheckAnswer(){
		questionArray [questionNumber].GetComponent<DivisionByMul> ().playerAnswer = int.Parse (questionArray [questionNumber].GetComponent<DivisionByMul> ().answerText.text);

		if (questionArray [questionNumber].GetComponent<DivisionByMul> ().answer == questionArray [questionNumber].GetComponent<DivisionByMul> ().playerAnswer) {
			CountQuestionsAnswered(true);
			questionArray[questionNumber].GetComponent<DivisionByMul>().ResetKeypad();
			NextQuestion();
		}
		else{
			CountQuestionsAnswered(false);
		}
	}


	//j13
	public void J13_CheckAnswer() {

		int temp = 0;
		int length = questionArray [questionNumber].GetComponent<MultiplicationGrid> ().numberBoxes.Count;
		for (int i = 1; i <= length - 1; i++) {
			if (questionArray [questionNumber].GetComponent<MultiplicationGrid> ().numberBoxes [i] == null) {
				break;
			} else {
				temp++;
			}

			if (temp >= length - 1) {
				CountQuestionsAnswered (true);
				NextQuestion ();
			} else {
				CountQuestionsAnswered(false);
			}
		}
	}


	//J17

	public void CountCoinsInTrays()
	{
		DistributedCoins trayCoin = questionArray [questionNumber].GetComponent<DistributedCoins> ();

		if(trayCoin.qType==DistributedCoins.QuestionType.withoutRemainder){

			if(trayCoin.playerAnswer.text==trayCoin.answer.ToString())
			{
				CountQuestionsAnswered(true);
				trayCoin.ResetKeypad();
				NextQuestion();
			}else{
				CountQuestionsAnswered(false);
			}

		}
		else if(trayCoin.qType==DistributedCoins.QuestionType.withRemainder){

			if(trayCoin.playerAnswer.text==trayCoin.answer.ToString() && trayCoin.remainderAnswer.text==trayCoin.remainder.ToString())
			{
				CountQuestionsAnswered(true);
				trayCoin.ResetKeypad();
				NextQuestion();
			}else{
				CountQuestionsAnswered(false);
			}

		}
	}

}
