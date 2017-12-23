using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterOrLesser_2 : MonoBehaviour {

	public string[] ratNumbers, elephantNumbers;
	public Transform[] snapPoints;

	public TextMesh number1, number2, number3, number4, number5, number6;
	public int n1, n2, n3;
	public List<int> numbers = new List<int>();

	public List<int> usedRandomNumbers = new List<int>();
	public List<GameObject> options = new List<GameObject>(); 

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

	public Animator ElephantAnime;
	public Animator RatAnime;

	public AudioSource elephantAudio;
	public AudioSource ratAudio;

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
		
		if (!manager.isGameComplete) {
			DragObject ();
		}

		ArrangeInOrder();
		FindFinalAnswers ();
	}

	public void DragObject()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		if (Input.GetMouseButtonDown(0))
		{
			ManageOptions ();
			if (!draggedObj)
			{
				if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag == "DraggableObject")
				{
					draggedObj = hit.transform;
					draggedObj.transform.GetComponent<BoxCollider>().enabled = false;

					if (!draggedObj.gameObject.GetComponent<OriginalPos> ().isSnapped) {
						draggedObj.gameObject.GetComponent<OriginalPos>().originalPos = draggedObj.transform.position;
					}

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
					} else {
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

	public void DropAnswer()
	{
		Vector3 temp = snapPoints[dropCount].transform.position;
		temp.z -= 1f;
		draggedObj.transform.position = temp;
		draggedObj.gameObject.GetComponent<OriginalPos>().indexValue = dropCount;
		finalAnswerNumbers[dropCount] = draggedObj.gameObject.GetComponentInChildren<TextMesh>().text;
		draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped = true;
		//draggedObj.gameObject.SetActive (false);

		manager.PlayDragDropAudio ();
	}

	public void ResetAnswer()
	{
		if (draggedObj == null) {
			for (int i = 0; i < options.Count; i++) {
				options [i].transform.position = options [i].gameObject.GetComponent<OriginalPos> ().originalPos;
				options [i].gameObject.GetComponent<OriginalPos> ().isSnapped = false;
				finalAnswerNumbers [i] = "";
			}

		} else if (draggedObj.gameObject.GetComponent<OriginalPos> ().isSnapped == true) {
			finalAnswerNumbers[draggedObj.gameObject.GetComponent<OriginalPos>().indexValue] = "";
			draggedObj.gameObject.GetComponent<OriginalPos> ().isSnapped = false;
			draggedObj.gameObject.GetComponent<OriginalPos> ().indexValue = 0;
		}
	}

	public void AssignNumbers()
	{
		n1 = UniqueRandomInt(1, 9);
		numbers.Add(n1);
		n2 = UniqueRandomInt(1, 9);
		numbers.Add(n2);
		n3 = UniqueRandomInt(1, 9);
		numbers.Add(n3);

		number1.text = n1.ToString();
		number2.text = n2.ToString();
		number3.text = n3.ToString();
		number4.text = n1.ToString();
		number5.text = n2.ToString();
		number6.text = n3.ToString();
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

	public void FindFinalAnswers()
	{
		finalRatNumber = finalAnswerNumbers[0] + finalAnswerNumbers[1] + finalAnswerNumbers[2];
		finalElephantNumber =  finalAnswerNumbers[3] + finalAnswerNumbers[4] + finalAnswerNumbers[5];                                                                                                                      
	}

	public void FindSmallestNBiggest()
	{
		numbers.Sort();

		smallestNum = numbers [0].ToString() + numbers [1].ToString() + numbers [2].ToString() ;
		biggestNum = numbers [2].ToString()  + numbers [1].ToString()  + numbers [0].ToString() ;
	}

	void ManageOptions()
	{
		if (finalAnswerNumbers [0] == "" || finalAnswerNumbers [1] == "" || finalAnswerNumbers[2] == "") {
			options[3].GetComponent<BoxCollider> ().enabled = false;
			options[4].GetComponent<BoxCollider> ().enabled = false;
			options[5].GetComponent<BoxCollider> ().enabled = false;
		} else {
			options[3].GetComponent<BoxCollider> ().enabled = true;
			options[4].GetComponent<BoxCollider> ().enabled = true;
			options[5].GetComponent<BoxCollider> ().enabled = true;
		}
	}
}
