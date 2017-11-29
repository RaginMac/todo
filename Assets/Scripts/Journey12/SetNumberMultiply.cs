using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNumberMultiply : MonoBehaviour {

	public enum Difficulty {Grade1, Grade2, Grade3};
	public Difficulty diff;

	public TextMesh no1, no2, answer;
	public int no1Number, no2Number, tempNum, answerNumber;
	public string answerPressed;

	public int dropCount;
	public Transform[] snapPoints;
	public Transform draggedObj, parent;
	public bool dropFlag;
	public Camera cam;
	Vector3 offset;
	RaycastHit hit;


	public string[] finalAnswerNumbers;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		CreateQuestion ();
		ArrangeInOrder ();
	}
	
	// Update is called once per frame
	void Update () {
		DragObject ();
		FinalAnswer ();
	}

	public void CreateQuestion()
	{
		if (diff == Difficulty.Grade1)
		{
			no1Number = Random.Range (1, 5);
			tempNum = (9 / no1Number);
			no2Number = Random.Range (1, tempNum);
			answerNumber = no1Number * no2Number;

		} else if (diff == Difficulty.Grade2) {
			no1Number = Random.Range (10, 49);
			tempNum = (99 / no1Number);
			no2Number = Random.Range (1, tempNum);
			answerNumber = no1Number * no2Number;
					
		} else if(diff == Difficulty.Grade3) {
			no1Number = Random.Range (10, 499);
			tempNum = (999 / no1Number);
			no2Number = Random.Range (1, tempNum);
			answerNumber = no1Number * no2Number;	
		}	
			
		no1.text = no1Number.ToString();
		no2.text = no2Number.ToString (); 
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
					draggedObj = hit.transform;
					//hit.transform.GetComponent<AudioSource> ().Play ();
					hit.transform.GetComponent<BoxCollider>().enabled = false;
					offset = draggedObj.position - ray.origin;
					ResetAnswer();
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
				if (Physics.Raycast (ray, out hit, Mathf.Infinity))
				{
					if(hit.collider.tag == "Snap")
					{
						draggedObj.transform.SetParent(hit.collider.transform);
						DropAnswer ();

					} else {

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

	public void DropAnswer ()
	{
		Vector3 temp = snapPoints[dropCount].transform.position;
		temp.z -= 1f;
		draggedObj.transform.position = temp;

		draggedObj.gameObject.GetComponent<OriginalPos>().indexValue = dropCount;
		finalAnswerNumbers[draggedObj.gameObject.GetComponent<OriginalPos>().indexValue] = draggedObj.gameObject.GetComponentInChildren<TextMesh>().text;
		draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped = true;
	}

	public void ResetAnswer()
	{
		if (draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped == true)
		{
			draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped = false;
			finalAnswerNumbers[draggedObj.gameObject.GetComponent<OriginalPos>().indexValue] = "";
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
	}

	public void FinalAnswer()
	{
		if (diff == Difficulty.Grade1) {
			answerPressed = finalAnswerNumbers [0];
		} else if (diff == Difficulty.Grade2) {
			answerPressed = finalAnswerNumbers [0] + finalAnswerNumbers [1];

		} else if(diff == Difficulty.Grade3) {
			answerPressed = finalAnswerNumbers [0] + finalAnswerNumbers [1] + finalAnswerNumbers [2];
		}	
	}
}
