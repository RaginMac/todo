using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFallScript : MonoBehaviour {
	public Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			Move (target);
		}
	}

	public void Move(Transform target){
		//move script
		this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, 0.5f);
	}
}
