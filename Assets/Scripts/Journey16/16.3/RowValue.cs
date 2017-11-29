using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowValue : MonoBehaviour {

	public DivisionByMul DivByMul; 

	public int row;
	public int startRowIndex;
	public Vector3 originalPos;

	// Use this for initialization
	void Start () {
		DetermineStartRowIndex ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void DetermineStartRowIndex() {
		startRowIndex = (DivByMul.columns * (row - 1)) + 1;
		
	}
}
