using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforeNAfter : MonoBehaviour {

	public enum Difficulty {Grade1, Grade2, Grade3};
	public Difficulty diff;

	public enum GameType {BeforeNAfter, InBetween};
	public GameType gameType;

	public Transform[] questionSpawnPoints;
	public Transform[] optionSpawnPoints;
	public GameObject[] caterpillarBodies;
	public string[] answer;

	private List<int> usedRandomNumbers = new List<int>();

	private int noOfOptions;
	public bool ansClicked;
	private int firstNumber;


	private Transform draggedObj;
	private bool dropFlag;
	private Camera cam;
	Vector3 offset;
	RaycastHit hit;

	public Animator anime;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		anime.SetTrigger ("Missing");

		ShuffleOptionsPositions ();

		CreateQuestion ();
		CreateOptions ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!Manager.Instance.isGameComplete)
			DragObject ();
	}

	void CreateQuestion()
	{
		if (diff == Difficulty.Grade1) {
			firstNumber = UniqueRandomInt(1, 6);
			answer = new string[questionSpawnPoints.Length];

			for (int i = 0; i <= caterpillarBodies.Length - 1; i++)
			{
				if (i <= questionSpawnPoints.Length - 1)
				{
					int tempNum = (firstNumber + i);
					usedRandomNumbers.Add (tempNum);
					answer [i] = (tempNum).ToString ();

					questionSpawnPoints [i].GetComponent<SnapIndexValue> ().indexValue = i;
					caterpillarBodies [i].GetComponent<OriginalPos> ().indexValue = i;
					caterpillarBodies [i].GetComponentInChildren<TextMesh> ().text = answer [i];
					caterpillarBodies [i].GetComponentInChildren<TextMesh> ().fontSize = 65;

				} else if(i > questionSpawnPoints.Length - 1) {
					caterpillarBodies [i].GetComponent<OriginalPos> ().indexValue = i;
					caterpillarBodies [i].GetComponentInChildren<TextMesh> ().text = UniqueRandomInt(1, 10).ToString();
					caterpillarBodies [i].GetComponentInChildren<TextMesh> ().fontSize = 65;
				}
			}
		} else if (diff == Difficulty.Grade2) {
			firstNumber = UniqueRandomInt(10, 96);
			answer = new string[questionSpawnPoints.Length];

			for (int i = 0; i <= caterpillarBodies.Length - 1; i++)
			{
				if (i <= questionSpawnPoints.Length - 1)
				{
					int tempNum = (firstNumber + i);
					usedRandomNumbers.Add (tempNum);
					answer [i] = (tempNum).ToString ();

					questionSpawnPoints [i].GetComponent<SnapIndexValue> ().indexValue = i;
					caterpillarBodies [i].GetComponent<OriginalPos> ().indexValue = i;
					caterpillarBodies [i].GetComponentInChildren<TextMesh> ().text = answer [i];
					caterpillarBodies [i].GetComponentInChildren<TextMesh> ().fontSize = 55;

				} else if(i > questionSpawnPoints.Length - 1) {
					caterpillarBodies [i].GetComponent<OriginalPos> ().indexValue = i;
					caterpillarBodies [i].GetComponentInChildren<TextMesh> ().text = UniqueRandomInt(10, 99).ToString();
					caterpillarBodies [i].GetComponentInChildren<TextMesh> ().fontSize = 55;
				}
			}
		} else if (diff == Difficulty.Grade3) {
			firstNumber = UniqueRandomInt(100, 996);
			answer = new string[questionSpawnPoints.Length];

			for (int i = 0; i <= caterpillarBodies.Length - 1; i++)
			{
				if (i <= questionSpawnPoints.Length - 1)
				{
					int tempNum = (firstNumber + i);
					usedRandomNumbers.Add (tempNum);
					answer [i] = (tempNum).ToString ();

					questionSpawnPoints [i].GetComponent<SnapIndexValue> ().indexValue = i;
					caterpillarBodies [i].GetComponent<OriginalPos> ().indexValue = i;
					caterpillarBodies [i].GetComponentInChildren<TextMesh> ().text = answer [i];
					caterpillarBodies [i].GetComponentInChildren<TextMesh> ().fontSize = 45;

				} else if(i > questionSpawnPoints.Length - 1) {
					caterpillarBodies [i].GetComponent<OriginalPos> ().indexValue = i;
					caterpillarBodies [i].GetComponentInChildren<TextMesh> ().text = UniqueRandomInt(100, 999).ToString();
					caterpillarBodies [i].GetComponentInChildren<TextMesh> ().fontSize = 45;
				}
			}
		}
	}


	public void CreateOptions()
	{
		int indexNum = 0;
		if (gameType == GameType.BeforeNAfter)
		{
			for (int i = 0; i < caterpillarBodies.Length; i++)
			{
				if (caterpillarBodies [i].GetComponent<OriginalPos> ().indexValue == 1) {
					Vector3 tempPosition = questionSpawnPoints [i].position;
					tempPosition.z -= 0.0001f;
					caterpillarBodies [i].transform.position = tempPosition;
					caterpillarBodies [i].GetComponent<BoxCollider> ().enabled = false;
					questionSpawnPoints [i].GetComponent<BoxCollider> ().enabled = false;

				} else if (caterpillarBodies [i].GetComponent<OriginalPos> ().indexValue != 1){
					Vector3 tempPosition = optionSpawnPoints [indexNum].position;
					tempPosition.z -= 0.0001f;
					caterpillarBodies [i].transform.position = tempPosition;
					caterpillarBodies [i] = null;
					indexNum++;
				}
			}

		} else if (gameType == GameType.InBetween) {
			for (int i = 0; i < caterpillarBodies.Length; i++)
			{
				if (caterpillarBodies [i].GetComponent<OriginalPos> ().indexValue == 0 || caterpillarBodies [i].GetComponent<OriginalPos> ().indexValue == 2) {
					Vector3 tempPosition = questionSpawnPoints [i].position;
					tempPosition.z -= 0.0001f;
					caterpillarBodies [i].transform.position = tempPosition;
					caterpillarBodies [i].GetComponent<BoxCollider> ().enabled = false;
					questionSpawnPoints [i].GetComponent<BoxCollider> ().enabled = false;

				} else if (caterpillarBodies [i].GetComponent<OriginalPos> ().indexValue != 0 || caterpillarBodies [i].GetComponent<OriginalPos> ().indexValue != 2){
					Vector3 tempPosition = optionSpawnPoints [indexNum].position;
					tempPosition.z -= 0.0001f;
					caterpillarBodies [i].transform.position = tempPosition;
					caterpillarBodies [i] = null;
					indexNum++;
				}
			}
		}
	}

	public void ShuffleOptionsPositions() 
	{
		if (optionSpawnPoints.Length > 0) {
			for (int i = 0; i < optionSpawnPoints.Length; i++) {
				Vector3 temp = optionSpawnPoints [i].transform.position;
				int r = Random.Range (i, optionSpawnPoints.Length);
				optionSpawnPoints [i].transform.position = optionSpawnPoints [r].transform.position;
				optionSpawnPoints [r].transform.position = temp;
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
					//ResetAnswer ();

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
		//tempPos.y -= 0.055f;
		draggedObj.position = tempPos;
		draggedObj.GetComponent<OriginalPos> ().isSnapped = true;
		draggedObj.GetComponent<OriginalPos> ().indexValue = tempIndex;
		caterpillarBodies [tempIndex] = draggedObj.gameObject;

		Manager.Instance.PlayDragDropAudio ();
	}

	public void ResetAnswer ()
	{
		for (int i = 0; i < answer.Length; i++)
		{
			if(caterpillarBodies [i] == null){
				continue;
			} else if (caterpillarBodies [i].GetComponent<OriginalPos> ().isSnapped) {
				caterpillarBodies [i].transform.position = caterpillarBodies [i].gameObject.GetComponent<OriginalPos> ().originalPos;
				caterpillarBodies [i] = null;
			}
		}

	}
}
