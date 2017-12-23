using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class G2_2 : MonoBehaviour {
	
	public enum Difficulty {Grade1, Grade2, Grade3};
	public Difficulty diff;

	public GameObject[] caterpillar1;
	public GameObject[] caterpillar2;

	public int[] answerArray;
	public int firstNumber;

	public bool ansClicked = false;
	// Use this for initialization
	void Start () {
		answerArray = new int[caterpillar1.Length];
		CreateQuestions ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateQuestions()
	{
		if (diff == Difficulty.Grade1) {
			firstNumber = Random.Range (1, 15);

			for (int i = 0; i < caterpillar1.Length; i++) {
				caterpillar1 [i].GetComponentInChildren<Text> ().text = firstNumber.ToString ();
				caterpillar2 [i].GetComponentInChildren<Text> ().text = firstNumber.ToString ();

				caterpillar1 [i].GetComponentInChildren<Text> ().fontSize = 35;
				caterpillar2 [i].GetComponentInChildren<Text> ().fontSize = 35;

				answerArray [i] = firstNumber;
				firstNumber++;
			}

		} else if (diff == Difficulty.Grade2) {
			firstNumber = Random.Range (20, 95);

			for (int i = 0; i < caterpillar1.Length; i++) {
				caterpillar1 [i].GetComponentInChildren<Text> ().text = firstNumber.ToString ();
				caterpillar2 [i].GetComponentInChildren<Text> ().text = firstNumber.ToString ();

				caterpillar1 [i].GetComponentInChildren<Text> ().fontSize = 30;
				caterpillar2 [i].GetComponentInChildren<Text> ().fontSize = 30;

				answerArray [i] = firstNumber;
				firstNumber++;
			}
		} else if (diff == Difficulty.Grade3) {
			firstNumber = Random.Range (100, 995);

			for (int i = 0; i < caterpillar1.Length; i++) {
				caterpillar1 [i].GetComponentInChildren<Text> ().text = firstNumber.ToString ();
				caterpillar2 [i].GetComponentInChildren<Text> ().text = firstNumber.ToString ();

				caterpillar1 [i].GetComponentInChildren<Text> ().fontSize = 25;
				caterpillar2 [i].GetComponentInChildren<Text> ().fontSize = 25;

				answerArray [i] = firstNumber;
				firstNumber++;
			}
		}

		RandomiseShuffleOptions ();
	}

	void ShuffleBodies(GameObject[] bodyArray) 
	{
		for (int i = 0; i < bodyArray.Length; i++)
		{
			GameObject temp = bodyArray [i];
			Vector3 tempPos = bodyArray [i].transform.position;

			int r = Random.Range (i, bodyArray.Length);

			bodyArray [i].transform.position = bodyArray [r].transform.position;
			bodyArray [r].transform.position = tempPos;

			bodyArray [i] = bodyArray [r];
			bodyArray [r] = temp;
		}
	}

	void RandomiseShuffleOptions()
	{
		int randomVal = Random.Range (0, 2);
		Debug.Log (randomVal);
		if (randomVal == 0) {
			ShuffleBodies (caterpillar1);
		} else if (randomVal == 1) {
			ShuffleBodies (caterpillar2);
		}
	}
}
