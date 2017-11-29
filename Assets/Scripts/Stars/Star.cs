using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {
	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
		anim.SetTrigger("StarAnimation");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
