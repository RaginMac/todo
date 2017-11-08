﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceCollision : MonoBehaviour {

	public Trace traceScript;

	void Awake()
	{
		traceScript = GetComponentInParent<Trace> ();
		//traceScript.toDrag = this.gameObject;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter(Collider col)
	{
		//print (col.name);
		traceScript.UpdateTraceArray (col.name, col.transform.gameObject);

		if (col.GetComponent<Collider>().tag != "TraceCollider") {
			traceScript.ResetPosition ();

			for (int i = 0; i <= traceScript.helper.Length - 1; i++) { 
				traceScript.helper [i].hasEntered = false;
			}
		}
	}
}
