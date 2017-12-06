﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAnim : MonoBehaviour {

	public Animator animator;
	public RuntimeAnimatorController questionController, fishController;
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();

		animator.SetTrigger ("FishIdle");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayAnim(string isCorrect)
	{
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("Fish");

		if (isCorrect == "Correct") {
			for (int i = 0; i < temp.Length; i++) {
				temp [i].GetComponent<Animator> ().SetTrigger ("FishHappy");
			}
		} else if (isCorrect == "Wrong") {
			for (int i = 0; i < temp.Length; i++) {
				temp [i].GetComponent<Animator> ().SetTrigger ("FishSad");
			}
		}
	}
}