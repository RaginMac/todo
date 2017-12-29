using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subtraction : MonoBehaviour {
	
	public enum Difficulty {Grade1, Grade2, Grade3};

	public List<GameObject> Coin1Array = new List<GameObject>();
	public List<GameObject> Coin10Array = new List<GameObject>();
	public List<GameObject> Coin100Array = new List<GameObject>();

	[Header("Select a grade")]
	public Difficulty diff;

	[Header("Select any one")]
	public bool subWithBorrow;
	public bool subWithoutBorrow;

	[Header("N1 Texts:")]
	// display first number
	public TextMesh n1FirstDigit;
	public TextMesh n1SecondDigit;
	public TextMesh n1ThirdDigit;
	public int firstN1;
	public int secondN1;
	public int thirdN1;

	[Header("N2 Texts:")]
	// display second number
	public TextMesh n2FirstDigit;
	public TextMesh n2SecondDigit;
	public TextMesh n2ThirdDigit;
	public int firstDigit;
	public int secondDigit;
	public int thirdDigit;

	[Header("Answer Texts:")]
	//answer texts
	public TextMesh ans1;
	public TextMesh ans10;
	public TextMesh ans100;

	[Header("Counter Panels")]
	public GameObject counterPanel1;
	public GameObject counterPanel10;
	public GameObject counterPanel100;

	[Header("Exchange Panels")]
	public GameObject exchangePanel10;
	public GameObject exchangePanel100;

	[Header("Coin Prefabs")]
	public GameObject coin1;
	public GameObject coin10;
	public GameObject coin100;

	public Transform coin1Parent;
	public Transform coin10Parent;
	public Transform coin100Parent;

	public Transform[] coin1Spawn;
	public Transform[] coin10Spawn;
	public Transform[] coin100Spawn;


	public Transform coin1Fall;
	public Transform coin10Fall;

	//private variables
	private Camera cam;
	private string n1;
	private string n2;
	public string answer;
	public string playerAnswer;

	private GameObject draggedObject;
	private Vector3 offset;
	public Animator camAnimation;

	public bool exchange10;
	public bool exchange100;
	public bool exchangeDrop = false;
	public bool answered;
	public bool resetFlag;

	void Start () {
		 
		cam = Camera.main;
		//camAnimation = cam.GetComponent<Animator> ();

		if (subWithoutBorrow) {
			CreateQuestionsSWOB ();
		} else if (subWithBorrow) {
			CreateQuestionsSWB ();
		}
	}

	void Update () {
		DragObject2D ();
	}


	//SWOB - subtraction without borrow
	void CreateQuestionsSWOB()
	{
		if (diff == Difficulty.Grade1) {
			int tempN1 = 1;
			int tempN2 = 9;

			int tempN3 = Random.Range (0, 2);
			int tempN4 = Random.Range (1, 9);

			n1 = tempN1.ToString () + tempN2.ToString ();
			n2 = tempN3.ToString () + tempN4.ToString ();

			secondN1 = tempN1;
			thirdN1 = tempN2;

			secondDigit = tempN3;
			thirdDigit = tempN4;

			InstantiateCoins (0, tempN1, tempN2);
			StartCoroutine(DisplayQuestion("0", tempN1.ToString (), tempN2.ToString (), "0", tempN3.ToString (), tempN4.ToString ()));
			FindAnswer (n1, n2);
		
		} else if (diff == Difficulty.Grade2) {
			int tempN1 = Random.Range (1, 10);
			int tempN2 = Random.Range (1, 10);

			int tempN3 = Random.Range (1, tempN1);
			int tempN4 = Random.Range (1, tempN2);

			n1 = tempN1.ToString () + tempN2.ToString ();
			n2 = tempN3.ToString () + tempN4.ToString ();

			secondN1 = tempN1;
			thirdN1 = tempN2;

			secondDigit = tempN3;
			thirdDigit = tempN4;

			InstantiateCoins (0, tempN1, tempN2);
			StartCoroutine(DisplayQuestion("0", tempN1.ToString (), tempN2.ToString (), "0", tempN3.ToString (), tempN4.ToString ()));
			FindAnswer (n1, n2);

		} else if (diff == Difficulty.Grade3) {
			int tempN1 = Random.Range (1, 10);
			int tempN2 = Random.Range (1, 10);
			int tempN3 = Random.Range (1, 10);

			int tempN4 = Random.Range (1, tempN1);
			int tempN5 = Random.Range (1, tempN2);
			int tempN6 = Random.Range (1, tempN3);

			n1 = tempN1.ToString () + tempN2.ToString () + tempN3.ToString();
			n2 = tempN4.ToString () + tempN5.ToString () + tempN6.ToString();

			firstN1 = tempN1;
			secondN1 = tempN2;
			thirdN1 = tempN3;

			firstDigit = tempN4;
			secondDigit = tempN5;
			thirdDigit = tempN6;

			InstantiateCoins (tempN1, tempN2, tempN3);
			StartCoroutine(DisplayQuestion(tempN1.ToString (), tempN2.ToString (), tempN3.ToString (), tempN4.ToString (), tempN5.ToString (), tempN6.ToString ()));
			FindAnswer (n1, n2);
		}

		CheckFirstCoin ();
	}



	//SWB - subtraction with borrow
	void CreateQuestionsSWB()
	{
		if (diff == Difficulty.Grade1) {
			int tempN1 = 2;
			int tempN2 = 0;

			int tempN3 = Random.Range (0, tempN1);
			int tempN4 = Random.Range (1, 10);

			n1 = tempN1.ToString () + tempN2.ToString ();
			n2 = tempN3.ToString () + tempN4.ToString ();

			secondN1 = tempN1;
			thirdN1 = tempN2;

			secondDigit = tempN3;
			thirdDigit = tempN4;

			InstantiateCoins (0, tempN1, tempN2);
			StartCoroutine(DisplayQuestion("0", tempN1.ToString (), tempN2.ToString (), "0", tempN3.ToString (), tempN4.ToString ()));
			FindAnswer (n1, n2);

		} else if (diff == Difficulty.Grade2) {
			int tempN1 = Random.Range (1, 10);
			int tempN2 = Random.Range (1, 10);

			int tempN3 = Random.Range (0, tempN1 - 1);
			int tempN4 = Random.Range (tempN2 + 1, 10);

			n1 = tempN1.ToString () + tempN2.ToString ();
			n2 = tempN3.ToString () + tempN4.ToString ();

			secondN1 = tempN1;
			thirdN1 = tempN2;

			secondDigit = tempN3;
			thirdDigit = tempN4;

			InstantiateCoins (0, tempN1, tempN2);

			StartCoroutine(DisplayQuestion("0", tempN1.ToString (), tempN2.ToString (), "0", tempN3.ToString (), tempN4.ToString ())); 
			FindAnswer (n1, n2);

		} else if (diff == Difficulty.Grade3) {
			int tempN1 = Random.Range (1, 10);
			int tempN2 = Random.Range (1, 10);
			int tempN3 = Random.Range (1, 10);

			int tempN4 = Random.Range (0, tempN1 - 1);
			int tempN5 = Random.Range (tempN2 + 1, 10);
			int tempN6 = Random.Range (tempN3 + 1, 10);

			n1 = tempN1.ToString () + tempN2.ToString () + tempN3.ToString();
			n2 = tempN4.ToString () + tempN5.ToString () + tempN6.ToString();

			firstN1 = tempN1;
			secondN1 = tempN2;
			thirdN1 = tempN3;

			firstDigit = tempN4;
			secondDigit = tempN5;
			thirdDigit = tempN6;

			InstantiateCoins (tempN1, tempN2, tempN3);
			StartCoroutine(DisplayQuestion(tempN1.ToString (), tempN2.ToString (), tempN3.ToString (), tempN4.ToString (), tempN5.ToString (), tempN6.ToString ()));
			FindAnswer (n1, n2);
		}

		CheckFirstCoin ();
	}

	void FindAnswer(string num1, string num2){
		int tempN1 = int.Parse (num1);
		int tempN2 = int.Parse (num2);

		int ans = tempN1 - tempN2;
		answer = ans.ToString ();
	}



	IEnumerator DisplayQuestion(string n1First, string n1Second, string n1Third, string n2First, string n2Second, string n2Third)
	{
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
		ans10.text = "?";
		ans100.text = "?";

		//camAnimation.SetBool ("moveCamera", true);

		yield return new WaitForSeconds (1);
		ans1.text = "";
		ans10.text = "";
		ans100.text = "";
	}

	public void CheckFirstCoin()
	{
		for (int i = Coin1Array.Count - 1; i >= 0; i--) 
		{
			if (Coin1Array [i] != null) 
			{
				Coin1Array [i].GetComponent<BoxCollider2D> ().enabled = true;
				exchangeDrop = false;
				exchange10 = false;
				//CheckExchange (); 
				break;
			} else if (i == 0 && Coin1Array [i] == null) {
				exchangeDrop = true;
				exchange10 = true;
				//CheckExchange ();
			} else {
				continue;
			}
		}

		if(exchange10 || counterPanel1.GetComponent<EnableScript>().counter >= thirdDigit) 
		{
			for (int i = Coin10Array.Count - 1; i >= 0; i--) {
				if (Coin10Array [i] != null) {
					Coin10Array [i].GetComponent<BoxCollider2D> ().enabled = true;
					exchangeDrop = false;
					exchange100 = false;
					//CheckExchange (); 
					break;
				} else if (i == 0 && Coin10Array [i] == null) {
					exchangeDrop = true;
					exchange100 = true;
					//CheckExchange (); 
				} else {
					continue;
				}
			}
		}

		if (diff == Difficulty.Grade3 && (exchange100 || counterPanel10.GetComponent<EnableScript>().counter  >= secondDigit)) {
			for (int i = Coin100Array.Count - 1; i >= 0; i--) {
				if (Coin100Array [i] != null) {
					Coin100Array [i].GetComponent<BoxCollider2D> ().enabled = true;
					//CheckExchange (); 
					break;
				} else if (i == 0 && Coin10Array [i] == null) {
					//CheckExchange (); 
				} else {
					continue;
				}
			}
		}
	}

	void InstantiateCoins(int firstDigit, int secondDigit, int thirdDigit)
	{
		if (diff == Difficulty.Grade1 || diff == Difficulty.Grade2) {
			for (int i = 0; i < thirdDigit; i++) {
				//				Coin1Array [i].SetActive (true);
				Coin1Array [i] = Instantiate (coin1, coin1Spawn[i].position, Quaternion.identity, coin1Parent);
				Coin1Array [i].GetComponent <OriginalPos> ().indexValue = i;
			}

			for (int i = 0; i < secondDigit; i++) {
				Coin10Array [i] = Instantiate (coin10, coin10Spawn[i].position, Quaternion.identity, coin10Parent);
				Coin10Array [i].GetComponent <OriginalPos> ().indexValue = i;
			}

		} else if (diff == Difficulty.Grade3) {
			for (int i = 0; i < thirdDigit; i++) {
				Coin1Array[i] = Instantiate(coin1, coin1Spawn[i].position, Quaternion.identity, coin1Parent);
				Coin1Array [i].GetComponent <OriginalPos> ().indexValue = i;
			}

			for (int i = 0; i < secondDigit; i++) {
				Coin10Array[i] = Instantiate(coin10, coin10Spawn[i].position, Quaternion.identity, coin10Parent);
				Coin10Array [i].GetComponent <OriginalPos> ().indexValue = i;
			}

			for (int i = 0; i < firstDigit; i++) {
				Coin100Array[i] = Instantiate(coin100, coin100Spawn[i].position, Quaternion.identity, coin100Parent);
				Coin100Array [i].GetComponent <OriginalPos> ().indexValue = i;
			}
		}
	}

	public void DestroyCoins() {
		for (int i = 0; i < Coin1Array.Count; i++) 
		{
			if (Coin1Array [i] != null) {
				Destroy (Coin1Array [i]);
				Coin1Array [i] = null;
			}
			

			if(Coin10Array[i] != null){
				Destroy (Coin10Array [i]);
				Coin10Array [i] = null;
			}

			if(Coin100Array[i] != null) {
				Destroy (Coin100Array [i]);
				Coin100Array [i] = null;
			}
		}
	}

	void DropInCounter(GameObject hitObject)
	{
		if (hitObject.tag == "CounterPanel1" && draggedObject.tag == "Coin1") {
			hitObject.GetComponent<EnableScript> ().counter++;
			hitObject.GetComponentInChildren<TextMesh> ().text = hitObject.GetComponent<EnableScript> ().counter.ToString ();
			Coin1Array [draggedObject.GetComponent<OriginalPos> ().indexValue] = null; 
			Manager.Instance.PlayDragDropAudio ();
			Destroy (draggedObject); 

		} else if (hitObject.tag == "CounterPanel10" && draggedObject.tag == "Coin10") {
			hitObject.GetComponent<EnableScript> ().counter++;
			hitObject.GetComponentInChildren<TextMesh> ().text = hitObject.GetComponent<EnableScript> ().counter.ToString ();
			Coin10Array [draggedObject.GetComponent<OriginalPos> ().indexValue] = null; 
			Manager.Instance.PlayDragDropAudio ();
			Destroy (draggedObject);

		} else if (hitObject.tag == "CounterPanel100" && draggedObject.tag == "Coin100") {
			hitObject.GetComponent<EnableScript>().counter++;
			hitObject.GetComponentInChildren<TextMesh> ().text = hitObject.GetComponent<EnableScript>().counter.ToString ();
			Coin100Array [draggedObject.GetComponent<OriginalPos> ().indexValue] = null; 
			Manager.Instance.PlayDragDropAudio ();
			Destroy (draggedObject);
		} else {
			draggedObject.transform.position = draggedObject.GetComponent<OriginalPos> ().originalPos;
		}


		CheckFirstCoin ();
	}
		

	IEnumerator DropInExchange(GameObject hitObject)
	{
		if (hitObject.tag == "ExchangePanel10" && draggedObject.tag == "Coin10" && exchange10) {
			
			Coin10Array [draggedObject.GetComponent<OriginalPos> ().indexValue] = null; 
			Destroy (draggedObject.transform.gameObject);

			for (int i = 0; i < 10; i++) {
				Coin1Array [i] = Instantiate (coin1, coin1Fall.position, Quaternion.identity, coin1Parent);
				Coin1Array [i].GetComponent <OriginalPos> ().indexValue = i;
				Coin1Array [i].GetComponent <CoinFallScript> ().target = coin1Spawn[i];
				Manager.Instance.PlayDragDropAudio ();

				yield return new WaitForSeconds (0.8f);
			}

		} else if (hitObject.tag == "ExchangePanel100" && draggedObject.tag == "Coin100" && exchange100) {
			Coin100Array [draggedObject.GetComponent<OriginalPos> ().indexValue] = null; 
			Destroy (draggedObject.transform.gameObject);

			for (int i = 0; i < 10; i++) {
				
				Coin10Array [i] = Instantiate (coin10, coin10Fall.position, Quaternion.identity, coin10Parent);
				Coin10Array [i].GetComponent <OriginalPos> ().indexValue = i;
				Coin10Array [i].GetComponent <CoinFallScript> ().target = coin10Spawn[i];
				Manager.Instance.PlayDragDropAudio ();

				yield return new WaitForSeconds (0.8f);
			}
		}

		CheckFirstCoin ();
	}


	public void CheckCounter()
	{
		if (diff == Difficulty.Grade2 || diff == Difficulty.Grade1) {
			if (thirdDigit == counterPanel1.GetComponent<EnableScript> ().counter) {
				counterPanel10.GetComponent<BoxCollider2D> ().enabled = true;
				counterPanel1.GetComponent<BoxCollider2D> ().enabled = false;
			} else {
				counterPanel10.GetComponent<BoxCollider2D> ().enabled = false;
				counterPanel1.GetComponent<BoxCollider2D> ().enabled = true;
			}

		} else if (diff == Difficulty.Grade3) {
			if (thirdDigit == counterPanel1.GetComponent<EnableScript> ().counter) {
				counterPanel10.GetComponent<BoxCollider2D> ().enabled = true;
				counterPanel1.GetComponent<BoxCollider2D> ().enabled = false;
			} else {
				counterPanel10.GetComponent<BoxCollider2D> ().enabled = false;
				counterPanel1.GetComponent<BoxCollider2D> ().enabled = true;
			}

			if (secondDigit == counterPanel10.GetComponent<EnableScript> ().counter) {
				counterPanel100.GetComponent<BoxCollider2D> ().enabled = true;
				counterPanel10.GetComponent<BoxCollider2D> ().enabled = false;

			} else if(secondDigit != counterPanel10.GetComponent<EnableScript> ().counter && thirdDigit == counterPanel1.GetComponent<EnableScript> ().counter){
				counterPanel100.GetComponent<BoxCollider2D> ().enabled = false;
				counterPanel10.GetComponent<BoxCollider2D> ().enabled = true;
			}
		}
	}


//
//	void CheckExchange() {
//		if (diff == Difficulty.Grade2 || diff == Difficulty.Grade1) {
//			for (int i = 0; i < Coin1Array.Count; i++) {
//				if (Coin1Array [i] != null) {
//					exchange10 = false;
//					break;
//				} else if (Coin1Array [i] == null) {
//					exchange10 = true;
//				}
//			}
//
//		} else if (diff == Difficulty.Grade3) {
//
//			for (int i = 0; i < Coin1Array.Count; i++) {
//				if (Coin1Array [i] != null) {
//					exchange10 = false;
//					break;
//				} else if (Coin1Array [i] == null) {
//					exchange10 = true;
//				}
//			}
//
//			for (int i = 0; i < Coin10Array.Count; i++) {
//				if (Coin10Array [i] != null) {
//					exchange100 = false;
//					break;
//				} else if (Coin10Array [i] == null) {
//					exchange100 = true;
//				}
//			}
//		}
//	}



	public void DragObject2D()
	{
		Ray ray = cam.ScreenPointToRay (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 100f);

		if (Input.GetMouseButtonDown (0))
		{
			if (draggedObject == null)
			{
				if ((hit.collider.tag == "Coin1" || hit.collider.tag == "Coin10" || hit.collider.tag == "Coin100")) {
					draggedObject = hit.transform.gameObject;
					draggedObject.GetComponent<BoxCollider2D> ().enabled = false;
					draggedObject.GetComponent<OriginalPos> ().originalPos = draggedObject.transform.position;
					offset = draggedObject.transform.position - ray.origin;
				} 
			}
		}

		if(draggedObject) {
//			if (hit.transform.name == "SlotMachine" || hit.transform.tag == "ExchangePanel10" || hit.transform.tag == "ExchangePanel100" 
//				|| hit.transform.tag == "CounterPanel100" || hit.transform.tag == "CounterPanel10" || hit.transform.tag == "CounterPanel1" 
//				|| hit.transform.name == "Coin10(Clone)" || hit.transform.name == "Coin100(Clone)" || hit.transform.name == "Coin1(Clone)") {
//
//				draggedObject.transform.position = new Vector3 (ray.origin.x + offset.x, ray.origin.y + offset.y, draggedObject.transform.position.z);
//			} else {
//				draggedObject.transform.position = draggedObject.GetComponent<OriginalPos> ().originalPos;
//			}

			if (hit.transform.name == "BG") {
				draggedObject.transform.position = draggedObject.GetComponent<OriginalPos> ().originalPos;draggedObject.transform.position = draggedObject.GetComponent<OriginalPos> ().originalPos;
			} else {
				draggedObject.transform.position = new Vector3 (ray.origin.x + offset.x, ray.origin.y + offset.y, draggedObject.transform.position.z);
			}
		}

		if (Input.GetMouseButtonUp (0))
		{
			if (draggedObject != null)
			{
				DropInCounter (hit.transform.gameObject);

				if(subWithBorrow)
					StartCoroutine(DropInExchange (hit.transform.gameObject));
				
				CheckCounter ();

				draggedObject.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
				draggedObject = null;
			}
		}
	}


	public void ResetAnswers()
	{
		ans1.text = "";
		ans10.text = "";
		ans100.text = "";

		DestroyCoins ();
		InstantiateCoins (firstN1, secondN1, thirdN1);

		counterPanel1.GetComponent<EnableScript> ().ResetCounters();
		counterPanel10.GetComponent<EnableScript> ().ResetCounters();
		counterPanel100.GetComponent<EnableScript> ().ResetCounters();

		counterPanel1.GetComponentInChildren<TextMesh>().text = "0";
		counterPanel10.GetComponentInChildren<TextMesh>().text = "0";
		counterPanel100.GetComponentInChildren<TextMesh>().text = "0";

		CheckFirstCoin ();
		CheckCounter ();
	}
}
