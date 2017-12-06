using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalPos : MonoBehaviour {
	public Vector3 originalPos;
	public int indexValue;
	public int answerValue;
	public string text;

	public bool isSnapped, isDroppabble;
	public GameObject snappedObject;

	//for sprite change after swapping
	public GameObject sprite;

	// Use this for initialization

	void Start () {
		originalPos = this.transform.position;
		isSnapped = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (sprite != null)
			SwapSprite ();
	}

	void SwapSprite()
	{
		if (isSnapped) {
			sprite.SetActive (true);
		} else if (!isSnapped) {
			sprite.SetActive (false);
		}
	}
}
