using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarManager : MonoBehaviour {

	public Image[] stars;
	public GameObject completedQuestions;


	void Awake()
	{
		for(int i=0;i<stars.Length;i++){
			stars[i].gameObject.SetActive(false);
		}
	}

	// Use this for initialization
	void Start () {
		completedQuestions.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RewardStars(int starIndex)
	{
		stars[starIndex].gameObject.SetActive(true);
	}

	public void ShowCompleteScreen()
	{
		completedQuestions.SetActive(true);
	}
}
