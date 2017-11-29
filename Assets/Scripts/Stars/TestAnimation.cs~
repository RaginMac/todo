using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour {

	public Transform parent;
	public Transform myPosition;

	// Use this for initialization
	void Start () {
		
		myPosition = this.transform;

	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Vector2.MoveTowards (myPosition.position, parent.position, 3.5f);
	}
}
