using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceDrag : MonoBehaviour {

	public Vector3 offset;
	public GameObject draggedObj;
	public Camera cam;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
		draggedObj = null;
	}
	
	// Update is called once per frame
	void Update () {
		DragTraceObject ();
	}

	public void DragTraceObject()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		if(Input.GetMouseButtonDown(0))
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

		if(draggedObj) {
			draggedObj.transform.position = new Vector3(ray.origin.x + offset.x, ray.origin.y + offset.y, draggedObj.transform.position.z);
		}

		if(Input.GetMouseButtonUp(0))
		{
			draggedObj = null;
		}

	}
}
