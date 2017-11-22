using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DivideCarrots : MonoBehaviour {

	public enum QuestionType {withoutRemainder, withRemainder};
	public QuestionType qType;

	public Animator thoughtBubble;
	public int number1, number2, answer, remainder; 
	public string clickedAnswer, clickedRemainder;
	public List<int> randomNumbers;

	public Text n1, n2;
	public GameObject[] q_carrots, rabbits, dragCarrots;

	public Transform target, activeRabbit;


	public GameObject draggedObject,keypad;
	public Camera cam;
	public Vector3 offset;


	[SerializeField]
	int dropIndex = -1;
	[SerializeField]
	int noOfRabbits = 0;

	bool readyToMove = false;





	void Awake()
	{
		cam = Camera.main;
		SetQuestionCarrots(false, q_carrots.Length, q_carrots);
		GameObject.Find("Keypad").GetComponent<Keypad>().EraseAllText();
	}

	// Use this for initialization
	void Start () {
		thoughtBubble.SetBool("Think", true);

		number1 = GetDividend(randomNumbers);
		number2 = GetDivisor();
		answer = number1/number2;

		remainder = (qType == QuestionType.withRemainder) ? number1%number2 : 0;		

		n1.text = number1.ToString();
		n2.text = number2.ToString();
		SetQuestionCarrots(true, number1, q_carrots);
		//SetQuestionCarrots(true, number2, dragCarrots);
	}
	
	// Update is called once per frame
	void Update () {
		if(readyToMove)
		{
			MoveRabbit();
		}

		DragObject2D();
	}

	int FindRandomNumber(int n1, int n2)
	{
		int num = Random.Range(n1, n2);

		return num;
	}


	public int GetDividend(List<int> myList)
	{
		int val = FindRandomNumber(10, 20);
		if(qType == QuestionType.withoutRemainder){
			while (myList.Contains (val)) {
				val = FindRandomNumber(10, 20);
			}
		}
		return val;
	}

	public int GetDivisor()
	{
		int val  = FindRandomNumber(2, 10);
		if(qType == QuestionType.withoutRemainder){
			while(number1%val!=0)
			{
				val  = FindRandomNumber(2, 10);
			}
		}
		return val;
	}

	void SetQuestionCarrots(bool visibility, int count, GameObject[] array)
	{
		for (int i = 0; i < count; i++) {
			array[i].SetActive(visibility);
		}
	}

	public void MoveRabbit()
	{
		if(activeRabbit!=null){
			activeRabbit.position = Vector3.MoveTowards(activeRabbit.position, target.position, 10*Time.deltaTime);
			activeRabbit.localScale = Vector3.MoveTowards(activeRabbit.localScale, target.localScale, 2*Time.deltaTime);
		}
		if(Vector3.Distance(activeRabbit.position, target.position) < 0.3f)
		{
			SetNextTargetPosition();
		}
	}

	public void ResetAnimation()
	{
		thoughtBubble.SetBool("Think", false);
		Invoke("SetMoveStatus", 0.8f);
	}

	void SetMoveStatus()
	{
		readyToMove = true;
	}

	void SetNextTargetPosition()
	{
		readyToMove = false;
		target.position = new Vector3(target.position.x - 1.3f, target.position.y, target.position.z);
		ResetValues();
		SetNewRabbit();
	}

	void SetNewRabbit()
	{
		if(noOfRabbits<answer-1){
			noOfRabbits++;
			activeRabbit = rabbits[noOfRabbits].transform;
			rabbits[noOfRabbits].SetActive(true);
		}
//		activeRabbit = rabbits[noOfRabbits].transform;
//		rabbits[noOfRabbits].SetActive(true);
	}

	void ResetValues()
	{
		dropIndex = -1;
		for (int i = 0; i < dragCarrots.Length; i++) {
			dragCarrots[i].SetActive(false);
		}
	}

	public void DragObject2D()
	{
		Ray ray = cam.ScreenPointToRay (Input.mousePosition);

		if (Input.GetMouseButtonDown (0))
		{
			if (!draggedObject)
			{
				RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 100f);

				if (hit != null && hit.collider!=null && hit.collider.tag == "DraggableObject")
				{
					print(hit.collider.tag);
					draggedObject = hit.transform.gameObject;
					draggedObject.GetComponent<BoxCollider2D> ().enabled = false;
					offset = draggedObject.transform.position - ray.origin;
				}
			}
		}

		if(draggedObject) {
			draggedObject.transform.position = new Vector3(ray.origin.x + offset.x, ray.origin.y + offset.y, draggedObject.transform.position.z);
		}

		if (Input.GetMouseButtonUp (0))
		{
			if (draggedObject != null) {
				RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 100f);

				if(hit.collider!=null && hit.collider.tag=="Snap" && dropIndex<number2-1)
				{
					DropObjects(hit.collider.transform, draggedObject.transform);
				}
				else if((hit.collider==null || hit.collider.tag!="Snap") || dropIndex>=number2-1)
				{
					draggedObject.transform.position = draggedObject.gameObject.GetComponent<OriginalPos> ().originalPos;
				}


				draggedObject.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
				draggedObject = null;

			}
		}
	}

	void DropObjects(Transform hit, Transform drag)
	{
		drag.position = drag.gameObject.GetComponent<OriginalPos> ().originalPos;
		if(dropIndex<number2-1)
		{
			dropIndex++;
		}
		drag.gameObject.SetActive(false);
		dragCarrots[dropIndex].SetActive(true);
		CheckDropLimit();
	}

	void CheckDropLimit()
	{
		if(dropIndex>=number2-1)
		{
			ResetAnimation();
		}
	}

	public void ResetKeypad()
	{
		keypad.GetComponent<Animator>().SetBool("KeypadShow", false);
	}
}


