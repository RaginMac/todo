using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSubFromAdd : MonoBehaviour {

	public enum Difficulty {Grade1, Grade2, Grade3};
	public Difficulty diff;

	public TextMesh addNo1, addNo2, subNo1, addAnswer, subAnswer;
	public int noOneNumber, noTwoNumber, answerNumber; 


	public int dropCount, dropCount2, dropCount3;
	public Transform[] snapPoints, snapPoints2, snapPoints3;
	public Transform draggedObj, parent;
	public bool dropFlag;
	public Camera cam;
	Vector3 offset;
	RaycastHit hit;

	public string answerPressed, answerPressed2, answerPressed3;
	public string[] finalAnswerNumbers, finalAnswerNumbers2, finalAnswerNumbers3;

	public SetBubbles balloonSpawner;
	public bool isAnswered = false;

	void Awake(){
		CreateQuestion ();
		ArrangeInOrder ();
	}


	// Use this for initialization
	void Start () {
		cam = Camera.main;
		balloonSpawner = this.GetComponentInChildren<SetBubbles>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!Manager.Instance.isGameComplete)
		{
			DragObject ();
		}
		FinalAnswer ();
	}

	public void CreateQuestion(){
		answerNumber = Random.Range (1, 9);
		noOneNumber = Random.Range (0, answerNumber - 1);
		noTwoNumber = answerNumber - noOneNumber;
//		if (diff == Difficulty.Grade1) {
//			answerNumber = Random.Range (1, 9);
//			noOneNumber = Random.Range (0, answerNumber - 1);
//			noTwoNumber = answerNumber - noOneNumber;
//		}
//		else if (diff == Difficulty.Grade2) {
//			answerNumber = Random.Range (30, 99);
//			noOneNumber = Random.Range (20, answerNumber-10);
//			noTwoNumber = answerNumber - noOneNumber;
//		}
//		else if (diff == Difficulty.Grade3) {
//			answerNumber = Random.Range (300, 999);
//			noOneNumber = Random.Range (100, answerNumber-100);
//			noTwoNumber = answerNumber - noOneNumber;
//		}

		addNo1.text = noOneNumber.ToString ();
		addNo2.text = noTwoNumber.ToString ();
		addAnswer.text = answerNumber.ToString ();
		subNo1.text = answerNumber.ToString ();
		subAnswer.text = noTwoNumber.ToString ();
	}

	public void DragObject()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		if(Input.GetMouseButtonDown(0))
		{
			ArrangeInOrder ();
			//print ("pressing");
			if(!draggedObj)
			{
				if(Physics.Raycast(ray, out hit, Mathf.Infinity)  && hit.collider.tag == "DraggableObject")
				{
					//print (hit.collider.tag);
					//ResetAnswerText();
					//	hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = buttonPressedSprite;
					draggedObj = hit.transform;
					//hit.transform.GetComponent<AudioSource> ().Play ();
					hit.transform.GetComponent<BoxCollider>().enabled = false;
					offset = draggedObj.position - ray.origin;
					ResetAnswer ();
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
				//print ("start to drop");
				if (Physics.Raycast (ray, out hit, Mathf.Infinity))
				{
					//	print ("casting" + hit.collider.tag);
					if (hit.collider.tag == "Snap") {
						draggedObj.transform.SetParent (hit.collider.transform);
						DropAnswer (finalAnswerNumbers, snapPoints, dropCount);

					}
					else if (hit.collider.tag == "Snap2") 
					{
						draggedObj.transform.SetParent (hit.collider.transform);
						DropAnswer (finalAnswerNumbers2, snapPoints2, dropCount2);
					}
					else if (hit.collider.tag == "Snap3")
					{
						draggedObj.transform.SetParent (hit.collider.transform);
						DropAnswer (finalAnswerNumbers3, snapPoints3, dropCount3);
					}

					else {
						draggedObj.transform.position = draggedObj.gameObject.GetComponent<OriginalPos> ().originalPos;
						draggedObj.transform.SetParent (parent);
					}
				}
			}

			if (draggedObj != null) {
				draggedObj.gameObject.GetComponent<BoxCollider> ().enabled = true;
			}

			draggedObj = null;
		}
	}

	public void DropAnswer (string[] answerArray, Transform[] snap, int count)
	{
		Vector3 temp = snap[count].transform.position;
		temp.z -= 1f;
		draggedObj.transform.position = temp;

		draggedObj.gameObject.GetComponent<OriginalPos>().indexValue = count;
//		finalAnswerNumbers[draggedObj.gameObject.GetComponent<OriginalPos>().indexValue] = draggedObj.gameObject.GetComponentInChildren<TextMesh>().text;
		answerArray[draggedObj.gameObject.GetComponent<OriginalPos>().indexValue] = draggedObj.gameObject.GetComponentInChildren<TextMesh>().text;

		draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped = true;
		//FinalAnswer ();

		Manager.Instance.PlayDragDropAudio ();

	}

	public void ResetAnswer()
	{
		if (draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped == true)
		{
			draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped = false;

			if(diff==Difficulty.Grade1) {
				finalAnswerNumbers[draggedObj.gameObject.GetComponent<OriginalPos>().indexValue] = "";
			}
//			else if (diff == Difficulty.Grade2) {
//				if ((draggedObj.parent.name == "Snap (2)" || draggedObj.parent.name == "Snap (3)")) {
//					finalAnswerNumbers2 [draggedObj.gameObject.GetComponent<OriginalPos> ().indexValue] = "";
//				}
//				else  {
//					finalAnswerNumbers[draggedObj.gameObject.GetComponent<OriginalPos>().indexValue] = "";
//				}
//			}
//		
//			else if(diff == Difficulty.Grade3) {
//				if ((draggedObj.parent.name == "Snap")) {
//					finalAnswerNumbers2 [draggedObj.gameObject.GetComponent<OriginalPos> ().indexValue] = "";
//				} else if ((draggedObj.parent.name == "Snap (2)")) {
//					finalAnswerNumbers [draggedObj.gameObject.GetComponent<OriginalPos> ().indexValue] = "";
//				} else {
//					finalAnswerNumbers3 [draggedObj.gameObject.GetComponent<OriginalPos> ().indexValue] = "";
//				}
//			}
//
			draggedObj.gameObject.GetComponent<OriginalPos>().indexValue = 0;
		}

	}

	public void ArrangeInOrder()
	{
		for (int i = snapPoints.Length - 1; i >= 0; i--)
		{
			if (finalAnswerNumbers[i] == "")
			{
				dropFlag = true;
				dropCount = i;
				break;
			}
			else
			{
				dropFlag = false;
				continue;
			}
		}
		for (int i = snapPoints2.Length - 1; i >= 0; i--)
		{
			if (finalAnswerNumbers2[i] == "")
			{
				dropFlag = true;
				dropCount2 = i;
				break;
			}
			else
			{
				dropFlag = false;
				continue;
			}
		}
		for (int i = snapPoints3.Length - 1; i >= 0; i--)
		{
			if (finalAnswerNumbers3[i] == "")
			{
				dropFlag = true;
				dropCount3 = i;
				break;
			}
			else
			{
				dropFlag = false;
				continue;
			}
		}
	}

	public void FinalAnswer()
	{
		if (diff == Difficulty.Grade1) {
			answerPressed = finalAnswerNumbers [0];

		} else if (diff == Difficulty.Grade2) {
			answerPressed = finalAnswerNumbers [0];
			answerPressed2 = finalAnswerNumbers2 [0];

		} else if(diff == Difficulty.Grade3) {
		//	print (finalAnswerNumbers [0] + finalAnswerNumbers [1] + finalAnswerNumbers [2]);
			answerPressed = finalAnswerNumbers [0];
			answerPressed2 = finalAnswerNumbers2 [0];
			answerPressed3 = finalAnswerNumbers3 [0];
		}	
	}

	public void ResetBalloonPositions(){
		//print("Reset");
		balloonSpawner.ResetBalloons();
	}
}
