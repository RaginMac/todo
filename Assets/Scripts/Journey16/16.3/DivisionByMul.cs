using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DivisionByMul : MonoBehaviour {

	public int firstNum;
	public int secondNum;
	public int answer;
	public int playerAnswer;
	public int basketIndex;

	public int noOfEggTrays;
	public int columns;
	private Vector3 xOffset;

	public List<int> randomDividends;
	public GameObject[] eggTrays;
	public GameObject[] eggs;
	public GameObject[] baskets;
	public GameObject[] movePoints;

	public Text n1;
	public Text n2;
	public Text answerText;

	public Transform parent;
	public Transform mainParent;
	public Transform draggedObj;
	public GameObject keypad;

	public Camera cam;
	Vector3 offset;
	RaycastHit hit;

	void Start() {
		cam = Camera.main;

		firstNum = GetDividend (randomDividends);
		secondNum = GetDivisor ();
		answer = firstNum / secondNum;

		CreateEggTrayGrid ();
	}

	void Update()
	{
		DragObject ();

	}

	int FindRandomNumber(int n1, int n2) 
	{
		int num = Random.Range (n1, n2);
		return num;
	}

	int GetDividend(List<int> myList) 
	{
		int val = FindRandomNumber (10, 19);

		while(myList.Contains(val)) {
			val = FindRandomNumber(10, 19);
		}

		n1.text = val.ToString (); 
		return val;
	}

	int GetDivisor() 
	{
		int val = FindRandomNumber (2, 11);
		while (firstNum % val != 0) {
			val = FindRandomNumber (2, 11);
		}

		n2.text = val.ToString (); 
		return val;
	}

	public void CreateEggTrayGrid()
	{
		xOffset = eggTrays [0].transform.position;
		xOffset.z -= 1.5f;
		for (int i = 1; i <= noOfEggTrays; i++) {
			eggTrays [i].gameObject.SetActive (true);
			eggTrays [i].transform.position = xOffset;

			xOffset.x += 0.85f;

			if (i % columns == 0) {
				xOffset.y -= 0.85f;
				xOffset.x = eggTrays [0].transform.position.x;
			}
		}

		DisplayEggs ();
		StartCoroutine (DisplayBaskets ());
	}

	void DisplayEggs()
	{
		int indexNum = 1;
		int tempRowValue = 1;
		for (int j = 0; j < secondNum; j++)
		{
			int temp = indexNum;
			for (int k = 0; k < answer; k++)
			{
//				eggTrays[temp].transform.GetChild(0).gameObject.SetActive(true);
//				eggTrays [temp].transform.GetChild (0).GetComponent<BoxCollider> ().enabled = true;
//				eggTrays [temp].transform.GetChild (0).GetComponent<RowValue> ().row = tempRowValue;
				Vector3 tempPos = eggTrays[temp].transform.position;
				tempPos.z -= 0.5f;
				eggs[temp].transform.position = tempPos;
				eggs[temp].GetComponent<RowValue> ().row = tempRowValue;
				//eggs[temp].GetComponent<RowValue> ().indexValue = tempIndexValue;
				eggs[temp].SetActive(true);
				temp += 10;
				tempRowValue++;
			}

			indexNum++;
			tempRowValue = 1;
		}
	}

	IEnumerator DisplayBaskets()
	{
		
		yield return new WaitForSeconds (1);
		baskets [basketIndex].SetActive (true);
	}

	void DropEggs(GameObject hitObject)
	{
		hitObject.GetComponent<BasketScript> ().ShowEggsInBasket (secondNum);
		hitObject.GetComponent<BasketScript> ().temp = basketIndex;
		hitObject.GetComponent<BasketScript> ().moveBasket = true;
		parent.transform.gameObject.SetActive(false);
		basketIndex++;
	}

	public void DragObject()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		if(Input.GetMouseButtonDown(0))
		{
			if(draggedObj == null)
			{
				if(Physics.Raycast(ray, out hit, Mathf.Infinity)  && hit.collider.tag == "DraggableObject")
				{
					draggedObj = hit.transform;
					draggedObj.transform.GetComponent<RowValue>().originalPos = draggedObj.transform.position;
					draggedObj.transform.GetComponent<BoxCollider>().enabled = false;
					offset = draggedObj.position - ray.origin;
					SettingParent ();
				}
			}
		}

		if(draggedObj != null) {
			draggedObj.position = new Vector3(ray.origin.x + offset.x, ray.origin.y + offset.y, draggedObj.position.z);
		}

		if(Input.GetMouseButtonUp(0))
		{
			if(draggedObj)
			{
				if (Physics.Raycast (ray, out hit, Mathf.Infinity))
				{
					if(hit.collider.tag == "Snap" && basketIndex <= answer - 1)
					{
						DropEggs (hit.collider.gameObject);
						StartCoroutine (DisplayBaskets ());
					} else {
						draggedObj.transform.position = draggedObj.transform.GetComponent<RowValue> ().originalPos;
					}
				}

				draggedObj.transform.GetComponent<BoxCollider>().enabled = true;
				parent.transform.SetParent (mainParent);
			}

			draggedObj = null;
			parent = null;
		}
	}

	public void SettingParent()
	{
		int tempIndex = draggedObj.GetComponent<RowValue> ().startRowIndex;
		parent = draggedObj;
		draggedObj.transform.parent = null;

		for (int i = 0; i < secondNum; i++) {
			eggs[tempIndex].transform.SetParent (parent);
			tempIndex++;
		}
	}

	public void ResetKeypad()
	{
		keypad.GetComponent<Animator>().SetBool("KeypadShow", false);
	}
}
