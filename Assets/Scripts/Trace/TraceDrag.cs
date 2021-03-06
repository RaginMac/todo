﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TraceDrag : MonoBehaviour {

	public Vector3 offset;
	public GameObject draggedObj;
	public Camera cam;
	public FishManager manager;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find("FishManager").GetComponent<FishManager>();
		draggedObj = null;
	}
	
	// Update is called once per frame
	void Update () {
		if(!manager.isGameComplete){
			DragTraceObject ();
		}
	}

	public void DragTraceObject()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		if(Input.GetMouseButtonDown(0))
		{
			if(!EventSystem.current.IsPointerOverGameObject())
			{
				if(!draggedObj)
				{
					if(Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag == "DraggableObject")
					{
						//print ("hit");
						draggedObj = hit.transform.gameObject;
						offset = draggedObj.transform.position - ray.origin;
					}
				}
			}
		}

		if(draggedObj) {
			draggedObj.transform.position = new Vector3(ray.origin.x + offset.x, ray.origin.y + offset.y, draggedObj.transform.position.z);
		}

		if(Input.GetMouseButtonUp(0))
		{
			draggedObj = null;
		}

	}
}
