using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTPOS : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.transform.position = Camera.main.WorldToScreenPoint(new Vector3(Screen.width*0.5f, Screen.height*0.5f, transform.position.z));
//		this.transform.position = new Vector3(Screen.width*0.5f, Screen.height*0.5f, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
