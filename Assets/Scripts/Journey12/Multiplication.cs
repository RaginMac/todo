using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplication : MonoBehaviour {

	public TextMesh no1, no2, answer;
	public int no1Number, no2Number, answerNumber;
	public string answerPressed;

	public int dropCount;
	public Transform[] snapPoints;
	public Transform draggedObj, parent;
	public bool dropFlag;
	public Camera cam;
	Vector3 offset;
	RaycastHit hit;

	public string[] finalAnswerNumbers;
	int tempLen;
	public GameObject answerSlot1;
	public GameObject answerSlot2;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		CreateQuestion ();

		if (tempLen > 1)
			ArrangeInOrder ();
	}
	
	// Update is called once per frame
	void Update () {
		DragObject ();
		answerPressed = finalAnswerNumbers [0] + finalAnswerNumbers [1];
	}

	public void CreateQuestion()
	{
		no1Number = Random.Range (1, 3);
		no2Number = Random.Range (1, 9);
		answerNumber = no1Number * no2Number;

		string temp = answerNumber.ToString ();
		tempLen = temp.Length;

		if (tempLen > 1) {
			answerSlot1.SetActive (true);
			answerSlot2.SetActive (true);
		} else {
			answerSlot1.SetActive (true);
		}

		no1.text = no1Number.ToString();
		no2.text = no2Number.ToString (); 
	}

	public void DragObject()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		if(Input.GetMouseButtonDown(0))
		{
			if (tempLen > 1)
				ArrangeInOrder ();
			
			if(!draggedObj)
			{

				if(Physics.Raycast(ray, out hit, Mathf.Infinity)  && hit.collider.tag == "DraggableObject")
				{
					draggedObj = hit.transform;
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
			else {
				dropFlag = false;
				continue;
			}
		}
	}
}
