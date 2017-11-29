using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJ3 : MonoBehaviour {

	public enum Difficulty {Grade1, Grade2, Grade3};
	public Difficulty diff;
	public Animator CamAnime;

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

	[Header("Final arrays")]
	public List<GameObject> droppedOneCoins;
	public List<GameObject> droppedTenCoins;
	public List<GameObject> droppedHunCoins;


	[Header("Final numbers")]
	public string n1;
	public string n2;


	public string answer100;
	public string answer10;
	public string answer1;

	public string playerAnswer100;
	public string playerAnswer10;
	public string playerAnswer1;


	public int N1D1, N1D2, N1D3;
	public int N2D1, N2D2, N2D3;

	[Header("Coin Parents")]
	public Transform coinOneParent;
	public Transform coinTenParent;
	public Transform coinHunParent;


	// display first number
	[Header("First number")]
	public TextMesh n1FirstDigit;
	public TextMesh n1SecondDigit;
	public TextMesh n1ThirdDigit;

	// display second number
//	[Header("Second number")]
//	public TextMesh n2FirstDigit;
//	public TextMesh n2SecondDigit;
//	public TextMesh n2ThirdDigit;

	//answer texts
//	[Header("Answer number")]
//	public TextMesh ans1;
//	public TextMesh ans2;
//	public TextMesh ans3;
//
	public TextMesh[] answerText1;
	public TextMesh[] answerText2;
	public TextMesh[] answerText3;

	public bool answered;

	void Awake(){
		//	ResetAnim();
	}

	void Start () {
		//coinOneAnim = coinOneAnimImage.GetComponent<Animator>();
		CreateQuestionsAWB ();

	}

	void Update () {
		SetPlayerAnswer();
	}


	//AWOB Addition without carry
	void CreateQuestionsAWB()
	{
		if (diff == Difficulty.Grade1) {
			int tempN1 = Random.Range(1, 9);
			//int tempN2 = Random.Range (1, 9-tempN1);

			n1 = tempN1.ToString ();
		//	n2 = tempN2.ToString ();
			N1D1 = tempN1;
		//	N2D2 = (tempN2);


			StartCoroutine(DisplayQuestion("0", "0", tempN1.ToString ()));
			//FindAnswer (n1, n2);
			//SetCoinsOnStart(0, 0, tempN1);

		} else if (diff == Difficulty.Grade2) {
			int tempN1 = Random.Range (1, 9);
			int tempN2 = Random.Range (1, 9);
			//int tempN3 = Random.Range (1, 9-tempN1);
			//int tempN4 = Random.Range (1, 9-tempN2);

			n1 = tempN1.ToString () + tempN2.ToString ();
		//	n2 = tempN3.ToString () + tempN4.ToString ();
			N1D1 = tempN2;
			N1D2 = tempN1;
		//	N2D1 = (tempN4);
		//	N2D2 = (tempN3);


			StartCoroutine(DisplayQuestion("0", tempN1.ToString (), tempN2.ToString ()));

			answer100 = "0";
			answer10 = tempN1.ToString () + "0";
			answer1 = tempN2.ToString ();

			//FindAnswer (n1, n2);
			//SetCoinsOnStart(0,tempN1, tempN2);

		} else if (diff == Difficulty.Grade3) {
			int tempN1 = Random.Range (1, 9);
			int tempN2 = Random.Range (1, 9);
			int tempN3 = Random.Range (1, 9);

//			int tempN4 = Random.Range (1, 9-tempN1);
//			int tempN5 = Random.Range (1, 9-tempN2);
//			int tempN6 = Random.Range (1, 9-tempN3);

			n1 = tempN1.ToString () + tempN2.ToString () + tempN3.ToString();
//			n2 = tempN4.ToString () + tempN5.ToString () + tempN6.ToString();
			N1D1 = (tempN3);
			N1D2 = (tempN2);
			N1D3 = (tempN1);

//			N2D1 = (tempN6);
//			N2D2 = (tempN5);
//			N2D3 = (tempN4);

			StartCoroutine(DisplayQuestion(tempN1.ToString (), tempN2.ToString (), tempN3.ToString ()));

			answer100 = tempN1.ToString () + "0" + "0";
			answer10 = tempN2.ToString () + "0";
			answer1 = tempN3.ToString ();

			//FindAnswer (n1, n2);
			//SetCoinsOnStart(tempN1, tempN2, tempN3);
		}


	}

	//AWB - addition with borrow
//	void CreateQuestionsAWB()
//	{
//		if (diff == Difficulty.Grade1) {
//			int tempN1 = Random.Range (1, 9);
//			int tempN2 = Random.Range (1, 9);
//
//			n1 = tempN1.ToString ();
//			n2 = tempN2.ToString ();
//			N2D2 = (tempN2);
//
//			FindAnswer (n1, n2);
//			StartCoroutine(DisplayQuestion("0", "0", tempN1.ToString (), "0", "0", tempN2.ToString ()));
//			SetCoinsOnStart(0, 0, tempN1);
//
//		} else if (diff == Difficulty.Grade2) {
//			int tempN1 = Random.Range (1, 9);
//			int tempN2 = Random.Range (1, 9);
//
//			int tempN3 = Random.Range (1, 9);
//			int tempN4 = Random.Range (1, 9);
//
//			n1 = tempN1.ToString () + tempN2.ToString ();
//			n2 = tempN3.ToString () + tempN4.ToString ();
//
//			N2D1 = (tempN4);
//			N2D2 = (tempN3);
//
//			StartCoroutine(DisplayQuestion("0", tempN1.ToString (), tempN2.ToString (), "0", tempN3.ToString (), tempN4.ToString ())); 
//			FindAnswer (n1, n2);
//			SetCoinsOnStart(0,tempN1, tempN2);
//
//		} else if (diff == Difficulty.Grade3) {
//			int tempN1 = Random.Range (1, 8);
//			int tempN2 = Random.Range (1, 9);
//			int tempN3 = Random.Range (1, 9);
//
//			int tempN4 = Random.Range (1, 8 - tempN1);
//			int tempN5 = Random.Range (1, 9);
//			int tempN6 = Random.Range (1, 9);
//
//			N2D1 = (tempN6);
//			N2D2 = (tempN5);
//			N2D3 = (tempN4);
//
//			n1 = tempN1.ToString () + tempN2.ToString () + tempN3.ToString();
//			n2 = tempN4.ToString () + tempN5.ToString () + tempN6.ToString();
//
//			StartCoroutine(DisplayQuestion(tempN1.ToString (), tempN2.ToString (), tempN3.ToString (), tempN4.ToString (), tempN5.ToString (), tempN6.ToString ()));
//			FindAnswer (n1, n2);
//			SetCoinsOnStart(tempN1, tempN2, tempN3);
//		}
//	}

//	void FindAnswer(string num1, string num2){
//		int tempN1 = int.Parse (num1);
//		int tempN2 = int.Parse (num2);
//
//		int ans = tempN1 + tempN2;
//		answer = ans.ToString ();
//	}

	IEnumerator DisplayQuestion(string n1First, string n1Second, string n1Third) {

		yield return new WaitForSeconds (1);
		n1FirstDigit.text = n1First;
		n1SecondDigit.text = n1Second;
		n1ThirdDigit.text = n1Third;

//		yield return new WaitForSeconds (1);
//		n2FirstDigit.text = n2First;
//		n2SecondDigit.text = n2Second;    
//		n2ThirdDigit.text = n2Third;

//		yield return new WaitForSeconds (1);
//		ans1.text = "?";
//		ans2.text = "?";
//		ans3.text = "?";
//
		CamAnime.SetBool ("moveCamera", true);
//
//		yield return new WaitForSeconds (1);
//		ans1.text = "";
//		ans2.text = "";
//		ans3.text = "";
	}


	public void SetCoinsOnStart(int no1, int no2 = 0, int no3 = 0){
		print(no1.ToString() + no2.ToString() + no3.ToString());

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
		}
		else if(spawn.gameObject.tag=="Drop10"){
			//temp.transform.SetParent(coinTenParent);
			AddToDropList(droppedTenCoins, temp);

		}else{
			//temp.transform.SetParent(coinHunParent);
			AddToDropList(droppedHunCoins, temp);
		}
	}

	public void SetPlayerAnswer(){
		playerAnswer100 = answerText1[0].text + answerText1[1].text + answerText1[2].text;
		playerAnswer10 = answerText2[0].text + answerText2[1].text + answerText2[2].text;
		playerAnswer1 = answerText3[0].text + answerText3[1].text + answerText3[2].text;

	}

	public void ResetAnim(){
		CamAnime.SetBool("moveCamera", false);
		GameObject.Find("Keypad").GetComponent<Animator>().SetBool("KeypadShow", false);
		return;
	}


	public void AddToDropList(List<GameObject> droppedCoinsList, GameObject spawnedCoin){
		droppedCoinsList.Add(spawnedCoin);
	}

}
