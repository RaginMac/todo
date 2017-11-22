using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimFromRabbit : MonoBehaviour {

	public Animator thoughtAnim;


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
}
