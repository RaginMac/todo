using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapNumbers : MonoBehaviour {

	public enum Difficulty {Grade1, Grade2, Grade3};
	public Difficulty diff;

	public Manager manager;

	public Animator animator;

	public GameObject[] caterpillarBodies;
	public GameObject[] optionSpawn;
	public Transform[] snapPoints;
	public string[] answerArray;
	public string[] newAnswerArray;

	public List<int> usedRandomNumbers = new List<int>();

	public int answerCounter;

	public Transform draggedObj;
	public Camera cam;
	Vector3 offset;
	RaycastHit hit;
	public bool isSwap;

	public int noOfOptions;
	public int newAnswerArrayIndex;
	public bool dropFlag;
	public bool arrangeInOrder;
	public int textValue;
	public int previousSnapIndex;

	public bool ansClicked;

	public int firstNumber;

	void Start () {
		manager = GameObject.Find ("Manager").GetComponent<Manager>();
		cam = Camera.main;

		ShuffleOptionsPositions ();
		CreateQuestions ();

		animator.SetTrigger ("Missing");

	}

	void Update () {
		
		if(!manager.isGameComplete)
			DragObject ();

//		if (arrangeInOrder)
//			ArrangeInOrder ();
//		else
//			Arrange();

	}

	public int UniqueRandomInt(int min, int max)
	{
		int val = Random.Range (min, max);

		while (usedRandomNumbers.Contains (val)) {
			val = Random.Range (min, max);
		}

		usedRandomNumbers.Add (val);
		return val;
	}

	public void CreateQuestions()
	{
		if (diff == Difficulty.Grade1) {
			firstNumber = Random.Range (1, 15);
			answerArray = new string[noOfOptions];

			for (int i = 0; i <= noOfOptions - 1; i++) {
				answerArray [i] = firstNumber.ToString ();
				snapPoints [i].GetComponent<SnapIndexValue> ().indexValue = i;

				Vector3 tempPosition = optionSpawn [i].transform.position;
				tempPosition.z -= 0.001f;
				caterpillarBodies [i].transform.position = tempPosition;

				caterpillarBodies [i].GetComponent<OriginalPos> ().indexValue = i;
				caterpillarBodies [i].GetComponentInChildren<TextMesh> ().text = firstNumber.ToString ();
				caterpillarBodies [i].GetComponentInChildren<TextMesh> ().fontSize = 50;
				firstNumber++;
			}
		} else if (diff == Difficulty.Grade2) {
			firstNumber = Random.Range (20, 96);
			answerArray = new string[noOfOptions];

			for (int i = 0; i <= noOfOptions - 1; i++) {
				answerArray [i] = firstNumber.ToString ();
				snapPoints [i].GetComponent<SnapIndexValue> ().indexValue = i;

				Vector3 tempPosition = optionSpawn [i].transform.position;
				tempPosition.z -= 0.001f;
				caterpillarBodies [i].transform.position = tempPosition;

				caterpillarBodies [i].GetComponent<OriginalPos> ().indexValue = i;
				caterpillarBodies [i].GetComponentInChildren<TextMesh> ().text = firstNumber.ToString ();
				caterpillarBodies [i].GetComponentInChildren<TextMesh> ().fontSize = 40;
				firstNumber++;
			}
		} else if (diff == Difficulty.Grade3) {
			firstNumber = Random.Range (100, 996);
			answerArray = new string[noOfOptions];

			for (int i = 0; i <= noOfOptions - 1; i++) {
				answerArray [i] = firstNumber.ToString ();
				snapPoints [i].GetComponent<SnapIndexValue> ().indexValue = i;

				Vector3 tempPosition = optionSpawn [i].transform.position;
				tempPosition.z -= 0.001f;
				caterpillarBodies [i].transform.position = tempPosition;

				caterpillarBodies [i].GetComponent<OriginalPos> ().indexValue = i;
				caterpillarBodies [i].GetComponentInChildren<TextMesh> ().text = firstNumber.ToString ();
				caterpillarBodies [i].GetComponentInChildren<TextMesh> ().fontSize = 30;
				firstNumber++;
			}
		}
	} 

	public void ShuffleOptionsPositions() 
	{
		for (int i = 0; i < optionSpawn.Length; i++)
		{
			GameObject temp = optionSpawn [i];
			int r = Random.Range (i, optionSpawn.Length);
			optionSpawn [i] = optionSpawn [r];
			optionSpawn [r] = temp;
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

					if (!draggedObj.GetComponent<OriginalPos> ().isSnapped) {
						draggedObj.GetComponent<OriginalPos> ().originalPos = hit.transform.position;

					}

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
				if (Physics.Raycast (ray, out hit, Mathf.Infinity))
				{
					if (isSwap)
					{
						if (hit.collider.tag == "Snap") {
							//if (dropFlag) {
								SwapNumber (hit.collider.gameObject);
							//}
						} else
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

	public void SwapNumber(GameObject other)
	{
		Vector3 temp = other.gameObject.transform.position;
		temp.z -= 1f;
		draggedObj.transform.position = temp;

		int tempIndex = int.Parse(other.gameObject.name); 
		newAnswerArray [tempIndex] = draggedObj.GetComponentInChildren<TextMesh> ().text;
		draggedObj.gameObject.GetComponent<OriginalPos> ().indexValue = tempIndex;
		draggedObj.gameObject.GetComponent<OriginalPos> ().isSnapped = true;

		manager.PlayDragDropAudio ();
	}

	public void ResetAnswer()
	{
		if (!isSwap) {
			//draggedObj.GetChild (1).gameObject.SetActive (true);
			if (draggedObj.gameObject.GetComponent<OriginalPos> ().snappedObject != null) {
				draggedObj.gameObject.GetComponent<OriginalPos> ().snappedObject.GetComponentInChildren<TextMesh> ().text = "";
				draggedObj.gameObject.GetComponent<OriginalPos> ().isSnapped = false;
			}
		} else {
			if (draggedObj != null && draggedObj.gameObject.GetComponent<OriginalPos> ().isSnapped) {
				int tempIndex = draggedObj.gameObject.GetComponent<OriginalPos> ().indexValue;
				newAnswerArray [tempIndex] = "";
				//snapPoints [previousSnapIndex].GetComponent<BoxCollider> ().enabled = false;
				draggedObj.gameObject.GetComponent<OriginalPos> ().indexValue = 0;
				draggedObj.gameObject.GetComponent<OriginalPos> ().isSnapped = false;
			}
			
		}

		draggedObj.gameObject.GetComponent<OriginalPos> ().isSnapped = false;

	}

	public void ArrangeInOrder()
	{
		if (draggedObj != null) {
			textValue = int.Parse (draggedObj.gameObject.name);
			for (int i = textValue; i >= 1; i--) {
				if (i == 1 || (caterpillarBodies [i - 1].GetComponentInChildren<TextMesh> ().text != "" && caterpillarBodies [textValue].GetComponentInChildren<TextMesh> ().text == "")) {
					dropFlag = true;
				} else {
					dropFlag = false;
					break;
				}
			}
		}
	}

	public void Arrange()
	{
		for (int i = 0; i <= snapPoints.Length - 1; i++) {
			if (newAnswerArray [i] != "") {
				snapPoints [i].GetComponent<BoxCollider> ().enabled = false;
				continue;
			} else {
				snapPoints [i].GetComponent<BoxCollider> ().enabled = true;
				previousSnapIndex = i;
				break;
			}
		}
	}

	public void ResetOptions() {
		for (int i = 0; i < answerArray.Length; i++)
		{
			newAnswerArray [i] = "";

			if(answerArray [i] == null){
				continue;
			} else if (caterpillarBodies [i].GetComponent<OriginalPos> ().isSnapped) {
				caterpillarBodies [i].transform.position = caterpillarBodies [i].gameObject.GetComponent<OriginalPos> ().originalPos;
				caterpillarBodies [i].GetComponent<OriginalPos> ().isSnapped = false;
				//answerArray [i] = null;
			}
		}
	}
}
