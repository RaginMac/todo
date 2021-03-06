﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BubblePopper : MonoBehaviour {

	public GameObject childBubble;
	public Rigidbody2D rigidbodyComponent;

	public Manager manager;
	public BubbleManager bubbleManager;

	public Vector2 forceDirection;
	public float forceMultiplier;
	public float[] forceMagnitude;

	public int valueToHold;
	public Vector2 originalPos;
	public bool isBubblePopped = false;
	public Animator anim;
	public AudioSource source;
	private bool isAnswered = false;

	private bool IsPointerOverUIObject()
	{
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

		return results.Count>0;
	}

	void Awake()
	{
		manager = GameObject.Find ("Manager").GetComponent<Manager>();
		bubbleManager = GameObject.Find ("Bubbles").GetComponent<BubbleManager> ();
		rigidbodyComponent = this.GetComponent<Rigidbody2D> ();
	}
	// Use this for initialization
	void Start () {
		
		ApplyForce ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		if(!Manager.Instance.isGameComplete)
		{
			if (isBubblePopped && !isAnswered) {
				string validity = manager.ValidateAnswerDirectly (this.GetComponent<TextMesh> ().text);
				PopTheBubble (validity);
				source.Play();
			//	isBubblePopped = false;
			
			}
		}
	}

	public void PopTheBubble(string isCorrect)
	{
		if (isCorrect == "wrong") {
			manager.PlayWrongSound();
			this.GetComponent<Animator> ().SetTrigger ("ShakeBubble");
			if(manager.countWrongAnswer){
				Invoke ("ShowNext", 1f);
			}
		} else {
			anim.SetTrigger("PopCorrect");
//			this.GetComponentInChildren<Animator> ().SetTrigger ("PopCorrect");
			Invoke("Fall", 0.7f);
			Invoke ("ShowNext", 1f);
			isAnswered = true;
		}
	}

	public void ApplyForce ()
	{
		isBubblePopped = true;
		forceDirection = new Vector2 (forceMagnitude [Random.Range (0, 2)], forceMagnitude [Random.Range (0, 2)]);
		this.GetComponent<Rigidbody2D>().AddForce(forceDirection * forceMultiplier, ForceMode2D.Force);
	}

	public void PopulateBubbles()
	{
		this.GetComponent<TextMesh>().text = valueToHold.ToString();
	}

	public void ShowNext()
	{
		if (manager.noOfQuestionsAnswered < manager.totalNoOfQuestions) {
			//Question.Instance.ShuffleOptions ();
			bubbleManager.ShowQuestion ();
		}
	}

	public void Fall()
	{
		this.transform.GetChild(0).gameObject.SetActive(false);
		rigidbodyComponent.gravityScale = 6.5f;
		this.GetComponent<BoxCollider2D> ().enabled = false;
	}
}
