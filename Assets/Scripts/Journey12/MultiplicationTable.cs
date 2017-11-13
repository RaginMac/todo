using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplicationTable : MonoBehaviour {
	public string[] answerArray;
	//public TextMesh[] answerTextArray;
	public string[] playerAnswerArray;
	public TextMesh[] q1;
	public TextMesh[] q2;
	public TextMesh[] q3;
	public TextMesh[] q4;
	public TextMesh[] q5;
	public bool[] filledAns;


	public GameObject[] eggTrays;
	public GameObject[] eggs;
	public int noOfEggTrays;
	public Vector3 xOffset;
	public Vector3 yOffset;

	public int dropCount;
	public Transform draggedObj;
	Vector3 tempObjPos;
	public bool dropFlag;
	public Camera cam;
	Vector3 offset;
	RaycastHit hit;

	public int num1;
	public int maxNum2;
	public Transform parent;
	public Transform mainParent;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		filledAns = new bool[5];
		FinalAnswer ();
		CreateEggTrayGrid ();
	}
	
	// Update is called once per frame
	void Update () {
		DragObject ();
		FinalAnswer ();
	}

	public void CreateEggTrayGrid()
	{

		int indexNum = 1;
		for (int j = 0; j < num1; j++)
		{
			int temp = indexNum;
			for (int k = 0; k < maxNum2; k++)
			{
				eggTrays[temp].GetComponent<BoxCollider>().enabled = true;
				temp+=10;
			}
			indexNum++;
		}

		xOffset = eggTrays[0].transform.position;
		xOffset.z -= 1.5f;
		for (int i = 1; i < noOfEggTrays; i++) {
			eggTrays [i].gameObject.SetActive (true);
			eggTrays [i].transform.position = xOffset;

			xOffset.x += 0.8f;

			if (i % 10 == 0) {
				xOffset.y -= 0.8f;
				xOffset.x = eggTrays[0].transform.position.x;
			}
		}

		for (int i = 0; i <= num1 - 1; i++) { 
			eggs [i].SetActive (true);
		}

		for (int i = 0; i < playerAnswerArray.Length; i++) {
			if (playerAnswerArray [i] == "") {
				filledAns [i] = false;
			} else {
				filledAns [i] = true;
			}
		}
	}

	public void DragObject()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		if(Input.GetMouseButtonDown(0))
		{
			if(!draggedObj)
			{
				if(Physics.Raycast(ray, out hit, Mathf.Infinity)  && (hit.collider.tag == "DraggableObject" || hit.collider.tag == "Eggs") )
				{
					draggedObj = hit.transform;
					tempObjPos = draggedObj.transform.position;
					hit.transform.GetComponent<BoxCollider>().enabled = false;
					offset = draggedObj.position - ray.origin;

					if(hit.collider.tag == "Eggs")
						SettingParent ();
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
					if(hit.collider.tag == "EggTray" && draggedObj.tag == "Eggs")
					{
						DropEggs ();
					} 
					else if(hit.collider.tag == "Snap" && draggedObj.tag == "DraggableObject") {
						//print (hit.collider.name);
						DropAnswer (hit.collider.gameObject);
					}
					else {
						//draggedObj.transform.position = draggedObj.gameObject.GetComponent<OriginalPos> ().originalPos;
						draggedObj.transform.position = tempObjPos;
						//draggedObj.transform.SetParent (parent);
					}

				}
			}

			if (draggedObj != null) {
				draggedObj.gameObject.GetComponent<BoxCollider> ().enabled = true;
			}

			draggedObj = null;
			parent = null;
			for (int i = 0; i <= eggs.Length - 1; i++) {
				eggs[i].transform.SetParent (mainParent);
			}
		}
	}

//	public void DropEggs(GameObject other)
//	{
//		//draggedObj.gameObject.SetActive (false);
//		other.transform.GetChild (0).gameObject.SetActive (true);
//		other.GetComponent<BoxCollider> ().enabled = false;
//		draggedObj.transform.position = tempObjPos;
//		//print(other.gameObject.name);
//	}

	int iNum = 1; 
	public void DropEggs()
	{
		//draggedObj.gameObject.SetActive (false);
		draggedObj.transform.position = tempObjPos;

		dropCount++;
		if(dropCount <= maxNum2) 
		{
			int temp = iNum;
			for (int i = 0; i <= num1 - 1; i++) {
				eggTrays[temp].transform.GetChild (0).gameObject.SetActive (true);
				temp++;
				//print(temp);
			}
			iNum += 10;
		}
	}

	public void DropAnswer(GameObject other)
	{
		
		draggedObj.transform.position = draggedObj.gameObject.GetComponent<OriginalPos> ().originalPos;
		other.GetComponentInChildren<TextMesh>().text = draggedObj.GetComponentInChildren<TextMesh> ().text;


	}

	public void FinalAnswer()
	{
		playerAnswerArray[0] = q1[0].text + q1[1].text;
		playerAnswerArray[1] = q2[0].text + q2[1].text;
		playerAnswerArray[2] = q3[0].text + q3[1].text;
		playerAnswerArray[3] = q4[0].text + q4[1].text;
		playerAnswerArray[4] = q5[0].text + q5[1].text;
	}

	public void SettingParent()
	{
		parent = draggedObj;
		draggedObj.transform.parent = null;
		for (int i = 0; i <= num1 - 1; i++) {
			eggs[i].transform.SetParent (parent);
		}
	}

	public void Reset()
	{
		for (int i = 0; i < eggTrays.Length; i++) {
			eggTrays[i].transform.GetChild (0).gameObject.SetActive (false);
		}

//		for (int j = 0; j < q1.Length; j++) {
//			q1[j].GetComponent<TextMesh>().text = "";
//			q2[j].GetComponent<TextMesh>().text = "";
//			q3[j].GetComponent<TextMesh>().text = "";
//			q4[j].GetComponent<TextMesh>().text = "";
//			q5[j].GetComponent<TextMesh>().text = "";
//
//		}

		for (int j = 0; j < q1.Length; j++) {
			if(!filledAns[0])
				q1[j].GetComponent<TextMesh>().text = "";

			if(!filledAns[1])
				q2[j].GetComponent<TextMesh>().text = "";

			if(!filledAns[2])
				q3[j].GetComponent<TextMesh>().text = "";

			if(!filledAns[3])
				q4[j].GetComponent<TextMesh>().text = "";

			if(!filledAns[4])
				q5[j].GetComponent<TextMesh>().text = "";
		}
	}
}
