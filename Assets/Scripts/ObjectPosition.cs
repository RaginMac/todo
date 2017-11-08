using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPosition : MonoBehaviour {

	public Vector3 originalPos;
	public Animator anim;
	// Use this for initialization
	void Start () {
		originalPos = this.transform.position;
		anim = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMouseDown()
	{
//		this.GetComponent<Animator> ().SetTrigger ("ShakeMango");
//
//		GameObject[] temp = GameObject.FindGameObjectsWithTag ("DraggableObject");
//
//		for (int i = 0; i < temp.Length; i++) {
//			temp [i].GetComponent<Animator> ().SetTrigger ("ShakeMango");
//		}
	}
}
