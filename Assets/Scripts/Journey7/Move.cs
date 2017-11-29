﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	public Transform target;

	void Update () {
		if(target!=null){
			this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, 0.2f);
		}
	}


}
