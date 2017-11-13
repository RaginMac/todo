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
	// Use this for initialization
	void Start () {
		originalPos = this.transform.position;
		isSnapped = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
