using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	public Transform target;


	void Start()
	{
//		if(target!=null){
//			print("target");
//			this.GetComponent<OriginalPos>().originalPos = target.position;
//		}
	}

	void Update () {
		if(target!=null){
			this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, 0.35f);
			if(Vector3.Distance(transform.position, target.transform.position)<0.1)
			{
				//target = null;
				//print("reached");
				if(this.GetComponent<OriginalPos>()!=null){
					this.GetComponent<OriginalPos>().originalPos = target.transform.position;
				}
				if(this.GetComponent<Move>()!=null){
					this.GetComponent<Move>().enabled = false;
				}

			}
		}

	}


}
