using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistributedCoins : MonoBehaviour {

	public enum QuestionType {withoutRemainder, withRemainder};
	public QuestionType qType;


	public int unitsDigit, tensDigit;
	public  int numberOne, numberTwo,spawnDropindex = 0, answer, remainder;
	//public GameObject[] dropTrays; // unitsCoins, tensCoins;
	public Transform[] spawnArray;
	public List<GameObject> newCoins, unitsCoins, tensCoins, dropTrays, draggedCoins;
	public Text n1, n2, playerAnswer, remainderAnswer;
	public GameObject draggedObject,keypad, coin, newCoinParent, exchangePanel;
	public Camera cam;
	public Vector3 offset;
	public Transform spawnPos;

	public bool showStartAnimation = false;

	private int dropIndex = -1;

	[SerializeField]
	int n = 1;
	[SerializeField]
	int m = 1;
	[SerializeField]
	int tencounter = 0;
	[SerializeField]
	int onecounter = 0;
	[SerializeField]
	int dropcount = 0;

	Transform[] temp;
	int index, isExchanged = 0, animIndex;
	Vector3 animTarget;
	List<GameObject> animArray;

	void Awake()
	{
		cam = Camera.main;
	}

	// Use this for initialization
	void Start () {
		keypad.GetComponent<Keypad>().EraseAllText();
		animTarget = dropTrays[0].transform.position;
		numberOne = GetDividendNumber();
		numberTwo = GetDivisorNumber();
		answer = numberOne/numberTwo;
		remainder = numberOne % numberTwo;

		SetElements(numberTwo, true, dropTrays);
		SetElements(tensDigit, true, tensCoins);
		SetElements(unitsDigit, true, unitsCoins);

		SetText(n1, numberOne.ToString());
		SetText(n2, numberTwo.ToString());
		SetDropLimits();


		Invoke("CheckStartStatus", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		if(showStartAnimation)
		{
			ShowCoinAnim(animArray, animIndex, animTarget);
		}
		else{
			DragObject2D();
		}
	}

	void StartAnim(List<GameObject> arrayName, int indexNumber, Vector3 target)
	{
		animArray = arrayName;
		animIndex = indexNumber;
		animTarget = target;

		showStartAnimation = true;
	}

	void ShowCoinAnim(List<GameObject> array, int indexVal, Vector3 target)
	{
		if(array!=null){
			array[indexVal].transform.position = Vector3.MoveTowards(array[indexVal].transform.position, target, 0.3f);
		}

		if(Vector3.Distance(array[indexVal].transform.position, dropTrays[0].transform.position)<0.2f)
		{
			animTarget = array[indexVal].GetComponent<OriginalPos>().originalPos;
		}
		if(Vector3.Distance(array[indexVal].transform.position, array[indexVal].GetComponent<OriginalPos>().originalPos)<0.1f)
		{
			showStartAnimation = false;
		}
		//Invoke("AllowDrag", 2f);
	}

	void AllowDrag()
	{
		showStartAnimation = false;
	}

	void CheckStartStatus()
	{
		if(tensDigit>=numberTwo || unitsDigit<numberTwo){
			SetDragStatus(tensCoins, tensDigit-n, true);
			SetDragStatus(unitsCoins, unitsDigit-m, false);

			StartAnim(tensCoins, tensDigit-n,animTarget);
		}
		else if(tensDigit<numberTwo){
			SetDragStatus(unitsCoins, unitsDigit-m, true);
			SetDragStatus(tensCoins, tensDigit-n, false);

			StartAnim(unitsCoins, unitsDigit-m, animTarget);
		}
	}

	void SetDropLimits()
	{
		for (int i = 0; i < dropTrays.Count; i++) {
			dropTrays[i].GetComponent<CheckCoinDropLimit>().SetDropLimit(tensDigit, numberTwo);
		}
	}

	public int GetDividendNumber()
	{
		unitsDigit = Random.Range(1, 9);
		tensDigit = Random.Range(1, 9);

		int temp = int.Parse(tensDigit.ToString() + unitsDigit.ToString());

		return temp;
	}

	public int GetDivisorNumber()
	{
		int val = Random.Range(2, 5);
		if(qType==QuestionType.withoutRemainder){
			while(numberOne%val!=0)
			{
				val = Random.Range(2, 5);
			}
		}

		return val;
	}

	void SetElements(int count, bool visibility, List<GameObject> array)
	{
		newCoins.Clear();
		for (int i = 0; i < count; i++) {
			array[i].SetActive(visibility);
			if(array==unitsCoins)
			{
				newCoins.Add(array[i]);
			}
		}
	}


	void SetText(Text text, string val)
	{
		text.text = val;
	}

	void SetDragStatus(List<GameObject> coinArray, int index, bool isTrue)
	{
		coinArray[index].GetComponent<BoxCollider2D>().enabled = isTrue;
	}

	public void DragObject2D()
	{
		Ray ray = cam.ScreenPointToRay (Input.mousePosition);

		if (Input.GetMouseButtonDown (0))
		{
			if (!draggedObject)
			{
				RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 100f);

				if (hit != null && hit.collider!=null && (hit.collider.tag == "Coin1" || hit.collider.tag == "Coin10"))
				{
					draggedObject = hit.transform.gameObject;
					draggedObject.GetComponent<BoxCollider2D> ().enabled = false;
				//	draggedObject.GetComponent<OriginalPos>().originalPos = draggedObject.transform.position;
					offset = draggedObject.transform.position - ray.origin;
					//SetColliders(draggedObject.tag);
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

				if(hit.collider!=null && (hit.collider.tag=="Snap"))
				{
					//DropObjects();
					DropObjects(hit.transform, draggedObject);
				}
				else if(hit.collider!=null && (hit.collider.tag=="ExchangePanel10"))
				{
					InvokeRepeating("SpawnNewCoins", 0.5f, 0.5f);
					DropObjects(hit.transform, draggedObject);
				}
				else{
					draggedObject.transform.position = draggedObject.gameObject.GetComponent<OriginalPos> ().originalPos;
				}

				draggedObject.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
				draggedObject = null;

			}
		}
	}

	void DropObjects(Transform hit, GameObject drag)
	{
		//drag.transform.SetParent(hit.transform);
		if(drag.tag=="Coin10")
		{
			if(hit.gameObject.tag!="ExchangePanel10"){
				//hit.GetComponent<CheckCoinDropLimit>().tenCounter++;
				CheckCoinLimit(hit.GetComponent<CheckCoinDropLimit>().noOfTensCoins);
				temp = hit.GetComponent<CheckCoinDropLimit>().snapArray;
				index = hit.GetComponent<CheckCoinDropLimit>().dropindex;
			}else{
				isExchanged = 1;
			}
			if(n<tensDigit){
				n++;
			}

			//tensCoins.Remove(drag);
			SetDragStatus(tensCoins, tensDigit-n, true);
			CheckForChange();
		}
		else if(drag.tag=="Coin1")
		{
			print("1coin");
			newCoins.Remove(drag.gameObject);
			if(newCoins.Count==0)
			{
				exchangePanel.GetComponent<BoxCollider2D>().enabled = true;

				if(tensDigit%numberTwo!=0){	exchangePanel.SetActive(true);}

				if(tensDigit<numberTwo){ 
					SetDragStatus(tensCoins, tensDigit-n, true);			//if all units coins have been dragged, allow exchange for tens coins
				}			
			}
			hit.GetComponent<CheckCoinDropLimit>().oneCounter++;
			if(m<newCoins.Count){
				m++;
			}
			temp = hit.GetComponent<CheckCoinDropLimit>().snapArray2;
			index = hit.GetComponent<CheckCoinDropLimit>().dropIndex2;

			//SetDragStatus(unitsCoins, unitsDigit-m, true);
			if(newCoins.Count>0){
				SetDragStatus(newCoins, newCoins.Count-1, true);
			}
		}


		if(hit.gameObject.tag!="ExchangePanel10"){
			drag.transform.position = temp[index].transform.position;
			index++;
		}else{
			drag.SetActive(false);
		}

		if(drag.tag=="Coin10" && hit.gameObject.tag!="ExchangePanel10")
			hit.GetComponent<CheckCoinDropLimit>().dropindex = index;
		else if(drag.tag=="Coin1")
			hit.GetComponent<CheckCoinDropLimit>().dropIndex2 = index;
		
//		SetDragStatus(tensCoins, tensDigit-n, true);
//		if(tencounter==numberTwo)
//		{
//			SetDragStatus(unitsCoins, unitsDigit-m, true);
//		}
	}

	void CheckCoinLimit(int limit)
	{
		//if(dropTrays[tencounter].GetComponent<CheckCoinDropLimit>().tenCounter!=limit)
		{
			tencounter++;
		}

		if(tensDigit%numberTwo==0){
			if(tensDigit-tencounter==0)
			{
				print("drag ones");
				SetDragStatus(unitsCoins, unitsDigit-m, true);
				StartAnim(unitsCoins, unitsDigit-m, dropTrays[0].transform.position);
			}
		}else{
			if(tensDigit-tencounter<numberTwo)
			{
				print("drag ones");
				SetDragStatus(unitsCoins, unitsDigit-m, true);
				StartAnim(unitsCoins, unitsDigit-m, dropTrays[0].transform.position);
			}
		}
	}

	public void SpawnNewCoins()
	{
		GameObject temp = Instantiate(coin, spawnPos.position, Quaternion.identity) as GameObject;	
		temp.GetComponent<Move>().target = spawnArray[spawnDropindex].transform;
		temp.GetComponent<OriginalPos>().originalPos = spawnArray[spawnDropindex].transform.position;
		temp.transform.localScale -= new Vector3(0.25f, 0.25f, 0.25f);
		temp.transform.SetParent(newCoinParent.transform);
		newCoins.Add(temp);
		draggedCoins.Add(temp);
		spawnDropindex++;

		if(spawnDropindex==10)
		{
			CancelInvoke("SpawnNewCoins");
			spawnDropindex = 0;
			m=1;
			SetDragStatus(newCoins, newCoins.Count-1, true);
		}
	}

	void CheckForChange()
	{
		if(tencounter>=numberTwo && numberTwo!=1 && tensDigit%numberTwo!=0)
		{
			//exchangePanel.SetActive(true);
			exchangePanel.GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	public void ResetEverything()
	{
		m = 1;
		n = 1;
		spawnDropindex = 0;
		temp = null;
		exchangePanel.SetActive(false);
		tencounter = 0;


		if(isExchanged==1){
			for (int i = 0; i < newCoins.Count; i++) {
				if(newCoins[i]!=null){
					Destroy(newCoins[i]);
				}
			}
			for (int i = 0; i < draggedCoins.Count; i++) {
				if(draggedCoins[i]!=null){
					Destroy(draggedCoins[i]);
				}

			}
		}
		draggedCoins.Clear();
		for (int i = 0; i < unitsCoins.Count; i++) {
			unitsCoins[i].transform.position = unitsCoins[i].GetComponent<OriginalPos>().originalPos;
		//	unitsCoins[i].GetComponent<BoxCollider2D>().enabled = false;
		}
		for (int i = 0; i < tensCoins.Count; i++) {
			tensCoins[i].transform.position = tensCoins[i].GetComponent<OriginalPos>().originalPos;
		//	tensCoins[i].GetComponent<BoxCollider2D>().enabled = false;
		}
		for (int i = 0; i < dropTrays.Count; i++) {
			dropTrays[i].GetComponent<CheckCoinDropLimit>().tenCounter = 0;
			dropTrays[i].GetComponent<CheckCoinDropLimit>().dropindex = 0;
			dropTrays[i].GetComponent<CheckCoinDropLimit>().dropIndex2 = 0;

		}

		SetElements(numberTwo, true, dropTrays);
		SetElements(tensDigit, true, tensCoins);
		SetElements(unitsDigit, true, unitsCoins);
		animTarget = dropTrays[0].transform.position;

		SetDropLimits();
		CheckStartStatus();
	}

	public void ResetKeypad()
	{
		keypad.GetComponent<Animator>().SetBool("KeypadShow", false);
	}
}
