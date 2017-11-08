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
		if (this.transform.childCount > 0) {
			//text = this.transform.GetChild (0).GetComponent<TextMesh> ().text;
		}
		isSnapped = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
