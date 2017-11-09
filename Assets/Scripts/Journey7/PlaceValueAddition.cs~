using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceValueAddition : MonoBehaviour {

	public enum Difficulty {Grade1, Grade2, Grade3};
	public Difficulty diff;
	public Animator CamAnime;
	public bool addWithCarry;
	public bool addWithoutCarry;
	public bool answered = false;


	[Header("Spawn stuff")]
	public GameObject onesCoin;
	public GameObject tensCoin;
	public GameObject hunsCoin;
	public Transform onesCoinSpawner;
	public Transform tensCoinSpawner;
	public Transform hunsCoinSpawner;

	[Header("Drop Location")]
	public int dropIndexOne;
	public int dropIndexTen;
	public int dropIndexHun;
	public Transform dropLocation;

	[Header("On Start array")]
	public GameObject[] spawnedOneCoins;
	public GameObject[] spawnedTenCoins;
	public GameObject[] spawnedHunCoins;
	public GameObject[] newSpawnedOneCoins;
	public GameObject[] newSpawnedTenCoins;
	public GameObject[] newSpawnedHunCoins;


	[Header("Final arrays")]
	public List<GameObject> droppedOneCoins;
	public List<GameObject> droppedTenCoins;
	public List<GameObject> droppedHunCoins;


	[Header("Final numbers")]
	public string n1;
	public string n2;

	public string answer;
	public string playerAnswer;

	public int N1D1, N1D2, N1D3;
	public int N2D1, N2D2, N2D3;

	[Header("shift bools")]
	public bool shiftOnes = false;
	public bool shiftTens = false;
	public bool shiftHuns = false;

	[Header("Coin Parents")]
	public Transform coinOneParent;
	public Transform coinTenParent;
	public Transform coinHunParent;
	public Transform newCoinOneParent;
	public Transform newCoinTenParent;
	//public Transform coinHunParent;

	public Transform shiftOne;
	public Transform shiftTen;
	public Transform shiftHun;


	// display first number
	[Header("First number")]
	public TextMesh n1FirstDigit;
	public TextMesh n1SecondDigit;
	public TextMesh n1ThirdDigit;

	// display second number
	[Header("Second number")]
	public TextMesh n2FirstDigit;
	public TextMesh n2SecondDigit;
	public TextMesh n2ThirdDigit;

	//answer texts
	[Header("Answer number")]
	public TextMesh ans1;
	public TextMesh ans2;
	public TextMesh ans3;

	[Header("coins to exchange")]
	public bool exchangeOnesForTen = false;
	public bool exchangeTensForHun = false;
	public bool hasOnesShifted = false;
	public bool hasTensShifted = false;

	[Header("Drag Stuff")]
	public GameObject draggedObject;
	public Camera cam;
	public Vector3 offset;
	public GameObject dropArea1;
	public GameObject dropArea10;

	public GameObject coinOneAnimImage;
	public GameObject coinTenAnimImage;
	public GameObject outlineOne;
	public GameObject outlineTen;


	void Awake(){
	//	ResetAnim();
	}

	void Start () {
		//coinOneAnim = coinOneAnimImage.GetComponent<Animator>();
		cam = Camera.main;
		if (addWithoutCarry) {
			CreateQuestionsAWOB ();
		}
		else if (addWithCarry) {
			CreateQuestionsAWB ();
		}
	}

	void Update () {
		//SetPlayerAnswer();
		if(shiftOnes){
			Move(coinOneParent, shiftOne);
		}
		if(shiftTens){
			Move(coinTenParent, shiftTen);
		}
		DragObject2D();
	}


	//AWOB Addition without carry
	void CreateQuestionsAWOB()
	{
		if (diff == Difficulty.Grade1) {
			int tempN1 = Random.Range(1, 9);
			int tempN2 = Random.Range (1, 9-tempN1);

			n1 = tempN1.ToString ();
			n2 = tempN2.ToString ();
			N1D1 = tempN1;
			N2D2 = (tempN2);


			StartCoroutine(DisplayQuestion("0", "0", tempN1.ToString (), "0", "0", tempN2.ToString ()));
			FindAnswer (n1, n2);
			SetCoinsOnStart(0, 0, tempN1);

		} else if (diff == Difficulty.Grade2) {
			int tempN1 = Random.Range (1, 9);
			int tempN2 = Random.Range (1, 9);
			int tempN3 = Random.Range (1, 9-tempN1);
			int tempN4 = Random.Range (1, 9-tempN2);

			n1 = tempN1.ToString () + tempN2.ToString ();
			n2 = tempN3.ToString () + tempN4.ToString ();
			N1D1 = tempN2;
			N1D2 = tempN1;
			N2D1 = (tempN4);
			N2D2 = (tempN3);


			StartCoroutine(DisplayQuestion("0", tempN1.ToString (), tempN2.ToString (), "0", tempN3.ToString (), tempN4.ToString ()));
			FindAnswer (n1, n2);
			SetCoinsOnStart(0,tempN1, tempN2);

		} else if (diff == Difficulty.Grade3) {
			int tempN1 = Random.Range (1, 9);
			int tempN2 = Random.Range (1, 9);
			int tempN3 = Random.Range (1, 9);

			int tempN4 = Random.Range (1, 9-tempN1);
			int tempN5 = Random.Range (1, 9-tempN2);
			int tempN6 = Random.Range (1, 9-tempN3);

			n1 = tempN1.ToString () + tempN2.ToString () + tempN3.ToString();
			n2 = tempN4.ToString () + tempN5.ToString () + tempN6.ToString();
			N1D1 = (tempN3);
			N1D2 = (tempN2);
			N1D3 = (tempN1);

			N2D1 = (tempN6);
			N2D2 = (tempN5);
			N2D3 = (tempN4);

			StartCoroutine(DisplayQuestion(tempN1.ToString (), tempN2.ToString (), tempN3.ToString (), tempN4.ToString (), tempN5.ToString (), tempN6.ToString ()));
			FindAnswer (n1, n2);
			SetCoinsOnStart(tempN1, tempN2, tempN3);
		}


	}

	//AWB - addition with borrow
	void CreateQuestionsAWB()
	{
		if (diff == Difficulty.Grade1) {
			int tempN1 = Random.Range (1, 9);
			int tempN2 = Random.Range (1, 9);

			n1 = tempN1.ToString ();
			n2 = tempN2.ToString ();
			N2D2 = (tempN2);

			FindAnswer (n1, n2);
			StartCoroutine(DisplayQuestion("0", "0", tempN1.ToString (), "0", "0", tempN2.ToString ()));
			SetCoinsOnStart(0, 0, tempN1);

		} else if (diff == Difficulty.Grade2) {
			int tempN1 = Random.Range (1, 9);
			int tempN2 = Random.Range (1, 9);

			int tempN3 = Random.Range (1, 9);
			int tempN4 = Random.Range (1, 9);

			n1 = tempN1.ToString () + tempN2.ToString ();
			n2 = tempN3.ToString () + tempN4.ToString ();

			N2D1 = (tempN4);
			N2D2 = (tempN3);

			StartCoroutine(DisplayQuestion("0", tempN1.ToString (), tempN2.ToString (), "0", tempN3.ToString (), tempN4.ToString ())); 
			FindAnswer (n1, n2);
			SetCoinsOnStart(0,tempN1, tempN2);

		} else if (diff == Difficulty.Grade3) {
			int tempN1 = Random.Range (1, 8);
			int tempN2 = Random.Range (1, 9);
			int tempN3 = Random.Range (1, 9);

			int tempN4 = Random.Range (1, 8 - tempN1);
			int tempN5 = Random.Range (1, 9);
			int tempN6 = Random.Range (1, 9);

			N2D1 = (tempN6);
			N2D2 = (tempN5);
			N2D3 = (tempN4);

			n1 = tempN1.ToString () + tempN2.ToString () + tempN3.ToString();
			n2 = tempN4.ToString () + tempN5.ToString () + tempN6.ToString();

			StartCoroutine(DisplayQuestion(tempN1.ToString (), tempN2.ToString (), tempN3.ToString (), tempN4.ToString (), tempN5.ToString (), tempN6.ToString ()));
			FindAnswer (n1, n2);
			SetCoinsOnStart(tempN1, tempN2, tempN3);
		}
	}

	void FindAnswer(string num1, string num2){
		int tempN1 = int.Parse (num1);
		int tempN2 = int.Parse (num2);

		int ans = tempN1 + tempN2;
		answer = ans.ToString ();
	}

	IEnumerator DisplayQuestion(string n1First, string n1Second, string n1Third, string n2First, string n2Second, string n2Third) {

		yield return new WaitForSeconds (1);
		n1FirstDigit.text = n1First;
		n1SecondDigit.text = n1Second;
		n1ThirdDigit.text = n1Third;

		yield return new WaitForSeconds (1);
		n2FirstDigit.text = n2First;
		n2SecondDigit.text = n2Second;    
		n2ThirdDigit.text = n2Third;

		yield return new WaitForSeconds (1);
		ans1.text = "?";
		ans2.text = "?";
		ans3.text = "?";

		CamAnime.SetBool ("moveCamera", true);

		yield return new WaitForSeconds (1);
		ans1.text = "";
		ans2.text = "";
		ans3.text = "";
	}


	public void SetCoinsOnStart(int no1, int no2 = 0, int no3 = 0){

		dropIndexOne = no3;
		dropIndexTen = no2;
		dropIndexHun = no1;

		for (int i = 0; i < no1; i++) {
			spawnedHunCoins[i].SetActive(true);
			AddToDropList(droppedHunCoins, spawnedHunCoins[i]);
		}

		for (int i = 0; i < no2; i++) {
			spawnedTenCoins[i].SetActive(true);
			AddToDropList(droppedTenCoins, spawnedTenCoins[i]);
		}

		for (int i = 0; i < no3; i++) {
			spawnedOneCoins[i].SetActive(true);
			AddToDropList(droppedOneCoins, spawnedOneCoins[i]);

		}
	}

	public void SetDropLocation(string tag, GameObject[] coinArray, int index, Transform parent1, Transform parent2, Transform parent3){

		if(tag=="Drop1"){
			dropLocation = coinArray[index].transform;
			DropMoreCoins(dropLocation, onesCoin, onesCoinSpawner, parent1);
			index++;
			dropIndexOne = index;
		}else if(tag=="Drop10")
		{
			dropLocation = coinArray[index].transform;
			DropMoreCoins(dropLocation, tensCoin, tensCoinSpawner, parent2);
			index++;
			dropIndexTen = index;
		}else{
			dropLocation = coinArray[index].transform;
			DropMoreCoins(dropLocation, hunsCoin, hunsCoinSpawner, parent3);
			index++;
			dropIndexHun = index;
		}
	
	}


	public void DropMoreCoins(Transform target, GameObject coin, Transform spawn, Transform parent){
		GameObject temp = Instantiate(coin, spawn.position, Quaternion.identity);
		temp.GetComponent<Move>().target = target;
		temp.transform.SetParent(parent);

		if(spawn.gameObject.tag=="Drop1")
		{
			//temp.transform.SetParent(coinOneParent);
			AddToDropList(droppedOneCoins, temp);
			if(addWithCarry){
				CheckForShift(droppedOneCoins);
			}
		}
		else if(spawn.gameObject.tag=="Drop10"){
			//temp.transform.SetParent(coinTenParent);
			AddToDropList(droppedTenCoins, temp);
			if(addWithCarry){
				CheckForShift(droppedTenCoins);
			}
		}else{
			//temp.transform.SetParent(coinHunParent);
			AddToDropList(droppedHunCoins, temp);
		}
	}

	public void SetPlayerAnswer(){
		
		playerAnswer=ans3.text + ans2.text + ans1.text;
	}

	public void ResetAnim(){
		CamAnime.SetBool("moveCamera", false);
		GameObject.Find("Keypad").GetComponent<Animator>().SetBool("KeypadShow", false);
		return;
	}


	public void AddToDropList(List<GameObject> droppedCoinsList, GameObject spawnedCoin){
		droppedCoinsList.Add(spawnedCoin);
	}

	void CheckForShift(List<GameObject> droppedCoinsList){
		if(droppedCoinsList.Count==10){
			ShiftCoinSet(droppedCoinsList);
		}
	}

	void ShiftCoinSet(List<GameObject> droppedCoinsList){
		if(droppedCoinsList==droppedOneCoins){
		//	Move(coinOneParent, shiftOne);
			shiftOnes = true;
		//	Showoutline(outlineOne);
		}
		else if(droppedCoinsList==droppedTenCoins){
			shiftTens = true;
			//Showoutline (outlineTen);
		}
	}

	void Move(Transform toMove, Transform target){
		toMove.position = Vector3.MoveTowards(toMove.position, target.position, 0.1f);

		Invoke("GetReadyForNextDrop", 1f);
	}

	void GetReadyForNextDrop(){
		//dropIndexOne = 0;
		//coinOneParent.gameObject.SetActive(false);
		if(shiftOnes){
			dropIndexOne = 0;
			spawnedOneCoins = newSpawnedOneCoins;
			droppedOneCoins.Clear();
			SetDragProperties(coinOneParent);
			Showoutline(outlineOne);
		//	AddExchangeCoins();
			shiftOnes = false;
			hasOnesShifted = true;
		}

		if(shiftTens){
			exchangeOnesForTen = false;
			exchangeTensForHun = true;
			Showoutline (outlineTen);
			dropIndexTen = 0;
			spawnedTenCoins = newSpawnedTenCoins;
			SetDragProperties(coinTenParent);
			droppedTenCoins.Clear();
			//AddExchangeCoins();
			shiftTens = false;
			hasTensShifted = true;
		}
	}


	void AddExchangeCoins(){
//		coinOneParent.gameObject.SetActive(false);
		if(exchangeOnesForTen){
			//coinOneParent.gameObject.SetActive(false);
			//SetDragProperties(coinOneParent);
			SetDropLocation("Drop10", spawnedTenCoins, dropIndexTen,coinOneParent, coinTenParent, coinHunParent);
		//	hasOnesShifted = true;
		}
		if(exchangeTensForHun){
			//coinTenParent.gameObject.SetActive(false);
			//SetDragProperties(coinTenParent);
			SetDropLocation("Drop100", spawnedHunCoins, dropIndexHun, coinOneParent, coinTenParent, coinHunParent);
			//hasTensShifted = true;
		}
	}

	void SetDragProperties(Transform parent){
		parent.GetChild(parent.childCount-1).GetComponent<BoxCollider2D>().enabled = true; //use this collider to drag
	}

	public void DragObject2D()
	{
		Ray ray = cam.ScreenPointToRay (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 100f);

		if (Input.GetMouseButtonDown (0))
		{
			if (!draggedObject)
			{
				if (hit != null && hit.collider!=null && (hit.collider.tag == "Coin1" || hit.collider.tag == "Coin10"))
				{
					draggedObject = hit.transform.gameObject;
					draggedObject.GetComponent<BoxCollider2D> ().enabled = false;
					offset = draggedObject.transform.position - ray.origin;
					SetColliders(draggedObject.tag);
				}
			}

		}
		if(draggedObject) {
			if(hit != null && hit.collider!=null && (hit.transform.tag=="SlotMachine" || hit.collider.tag=="Snap1" || hit.collider.tag=="Snap2") ){
				draggedObject.transform.position = new Vector3(ray.origin.x + offset.x, ray.origin.y + offset.y, draggedObject.transform.position.z);
			}
			else {
				//draggedObject.transform.position = draggedObject.GetComponent<OriginalPos>().originalPos;
			}
		}


		if (Input.GetMouseButtonUp (0))
		{
			if (draggedObject != null) {
				//RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 100f);

				if(hit.collider!=null && (hit.collider.tag=="Snap1" || hit.collider.tag=="Snap2"))
				{
					//DropObjects();
					DropObjects(hit.transform, draggedObject);

				}

				draggedObject.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
				draggedObject = null;

			}
		}
	}

	void SetColliders(string tag){
		if(tag =="Coin1"){
			dropArea10.GetComponent<BoxCollider2D>().enabled = false;
			dropArea1.GetComponent<BoxCollider2D>().enabled = true;
		}else if(tag =="Coin10"){
			dropArea10.GetComponent<BoxCollider2D>().enabled = true;
			dropArea1.GetComponent<BoxCollider2D>().enabled = false;
		}
	}


	void DropObjects(Transform drop, GameObject drag){
		drag.SetActive(false);
	

		if(drop.gameObject.tag=="Snap1"){
			//coinOneAnimImage.SetActive(true);
			ExchangeAllCoins(coinOneAnimImage);
			coinOneParent.gameObject.SetActive(false);
		}else{
			ExchangeAllCoins(coinTenAnimImage);
			coinTenParent.gameObject.SetActive(false);
		}

		Invoke("AddExchangeCoins", 1f);
	}

	void ExchangeAllCoins(GameObject image){
		image.SetActive(true);
//		if(image==coinOneAnimImage){
//			dropArea10.GetComponent<BoxCollider2D>().enabled = false;
//			dropArea1.GetComponent<BoxCollider2D>().enabled = true;
//		}else{
//			dropArea10.GetComponent<BoxCollider2D>().enabled = true;
//			dropArea1.GetComponent<BoxCollider2D>().enabled = false;
//		}
		//coinOneParent.gameObject.SetActive(false);
	}



	void Showoutline(GameObject outline)
	{
		outline.SetActive (true);
	}
}
