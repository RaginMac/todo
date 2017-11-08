using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCarrotForAdd : MonoBehaviour {

	public enum AdditionType {Simple, Derived};
	public AdditionType addType;

	public GameObject[] carrots, rabbitCarrots, carrotsOnRabbit;
	public Text number1, number2, answerText;
	public string clickedAnswer;

	public int no1, no2, answerNumber;
	public string answer;

	public int dropCount;
	//public Transform[] snapPoints;
	public Transform draggedObj, parent;
	public bool dropFlag;
	public Camera cam;
	Vector3 offset;
	RaycastHit hit;

	public AudioSource source;
	public AudioClip[] clips;

	public int isAnswered = 0;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		CreateQuestions ();
		if(addType == AdditionType.Simple){
			DisplayCarrots (no1, no2, answerNumber);
		}
		else if(addType == AdditionType.Derived){
			DisplayCarrots(no2, no1, answerNumber);
		}
		source.clip = clips [0];
		Invoke ("PlayRabbitAudio", 1.5f);
	}

	// Update is called once per frame
	void Update () {
		DragObject ();
	}

	void PlayRabbitAudio(){
		if (Manager.Instance.noOfQuestionsAnswered <= Manager.Instance.totalNoOfQuestions) {
			source.Play ();
		} else if(Manager.Instance.noOfQuestionsAnswered > Manager.Instance.totalNoOfQuestions){
			source.Stop ();
		}
	}
	void CreateQuestions(){
		answerNumber = Random.Range(1,9);
		no1 = Random.Range(1, answerNumber);
		no2 = answerNumber - no1;

		if(addType == AdditionType.Simple){
			number1.text = no1.ToString();
			number2.text = no2.ToString();
			answer = answerNumber.ToString();
			dropCount = no2;
		}else if(addType == AdditionType.Derived){
			number1.text = answerNumber.ToString();
			number2.text = no1.ToString();
			answer = no2.ToString();
			dropCount = no1;
		}
	}

	public void DisplayCarrots(int n1, int n2, int n3){
		for (int i = 0; i < n1; i++) {
			carrots [i].SetActive (true);
		}
		for (int k = 0; k < n2; k++) {
			carrotsOnRabbit [k].SetActive (true);			
		}
		for (int j = 0; j < n3; j++) {
			rabbitCarrots [j].SetActive (true);			
		}
	}

	public void DragObject()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		if(Input.GetMouseButtonDown(0))
		{
			//ArrangeInOrder ();
			//print ("pressing");
			if(!draggedObj)
			{
				if(Physics.Raycast(ray, out hit, Mathf.Infinity)  && hit.collider.tag == "DraggableObject")
				{
					//print (hit.collider.tag);
					//ResetAnswerText();
					draggedObj = hit.transform;
					//hit.transform.GetComponent<AudioSource> ().Play ();
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
				//print ("start to drop");
				if (Physics.Raycast (ray, out hit, Mathf.Infinity))
				{
					if(hit.collider.tag == "Snap")
					{
						//	print ("drop collider" + hit.collider.tag);
						draggedObj.transform.SetParent(hit.collider.transform);
						draggedObj.GetComponent<BoxCollider>().enabled = false;
						//	answerPressed = draggedObj.GetComponentInChildren<TextMesh> ().text;
						//	temp = answerPressed + temp;
						//	answerPressed = temp;

						DropAnswer ();

					} else {
						//						
						draggedObj.transform.position = draggedObj.gameObject.GetComponent<OriginalPos> ().originalPos;
						draggedObj.GetComponent<BoxCollider>().enabled = true;
						//draggedObj.transform.SetParent (parent);
					}
				}
			}

//			if (draggedObj != null) {
//				draggedObj.gameObject.GetComponent<BoxCollider> ().enabled = true;
//			}

			draggedObj = null;
		}
	}

	public void DropAnswer ()
	{
		draggedObj.GetComponent<Animator>().SetTrigger("CarrotBite");
		Vector3 temp = rabbitCarrots[dropCount].transform.position;
		temp.z -= 1f;
		draggedObj.transform.position = temp;
		if (dropCount < answerNumber-1) {
			dropCount++;
		}
		draggedObj.gameObject.GetComponent<OriginalPos>().indexValue = dropCount;
		//finalAnswerNumbers[draggedObj.gameObject.GetComponent<OriginalPos>().indexValue] = draggedObj.gameObject.GetComponentInChildren<TextMesh>().text;
		draggedObj.gameObject.GetComponent<OriginalPos>().isSnapped = true;


	}
}
