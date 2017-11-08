using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
	
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
	public AudioSource audioSource, UIAudioSource, UIAudioSource2, source;

	public int noOfQuestionsAnswered, totalNoOfQuestions;

	public GameObject gameCompletePopup;
	public int isCorrect;

	public AudioClip UIClick, correctAudio, wrongAudio, popupAudio, starAudio, balloonPop;
	public AudioSource questionSource;

//	int n1, n2;
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
					PlayAudio (questionArray [questionNumber].GetComponent<Question> ().question.questionAudio);
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
		print ("One section finished");

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
			CountQuestionsAnswered (true);

		} else {
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
		if(questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered==0){
			if (questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n1 > questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n2) {
				CountQuestionsAnswered (true);
			} else {
				CountQuestionsAnswered (false);
			}
			questionArray [questionNumber].GetComponent<SetQuestionNumber> ().crocAnime.SetTrigger ("Greater");
			questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 1;
			Invoke("NextQuestion", 3.9f);
		}
		//Invoke("NextQuestion", 3.9f);
	}

	public void LessThan(){
		if(questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered==0){
			if (questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n1 < questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n2) {
				CountQuestionsAnswered (true);
			} else {
				CountQuestionsAnswered (false);
			}
			questionArray [questionNumber].GetComponent<SetQuestionNumber> ().crocAnime.SetTrigger ("Lesser");
			questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 1;

			Invoke("NextQuestion", 3.9f);
		}
	}

	public void EqualTo(){
		if(questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered==0){
			if (questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n1 == questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n2) {
				CountQuestionsAnswered (true);
			}
			else {
				CountQuestionsAnswered (false);
			}

			questionArray [questionNumber].GetComponent<SetQuestionNumber> ().crocAnime.SetTrigger ("EqualTo");
			questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 1;
			Invoke("NextQuestion", 2.7f);
		}
	}

	public void NotEqualTo(){
		if(questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered==0){
			if (questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n1 == questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n2) {
			//	print ("Wrong");
				CountQuestionsAnswered (false);
			} 
			else {
			//	print ("Correct");
				CountQuestionsAnswered (true);
			}
			questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 1;
			Invoke("NextQuestion", 2.7f);
		}
	}

	public void CompareDraggedImage(){
		if(questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered==0){
			if(questionArray [questionNumber].GetComponent<SetQuestionNumber> ().parent.transform.childCount<=0){
				CountQuestionsAnswered(false);
			}
			else{
				string temp = questionArray [questionNumber].GetComponent<SetQuestionNumber> ().parent.GetChild(0).gameObject.name;

				if ((questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n1 > questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n2)&&(temp=="GreaterThan")) {
					CountQuestionsAnswered (true);
				}
				else if((questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n1 < questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n2)&&(temp=="LessThan")){
					CountQuestionsAnswered (true);
				}
				else if((questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n1 == questionArray [questionNumber].GetComponent<SetQuestionNumber> ().n2)&&(temp=="EqualTo")){
					CountQuestionsAnswered (true);
				}
				else {
					CountQuestionsAnswered(false);
				}
			}
			questionArray [questionNumber].GetComponent<SetQuestionNumber> ().answered = 1;
			NextQuestion();
		}

	}

	public void RatNElephant()
	{
		
		questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalRatNumber = questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalAnswerNumbers[0] + questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalAnswerNumbers[1];
		questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalElephantNumber = questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalAnswerNumbers[2] + questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalAnswerNumbers[3];

		if((questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalRatNumber=="")||(questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalElephantNumber=="")){
			CountQuestionsAnswered(false);
		}else{
			int n1 = int.Parse(questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalRatNumber);
			int n2 = int.Parse(questionArray[questionNumber].GetComponent<GreaterOrLesser>().finalElephantNumber);

			if (n1 < n2) 
			{
				CountQuestionsAnswered(true);
			}
			else
			{
				CountQuestionsAnswered(false);
			}
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

	public void CountFishesInBowl(bool hasRemainder){
		FishDrop fish = questionArray[questionNumber].GetComponent<FishDrop>();
		if(!hasRemainder){
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
		else if(hasRemainder)
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
}
