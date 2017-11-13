using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapNumbers : MonoBehaviour {

	public Manager manager;
	public Animator animator;
	public GameObject[] numberArray;
	public GameObject[] answerArray;

	public GameObject[] options;

	public int answerCounter;
	public Transform[] snapPoints;

	public Transform draggedObj;
	public Camera cam;
	Vector3 offset;
	RaycastHit hit;
	public bool isSwap;


	public GameObject[] caterpillarBodies;

	public string[] newAnswerArray;
	public int newAnswerArrayIndex;
	public bool dropFlag;
	public bool arrangeInOrder;
	public int textValue;
	public int previousSnapIndex;

	public bool ansClicked;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("Manager").GetComponent<Manager>();
		cam = Camera.main;
		ShuffleOptionsPositions ();
		animator.SetTrigger ("Missing");
		//draggedObj = null;
//		if(isSwap)
//		{
//			ShuffleNumberArray ();
//			SpawnObjects ();
//		}
	}
	
	// Update is called once per frame
	void Update () {
		DragObject ();

		if (arrangeInOrder)
			ArrangeInOrder ();
		else
			//dropFlag = false;
			Arrange();

	}

	public void ShuffleNumberArray() 
	{
		if (numberArray.Length > 0) {
			for (int i = 0; i < numberArray.Length; i++) {
				GameObject temp = numberArray [i];
				int r = Random.Range (i, numberArray.Length);
				numberArray [i] = numberArray [r];
				numberArray [r] = temp;
			}
		}
	}

	public void ShuffleOptionsPositions() 
	{
		if (options.Length > 0) {
			for (int i = 0; i < options.Length; i++) {
				Vector3 temp = options [i].transform.position;
				int r = Random.Range (i, options.Length);
				options [i].transform.position = options [r].transform.position;
				options [i].GetComponent<OriginalPos> ().originalPos = options [i].transform.position;
				options [r].transform.position = temp;
				options [r].GetComponent<OriginalPos> ().originalPos = options [r].transform.position;
			}
		}
	}

	public void SpawnObjects()
	{
		for (int i = 0; i < numberArray.Length; i++) {
			Vector3 tempPos = snapPoints [i].position;
//			GameObject obj = (GameObject)Instantiate (numberArray[i], tempPos, Quaternion.identity);
			numberArray[i].transform.position = tempPos;
			numberArray [i].gameObject.GetComponent<OriginalPos> ().indexValue = i;
			numberArray [i].SetActive (true);

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
					else {
						if ((!draggedObj.gameObject.GetComponent<OriginalPos> ().isSnapped)) {
							if (hit.collider.tag == "Snap") {
								DropAnswer (hit.collider.gameObject);
							} else {
								draggedObj.transform.position = draggedObj.gameObject.GetComponent<OriginalPos> ().originalPos;
							}
						} else {
							draggedObj.transform.position = draggedObj.gameObject.GetComponent<OriginalPos> ().originalPos;
						}
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
//		Vector3 temp = draggedObj.gameObject.GetComponent<OriginalPos> ().originalPos;
//		draggedObj.gameObject.GetComponent<OriginalPos> ().originalPos = other.gameObject.GetComponent<OriginalPos> ().originalPos;
//		draggedObj.transform.position = other.gameObject.GetComponent<OriginalPos> ().originalPos;
//		other.gameObject.GetComponent<OriginalPos> ().originalPos = temp;
//		other.transform.position = temp;
//
//		GameObject tempObj = numberArray[draggedObj.gameObject.GetComponent<OriginalPos> ().indexValue];
//		numberArray[draggedObj.gameObject.GetComponent<OriginalPos> ().indexValue] = numberArray[other.gameObject.GetComponent<OriginalPos> ().indexValue];
//		numberArray[other.gameObject.GetComponent<OriginalPos> ().indexValue] = tempObj;
//
//		int tempValue = draggedObj.gameObject.GetComponent<OriginalPos> ().indexValue;
//		draggedObj.gameObject.GetComponent<OriginalPos> ().indexValue = other.gameObject.GetComponent<OriginalPos> ().indexValue;
//		other.gameObject.GetComponent<OriginalPos> ().indexValue = tempValue;

		Vector3 temp = other.gameObject.transform.position;
		temp.z -= 1f;
		temp.y -= 0.055f;
		draggedObj.transform.position = temp;

		int tempIndex = int.Parse(other.gameObject.name); 
		//print (tempIndex);
		newAnswerArray [tempIndex] = draggedObj.GetComponentInChildren<TextMesh> ().text;
		draggedObj.gameObject.GetComponent<OriginalPos> ().indexValue = tempIndex;
		draggedObj.gameObject.GetComponent<OriginalPos> ().isSnapped = true;

	}

	public void DropAnswer (GameObject other)
	{
		Vector3 temp = other.gameObject.transform.position;
		temp.z -= 1f;
		temp.y -= 0.055f;
		draggedObj.transform.position = temp;
		other.gameObject.GetComponentInChildren<TextMesh> ().text = draggedObj.GetComponentInChildren<TextMesh> ().text;
		other.gameObject.GetComponentInChildren<TextMesh> ().characterSize = 0;
		//draggedObj.GetChild (1).gameObject.SetActive (false);
		draggedObj.gameObject.GetComponent<OriginalPos> ().snappedObject = other.gameObject;
		draggedObj.gameObject.GetComponent<OriginalPos> ().isSnapped = true;

		//draggedObj.gameObject.SetActive (false);
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
				snapPoints [previousSnapIndex].GetComponent<BoxCollider> ().enabled = false;
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
}
