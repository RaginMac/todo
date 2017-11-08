﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetQuestionNumber : MonoBehaviour {

	public bool isDragQuestion;
	public Text number1, number2;
	public TextMesh num1, num2;
	public int n1, n2;
	public GameObject[] appleSet1, appleSet2;

	public enum QuestionType {Image, Text, TrueFalse};
	public QuestionType qType;

	public bool isEqual;
	public Animator crocAnime;
	//public Transform snap;

	public GameObject draggedObject;
	private Vector3 originalObjPos;
	private Vector3 offset;
	public Camera cam;
	public Transform options, parent;
	public string snappedObjName;

	public SpriteRenderer imgRenderer;
	public AudioSource crocodileAudio;
	public AudioClip greaterOrless;
	//public Sprite greater_old, greater_new;

	public int answered = 0;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		crocAnime = this.transform.GetChild (0).GetComponent<Animator>();

		if (qType == QuestionType.Text) {
			AssignNumbers ();
		} else if (qType == QuestionType.Image) {
			SetAppleCount ();
		}

		if(crocodileAudio!=null){
			
			crocodileAudio.clip = greaterOrless;
			crocodileAudio.Play();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(isDragQuestion)
		{
			DragObject2D();
		}
	}

	public void AssignNumbers()
	{
		n1 = Random.Range (1, 8);
		n2 = Random.Range (1, 9);
		
		num1.text = n1.ToString ();
		num2.text = n2.ToString ();
	}
	
	public void DragObject2D()
	{
		Ray ray = cam.ScreenPointToRay (Input.mousePosition);

		if (Input.GetMouseButtonDown (0))
		{
			if (!draggedObject)
			{
				RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 100f);
				if (hit != null && hit.collider.tag == "DraggableObject")
				{
					draggedObject = hit.transform.gameObject;
					imgRenderer = draggedObject.transform.GetComponent<SpriteRenderer>();
					imgRenderer.sprite = draggedObject.transform.GetComponent<SetSprites>().newSprite;
					//originalObjPos = draggedObject.transform.position;
					draggedObject.GetComponent<BoxCollider2D> ().enabled = false;
					offset = draggedObject.transform.position - ray.origin;


					//parent = draggedObject.transform;
					//draggedObject.transform.parent = null;

					//CheckBlockSet ();
				}
			}
		}

		if(draggedObject) {
			draggedObject.transform.position = new Vector3(ray.origin.x + offset.x, ray.origin.y + offset.y, draggedObject.transform.position.z);
		}

		if (Input.GetMouseButtonUp (0))
		{
			if (draggedObject != null) {
				imgRenderer = draggedObject.transform.GetComponent<SpriteRenderer>();
				imgRenderer.sprite = draggedObject.transform.GetComponent<SetSprites>().oldSprite;
				RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 100f);
				if (draggedObject && hit.collider.tag != "Snap") {
//					draggedObject.transform.position = originalObjPos;
					draggedObject.transform.position = draggedObject.GetComponent<OriginalPos>().originalPos;
					draggedObject.transform.SetParent(options.transform);
				} else {
					
					draggedObject.transform.SetParent(hit.transform);
					DropAnswer (hit.collider.transform);
				}
				draggedObject.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
				draggedObject = null;
				//parent.transform.SetParent (mainParent);
				//parent = null;
			}
		}
	}
	
	
	
	public void SetAppleCount()
	{
		n1 = Random.Range (1, 8);
		n2 = Random.Range (1, 9);

		if (!isEqual) {
			if (n1 == n2) {
				n2 = n1 + 1;
			}
		} else if (isEqual) {
			n2 = n1;
		}

		for (int i = 0; i < n1; i++) {
			appleSet1 [i].SetActive (true);
		}
		for (int j = 0; j < n2; j++) {
			appleSet2 [j].SetActive (true);
		}
	}
	
	public void DropAnswer(Transform snapCol){
		draggedObject.transform.position = snapCol.position;
		//snappedObjName = objName;
	}

//	public void CheckDraggedAnswer(){
//		if(parent.transform.childCount>0){
//			Manager.Instance.CompareDraggedImage(parent.GetChild(0).gameObject.name);
//		}else{
//			Manager.Instance.CompareDraggedImage("wrongAnswer");
//		}
//	}

}
