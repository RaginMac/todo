using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour {

//	public Vector3 startPosition;
//	public int rows, columns;
	public int noToSpawn;
	public GameObject parent;
	public GameObject spawnPrefab;
	public Transform[] spawnPoints;
	public List<Transform> spawnedMangoes = new List<Transform>();
	public Text questionText;

	public Question question;
	public DragObjects dragScript;

	public Manager manager;
	// Use this for initialization
	void Start () {
		dragScript = this.gameObject.GetComponent<DragObjects> ();
		manager = this.gameObject.GetComponent<Manager> ();
		SpawnMango ();
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}


	void Shuffle()
	{
		for(int i=0; i<spawnPoints.Length;i++)
		{
			Transform temp = spawnPoints[i];
			int r = Random.Range(i, spawnPoints.Length);
			spawnPoints[i] = spawnPoints[r];
			spawnPoints[r] = temp;
		}
	}

	public void SpawnMango()
	{
		
		Reset();
		Shuffle();
		dragScript.checkAnswer.SetActive (false);
		spawnedMangoes.Clear();
		Manager.Instance.questionNumber++;
		noToSpawn = int.Parse(question.options [Manager.Instance.questionNumber].answerText);
		questionText.text = noToSpawn.ToString ();
		int randomNum = Random.Range (1, 3);

		for(int i = 0; i < noToSpawn + randomNum; i++){
			GameObject mango = Instantiate(spawnPrefab, spawnPoints[i].position, Quaternion.identity);

			spawnedMangoes.Add(mango.transform);
			//mango.transform.SetParent(parent.transform);
			mango.GetComponent<Animator>().SetTrigger("ShakeMango");
		}

		if(manager.autoPlayAudio)
			PlayQuestionAudio ();
	}

	public void Reset()
	{
		GameObject[] spawnedMangoes = GameObject.FindGameObjectsWithTag("DraggableObject");
		this.gameObject.GetComponent<DragObjects> ().numberOfMangoes = 0;
		for(int i=0;i<spawnedMangoes.Length;i++){
			Destroy(spawnedMangoes[i]);
		}
	}

	public void ResetMangoes()
	{
		for (int i = 0; i <= spawnedMangoes.Count - 1; i++) {
			Vector3 tempPos = spawnedMangoes[i].GetComponent<ObjectPosition>().originalPos;
			spawnedMangoes[i].position = tempPos;
			spawnedMangoes[i].GetComponent<BoxCollider>().enabled = true;
		}
	}

	public void PlayQuestionAudio()
	{
		if(question.options [Manager.Instance.questionNumber].answerAudio != null){
		//	print(question.options [Manager.Instance.questionNumber].answerAudio.name);
			Manager.Instance.PlayAudio (question.options [Manager.Instance.questionNumber].answerAudio);
		}
	}
}
