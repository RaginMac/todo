using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MulByRepeatedAdd : MonoBehaviour {

	public GameObject AppleSet1;
	public GameObject AppleSet2;
	public GameObject AppleSet3;
	//public GameObject AppleSet4;
	//public GameObject AppleSet5;
	public GameObject emptyBox, keypad;

	public Transform collide1;
	public Transform collide2;

	public GameObject[] questionBoxes;
	public GameObject[] plusOperators;
	public GameObject[] numberTexts;
	public Transform[] spawnPoints;


	public GameObject panel1;
	public GameObject panel2;
	public ScrollRect _scrollRect1;
	public ScrollRect _scrollRect2;


	public int n1;
	public int n2;
	public int answer;
	public string  playerAns;
	public TextMesh firstNumber;
	public TextMesh secondNumber;
	public Text answerText;

	public bool g11_2;
	public int count;
	public Transform appleBoxes;


	public Transform draggedObj;
	private Camera cam;
	private Vector3 offset , originalPos;
	RaycastHit hit;

	public GameObject[] CheckAnswer;

	void Awake()
	{
		_scrollRect1 = panel1.GetComponent<ScrollRect> ();
		_scrollRect2 = panel2.GetComponent<ScrollRect> ();
	}
	// Use this for initialization
	void Start () {
		cam = Camera.main;
		keypad = GameObject.Find("Keypad");


		count = 0;
		if (g11_2) {
			CheckAnswer [0].SetActive (false);
			CheckAnswer [1].SetActive (true);
		} 
//		else {
//			CheckAnswer [0].SetActive (true);
//			CheckAnswer [1].SetActive (false);
//		}

		Invoke("ScrollAnimation",0.5f);
		CreateQuestion ();
		CreateAppleSets ();
	}
	
	// Update is called once per frame
	void Update () {
		if (g11_2) {
			DragObject ();

			if (count >= n2) {
				CheckAnswer [0].SetActive (true);
				CheckAnswer [1].SetActive (false);
			}
		}

		//if(g11_2){
			//playerAns = collide1.GetComponent<ScrollValues> ().contentName + collide2.GetComponent<ScrollValues> ().contentName;
		//} else {
			playerAns = answerText.text;
		//}
	}

	public void ResetKeypad()
	{
		if (keypad != null) {
			keypad.GetComponent<Animator> ().SetBool ("KeypadShow", false);
		}
	}

	public void CreateQuestion()
	{
		n1 = Random.Range (1, 4);
		n2 = Random.Range (2, 6);
		answer = n1 * n2;

		//if (!g11_2) {
		firstNumber.text = n1.ToString ();
		secondNumber.text = n2.ToString ();
		//}
	}

	public void SettingQuestionBoxes(GameObject appleSet)
	{
		for (int i = 0; i < n2; i++) {
			Vector3 tempScale = appleSet.transform.localScale;
			questionBoxes [i].transform.localScale = tempScale;
			//print (questionBoxes [i].transform.localScale);
			questionBoxes [i].SetActive (true);
		}
	}

	public void CreateAppleSets()
	{
		if (n1 == 1) {
			for (int i = 0; i < n2; i++) {
				
				Vector3 tempPos = spawnPoints [i].position;
				tempPos.z -= 0.001f;

				Instantiate (AppleSet1, tempPos, Quaternion.identity, appleBoxes);


				if (g11_2) {
					Vector3 tempScale = AppleSet1.transform.localScale;
					emptyBox.transform.localScale = tempScale;
					SettingQuestionBoxes (AppleSet1);

					Instantiate (emptyBox, spawnPoints [i].position, Quaternion.identity, appleBoxes);


				} else {
					numberTexts [i].GetComponent<TextMesh> ().text = n1.ToString ();
					numberTexts [i].SetActive (true);
				}
				 
				if(i >= 1 && i < n2)
					plusOperators [i - 1].SetActive (true);
			}


		} else if (n1 == 2) {

			for (int i = 0; i < n2; i++) {

				Vector3 tempPos = spawnPoints [i].position;
				tempPos.z -= 0.001f;

				Instantiate (AppleSet2, tempPos, Quaternion.identity, appleBoxes);

				if (g11_2) {
					Vector3 tempScale = AppleSet2.transform.localScale;
					emptyBox.transform.localScale = tempScale;
					SettingQuestionBoxes (AppleSet2);

					Instantiate (emptyBox, spawnPoints[i].position, Quaternion.identity, appleBoxes);

				} else {
					numberTexts [i].GetComponent<TextMesh> ().text = n1.ToString ();
					numberTexts [i].SetActive (true);
				}

				if(i >= 1 && i < n2)
					plusOperators [i - 1].SetActive (true);
			}


		} else if (n1 == 3) {
			for (int i = 0; i < n2; i++) {

				Vector3 tempPos = spawnPoints [i].position;
				tempPos.z -= 0.001f;

				Instantiate (AppleSet3, tempPos, Quaternion.identity, appleBoxes);

				if (g11_2) {
					Vector3 tempScale = AppleSet3.transform.localScale;
					emptyBox.transform.localScale = tempScale;
					SettingQuestionBoxes (AppleSet3);

					Instantiate (emptyBox, spawnPoints[i].position, Quaternion.identity, appleBoxes);

				} else {
					numberTexts [i].GetComponent<TextMesh> ().text = n1.ToString ();
					numberTexts [i].SetActive (true);
				}

				if(i >= 1 && i < n2)
					plusOperators [i - 1].SetActive (true);
			}


		} 
//		else if (n1 == 4) {
//			for (int i = 0; i < n2; i++) {
//
//				Vector3 tempPos = spawnPoints [i].position;
//				tempPos.z -= 3f;
//
//				Instantiate (AppleSet4, tempPos, Quaternion.identity, appleBoxes);
//
//				if (g11_2) {
//					Vector3 tempScale = AppleSet4.transform.localScale;
//					emptyBox.transform.localScale = tempScale;
//
//					Instantiate (emptyBox, spawnPoints[i].position, Quaternion.identity, appleBoxes);
//					SettingQuestionBoxes (AppleSet4);
//				}
//
//				if(i >= 1 && i < n2)
//					plusOperators [i - 1].SetActive (true);
//			}
//
//
//		} else if (n1 == 5) {
//			for (int i = 0; i < n2; i++) {
//
//				Vector3 tempPos = spawnPoints [i].position;
//				tempPos.z -= 3f;
//
//				Instantiate (AppleSet5, tempPos, Quaternion.identity, appleBoxes);
//
//				if (g11_2) {
//					Vector3 tempScale = AppleSet5.transform.localScale;
//					emptyBox.transform.localScale = tempScale;
//
//					Instantiate (emptyBox, spawnPoints[i].position, Quaternion.identity, appleBoxes);
//					SettingQuestionBoxes (AppleSet5);
//				}
//
//				if(i >= 1 && i < n2)
//					plusOperators [i - 1].SetActive (true);
//			}
//
//		}
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
						draggedObj.GetComponent<OriginalPos> ().originalPos = draggedObj.transform.position;
					} else if (draggedObj.GetComponent<OriginalPos> ().isSnapped) {
						count--;
						draggedObj.GetComponent<OriginalPos> ().isSnapped = false;
					}
					
					hit.transform.GetComponent<BoxCollider>().enabled = false;
					offset = draggedObj.position - ray.origin;
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
						Manager.Instance.PlayDragDropAudio ();

						Vector3 tempPos = hit.transform.position;
						tempPos.z -= 1f;
						draggedObj.transform.position = tempPos;
						draggedObj.GetComponent<OriginalPos> ().isSnapped = true;
						count++;

					} else {
						draggedObj.transform.position = draggedObj.GetComponent<OriginalPos>().originalPos; 
					}
				}
			}

			if (draggedObj != null) {
				draggedObj.gameObject.GetComponent<BoxCollider> ().enabled = true;
			}

			draggedObj = null;

		}
	}


	public void ScrollAnimation()
	{
		_scrollRect1.verticalNormalizedPosition = Mathf.Clamp (_scrollRect1.verticalNormalizedPosition, 0.0001f, 0.9999f);
		_scrollRect1.velocity = Vector2.down * 2500f;

		_scrollRect2.verticalNormalizedPosition = Mathf.Clamp (_scrollRect1.verticalNormalizedPosition, 0.0001f, 0.9999f);
		_scrollRect2.velocity = Vector2.down * 2500f;
	}

}
