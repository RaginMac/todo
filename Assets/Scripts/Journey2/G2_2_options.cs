using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class G2_2_options : MonoBehaviour {

	public Manager manager;
	public Animator caterpillarAnime;
	public G2_2 question;

	int count = 0;

	void Start () {
		manager = GameObject.Find ("Manager").GetComponent<Manager>();
	}

	void Update () {
		
	}

	public void CheckAnswer(bool whichBodyToCheck)
	{
		
		if (!whichBodyToCheck) {
			for (int i = 0; i < question.caterpillar1.Length; i++) {
				int temp = int.Parse (question.caterpillar1 [i].GetComponentInChildren<Text> ().text);
				if (question.answerArray [i] == temp) {
					count++;
				} else {
					break;
				}
			}
		} else if (whichBodyToCheck) {
			for (int i = 0; i < question.caterpillar2.Length; i++) {
				int temp = int.Parse (question.caterpillar2 [i].GetComponentInChildren<Text> ().text);
				if (question.answerArray [i] == temp) {
					count++;
				} else {
					break;
				}
			}
		}
				
		if (count >= 4 && !question.ansClicked) {
			manager.CountQuestionsAnswered (true);
			caterpillarAnime.SetTrigger ("HappyCat");
			question.ansClicked = true; 
			Invoke("Next", 2.5f);
		} else {
			manager.CountQuestionsAnswered (false);
			caterpillarAnime.SetTrigger ("SadCat");
		}
	}

	void Next(){
		manager.NextQuestion ();
	}
}
