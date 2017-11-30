﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimFromRabbit : MonoBehaviour {

	public Animator thoughtAnim, rabbitAnim;
	public DivideCarrots carrotDivisionScript;

	// Use this for initialization
	void Start () {
		PlayAnim();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayAnim()
	{
		thoughtAnim.SetBool("Think", true);
	}

	public void PlayRunAnimation(bool shouldPlay)
	{
		rabbitAnim.SetBool("StartRunning", shouldPlay);
	}
}
