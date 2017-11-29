using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceCollision : MonoBehaviour {

	public Trace traceScript;
	public int dragObjectNo;
	public int lookAtObject;

	public Transform sprite;
	public Transform target;
	public Vector3 v_diff;
	public Quaternion currentRot;
	public float atan2;

	void Awake()
	{
		traceScript = GetComponentInParent<Trace> ();
		lookAtObject = -1;

		if(dragObjectNo == 2)
			lookAtObject = 2;
		//traceScript.toDrag = this.gameObject;
	}
	// Use this for initialization
	void Start () {
		sprite = this.transform.GetChild (0).transform.GetChild (0);
		//lookAtObject = -1;

	}
	
	// Update is called once per frame
	void Update () {
		
		currentRot = sprite.transform.rotation;

		if (lookAtObject < traceScript.traceColliderObjects.Count - 1) {
			//sprite.transform.LookAt (traceScript.traceColliderObjects [lookAtObject + 1].transform);
			target = traceScript.traceColliderObjects [lookAtObject + 1].transform;
			v_diff = (target.position - sprite.position);
			atan2 = Mathf.Atan2 (v_diff.y, v_diff.x);
			sprite.rotation = Quaternion.Slerp(currentRot, Quaternion.Euler (0f, 0f, atan2 * Mathf.Rad2Deg + 90), 0.1f);

		} else if(lookAtObject >= traceScript.traceColliderObjects.Count) {
			
			sprite.rotation = currentRot;
		}
	}

	public void OnTriggerEnter(Collider col)
	{
		//print (col.name);
		traceScript.UpdateTraceArray (col.name, col.transform.gameObject);

		if (col.transform.tag == "TraceCollider") {
			lookAtObject++;
			//traceScript.startPosC [dragObjectNo - 1] = col.transform.position;
		}

		if (col.GetComponent<Collider>().tag != "TraceCollider") {
			traceScript.ResetPosition ();

			for (int i = 0; i <= traceScript.helper.Length - 1;i++) { 
				traceScript.helper [i].hasEntered = false;
			}
		}
	}
}
