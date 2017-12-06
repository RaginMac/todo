using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intantiation_G_1_3 : MonoBehaviour {

	public int noOfObjects;
	public GameObject[] objectToInstantiate;
	public Transform[] spawnPoints;
	public Transform startPosition;
	public GameObject parent;
	public GameObject[] spawedFishes;

	// Use this for initialization
	void Start () {
		//noOfObjects = int.Parse(Question.Instance.question.answerValue);

		//InstatiateQuestionImages ();
		ShuffleObjects();
		spawnObjects ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void spawnObjects()
	{
	//	print ("Spawn fishes");
		ShuffleSpawnPoints ();
		//if (spawedFishes.Length > 0)
		{
			Reset ();
		}

		noOfObjects = int.Parse(GameObject.FindGameObjectWithTag("Player").GetComponent<Question>().question.answerValue);

		for (int i = 0; i < noOfObjects; i++) {
			Vector2 tempPos = spawnPoints [i].position;
			GameObject obj = (GameObject)Instantiate (objectToInstantiate[Manager.Instance.questionNumber], tempPos, Quaternion.identity);
			//spawedFishes [i] = obj;
			obj.transform.SetParent (parent.transform, true);
		}
	}

	public void ShuffleSpawnPoints()
	{
		if (spawnPoints.Length > 0) {
			for (int i = 0; i < spawnPoints.Length; i++) {
				Transform temp = spawnPoints [i];
				int r = Random.Range (i, spawnPoints.Length);
				spawnPoints [i] = spawnPoints [r];
				spawnPoints [r] = temp;
			}
		}
	}

	public void ShuffleObjects()
	{
		if(objectToInstantiate.Length>1){
			for (int j = 0; j < objectToInstantiate.Length; j++) {
				GameObject tem = objectToInstantiate [j];
				int r = Random.Range (j, objectToInstantiate.Length);
				objectToInstantiate [j] = objectToInstantiate [r];
				objectToInstantiate [r] = tem;
			}
		}
	}
	public void Reset()
	{
		//print ("Fish");
		spawedFishes = GameObject.FindGameObjectsWithTag("Fish");
		for(int i=0;i<spawedFishes.Length;i++){
			Destroy(spawedFishes[i]);
		}
	}

	 
}


