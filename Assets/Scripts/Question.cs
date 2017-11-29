using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class QuestionContent {
	public AudioClip questionAudio = null;
	public string questionString;
	public Sprite questionSprite;
	public string answerValue;
}

[System.Serializable]
public class AnswerContent {
	public AudioClip answerAudio = null;
	public Sprite answerSprite = null;
	public string answerText = null;
}


public class Question : MonoBehaviour {

	public GameObject manager;
	public QuestionContent question;
	public AnswerContent[] options;

	public static Question Instance;
	public bool shuffleOptions;
	public int isAnswered = 0;

	void Awake (){
		if(shuffleOptions)
			ShuffleOptions ();
	}

	// Use this for initialization
	void Start () {
		Instance = this;
		manager = GameObject.Find ("Manager");

		if(manager.GetComponent<Manager>().useDisplayQuestion)
			DisplayQuestions();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShuffleOptions()
	{
		for (int i = 0; i < options.Length; i++) {
			AnswerContent temp = options[i];
			int r = Random.Range (i, options.Length);
			options[i] =  options[r];
			options[r] =  temp;
		}
	}

	void DisplayQuestions()
	{
		for(int j=0; j < transform.GetChild(1).childCount; j++) {
		//	transform.GetChild (1).GetChild (j).GetComponent<Image> ().sprite = options [j].answerSprite;
			transform.GetChild (1).GetChild (j).GetComponentInChildren<Text> ().text = options [j].answerText;
		}

		if(question.questionString != null && question.questionString.Length > 0){
			print(question.questionString.Length);
			this.transform.GetChild(1).GetComponent<Text>().text = question.questionString;
		}
	}

}
