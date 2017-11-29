using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeInOrder : MonoBehaviour {
	
	public enum Difficulty {Grade1, Grade2, Grade3};
	public Difficulty diff;

	public Transform[] questionSpawnPoints;
	public Transform[] optionSpawnPoints;
	public GameObject[] caterpillarBodies;

	public List<int> usedRandomNumbers = new List<int>();

	public string[] answer;

	public int noOfOptions;
	public int firstNumber;
	public int previousSnapIndex;

	public Transform draggedObj;
	public bool dropFlag;
	public Camera cam;
	Vector3 offset;
	RaycastHit hit;


	public Animator anime;

	public bool ansClicked;

	// Use this for initialization
	void Start () {
		cam = Camera.main;

		CreateQuestions ();
		CreateOptions ();

		anime.SetTrigger ("Missing");
	}
	
	// Update is called once per frame
	void Update () {
		Arrange ();
		DragObject ();
	}

	public void CreateQuestions()
	{
		if (diff == Difficulty.Grade1) {
			
			noOfOptions = 3;
			firstNumber = 1;
			answer = new string[questionSpawnPoints.Length];

			for (int i = 0; i <= questionSpawnPoints.Length - 1; i++) {
				answer [i] = (i + 1).ToString ();
				questionSpawnPoints [i].GetComponent<SnapIndexValue> ().indexValue = i;

				Vector3 tempPosition = questionSpawnPoints [i].position;
				tempPosition.z -= 0.0001f;
				caterpillarBodies [i].transform.position = tempPosition;

				caterpillarBodies [i].GetComponent<OriginalPos> ().indexValue = i;
				caterpillarBodies [i].GetComponentInChildren<TextMesh> ().text = firstNumber.ToString ();
				firstNumber++;
			}
		} else if (diff == Difficulty.Grade2) {
			
			noOfOptions = 4;
			firstNumber = Random.Range (10, 91);
			answer = new string[questionSpawnPoints.Length];

			for (int i = 0; i <= questionSpawnPoints.Length - 1; i++) {
				answer [i] = (firstNumber).ToString ();
				questionSpawnPoints [i].GetComponent<SnapIndexValue> ().indexValue = i;

				Vector3 tempPosition = questionSpawnPoints [i].position;
				tempPosition.z -= 0.0001f;
				caterpillarBodies [i].transform.position = tempPosition;

				caterpillarBodies [i].GetComponent<OriginalPos> ().indexValue = i;
				caterpillarBodies [i].GetComponentInChildren<TextMesh> ().text = firstNumber.ToString ();
				caterpillarBodies [i].GetComponentInChildren<TextMesh> ().fontSize = 40;
				firstNumber++;
			}
		} else if (diff == Difficulty.Grade3) {
			
			noOfOptions = 5;
			firstNumber = Random.Range(100, 991);
			answer = new string[questionSpawnPoints.Length];

			for (int i = 0; i <= questionSpawnPoints.Length - 1; i++)
			{
				answer [i] = (firstNumber).ToString ();
				questionSpawnPoints [i].GetComponent<SnapIndexValue> ().indexValue = i;

				Vector3 tempPosition = questionSpawnPoints [i].position;
				tempPosition.z -= 0.0001f;
				caterpillarBodies [i].transform.position = tempPosition;

				caterpillarBodies [i].GetComponent<OriginalPos> ().indexValue = i;
				caterpillarBodies [i].GetComponentInChildren<TextMesh> ().text = firstNumber.ToString ();
				caterpillarBodies [i].GetComponentInChildren<TextMesh> ().fontSize = 28;
				firstNumber++;
			}
		}

	}

	public void CreateOptions()
	{
		int tempIndex = 0;

		if (diff == Difficulty.Grade1) {
			for (int i = 0; i < noOfOptions; i++) {
				UniqueRandomInt(1, 9);
				tempIndex = usedRandomNumbers[i];

				if (caterpillarBodies [tempIndex] != null) {
					Vector3 tempPosition = optionSpawnPoints [i + 1].position;
					tempPosition.z -= 0.0001f;

					caterpillarBodies [tempIndex].GetComponent<OriginalPos> ().originalPos = tempPosition;
					caterpillarBodies [tempIndex].transform.position = tempPosition;

					caterpillarBodies [tempIndex].GetComponent<BoxCollider> ().enabled = true;

					caterpillarBodies [tempIndex] = null;
				} 
			}
		} else if (diff == Difficulty.Grade2) {
			
			for (int i = 0; i < noOfOptions; i++)
			{
				UniqueRandomInt(1, 9);
				tempIndex = usedRandomNumbers[i];

				if (caterpillarBodies [tempIndex] != null) {
					Vector3 tempPosition = optionSpawnPoints [i + 1].position;
					tempPosition.z -= 0.0001f;

					caterpillarBodies [tempIndex].GetComponent<OriginalPos> ().originalPos = tempPosition;
					caterpillarBodies [tempIndex].transform.position = tempPosition;

					caterpillarBodies [tempIndex].GetComponent<BoxCollider> ().enabled = true;

					caterpillarBodies [tempIndex] = null;
				} 
			}
		} else if (diff == Difficulty.Grade3) {

			for (int i = 0; i < noOfOptions; i++)
			{
				UniqueRandomInt(1, 9);
				tempIndex = usedRandomNumbers[i];

				if (caterpillarBodies [tempIndex] != null) {
					Vector3 tempPosition = optionSpawnPoints [i].position;
					tempPosition.z -= 0.0001f;

					caterpillarBodies [tempIndex].GetComponent<OriginalPos> ().originalPos = tempPosition;
					caterpillarBodies [tempIndex].transform.position = tempPosition;

					caterpillarBodies [tempIndex].GetComponent<BoxCollider> ().enabled = true;

					caterpillarBodies [tempIndex] = null;
				}
			}
		}
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

					if(!draggedObj.GetComponent<OriginalPos> ().isSnapped)
						draggedObj.GetComponent<OriginalPos> ().originalPos = hit.transform.position;
					
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
					if(hit.collider.tag == "Snap")
					{
						DropAnswer (hit.transform);

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

	public void DropAnswer(Transform other)
	{
		int tempIndex = other.GetComponent<SnapIndexValue>().indexValue;

		Vector3 tempPos = other.position;
		tempPos.z -= 1f;
		tempPos.y -= 0.055f;
		draggedObj.position = tempPos;
		draggedObj.GetComponent<OriginalPos> ().isSnapped = true;
		draggedObj.GetComponent<OriginalPos> ().indexValue = tempIndex;
		caterpillarBodies [tempIndex] = draggedObj.gameObject;
	}


	public void Arrange()
	{
		for (int i = 0; i <= caterpillarBodies.Length - 1; i++) {
			if (caterpillarBodies [i] != null) {
				questionSpawnPoints [i].GetComponent<BoxCollider> ().enabled = false;
				continue;
			} else {
				questionSpawnPoints [i].GetComponent<BoxCollider> ().enabled = true;
				previousSnapIndex = i;
				break;
			}
		}
	}

	public void ResetAnswer ()
	{
		int tempIndex = draggedObj.GetComponent<OriginalPos> ().indexValue;

		questionSpawnPoints [previousSnapIndex].GetComponent<BoxCollider> ().enabled = false;

		if (draggedObj.GetComponent<OriginalPos> ().isSnapped) {
			draggedObj.GetComponent<OriginalPos> ().isSnapped = false;
			caterpillarBodies [tempIndex] = null;
		}
	}
}
