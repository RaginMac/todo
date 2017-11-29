using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterOrLesser_2 : MonoBehaviour {

	public string[] ratNumbers, elephantNumbers;
	public Transform[] snapPoints;
	public GameObject options1, options2, options3;

	public TextMesh number1, number2, number3, number4, number5, number6;
	public int n1, n2, n3;
	public List<int> numbers = new List<int>();

	public string[] finalAnswerNumbers;
	public string finalRatNumber;
	public string smallestNum;
	public string finalElephantNumber;
	public string biggestNum;

	public bool isEqual;
	//public bool ratNumbersFilled;

	public Transform draggedObj;
	public Camera cam;
	Vector3 offset;
	RaycastHit hit;
	public bool dropFlag;
	public int dropCount;

	public Manager manager;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find("Manager").GetComponent<Manager>();
		cam = Camera.main;
		AssignNumbers();
		ArrangeInOrder();
		FindSmallestNBiggest ();
	}

	// Update is called once per frame
	void Update () {
		DragObject();

		if (finalAnswerNumbers[2] != "")
		{
			options1.GetComponent<BoxCollider>().enabled = true;
			options2.GetComponent<BoxCollider>().enabled = true;
			options3.GetComponent<BoxCollider>().enabled = true;
		}
		else if(finalAnswerNumbers[3] == "") {
			options1.GetComponent<BoxCollider>().enabled = false;
			options2.GetComponent<BoxCollider>().enabled = false;
			options3.GetComponent<BoxCollider>().enabled = false;
		}

		FindFinalAnswers ();
	}

	public void DragObject()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		if (Input.GetMouseButtonDown(0))
		{
			if (!draggedObj)
			{
				ArrangeInOrder();
				if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag == "DraggableObject")
				{
					draggedObj = hit.transform;
					hit.transform.GetComponent<BoxCollider>().enabled = false;
					offset = draggedObj.position - ray.origin;
					ResetAnswer();
				}
			}
		}

		if (draggedObj)
		{
			draggedObj.position = new Vector3(ray.origin.x + offset.x, ray.origin.y + offset.y, draggedObj.position.z);
		}

		if (Input.GetMouseButtonUp(0))
		{
			if (draggedObj)
			{
				if (Physics.Raycast(ray, out hit, Mathf.Infinity))
				{
					if (hit.collider.tag == "Snap" && dropFlag && !draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped)
					{
						DropAnswer();
					}
					else
					{
						draggedObj.transform.position = draggedObj.gameObject.GetComponent<OriginalPos>().originalPos;
					}
				}
			}

			if (draggedObj != null)
			{
				draggedObj.gameObject.GetComponent<BoxCollider>().enabled = true;
			}

			draggedObj = null;
		}
	}

	public void ArrangeInOrder()
	{
		//if (ascending)
		{
			for (int i = 0; i <= snapPoints.Length - 1; i++)
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
	}

	public void DropAnswer()
	{
		Vector3 temp = snapPoints[dropCount].transform.position;
		temp.z -= 1f;
		draggedObj.transform.position = temp;
		draggedObj.gameObject.GetComponent<OriginalPos>().indexValue = dropCount;
		finalAnswerNumbers[draggedObj.gameObject.GetComponent<OriginalPos>().indexValue] = draggedObj.gameObject.GetComponentInChildren<TextMesh>().text;
		draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped = true;
		//draggedObj.gameObject.SetActive (false);
	}

	public void ResetAnswer()
	{
		if (draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped == true)
		{
			draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped = false;
			//dropCount = draggedObj.gameObject.GetComponent<OriginalPos>().indexValue;
			finalAnswerNumbers[draggedObj.gameObject.GetComponent<OriginalPos>().indexValue] = "";
			draggedObj.gameObject.GetComponent<OriginalPos>().indexValue = 0;
		}
		//draggedObj.gameObject.GetComponent<OriginalPos>().indexValue = 0;
	}

	public void AssignNumbers()
	{
		n1 = Random.Range(1, 8);
		numbers.Add(n1);
		n2 = Random.Range(1, 8);
		numbers.Add(n2);
		n3 = Random.Range(1, 9);
		numbers.Add(n3);

//		if (!isEqual)
//		{
//			if (n1 == n2)
//			{
//				n2 = n1 + 1;
//			}
//
//			if (n2 == n3)
//			{
//				n2 = n3 + 1;
//			}
//
//			if (n1 == n3)
//			{
//				n1 = n3 + 1;
//			}
//		}
//		else if (isEqual)
//		{
//			n2 = n1;
//			n2 = n3;
//			n3 = n1;
//		}

		number1.text = n1.ToString();
		number2.text = n2.ToString();
		number3.text = n3.ToString();
		number4.text = n1.ToString();
		number5.text = n2.ToString();
		number6.text = n3.ToString();
	}

	public void FindFinalAnswers()
	{
		finalRatNumber = finalAnswerNumbers[0] + finalAnswerNumbers[1] + finalAnswerNumbers[2];
		finalElephantNumber =  finalAnswerNumbers[3] + finalAnswerNumbers[4] + finalAnswerNumbers[5];                                                                                                                      
	}

	public void FindSmallestNBiggest()
	{

		numbers.Sort();
		for (int i = 0; i < numbers.Count; i++) {
			
			print (numbers[i]);
		}

		smallestNum = numbers [0].ToString() + numbers [1].ToString() + numbers [2].ToString() ;
		biggestNum = numbers [2].ToString()  + numbers [1].ToString()  + numbers [0].ToString() ;


	}
}
