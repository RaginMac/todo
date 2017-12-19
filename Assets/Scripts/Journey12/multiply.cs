using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class multiply : MonoBehaviour {

	public enum Difficulty {Grade0, Grade1, Grade2, Grade3};
	public Difficulty diff;

	public GameObject eggTrayObj;
	public Text T1, T2;

	public Text[] answerText;

    public int t1Number, t2Number;

	public string finalAnswer, answer;

	public GameObject[] eggTrays;
	public GameObject[] eggs;
	public Transform parent;
	public Transform mainParent;

	public int noOfEggTrays;
	public Vector3 xOffset;
	public Vector3 yOffset;

	public int dropCount;
	//public Transform[] snapPoints;
	public Transform draggedObj;
	Vector3 tempObjPos;
	public bool dropFlag;
	public Camera cam;
	Vector3 offset;
	RaycastHit hit;

	public bool Arrange;
	public Animator anime;
	public Animator moveCalci;

	public bool hintPressed;

	// Use this for initialization
	void Start () {
		cam = Camera.main;

		for (int i = 0; i <= eggs.Length - 1; i++) { 
			eggs [i].SetActive (false);
		}

        CreateQuestion();
        if (Arrange) {
			CreateEggTrayGrid ();
		} else {
			eggTrayObj.SetActive (false);
		}

		if(anime == null)
			anime = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		FinalAnswer ();
		DragObject ();
	}

	public void CreateQuestion()
	{
		if (diff == Difficulty.Grade0) {
			answerText[0].text = "";

			t1Number = Random.Range (1, 4);
			t2Number = Random.Range (1, 10);
			int tempAns = t1Number * t2Number;
			answer = tempAns.ToString ();
            //print(answer.Length);
        }
		else if (diff == Difficulty.Grade1)
		{
//			answerText[0].text = "";
//
//			t1Number = Random.Range (1, 5);
//			int tempNum = (9 / t1Number);
//			t2Number = Random.Range (1, tempNum);
//			int tempAns = t1Number * t2Number;
//			answer = tempAns.ToString ();

			answerText[0].text = "";

			t1Number = Random.Range (1, 6);
			t2Number = Random.Range (1, 10);
			int tempAns = t1Number * t2Number;
			answer = tempAns.ToString ();
		}
		else if (diff == Difficulty.Grade2)
		{
//			answerText[0].text = "";
//			answerText[1].text = "";
//
//			t1Number = Random.Range (6, 49);
//			int tempNum = (99 / t1Number);
//			t2Number = Random.Range (1, tempNum);
//			int tempAns = t1Number * t2Number;
//			answer = tempAns.ToString ();

			answerText[0].text = "";

			t1Number = Random.Range (6, 8);
			t2Number = Random.Range (1, 10);
			int tempAns = t1Number * t2Number;
			answer = tempAns.ToString ();
		}
		else if (diff == Difficulty.Grade3)
		{
//			answerText[0].text = "";
//			answerText[1].text = "";
//			answerText[2].text = "";
//
//			t1Number = Random.Range (1, 499);
//			int tempNum = (999 / t1Number);
//			t2Number = Random.Range (1, tempNum);
//			int tempAns = t1Number * t2Number;
//			answer = tempAns.ToString ();

			answerText[0].text = "";

			t1Number = Random.Range (8, 10);
			t2Number = Random.Range (1, 10);
			int tempAns = t1Number * t2Number;
			answer = tempAns.ToString ();
		}

        T1.text = t1Number.ToString();
		T2.text = t2Number.ToString ();
    }

	public void FinalAnswer()
	{
		finalAnswer = answerText [0].GetComponentInChildren<Text>().text;
	}

	public void CreateEggTrayGrid()
	{
		if (Arrange) {
            int indexNum = 1;
            for (int j = 0; j < t1Number; j++)
            {
                int temp = indexNum;
                for (int k = 0; k < t2Number; k++)
                {
                    eggTrays[temp].GetComponent<BoxCollider>().enabled = true;
                    temp+=10;
                }
                indexNum++;
            }

            xOffset = eggTrays [0].transform.position;
			xOffset.z -= 1.5f;
			for (int i = 1; i < noOfEggTrays; i++) {
				eggTrays [i].gameObject.SetActive (true);
				eggTrays [i].transform.position = xOffset;

				xOffset.x += 0.85f;

				if (i % 10 == 0) {
					xOffset.y -= 0.85f;
					xOffset.x = eggTrays [0].transform.position.x;
				}
			}
		} else if (!Arrange) {

			xOffset = eggTrays [0].transform.position;
			xOffset.z -= 1.5f;
			for (int i = 1; i < noOfEggTrays; i++) {
				eggTrays [i].gameObject.SetActive (true);
				eggTrays [i].transform.position = xOffset;

				xOffset.x += 0.85f;

				if (i % 10 == 0) {
					xOffset.y -= 0.85f;
					xOffset.x = eggTrays [0].transform.position.x;
				}
			}	
		}

		for (int i = 0; i <= t1Number - 1; i++) { 
			eggs [i].SetActive (true);
		}
	}

	public void DragObject()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		if(Input.GetMouseButtonDown(0))
		{
			if(!draggedObj)
			{
				if(Physics.Raycast(ray, out hit, Mathf.Infinity)  && hit.collider.tag == "DraggableObject")
				{
					draggedObj = hit.transform;
					tempObjPos = draggedObj.transform.position;
					hit.transform.GetComponent<BoxCollider>().enabled = false;
					offset = draggedObj.position - ray.origin;
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
					if(hit.collider.tag == "Snap")
					{
						DropEggs ();



					} else {

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


	int iNum = 1; 
	public void DropEggs()
	{
		//draggedObj.gameObject.SetActive (false);
		draggedObj.transform.position = tempObjPos;

		dropCount++;
		if(dropCount <= t2Number) 
		{
			int temp = iNum;
			for (int i = 0; i <= t1Number - 1; i++) {
				eggTrays[temp].transform.GetChild (0).gameObject.SetActive (true);
				temp++;
				//print(temp);
			}
			iNum += 10;
		}

		Manager.Instance.PlayDragDropAudio ();
	}

	public void SettingParent()
	{
		parent = draggedObj;
		draggedObj.transform.parent = null;
		for (int i = 0; i <= t1Number - 1; i++) {
			eggs[i].transform.SetParent (parent);
		}
	}

	public void Reset()
	{
//
//		finalAnswer = "";
//		for (int i = 0; i < eggTrays.Length; i++) {
//			eggTrays[i].transform.GetChild (0).gameObject.SetActive (false);
//		}
//		dropCount = 0;
//		iNum = 1;
//		int i = -1;
//		for (i = 0; i < answerText.Length; i++) {
//			answerText [i].GetComponentInChildren<Text>().text = "";
//		}
	}

}
