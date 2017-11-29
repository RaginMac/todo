using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositions : MonoBehaviour {

	public GameObject equalSign;

	public int columns;

	public Transform startPos, prefabObj;

	// Use this for initialization
	void Start () {
		SetEqualPosition ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void SetEqualPosition()
	{
//		float temp = equalSign.transform.position.x;
//		temp = startPos.position.x + ((startPos.localScale.x+2.0f)*4);
//		equalSign.transform.position.x = temp;
	}
}
