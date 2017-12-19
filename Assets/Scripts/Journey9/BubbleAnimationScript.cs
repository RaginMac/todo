using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BubbleAnimationScript : MonoBehaviour {

	public Transform startPos, endPos;
	public float speed;
	public Animator balloonAnimator;
	public string whichBalloonColor;

	public AudioSource source;
	public AudioClip blowBallon;
	public Vector3 startPosition;
	public bool isReady = false;

	void Awake(){
		balloonAnimator = this.transform.GetComponent<Animator> ();
		startPos = this.transform;
		startPosition = startPos.position = this.transform.position;
		//print(startPos.position);
	}
	// Use this for initialization
	void Start () {
//		balloonAnimator.SetTrigger("BalloonSpa");
		source.clip = blowBallon;
		source.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards(this.transform.position, endPos.position, speed*Time.deltaTime);
	}

//	public override void OnPointerClick(PointerEventData data){
//		print ("Clicked");
//	}

	void OnMouseDown(){

//		if(whichBalloonColor=="Pink"){
//			balloonAnimator.SetTrigger ("BurstBalloon");
//		}
		if(whichBalloonColor=="Red"){
			balloonAnimator.SetTrigger("BurstRedBalloon");
			Manager.Instance.UIAudioSource.clip = Manager.Instance.balloonPop;
			Manager.Instance.UIAudioSource.Play ();
		}
		//Invoke ("BalloonPop", 1f);
	}

	void BalloonPop(){
		//balloonAnimator.SetTrigger("BalloonIdle");
		this.gameObject.SetActive (false);
	}

	void ReadyToAnswer(){
		print("ready");
		isReady = true;
	}

}
