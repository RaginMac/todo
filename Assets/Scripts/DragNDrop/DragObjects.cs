﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObjects : MonoBehaviour {

	public Manager manager;
	public Question questionScript;
	public Spawn spawnScript;

	public Vector3 offset;
	public Transform draggedObj;
	public Transform[] basketSnapPoints;
	public int snapIndex = 0;
	public GameObject mango;

	public GameObject checkAnswer;

	public int numberOfMangoes = 0;
	public Camera cam;
	RaycastHit hit;
	public GameObject basket;
	public List<Transform> placedMangoes = new List<Transform>(); 

	public bool isText;
	public int isAnswered = 0;

	// Use this for initialization
	void Start ()
	{
		manager = this.GetComponent<Manager> ();
		questionScript = this.GetComponent<Question> ();
		spawnScript = this.GetComponent<Spawn> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!manager.isGameComplete){
			DragObject ();
		}
	}

	public void DragObject()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		if(Input.GetMouseButtonDown(0))
		{
			if(!EventSystem.current.IsPointerOverGameObject()){
				if(!draggedObj)
				{
					if(Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag == "DraggableObject")
					{
						//print ("hit");
						draggedObj = hit.transform;
						hit.transform.GetComponent<BoxCollider>().enabled = false;
						offset = draggedObj.position - ray.origin;
						//draggedObj.GetComponent<Animator> ().SetTrigger ("ShakeMango");
					}
				}
			}
		}

		if(draggedObj) {
			draggedObj.position = new Vector3(ray.origin.x + offset.x, ray.origin.y + offset.y, draggedObj.position.z);
		}

		if(Input.GetMouseButtonUp(0))
		{
			if(draggedObj)
			{
				if(Physics.Raycast(ray, out hit, Mathf.Infinity))
				{
					if(hit.collider.tag != "Basket" || (numberOfMangoes >= 9))
					{
						if (draggedObj.gameObject.GetComponent<ObjectPosition> () != null) {
							draggedObj.position = draggedObj.gameObject.GetComponent<ObjectPosition> ().originalPos;
						//	basket.GetComponent<Animator> ().SetTrigger ("ShakeMango");
							draggedObj.GetComponent<BoxCollider> ().enabled = true;
						}
					}
					else {
						manager.PlayDragDropAudio ();

						draggedObj.position = basketSnapPoints[snapIndex].transform.position;
						//draggedObj.transform.SetParent(basket.transform);
						placedMangoes.Add(draggedObj.transform);
						numberOfMangoes++;
						snapIndex++;
						checkAnswer.SetActive (true);
					
						//print(numberOfMangoes);
					}
				}
			}

			draggedObj = null;
		}
	}

	public void CheckAnswer()
	{
		
		if (numberOfMangoes.ToString () == questionScript.options [Manager.Instance.questionNumber].answerText) {
			if(isAnswered ==0){
				PlayMangoJump();
				manager.CountQuestionsAnswered (true);
				Invoke ("ShowMangoes", 2.5f);
				isAnswered = 1;
			}
		} else {
			basket.GetComponent<Animator> ().SetTrigger ("ShakeMango");
			manager.CountQuestionsAnswered (false);
			spawnScript.ResetMangoes();
			placedMangoes.Clear();
			snapIndex = 0;
			numberOfMangoes = 0;

			if(manager.countWrongAnswer){
				Invoke ("ShowMangoes", 1.5f);
			}
		}


	}

	public void PlayMangoJump(){
	//	print(numberOfMangoes);
		//Animator[] mangoAnims = basket.GetComponentsInChildren<Animator>();

		for (int i = 0; i < placedMangoes.Count; i++) {
			placedMangoes[i].GetComponent<ObjectPosition>().Init();
		}
	}

	public void ShowMangoes(){
		if ((manager.noOfQuestionsAnswered < manager.totalNoOfQuestions)) { 
			isAnswered = 0;
			placedMangoes.Clear();
			spawnScript.SpawnMango ();
			snapIndex = 0;
		}
	}
}


