using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangementScript : MonoBehaviour {
	public bool ascending, descending;
	public Transform[] snapPoints;
	public string[] playerAnswer;
	public string[] answerArray;
	public int answerCounter;
	public bool dropFlag;
	public int dropCount;

	public Transform draggedObj;
	public Camera cam;
	Vector3 offset;
	RaycastHit hit;

	public Manager manager;

	void Start () {
		manager = GameObject.Find ("Manager").GetComponent<Manager>();
		cam = Camera.main;
		ArrangeInOrder();
		if(ascending)
			dropCount = snapPoints.Length - 1;

		if(descending)
			dropCount = 0;
	}

	void Update () {
		DragObject ();

	}

	public void DragObject()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		if(Input.GetMouseButtonDown(0))
		{
			ArrangeInOrder();
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
					if(hit.collider.tag == "Snap" && dropFlag)
					{
						DropAnswer ();
					} else {
						
						draggedObj.transform.position = draggedObj.gameObject.GetComponent<OriginalPos> ().originalPos;
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
		playerAnswer[dropCount] = draggedObj.gameObject.GetComponentInChildren<TextMesh>().text;
		draggedObj.gameObject.GetComponent<OriginalPos>().indexValue = dropCount;
		draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped = true;
		//draggedObj.gameObject.SetActive (false);
	}

	public void ResetAnswer()
	{
		if(draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped == true)
		{
			draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped = false;
			dropCount = draggedObj.gameObject.GetComponent<OriginalPos>().indexValue;
			playerAnswer[draggedObj.gameObject.GetComponent<OriginalPos>().indexValue] = "";
			draggedObj.gameObject.GetComponent<OriginalPos>().indexValue = 0;
		}
		//draggedObj.gameObject.GetComponent<OriginalPos>().indexValue = 0;
	}

	public void ArrangeInOrder()
	{
		if(ascending)
		{
			for (int i = dropCount; i >= 0; i--)
			{
				if(playerAnswer[i] == "")
				{
					dropFlag = true;
					dropCount = i;
					break;
				} 
				else {
					dropFlag = false;
				}
			}

		}

		if(descending)
		{
			for (int i = 0; i < snapPoints.Length; i++)
			{
				if(playerAnswer[i] == "")
				{
					dropFlag = true;
					dropCount = i;
					break;
				} 
				else {
					dropFlag = false;
				}
			}

		}
		}
}
