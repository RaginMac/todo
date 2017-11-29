using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleManager : MonoBehaviour {


	public GameObject[] bubbleObjects;
	public Transform[] bubbleSpawns;

	public GameObject bubblePrefab;

	public Text tempQuestionText;

	public Question question;
	public Manager manager;

	void Awake()
	{
		//Question.Instance.ShuffleOptions();
	}
	// Use this for initialization
	void Start () {
		question = GameObject.Find("Manager").GetComponent<Question> ();
		//bubbleObjects - new GameObject[10];
		//question.ShuffleOptions();
		SetQuestions();

		//ResetOptions ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void ShowQuestion()
	{
		Invoke ("SetQuestions", 1f);
	}
	public void SetQuestions()
	{
		
	//	manager.CountQuestionsAnswered ();
		Manager.Instance.questionNumber++;
		Question.Instance.ShuffleOptions();
		tempQuestionText.text = Question.Instance.options [Manager.Instance.questionNumber].answerText;

		if(question.options [Manager.Instance.questionNumber].answerAudio != null)
			Manager.Instance.PlayAudio (question.options [Manager.Instance.questionNumber].answerAudio);
		
		SpawnAndResetOptions ();
	}

	public void ShuffleSpawns()
	{
		for (int i = 0; i < bubbleSpawns.Length; i++) {
			Transform temp  = bubbleSpawns[i];
			int r = Random.Range(i, bubbleSpawns.Length);
			bubbleSpawns[i] = bubbleSpawns[r];
			bubbleSpawns[r] = temp;
		}

//		for (int j=0; j
	}

	public void SpawnAndResetOptions()
	{
		//Manager.Instance.questionNumber = 0;
		ShuffleSpawns ();
		//question.ShuffleOptions ();
		for (int i = 0; i < bubbleObjects.Length; i++) {
			if (bubbleObjects [i] != null) {
				Destroy (bubbleObjects [i]);
			}
			bubbleObjects[i]= Instantiate (bubblePrefab, bubbleSpawns [i].position, Quaternion.identity);
			bubbleObjects [i].GetComponent<TextMesh> ().text = Question.Instance.options [i].answerText;

		}
	}
}
