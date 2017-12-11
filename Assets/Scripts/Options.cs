using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {

	public Manager manager;
	public GameObject parentSprites, ballParent;
	public Intantiation_G_1_3 fishScript;
	public Animator anim;

	public AudioClip[] countAudio;
	public string[] countAudioNumbers;
	public AudioSource audioSource;
	public int clipNumber;
	public int answerValue;
	public string answerString;


	public bool ansClicked;
	//public Transform spawnedFish;

	// Use this for initialization
	void Start () {
		audioSource = GameObject.Find ("Main Camera").GetComponent<AudioSource> ();
		parentSprites = GameObject.Find("QuestionSprites");
		manager = GameObject.Find ("Manager").GetComponent<Manager>();
		fishScript = GameObject.Find ("Questions").GetComponent<Intantiation_G_1_3> ();
		if(fishScript==null){
			ballParent = this.gameObject;
		}

	}

	public void CheckAnswer() {
		string validity = manager.ValidateAnswer(this.GetComponentInChildren<Text>().text);

		if (validity == "correct") {

			parentSprites.transform.GetChild (0).GetComponent<FishAnim> ().PlayAnim ("Correct");	
			//	manager.NextQuestion ();	//called from manager
			Invoke("Spawn", 3f);
		} else {
			parentSprites.transform.GetChild (0).GetComponent<FishAnim> ().PlayAnim ("Wrong");
			if(manager.countWrongAnswer){
				Invoke("Spawn", 3f);
			}else{
				DestroyThisOption ();
			}
		}

	}

	public void Spawn(){
		manager.NextQuestion ();
		if (fishScript != null) {
			fishScript.spawnObjects ();
		}
	}



	public void CheckAnswerButton() {
		string valid = manager.ValidateAnswer (this.GetComponent<Image>().sprite.name);
	
		if (valid == "correct") {

			//ballParent.GetComponentInChildren<Animator>().SetTrigger("BallJump");
			//ballParent.GetComponentsInChildren<Animator>().SetTrigger("BallJump");
			Animator[] balls = ballParent.transform.GetComponentsInChildren<Animator>();
			for (int i = 0; i < balls.Length; i++) {
				balls[i].SetTrigger("BallJump");
			}
			//manager.NextQuestion ();
			Invoke("Next", 3f);

			if (fishScript != null) {
				fishScript.spawnObjects ();
			}

		} else {
			
			if(manager.countWrongAnswer){
				Invoke("Next", 2f);;
			}else{
				//DestroyThisOption ();
				this.GetComponent<Animator>().SetTrigger("OptionShake");
			}
		}
	}


	public void CheckAnswerString(){
		//print ("oo");
		if (!ansClicked)
		{
			string valid = manager.ValidateAnswer(answerString);
			if (anim != null) {
				if (valid == "correct") {
					anim.SetTrigger("HappyCat");
					ansClicked = true;
					Invoke("Next", 2.5f);
				} else if (valid == "wrong") {
					anim.SetTrigger("SadCat");
					if (manager.countWrongAnswer) {
						ansClicked = true;
						Invoke("Next", 2.5f);
					}
				}
			}
			//		manager.NextQuestion ();

		}
	}

	public void Next(){
		manager.NextQuestion ();
	}

	public void BatCountAudio()
	{
		//answerValue = int.Parse(this.GetComponent<Image>().sprite.name);

		//print("run +    " + answerValue );
		if(clipNumber < answerValue) {
			if (audioSource.isPlaying) {
				audioSource.Stop ();
			}

			if (countAudio[clipNumber] != null) {
				audioSource.clip = Resources.Load(PlayerPrefs.GetString("Language") + countAudioNumbers[clipNumber]) as AudioClip;//countAudio[clipNumber];
			}
			audioSource.Play ();

			clipNumber++;
		}
		else {
			clipNumber = 0;
		}
	}

	public void DestroyThisOption()
	{
		this.gameObject.SetActive (false);
	}
}
