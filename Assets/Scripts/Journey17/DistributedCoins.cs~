using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistributedCoins : MonoBehaviour {

	public  int numberOne, numberTwo;
	public GameObject[] dropTrays, unitsCoins, tensCoins;
	public Text n1, n2;

	private int unitsDigit, tensDigit;


	void Awake()
	{
		SetElements(dropTrays.Length, false, dropTrays);
		SetElements(unitsCoins.Length, false, unitsCoins);
		SetElements(tensCoins.Length, false, tensCoins);
	}

	// Use this for initialization
	void Start () {
		numberOne = GetDividendNumber();
		numberTwo = GetDivisorNumber();

		SetElements(numberTwo, true, dropTrays);
		SetElements(unitsDigit, true, unitsCoins);
		SetElements(tensDigit, true, tensCoins);
		SetText(n1, numberOne.ToString());
		SetText(n2, numberTwo.ToString());

		SetDragStatus(tensCoins, tensDigit-1);
	}
	
	// Update is called once per frame
	void Update () {
		
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
		int val = Random.Range(1, 5);
		while(numberOne%val!=0)
		{
			val = Random.Range(1, 5);
		}

		return val;
	}

	void SetElements(int count, bool visibility, GameObject[] array)
	{
		for (int i = 0; i < count; i++) {
			array[i].SetActive(visibility);
		}
	}

	void SetText(Text text, string val)
	{
		text.text = val;
	}

	void SetDragStatus(GameObject[] coinArray, int index)
	{
		coinArray[index].GetComponent<BoxCollider2D>().enabled = true;
	}

}
