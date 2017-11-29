using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBubbles : MonoBehaviour {

	public GameObject[] balloons;
	public GetSubFromAdd subAddScript;
	public int balloonCount = -1;
	public Sprite balloon1, balloon2;
	public Transform[] ansOptions;

	void Awake(){
		InvokeRepeating("ShowBalloons", 0.3f, 0.8f);
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowBalloons(){

		balloonCount+=1;


		if (balloonCount <= subAddScript.noOneNumber-1) {
		//	print("balloon1");
			balloons [balloonCount].GetComponent<SpriteRenderer>().sprite = balloon1;
			balloons [balloonCount].GetComponent<BubbleAnimationScript>().whichBalloonColor = "Pink";
		}
		else{
			//print("balloon2");
			balloons [balloonCount].GetComponent<SpriteRenderer>().sprite = balloon2;
			balloons [balloonCount].GetComponent<BubbleAnimationScript>().whichBalloonColor = "Red";
		}

		balloons [balloonCount].SetActive (true);
		balloons[balloonCount].GetComponent<Animator>().SetTrigger("BalloonSpawn");

		if (balloonCount >= subAddScript.answerNumber-1) {
			CancelInvoke ("ShowBalloons");
		}
	}

	public void ResetBalloons(){
		CancelInvoke ("ShowBalloons");

		for(int i=0; i<subAddScript.answerNumber;i++){
			balloons[i].GetComponent<Animator>().SetTrigger("BalloonIdle");
			balloons[i].transform.position = balloons[i].GetComponent<BubbleAnimationScript>().startPosition;
			balloons[i].SetActive(false);
		}
		for (int i = 0; i < ansOptions.Length; i++) {
			ansOptions[i].position = ansOptions[i].GetComponent<OriginalPos>().originalPos;
		}
		balloonCount = -1;
		InvokeRepeating("ShowBalloons", 0.3f, 0.8f);
		//Invoke("RepeatSpawn", 2f);
	}

	public void RepeatSpawn(){
		InvokeRepeating("ShowBalloons", 0.3f, 0.8f);
	}
}
